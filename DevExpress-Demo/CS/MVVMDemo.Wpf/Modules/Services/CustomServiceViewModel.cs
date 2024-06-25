using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace MVVMDemo.Services {
    public class CustomServiceViewModel {
        public void Export(ExportFormat format) {
            var path = Path.ChangeExtension(Path.GetTempFileName(), format.ToString().ToLower());

            IExportService service = this.GetRequiredService<IExportService>();
            using(FileStream file = File.Create(path)) {
                service.Export(file, format);
            }

            if(MessageBox.Show("Do you want to open the following file?\r\n" + path, "Open File", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Process.Start(new ProcessStartInfo { FileName = path, UseShellExecute = true });
            else
                File.Delete(path);
        }
    }
    public interface IExportService {
        void Export(Stream stream, ExportFormat format);
    }
    public enum ExportFormat {
        Xlsx, Pdf
    }
}
