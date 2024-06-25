using System;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Data.Utils;
using DevExpress.DevAV.DevAVDbDataModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.DevAV.Common;
using DevExpress.Mvvm.DataModel;
using DevExpress.Mvvm.ViewModel;

namespace DevExpress.DevAV.ViewModels {
    public class EmployeeTaskDetailsCollectionViewModel : CollectionViewModel<EmployeeTask, long, IDevAVDbUnitOfWork> {
        public static EmployeeTaskDetailsCollectionViewModel Create(IUnitOfWorkFactory<IDevAVDbUnitOfWork> unitOfWorkFactory = null) {
            return ViewModelSource.Create(() => new EmployeeTaskDetailsCollectionViewModel(unitOfWorkFactory));
        }
        protected EmployeeTaskDetailsCollectionViewModel(IUnitOfWorkFactory<IDevAVDbUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Tasks, query => query.OrderByDescending(x => x.DueDate)) {
        }
        public IDocumentManagerService AssignTaskService { get { return this.GetService<IDocumentManagerService>("EmployeeAssignService"); } }

        public override void Edit(EmployeeTask projectionEntity) {
            base.Edit(projectionEntity);
            UpdateFilter();
        }
        public override void Delete(EmployeeTask projectionEntity) {
            base.Delete(projectionEntity);
            UpdateFilter();
        }
        public static long? LastSelectedEmployeeId { get; set; }
        public override void New() {
            LastSelectedEmployeeId = this.GetParentViewModel<EmployeeViewModel>().Entity.Id;
            DocumentManagerService.ShowNewEntityDocument<EmployeeTask>(this);
            LastSelectedEmployeeId = null;
            UpdateFilter();
        }
        public virtual void AssignTask(EmployeeTask task) {
            IDocument document = AssignTaskService.CreateDocument("EmployeeAssignView", task.Id, this);
            if(document != null)
                document.Show();
            UpdateFilter();
        }
        public virtual bool CanAssignTask(EmployeeTask task) {
            return !IsLoading && task != null;
        }
        public void UpdateFilter() {
            var viewModel = this.GetParentViewModel<EmployeeViewModel>();
            if(viewModel.Entity == null)
                return;
            var unitOfWork = UnitOfWorkFactory.CreateUnitOfWork();
            var actualEntity = unitOfWork.Employees.FirstOrDefault(x => x.Id == viewModel.Entity.Id);
            FilterExpression = CriteriaOperatorToExpressionConverter.GetGenericWhere<EmployeeTask>(CriteriaOperator.Parse(EmployeeTaskCollectionViewModel
                .TasksToFilterCriteriaConverter((actualEntity == null) ? null : actualEntity.AssignedEmployeeTasks)));
        }
    }
}
