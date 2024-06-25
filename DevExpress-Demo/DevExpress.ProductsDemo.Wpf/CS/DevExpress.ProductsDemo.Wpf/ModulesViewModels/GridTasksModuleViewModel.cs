using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;
using DevExpress.Data;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Utils;
using DevExpress.Xpf.Core;
using DevExpress.XtraGrid;
using ProductsDemo.Controls;

namespace ProductsDemo.Modules {
    public class GridTasksModuleViewModel : GridViewModelBase<Task> {
        public List<EmployeeTasks> Employees { get; set; }
        public virtual EmployeeTasks SelectedEmployee { get; set; }
        public void ShowNewTaskWindow() {
            ShowEditTaskWindowCore(new Task("New Task", TaskCategory.Work), true);
        }

        protected override void InitializeDefaultView() {
            FollowUpSelector = new ObservableCollection<bool>() { false, false, false, false, false, false, false };
            SetListView();
            CheckedItemType = ItemType.List;
        }

        protected override List<Task> GetItemsSource() {
            return SelectedEmployee.Tasks;
        }

        protected virtual INotificationService NotificationService { get { return null; } }
        public virtual ObservableCollection<bool> FollowUpSelector { get; set; }

        ObservableCollection<Task> AllTasks;
        void InitializeEmployees() {
            AllTasks = new ObservableCollection<Task>(DataBaseHelper.Instance.Tasks);

            List<Contact> uniqueContacts = new List<Contact>();
            foreach(Task task in AllTasks) {
                if(task.AssignTo == null || task.AssignTo.Photo == null)
                    continue;
                if(!uniqueContacts.Contains(task.AssignTo))
                    uniqueContacts.Add(task.AssignTo);
            }


            Employees = new List<EmployeeTasks>();
            foreach(Contact contact in uniqueContacts) {
                EmployeeTasks employeeTasks = ViewModelSource.Create(() => new EmployeeTasks(contact, AllTasks.Where(t => object.Equals(t.AssignTo, contact)).ToList()));
                employeeTasks.OnShow += (s, e) => {
                    SelectedEmployee = employeeTasks;
                    InitializeItemsSource();
                };
                Employees.Add(employeeTasks);
            }
            EmployeeTasks _allTasks = ViewModelSource.Create(() => new EmployeeTasks(null, AllTasks.ToList()));
            _allTasks.OnShow += (s, e) => {
                SelectedEmployee = _allTasks;
                InitializeItemsSource();
            };
            Employees.Add(_allTasks);
            SelectedEmployee = Employees[0];
        }
        protected override void InitializeData() {
            InitializeEmployees();
        }
        void UpdateEmployees() {
            foreach(EmployeeTasks employeeTask in Employees) {
                if(employeeTask.Employee == null)
                    employeeTask.Tasks = AllTasks.ToList();
                else
                    employeeTask.Tasks = AllTasks.Where(t => object.Equals(t.AssignTo, employeeTask.Employee)).ToList();
            }
            InitializeItemsSource();
        }
        protected override List<GridColumnModel> GetColumns() {
            List<GridColumnModel> result = new List<GridColumnModel>();
            result.Add(new GridColumnModel() { Name = "Complete", Width = 45, HorizontalHeaderContentAlignment = System.Windows.HorizontalAlignment.Center, AllowFiltering = DefaultBoolean.False, AllowSorting = DefaultBoolean.False });
            result.Add(new GridColumnModel() { Name = "Icon", Width = 40, HorizontalHeaderContentAlignment = System.Windows.HorizontalAlignment.Center, AllowFiltering = DefaultBoolean.False, AllowSorting = DefaultBoolean.False });
            result.Add(new GridColumnModel() { Name = "Priority", Width = 40 });
            result.Add(new GridColumnModel() { Name = "Subject", Width = 150, DisplayName = "Task Subject", AllowEditing = DefaultBoolean.False });
            result.Add(new GridColumnModel() { Name = "Status", Width = 100 });
            result.Add(new GridColumnModel() { Name = "PercentComplete" });
            result.Add(new GridColumnModel() { Name = "CreatedDate", Width = 90, DisplayName = "Date Created", GroupInterval = ColumnGroupInterval.DateRange });
            result.Add(new GridColumnModel() { Name = "StartDate", Width = 90 });
            result.Add(new GridColumnModel() { Name = "DueDate", Width = 90, GroupInterval = ColumnGroupInterval.DateRange });
            result.Add(new GridColumnModel() { Name = "CompletedDate", Width = 90, DisplayName = "Date Completed", AllowEditing = DefaultBoolean.False, GroupInterval = ColumnGroupInterval.DateRange });
            result.Add(new GridColumnModel() { Name = "Category", AllowEditing = DefaultBoolean.False });
            result.Add(new GridColumnModel() { Name = "FlagStatus", Width = 40 });
            return result;
        }
        public override void ShowNewItemWindow() { ShowNewTaskWindow(); }

