using DevExpress.Mvvm.POCO;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyDX_Demo.Module.MVVM_Demo.ServicesDemo
{
    public class CustomServiceViewModel
    {
        public void Export(ExportFormat format)
        {
            var path = Path.ChangeExtension(Path.GetTempFileName(), format.ToString().ToLower());
            IExportService service = this.GetRequiredService<IExportService>();
            using (FileStream file = File.Create(path))
            {
                service.Export(file, format);
            }
            if (MessageBox.Show("Do you want to open the following file?\r\n" + path, "Open File", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Process.Start(new ProcessStartInfo { FileName = path, UseShellExecute = true });
            else
                File.Delete(path);
        }
    }
    public interface IExportService
    {
        void Export(Stream stream, ExportFormat format);
    }
    public enum ExportFormat
    {
        Xlsx, Pdf
    }
}
