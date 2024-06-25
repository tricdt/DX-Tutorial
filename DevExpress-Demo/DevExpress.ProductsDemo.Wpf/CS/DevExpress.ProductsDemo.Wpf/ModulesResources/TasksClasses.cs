using System;
using System.Linq;
using DevExpress.DevAV.Properties;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.XtraEditors.DXErrorProvider;

namespace ProductsDemo.Modules {
    public enum TaskStatus { NotStarted, InProgress, Completed, WaitingOnSomeoneElse, Deferred };
    public enum TaskCategory {
        [Image("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Category_HouseChores.svg")]
        HouseChores,
        [Image("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Category_Shopping.svg")]
        Shopping,
        [Image("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Category_Work.svg")]
        Work 
    };
    public enum FlagStatus { Today, Tomorrow, ThisWeek, NextWeek, NoDate, Custom, Completed };
    public enum TaskPriority {
        [Image("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Priority_High.svg")]
        Low = 0, 
        Medium = 1,
        [Image("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Priority_Low.svg")]
        High = 2 
    };

    public class Task : BindableBase, IDXDataErrorInfo {
        TaskPriority priority = TaskPriority.Medium;
        int percentComplete = 0;
        DateTime createdDate;
        DateTime? startDate = null, dueDate = null, completedDate = null;
        string subject, description;
        TaskStatus status = TaskStatus.NotStarted;
        TaskCategory category;
        Contact assignTo = null;
        DelegateCommand changeCompleteCommand;

        public Task(string subject, TaskCategory category)
            : this(subject, category, DateTime.Now) {
        }
        internal Task(string subject, TaskCategory category, DateTime date) {
            this.subject = subject;
            this.category = category;
            this.createdDate = date;
        }
        public TaskPriority Priority {
            get { return priority; }
            set { SetProperty(ref priority, value, "Priority"); }
        }
        public int PercentComplete {
            get { return percentComplete; }
            set { SetProperty(ref percentComplete, value < 0 ? 0 : value > 100 ? 100 : value, "PercentComplete", OnPercentCompleteChanged); }
        }
        void OnPercentCompleteChanged() {
            if(percentComplete == 100)
                Status = TaskStatus.Completed;
            else if(percentComplete > 0)
                Status = TaskStatus.InProgress;
        }
        public DateTime CreatedDate { get { return createdDate; } }
        public DateTime? StartDate {
            get { return startDate; }
            set { 
                SetProperty(ref startDate, value, "StartDate");
                RaisePropertyChanged("Overdue");
            }
        }
        public DateTime? DueDate {
            get { return dueDate; }
            set { 
                SetProperty(ref dueDate, value, "DueDate");
                RaisePropertyChanged("Overdue");
            }
        }
        public DateTime? CompletedDate {
            get { return completedDate; }
            set { SetProperty(ref completedDate, value, "CompletedDate"); }
        }
        public string Subject {
            get { return subject; }
            set { SetProperty(ref subject, value, "Subject"); }
        }
        public string Description {
            get { return description; }
            set { SetProperty(ref description, value, "Description"); }
        }
        public TaskCategory Category {
            get { return category; }
            set { SetProperty(ref category, value, "Category"); }
        }
        public TaskStatus Status {
            get { return status; }
            set { SetProperty(ref status, value, "Status", OnStatusChanged); }
        }
        void OnStatusChanged() {
            if(Status == TaskStatus.Completed) {
                PercentComplete = 100;
                CompletedDate = DateTime.Now;
            } else
                CompletedDate = null;
            if(Status == TaskStatus.NotStarted)
                PercentComplete = 0;
            if(Status == TaskStatus.InProgress && PercentComplete == 100)
                PercentComplete = 75;
            if(Status == TaskStatus.Deferred || Status == TaskStatus.WaitingOnSomeoneElse)
                DueDate = null;
            RaisePropertyChanged("Complete");
        }
        public Contact AssignTo { get { return assignTo; } set { assignTo = value; } }
        internal TimeSpan TimeDiff { get { return (DateTime.Now - CreatedDate); } }
        public bool Overdue {
            get {
                if(Status == TaskStatus.Completed || !DueDate.HasValue) return false;
                DateTime dDate = DueDate.Value.Date.AddDays(1);
                if(DateTime.Now >= dDate) return true;
                return false;
            }
        }
        public bool Complete {
            get { return Status == TaskStatus.Completed; }
            set {
                Status = value ? TaskStatus.Completed : TaskStatus.NotStarted;
                RaisePropertiesChanged(new[] { "Complete", "FlagStatus" });
            }
        }
        public DelegateCommand ChangeCompleteCommand {
            get {
                if(changeCompleteCommand == null)
                    changeCompleteCommand = new DelegateCommand(ChangeComplete);
                return changeCompleteCommand;
            }
        }
        void ChangeComplete() { Complete = !Complete; }
        public int Icon { get { return Complete ? 0 : 1; } }
        public FlagStatus FlagStatus {
            get {
                DateTime today = DateTime.Today;
                if(Complete) return FlagStatus.Completed;
                if(!DueDate.HasValue) return FlagStatus.NoDate;
                if(DueDate.Value.Date.Equals(today)) return FlagStatus.Today;
                if(DueDate.Value.Date.Equals(today.AddDays(1))) return FlagStatus.Tomorrow;
                DateTime thisWeekStart = DevExpress.Data.Filtering.Helpers.EvalHelpers.GetWeekStart(today);
                if(DueDate.Value.Date >= thisWeekStart && DueDate.Value.Date < thisWeekStart.AddDays(7)) return FlagStatus.ThisWeek;
                if(DueDate.Value.Date >= thisWeekStart.AddDays(7) && DueDate.Value.Date < thisWeekStart.AddDays(14)) return FlagStatus.NextWeek;
                return FlagStatus.Custom;
            }
        }
        public void Assign(Task task) {
            this.subject = task.Subject;
            this.priority = task.Priority;
            this.percentComplete = task.PercentComplete;
            this.createdDate = task.CreatedDate;
            this.startDate = task.StartDate;
            this.dueDate = task.DueDate;
            this.completedDate = task.CompletedDate;
            this.description = task.Description;
            this.category = task.Category;
            this.status = task.Status;
            this.assignTo = task.AssignTo;
            RaisePropertiesChanged(string.Empty);
        }
        public Task Clone() {
            Task task = new Task(this.Subject, this.Category);
            task.Assign(this);
            return task;
        }
        #region IDXDataErrorInfo Members
        void IDXDataErrorInfo.GetError(DevExpress.XtraEditors.DXErrorProvider.ErrorInfo info) { }

        void IDXDataErrorInfo.GetPropertyError(string propertyName, DevExpress.XtraEditors.DXErrorProvider.ErrorInfo info) {
            if(propertyName == "DueDate") {
                if((DueDate.HasValue && StartDate.HasValue) && DueDate < StartDate)
                    SetErrorInfo(info, Resources.DueDateError, ErrorType.Critical);
                if(!DueDate.HasValue && Status == TaskStatus.InProgress)
                    SetErrorInfo(info, Resources.DueDateWarning, ErrorType.Warning);
            }
        }
        void SetErrorInfo(DevExpress.XtraEditors.DXErrorProvider.ErrorInfo info, string errorText, ErrorType errorType) {
            info.ErrorText = errorText;
            info.ErrorType = errorType;
        }
        #endregion
    }

}
