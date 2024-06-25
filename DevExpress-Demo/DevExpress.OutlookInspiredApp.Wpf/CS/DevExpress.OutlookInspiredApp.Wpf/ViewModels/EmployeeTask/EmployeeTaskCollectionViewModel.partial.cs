using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Media;
using DevExpress.Data;
using DevExpress.Data.Filtering;
using DevExpress.DevAV.Common.Utils;
using DevExpress.DevAV.Common.ViewModel;
using DevExpress.DevAV.DevAVDbDataModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.DataModel;
using DevExpress.Mvvm.ViewModel;
using DevExpress.Mvvm.Localization;

namespace DevExpress.DevAV.ViewModels {
    partial class EmployeeTaskCollectionViewModel : ISupportFiltering<EmployeeTask>, IFilterTreeViewModelContainer<EmployeeTask, long> {

        protected virtual IOpenFileDialogService OpenFileDialogService { get { return null; } }
        public void ShowPrintPreview() {
            var printModel = this.GetRequiredService<IPrintableControlPreviewService>().GetPreview();
        }

        EmployeeTaskDetailViewModel entityPanelViewModel;
        public EmployeeTaskDetailViewModel EntityPanelViewModel {
            get {
                if(entityPanelViewModel == null)
                    entityPanelViewModel = EmployeeTaskDetailViewModel.Create();
                return entityPanelViewModel;
            }
        }
        protected override void OnInitializeInRuntime() {
            base.OnInitializeInRuntime();
            SelectedEntities = new ObservableCollection<EmployeeTask>();
            FollowUpSelector = new ObservableCollection<bool>() { false, false, false, false, false, false };
            PrioritySelector = new ObservableCollection<bool>() { false, false };
        }
        public override void OnLoaded() {
            if(FilterTreeViewModel == null) return;
            FilterTreeViewModel.AllowFiltersContextMenu = false;
            RefreshEmployeeFilters();
        }
        public void ChangeTaskView(string parameter) {
            TasksViewKind viewKind;
            Enum.TryParse(parameter, out viewKind);
            if(viewKind == TasksViewKind.Active)
                FilterTreeViewModel.SelectedItem = FilterTreeViewModel.StaticFilters[2];
            if(viewKind != ViewKind && ViewKind == TasksViewKind.Active && FilterTreeViewModel.SelectedItem == FilterTreeViewModel.StaticFilters[2])
                FilterTreeViewModel.SelectedItem = FilterTreeViewModel.StaticFilters[0];
            ViewKind = viewKind;
            EntityPanelViewModel.ZoomEditEnabled = (ViewKind == TasksViewKind.Detailed);
            Logger.Log(string.Format("OutlookInspiredApp: Change Task View: {0}", ViewKind.ToString()));
        }
        public override void New() {
            DocumentManagerService.ShowNewEntityDocument<EmployeeTask>(this);
            Refresh();
            UpdateEntityPanelViewModel();
            RefreshEmployeeFilters();
        }
        public override void Edit(EmployeeTask entity) {
            base.Edit(entity);
            Refresh();
            UpdateEntityPanelViewModel();
            RefreshEmployeeFilters();
        }
        public override bool CanEdit(EmployeeTask projectionEntity) {
            return base.CanEdit(projectionEntity) && (SelectedEntities.Count == 1);
        }

