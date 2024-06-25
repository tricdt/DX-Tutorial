using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace MVVMDemo.Services {
    public class UIObjectServiceViewModel {
        public void Export() {
            const string format = "Pdf";
            var path = Path.ChangeExtension(Path.GetTempFileName(), format.ToLower());

            IUIObjectService service = this.GetRequiredService<IUIObjectService>("GridService");
            using (FileStream file = File.Create(path)) {
                service.Object.View.ExportToPdf(file);
            }
            if (MessageBox.Show("Do you want to open the following file?\r\n" + path, "Open File", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Process.Start(new ProcessStartInfo { FileName = path, UseShellExecute = true });
            else
                File.Delete(path);
        }

        public void ShowColumnChooser() {
            IUIObjectService service = this.GetRequiredService<IUIObjectService>("TableViewService");
            service.Object.ShowColumnChooser();
        }
    }
}
