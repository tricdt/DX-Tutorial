using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace MVVMDemo.Services {
    public class SaveFileDialogServiceViewModel {
        public virtual string FileName { get; protected set; }
        public void ShowDialog(string serviceName) {
            ISaveFileDialogService service = this.GetService<ISaveFileDialogService>(serviceName);
            service.Filter = "All files (*.*)|*.*|Image files (*.png;*.jpeg)|*.png;*.jpeg";
            service.DefaultFileName = "ISaveFileDialogService";
            service.DefaultExt = "png";
            
            if(service.ShowDialog()) {
                FileName = service.File.GetFullName();
            }
        }
    }
}