        void model_Closed(object sender, EventArgs e) {
            EditTaskViewModel model = (EditTaskViewModel)sender;
            model.Closed -= model_Closed;
            if(editTaskWindow != null) editTaskWindow.Close();
            editTaskWindow = null;
            if(!model.Result)
                return;
            if(!model.IsNew) {
                SelectedItem.Assign(model.Task);
                NotificationService.CreatePredefinedNotification("Task Changed", model.Task.Subject, "").ShowAsync();
            } else {
                model.Task.AssignTo = SelectedEmployee.Employee;
                AllTasks.Add(model.Task);
                UpdateEmployees();
                SelectedItem = model.Task;
                NotificationService.CreatePredefinedNotification("New Task Created", model.Task.Subject, "").ShowAsync();
            }
        }
        public void ShowEditTaskWindow() {
            if(SelectedItem == null)
                return;
            ShowEditTaskWindowCore(SelectedItem.Clone(), false);
        }
        EditTaskWindow editTaskWindow;
        void ShowEditTaskWindowCore(Task task, bool isNew) {
            EditTaskViewModel model = ViewModelSource.Create(() => new EditTaskViewModel(task, isNew));
            model.Closed += model_Closed;
            editTaskWindow = new EditTaskWindow();
            editTaskWindow.DataContext = model;
            editTaskWindow.ShowDialog();
        }

