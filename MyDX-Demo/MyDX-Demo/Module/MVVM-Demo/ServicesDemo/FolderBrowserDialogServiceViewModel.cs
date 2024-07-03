using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDX_Demo.Module.MVVM_Demo.ServicesDemo
{
    public class FolderBrowserDialogServiceViewModel
    {
        public virtual string SelectedFolder { get; protected set; }
        public void ShowDialog(string serviceName)
        {
            IFolderBrowserDialogService service = this.GetService<IFolderBrowserDialogService>(serviceName);
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            service.StartPath = directory;
            if (service.ShowDialog())
            {
                SelectedFolder = service.ResultPath;
            }
        }
    }
}
