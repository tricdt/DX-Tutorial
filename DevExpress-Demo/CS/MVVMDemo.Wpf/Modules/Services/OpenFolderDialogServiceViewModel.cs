using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVVMDemo.Services {
    public class OpenFolderDialogServiceViewModel {
        public virtual string OpenedFolders { get; protected set; }
        public void ShowDialog() {
            IOpenFolderDialogService service = this.GetService<IOpenFolderDialogService>();
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (service.ShowDialog(null, directory)) {
                IEnumerable<string> folderNames = service.Folders.Select(folderInfo => folderInfo.Path);
                OpenedFolders = string.Join(Environment.NewLine, folderNames);
            }
        }
    }
}
