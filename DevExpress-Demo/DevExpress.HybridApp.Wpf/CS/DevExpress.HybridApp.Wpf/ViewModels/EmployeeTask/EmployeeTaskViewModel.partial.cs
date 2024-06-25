using System;
using System.Linq;
using DevExpress.DevAV.ViewModels;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.DevAV;

namespace DevExpress.DevAV.ViewModels {
    partial class EmployeeTaskViewModel {
        protected override EmployeeTask CreateEntity() {
            EmployeeTask entity = base.CreateEntity();
            entity.StartDate = DateTime.Now;
            entity.DueDate = DateTime.Now + new TimeSpan(48, 0, 0);
            return entity;
        }
        protected override void OnEntityChanged() {
            base.OnEntityChanged();
            this.RaisePropertyChanged(vm => vm.ReminderTime);
            if(Entity != null)
                Logger.Log(string.Format("HybridApp: Edit Task: {0}",
                    string.IsNullOrEmpty(Entity.Subject) ? "<New>" : Entity.Subject));
        }
        public DateTime? ReminderTime {
            get {
                if(this.Entity == null || this.Entity.ReminderDateTime == null)
                    return null;
                return this.Entity.ReminderDateTime.Value;
            }
            set {
                if(this.Entity == null || value == null || this.Entity.ReminderDateTime == null)
                    return;
                DateTime reminderDateTime = (DateTime)this.Entity.ReminderDateTime;
                this.Entity.ReminderDateTime = new DateTime(reminderDateTime.Year, reminderDateTime.Month, reminderDateTime.Day, ((DateTime)value).Hour, ((DateTime)value).Minute, reminderDateTime.Second);
            }
        }
        protected override string GetTitle() {            
            return Entity.Owner != null ? Entity.Owner.FullName : string.Empty;
        }
    }
    public class EmployeeTaskStatusWithExternalMetadata {
        public static void BuildMetadata(EnumMetadataBuilder<EmployeeTaskStatus> builder) {
            builder
                .Member(EmployeeTaskStatus.NotStarted)
                    .ImageUri("pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/Status/NotStarted.svg")
                .EndMember()
                .Member(EmployeeTaskStatus.NeedAssistance)
                    .ImageUri("pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/Status/NeedAssistance.svg")
                .EndMember()
                .Member(EmployeeTaskStatus.InProgress)
                    .ImageUri("pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/Status/InProgress.svg")
                .EndMember()
                .Member(EmployeeTaskStatus.Deferred)
                    .ImageUri("pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/Status/Deferred.svg")
                .EndMember()
                .Member(EmployeeTaskStatus.Completed)
                    .ImageUri("pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/Status/Completed.svg")
                .EndMember();
        }
    }
    public class EmployeeTaskPriorityWithExternalMetadata {
        public static void BuildMetadata(EnumMetadataBuilder<EmployeeTaskPriority> builder) {
            builder
                .Member(EmployeeTaskPriority.High)
                    .ImageUri("pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/Priority/MediumPriority.svg")
                .EndMember()
                .Member(EmployeeTaskPriority.Low)
                    .ImageUri("pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/Priority/LowPriority.svg")
                .EndMember()
                .Member(EmployeeTaskPriority.Normal)
                    .ImageUri("pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/Priority/NormalPriority.svg")
                .EndMember()
                .Member(EmployeeTaskPriority.Urgent)
                    .ImageUri("pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/Priority/HighPriority.svg")
                .EndMember();
        }
    }
}
