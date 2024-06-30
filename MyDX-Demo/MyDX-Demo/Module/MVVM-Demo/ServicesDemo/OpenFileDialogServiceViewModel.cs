using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDX_Demo.Module.MVVM_Demo.ServicesDemo
{
    public class OpenFileDialogServiceViewModel
    {
        public virtual string OpenedFiles { get; protected set; }
        public void ShowDialog(string serviceName)
        {
            IOpenFileDialogService service = this.GetService<IOpenFileDialogService>(serviceName);
            service.Filter = "All files (*.*)|*.*|Image files (*.png;*.jpeg)|*.png;*.jpeg";
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (service.ShowDialog(directory))
            {
                IEnumerable<string> fileNames = service.Files.Select(fileInfo => fileInfo.GetFullName());
                OpenedFiles = string.Join(Environment.NewLine, fileNames);
            }
        }
    }
}