        public void DeleteTask() {
            NotificationService.CreatePredefinedNotification("Task Deleted", SelectedItem.Subject, "").ShowAsync();
            AllTasks.Remove(SelectedItem);
            UpdateEmployees();
        }
        protected override void OnSelectedItemChanged(Task oldValue) {
            base.OnSelectedItemChanged(oldValue);
            UpdateSelector();
        }
        public virtual void FollowUp(string parameter) {
            var status = (FlagStatus)Enum.Parse(typeof(FlagStatus), parameter);
            switch(status) {
                case FlagStatus.Today:
                    SelectedItem.DueDate = DateTime.Today;
                    break;
                case FlagStatus.Tomorrow:
                    SelectedItem.DueDate = DateTime.Today.AddDays(1);
                    break;
                case FlagStatus.ThisWeek:
                    SelectedItem.DueDate = EvalHelpers.GetWeekStart(DateTime.Today).AddDays(5);
                    break;
                case FlagStatus.NextWeek:
                    SelectedItem.DueDate = EvalHelpers.GetWeekStart(DateTime.Today).AddDays(12);
                    break;
                case FlagStatus.NoDate:
                    SelectedItem.DueDate = null;
                    break;
                case FlagStatus.Custom:
                    CustomFlagViewModel vm = ViewModelSource.Create(() => new CustomFlagViewModel(SelectedItem.StartDate, SelectedItem.DueDate));
                    CustomFlagWindow window = new CustomFlagWindow() { DataContext = vm };
                    bool? result = window.ShowDialog();
                    if(result.HasValue && result.Value) {
                        SelectedItem.StartDate = vm.StartDate;
                        SelectedItem.DueDate = vm.DueDate;
                    }
                    break;
            }
            SelectedItem.Complete = false;
            UpdateSelector();
        }
        void UpdateSelector() {
            int index = 0;
            var statusCollection = Enum.GetValues(typeof(FlagStatus)).Cast<FlagStatus>();
            foreach(var flagStatus in statusCollection)
                FollowUpSelector[index++] = IsSelectedItemValid() && object.Equals(SelectedItem.FlagStatus, flagStatus);
        }
        void ChangeColumnCompletedDateVisibility(bool value) {
            BeginUpdate();
            Columns.First(column => column.Name == "CompletedDate").IsVisible = value;
            EndUpdate();
        }
        bool IsSelectedItemValid() {
            return SelectedItem != null;
        }
        public void SetListView() {
            ChangeColumnCompletedDateVisibility(true);
            ClearModel();
            BeginUpdate();
            GroupBy("CreatedDate");
            SortBy("CreatedDate", ColumnSortOrder.Descending);
            EndUpdate();
        }
        public void SetToDoListView() {
            ChangeColumnCompletedDateVisibility(false);
            ClearModel();
            BeginUpdate();
            FilterString = "[Status] <> 'Completed' And [DueDate] Is Not Null";
            GroupBy("DueDate");
            SortBy("DueDate", ColumnSortOrder.Ascending);
            EndUpdate();
        }
        public void SetCompletedView() {
            ChangeColumnCompletedDateVisibility(false);
            ClearModel();
            BeginUpdate();
            FilterString = "[Status] = 'Completed'";
            GroupBy("CompletedDate");
            SortBy("CompletedDate", ColumnSortOrder.Descending);
            EndUpdate();
        }
        public void SetTodayView() {
            ChangeColumnCompletedDateVisibility(false);
            ClearModel();
            FilterString = "IsOutlookIntervalToday([DueDate])";
        }
        public void SetPrioritizedView() {
            ChangeColumnCompletedDateVisibility(true);
            ClearModel();
            GroupBy("Category");
            GroupBy("Priority");
            SortBy("Priority", ColumnSortOrder.Descending);
            SortBy("DueDate", ColumnSortOrder.Ascending);
        }
        public void SetOverdueView() {
            ChangeColumnCompletedDateVisibility(false);
            ClearModel();
            BeginUpdate();
            FilterString = "[Overdue] = 'True'";
            GroupBy("DueDate");
            SortBy("DueDate", ColumnSortOrder.Ascending);
            EndUpdate();

        }
        public void SetSimpleListView() {
            ChangeColumnCompletedDateVisibility(false);
            ClearModel();
            SortBy("DueDate", ColumnSortOrder.Ascending);
        }
        public void SetDeferredView() {
            ChangeColumnCompletedDateVisibility(true);
            ClearModel();
            BeginUpdate();
            GroupBy("CompletedDate");
            SortBy("CreatedDate", ColumnSortOrder.Ascending);
            EndUpdate();
            FilterString = "[Status] = 'Deferred' Or [Status] = 'WaitingOnSomeoneElse'";
        }
    }
    public class CustomFlagViewModel {
        public CustomFlagViewModel(DateTime? startDate, DateTime? dueDate) {
            this.StartDate = startDate;
            this.DueDate = dueDate;
        }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? DueDate { get; set; }
    }
    public class EditTaskViewModel {
        public EditTaskViewModel(Task task, bool isNew) {
            this.Task = task;
            this.IsNew = isNew;
        }
        public bool IsNew { get; private set; }
        public Task Task { get; private set; }
        public virtual bool Result { get; set; }
        public event EventHandler Closed;
        public void Save() {
            Result = true;
            Close();
        }
        public void Close() {
            if(Closed != null)
                Closed(this, EventArgs.Empty);
        }
    }
    public class EmployeeTasks {
        public EmployeeTasks(Contact employee, List<Task> tasks) {
            this.Employee = employee;
            this.Tasks = tasks;
        }
        public Contact Employee { get; private set; }
        public virtual List<Task> Tasks { get; set; }
        public event EventHandler OnShow;
        public void Show() {
            if(OnShow != null)
                OnShow(this, EventArgs.Empty);
        }
    }
}
