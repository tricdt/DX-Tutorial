using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVVMDemo.Services {
    public class FolderBrowserDialogServiceViewModel {
        public virtual string SelectedFolder { get; protected set; }
        public void ShowDialog(string serviceName) {
            IFolderBrowserDialogService service = this.GetService<IFolderBrowserDialogService>(serviceName);
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            service.StartPath = directory;
            if(service.ShowDialog()) {
                SelectedFolder = service.ResultPath;
            }
        }
    }
}