        public override void Delete(EmployeeTask projectionEntity) {
            if(MessageBoxService.ShowMessage(string.Format(ScaffoldingLocalizer.GetString(ScaffoldingStringId.Confirmation_Delete), EntityDisplayName),
                ScaffoldingLocalizer.GetString(ScaffoldingStringId.Confirmation_Caption), MessageButton.YesNo) != MessageResult.Yes)
                return;
            try {
                do {
                    EmployeeTask task = SelectedEntities.First();
                    Entities.Remove(task);
                    long primaryKey = Repository.GetProjectionPrimaryKey(task);
                    EmployeeTask entity = Repository.Find(primaryKey);
                    if(entity != null) {
                        OnBeforeEntityDeleted(primaryKey, entity);
                        Repository.Remove(entity);
                        Repository.UnitOfWork.SaveChanges();
                        OnEntityDeleted(primaryKey, entity);
                    }
                } while(SelectedEntities.Count > 0);
            } catch(DbException e) {
                Refresh();
                MessageBoxService.ShowMessage(e.ErrorMessage, e.ErrorCaption, MessageButton.OK, MessageIcon.Error);
            }
            RefreshEmployeeFilters();
        }
        public override bool CanDelete(EmployeeTask projectionEntity) {
            return !IsLoading && SelectedEntities != null && SelectedEntities.Count > 0;
        }
        public virtual void MarkComplete() {
            var selectedIDs = SelectedEntities.Select(x => x.Id).ToList();
            selectedIDs.ForEach(id => {
                var entity = Entities.Single(x => x.Id == id);
                if(entity.Status != EmployeeTaskStatus.Completed) {
                    entity.Status = EmployeeTaskStatus.Completed;
                    entity.Completion = 100;
                    Save(entity);
                    if(entity == SelectedEntity)
                        this.UpdateSelectedEntity();
                }
            });
        }
        public virtual bool CanMarkComplete() {
            return !IsLoading && SelectedEntities != null && SelectedEntities.Count > 0 && SelectedEntities.Where(x => x.Status != EmployeeTaskStatus.Completed).Count() > 0;
        }
        public virtual void AssignTask(EmployeeTask entity) {
            long primaryKey = Repository.GetProjectionPrimaryKey(entity);
            IDocument document = AssignTaskService.CreateDocument("EmployeeAssignView", primaryKey, this);
            if(document != null)
                document.Show();
            Refresh();
            RefreshEmployeeFilters();
        }
        public virtual void AttachFile(EmployeeTask entity) {
            bool dialogResult = OpenFileDialogService.ShowDialog();
            if(dialogResult) {
                IFileInfo file = OpenFileDialogService.Files.First();
                if(file.Length > FilesHelper.MaxAttachedFileSize * 1050578) {
                    MessageBoxService.ShowMessage(string.Format("The file size exceeds {0} MB.", FilesHelper.MaxAttachedFileSize), "Error attaching file");
                    return;
                }
                IDevAVDbUnitOfWork unitOfWork = this.CreateUnitOfWork();
                try {
                    using(FileStream stream = new FileStream(Path.Combine(file.DirectoryName, file.Name), FileMode.Open, FileAccess.Read)) {
                        TaskAttachedFile attachedFile = unitOfWork.AttachedFiles.Create();
                        attachedFile.EmployeeTaskId = entity.Id;
                        attachedFile.Name = file.Name;
                        attachedFile.Content = new byte[(int)stream.Length];
                        stream.Read(attachedFile.Content, 0, (int)stream.Length);
                        unitOfWork.SaveChanges();
                        Refresh();
                    }
                } catch(Exception) {
                    MessageBoxService.ShowMessage("Error attaching file!", "Error");
                    return;
                }
            }
        }

        public virtual bool CanAttachFile(EmployeeTask entity) {
            return !IsLoading && SelectedEntities != null && SelectedEntities.Count == 1;
        }
        public virtual ObservableCollection<bool> FollowUpSelector { get; set; }
        public virtual ObservableCollection<bool> PrioritySelector { get; set; }
        public virtual bool? Private { get; set; }
        public virtual string Category { get; set; }

        public virtual string GridControlFilterString { get; set; }

        string tagsFilterString;

        EmployeeTaskFollowUp? CurrentFollowUp { get; set; }
        EmployeeTaskPriority? CurrentPriority { get; set; }

        public virtual void OnPrivateChanged() {
            SetFilterString();
        }
        public virtual void OnCategoryChanged() {
            SetFilterString();
        }

        public virtual void FollowUp(string parameter) {
            CurrentFollowUp = FollowUpSelector.Contains(true) ? (EmployeeTaskFollowUp?)Enum.Parse(typeof(EmployeeTaskFollowUp), parameter) : null;
            SetFilterString();
        }
        public virtual void Priority(string parameter) {
            CurrentPriority = PrioritySelector.Contains(true) ? (EmployeeTaskPriority?)Enum.Parse(typeof(EmployeeTaskPriority), parameter) : null;
            SetFilterString();
        }

        bool tagsFilterSetter;

        public virtual void OnGridControlFilterStringChanged() {
            if(tagsFilterSetter) {
                tagsFilterSetter = false;
                return;
            }
            for(int i = 0; i < FollowUpSelector.Count; i++) {
                FollowUpSelector[i] = false;
            }
            for(int i = 0; i < PrioritySelector.Count; i++) {
                PrioritySelector[i] = false;
            }
            CurrentFollowUp = null;
            CurrentPriority = null;
            Private = null;
            Category = Colors.Transparent.ToString();
        }

