using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDX_Demo.Module.MVVM_Demo.ServicesDemo
{
    public class GridExportService:ServiceBase, IExportService
    {
        void IExportService.Export(Stream stream, ExportFormat format)
        {
            GridControl grid = (GridControl)AssociatedObject;
            switch (format)
            {
                case ExportFormat.Xlsx:
                    grid.View.ExportToXlsx(stream);
                    break;
                case ExportFormat.Pdf:
                    grid.View.ExportToPdf(stream);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
