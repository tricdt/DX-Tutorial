using DevExpress.Mvvm.Native;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm;

namespace DockingDemo.ViewModels {
    public class DockLayoutManagerEventsViewModel : DevExpress.Mvvm.ViewModelBase {
        IDockLayoutManagerEventsService EventsService { get { return ServiceContainer.GetService<IDockLayoutManagerEventsService>(); } }
        public DockLayoutManagerEventsViewModel() { }
        ICommand clearLogCommand;
        public ICommand ClearLogCommand {
            get {
                if(clearLogCommand == null) clearLogCommand = DelegateCommandFactory.Create(ClearLog);
                return clearLogCommand;
            }
        }
        void ClearLog() {
            EventsService.ClearEventsLog();
        }
    }
}
