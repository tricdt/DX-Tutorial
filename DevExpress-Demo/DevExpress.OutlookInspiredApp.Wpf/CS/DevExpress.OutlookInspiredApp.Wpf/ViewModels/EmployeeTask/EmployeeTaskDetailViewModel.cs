using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DevExpress.DevAV.Common.Utils;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;


namespace DevExpress.DevAV.ViewModels {
    public class EmployeeTaskDetailViewModel {
        public static EmployeeTaskDetailViewModel Create() {
            return ViewModelSource.Create(() => new EmployeeTaskDetailViewModel());
        }

        public virtual ObservableCollection<AttachedFileInfo> FilesInfo { get; set; }
        public AttachedFileInfo SelectedFile { get; set; }
        List<string> createdFilePaths;

        protected EmployeeTaskDetailViewModel() { }
        public virtual EmployeeTask Entity { get; set; }
        protected IMessageBoxService MessageBoxService { get { return this.GetRequiredService<IMessageBoxService>(); } }
        public virtual bool ZoomEditEnabled { get; set; }

        public virtual void OnEntityChanged() {
            this.RaisePropertyChanged(x => x.FollowUp);
            if(Entity == null) return;
            FilesInfo = new ObservableCollection<AttachedFileInfo>();
            foreach(var file in Entity.AttachedFiles)
                FilesInfo.Add(FilesHelper.GetAttachedFileInfo(file.Name));
        }

        public virtual string FollowUp {
            get {
                return (Entity == null) ? string.Empty : "Follow Up. " + GetStartDate() + GetDueDate() + GetReminderDateTime()
                    + ((Entity.FollowUp != EmployeeTaskFollowUp.NoDate) ? String.Format("Due {0}.", GetFollowUp(Entity.FollowUp)) : string.Empty);
            }
        }
        public virtual void Unloaded() {
            FilesHelper.DeleteTempFiles(ref createdFilePaths);            
        }        
        public virtual void OpenFile() {
            if(createdFilePaths == null)
                createdFilePaths = new List<string>();
            int index = FilesInfo.IndexOf(SelectedFile);
            if(Entity.AttachedFilesCount > index)
                createdFilePaths.Add(FilesHelper.OpenFileFromDb(SelectedFile.Name, Entity.AttachedFiles[index].Content));
        }
        public virtual bool CanOpenFile() {
            return SelectedFile != null;
        }
        string GetStartDate() {
            if(Entity.StartDate == null) return string.Empty;
            return String.Format("Start by {0:dddd, MMMM d, yyyy}. ", Entity.StartDate.Value);
        }
        string GetDueDate() {
            if(Entity.DueDate == null) return string.Empty;
            return String.Format("Due by {0:dddd, MMMM d, yyyy}. ", Entity.DueDate.Value);
        }
        string GetReminderDateTime() {
            if(!Entity.Reminder || Entity.ReminderDateTime == null) return string.Empty;
            return String.Format("Reminder by {0:dddd, MMMM d, yyyy hh:mm tt}. ", Entity.ReminderDateTime.Value);
        }
        string GetFollowUp(EmployeeTaskFollowUp followUp) {
            var fieldInfo = followUp.GetType().GetField(followUp.ToString());
            return ((DisplayAttribute)(fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault())).Name;
        }

    }
}