        void SetFilterString() {
            List<string> filterStrings = new List<string>();
            if(CurrentFollowUp != null)
                filterStrings.Add("[FollowUp] == '" + CurrentFollowUp.ToString() + "'");
            if(CurrentPriority != null)
                filterStrings.Add("[Priority] == '" + CurrentPriority.ToString() + "'");
            if(Private != null)
                filterStrings.Add("[Private] == '" + Private.ToString() + "'");
            if(Category != Colors.Transparent.ToString() && Category != null)
                filterStrings.Add("[Category] == '" + Category.ToString() + "'");
            tagsFilterString = (filterStrings.Count == 0) ? string.Empty : filterStrings[0];
            for(int i = 1; i < filterStrings.Count(); i++) {
                tagsFilterString += " and ";
                tagsFilterString += filterStrings[i];
            }
            tagsFilterSetter = true;
            GridControlFilterString = tagsFilterString;
        }
        void RefreshEmployeeFilters() {
            Func<IDevAVDbUnitOfWork, IRepository<Employee, long>> getEmployeesFunction = x => x.Employees;
            var unitOfWork = UnitOfWorkFactory.CreateUnitOfWork();
            List<FilterInfo> newFilters = new List<FilterInfo>();
            foreach(var employee in getEmployeesFunction(unitOfWork))
                newFilters.Add(new FilterInfo() { Name = employee.FullName, FilterCriteria = TasksToFilterCriteriaConverter(employee.AssignedEmployeeTasks) });
            IEnumerable<FilterInfo> oldFilters = FilterTreeViewModel.CustomFilters.Select(x => new FilterInfo() { Name = x.Name, FilterCriteria = ReferenceEquals(x.FilterCriteria, null) ? string.Empty : x.FilterCriteria.ToString() });
            if(!newFilters.SequenceEqual(oldFilters, new FilterInfoComparer())) {
                if(newFilters.Count != oldFilters.Count() || !newFilters.SequenceEqual(oldFilters, new FilterInfoNameComparer())) {
                    FilterTreeViewModel.ResetToAll();
                    FilterTreeViewModel.ResetCustomFilters();
                    foreach(var newFilter in newFilters)
                        FilterTreeViewModel.AddCustomFilter(newFilter.Name, CriteriaOperator.Parse(newFilter.FilterCriteria));
                } else {
                    int index = 0;
                    foreach(var newFilter in newFilters) {
                        FilterTreeViewModel.ModifyCustomFilterCriteria(FilterTreeViewModel.CustomFilters.ElementAt(index),
                            CriteriaOperator.Parse(newFilter.FilterCriteria));
                        index++;
                    }
                }
            }
        }
        public void PrintTasksSummary() {
            ShowReport(ReportInfoFactory.TaskListReport(CreateUnitOfWork().Tasks.ToList()), "Summary");
        }
        IReportService PrintService { get { return this.GetRequiredService<IReportService>("PrintService"); } }
        IReportService ExportService { get { return this.GetRequiredService<IReportService>("ExportService"); } }
        public IDocumentManagerService AssignTaskService { get { return this.GetService<IDocumentManagerService>("EmployeeAssignService"); } }

        public virtual bool CanAssignTask(EmployeeTask entity) {
            return !IsLoading && SelectedEntities != null && SelectedEntities.Count == 1;
        }
        public virtual FilterTreeViewModel<EmployeeTask, long> FilterTreeViewModel { get; set; }
        #region ISupportFiltering
        Expression<Func<EmployeeTask, bool>> ISupportFiltering<EmployeeTask>.FilterExpression {
            get { return FilterExpression; }
            set { FilterExpression = value; }
        }
        #endregion
        public virtual TasksViewKind ViewKind { get; set; }
        public virtual ObservableCollection<EmployeeTask> SelectedEntities { get; set; }

        protected override void OnSelectedEntityChanged() {
            base.OnSelectedEntityChanged();
            UpdateEntityPanelViewModel();
        }
        public override void UpdateSelectedEntity() {
            base.UpdateSelectedEntity();
            UpdateEntityPanelViewModel();
        }
        void UpdateEntityPanelViewModel() {
            if(SelectedEntity != null) {
                EntityPanelViewModel.Entity = null;
                EntityPanelViewModel.Entity = SelectedEntity;
            }
        }
        public static string TasksToFilterCriteriaConverter(List<EmployeeTask> tasks) {
            string criteria = "[Id] = '-1'";
            if(tasks != null && tasks.Count > 0) {
                criteria = string.Empty;
                foreach(var task in tasks)
                    criteria += "[Id] = '" + task.Id + "'" + ((task != tasks.ElementAt(tasks.Count - 1)) ? " Or " : "");
            }
            return criteria;
        }
        void ShowReport(IReportInfo reportInfo, string reportId) {
            ExportService.ShowReport(reportInfo);
            PrintService.ShowReport(reportInfo);
            Logger.Log(string.Format("OutlookInspiredApp: Create Report : Tasks: {0}", reportId));
        }
    }
    class FilterInfoComparer : IEqualityComparer<FilterInfo> {
        public bool Equals(FilterInfo info1, FilterInfo info2) {
            return info1.Name == info2.Name && info1.FilterCriteria == info2.FilterCriteria;
        }
        public int GetHashCode(FilterInfo info) {
            return info.ToString().ToLower().GetHashCode();
        }
    }
    class FilterInfoNameComparer : IEqualityComparer<FilterInfo> {
        public bool Equals(FilterInfo info1, FilterInfo info2) {
            return info1.Name == info2.Name;
        }
        public int GetHashCode(FilterInfo info) {
            return info.ToString().ToLower().GetHashCode();
        }
    }

    public enum TasksViewKind {
        SimpleList,
        Detailed,
        Prioritized,
        Active
    }
}
