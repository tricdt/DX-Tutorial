using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDX_Demo.Module.MVVM_Demo.ServicesDemo
{
    public class SaveFileDialogServiceViewModel
    {
        public virtual string FileName { get; protected set; }
        public void ShowDialog(string serviceName)
        {
            ISaveFileDialogService service = this.GetService<ISaveFileDialogService>(serviceName);
            service.Filter = "All files (*.*)|*.*|Image files (*.png;*.jpeg)|*.png;*.jpeg";
            service.DefaultFileName = "ISaveFileDialogService";
            service.DefaultExt = "png";

            if (service.ShowDialog())
            {
                FileName = service.File.GetFullName();
            }
        }
    }
}
