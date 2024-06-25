using System;
using DevExpress.Spreadsheet;
using DevExpress.Xpf.Spreadsheet;
using System.Windows;

namespace SpreadsheetDemo {
    public partial class PivotTable : SpreadsheetDemoModule {
        public PivotTable() {
            InitializeComponent();
        }

        void ApplyOptions() {
            SpreadsheetPivotTableFieldListOptions options = spreadsheetControl1.Options.PivotTableFieldList;
            options.StartPosition = SpreadsheetPivotTableFieldListStartPosition.ManualScreen;
            options.StartSize = new Size(416, 601);
            options.StartLocation = CalculateStartLocation(options.StartSize.Width);
        }

        Point CalculateStartLocation(double width) {
            Point result = spreadsheetControl1.PointToScreen(new Point());
            result.X /= spreadsheetControl1.DpiX / 96.0;
            result.X += this.ActualWidth - width;
            result.Y /= spreadsheetControl1.DpiY / 96.0;
            return result;
        }

        void PivotTable_Unloaded(object sender, RoutedEventArgs e) {
            spreadsheetControl1.Selection = spreadsheetControl1.ActiveWorksheet["A1"];
        }

        void SpreadsheetControl1_DocumentLoaded(object sender, EventArgs e) {
            ApplyOptions();
            spreadsheetControl1.Document.PivotCaches.RefreshAll();
            spreadsheetControl1.Selection = spreadsheetControl1.ActiveWorksheet["C3"];
        }
    }
}
