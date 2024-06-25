using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Core;
using System;
using System.Collections.ObjectModel;

namespace EditorsDemo {
    [POCOViewModel]
    public class NotifyIconViewModel {
        public ObservableCollection<string> EventsLog { get; set; }

        protected virtual IMessageBoxService MessageBoxService { get { return null; } }
        protected virtual INotifyIconService NotifyIconService { get { return null; } }

        void AddStringInLog(string logString) {
            EventsLog.Add(DateTime.Now.ToShortTimeString() + logString);
            if (EventsLog.Count > 20)
                EventsLog.RemoveAt(0);
        }

        public NotifyIconViewModel() {            
            EventsLog = new ObservableCollection<string>();
            AddStringInLog(" - NotifyIcon is created");
        }

        public void ShowMessageBox() {
            MessageBoxService.ShowMessage("hello");
        }

        public void SetStatusIcon(object icon) {
            AddStringInLog(" - NotifyIcon state changed to " + icon.ToString());
            NotifyIconService.SetStatusIcon(icon);
        }

        public void ResetStatusIcon() {
            AddStringInLog(" - NotifyIcon state reseted on default state");
            NotifyIconService.ResetStatusIcon();
        }

        public void DoIconLeftAction() {
            AddStringInLog(" - NotifyIcon left mouse button click");
            MessageBoxService.ShowMessage("NotifyIcon left mouse button click!");
        }

        public void PopupButtonClick(object buttonName) {
            AddStringInLog(" - NotifyIcon context menu button '" + buttonName.ToString() + "' click");
        }
    }
}
