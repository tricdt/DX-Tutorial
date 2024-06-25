using System;
using System.Windows.Threading;

namespace SpreadsheetDemo {
    public partial class RangeAsDataSource : SpreadsheetDemoModule {
        private bool updateLocked = false;
        private DispatcherOperation operation = null;

        public RangeAsDataSource() {
            InitializeComponent();
        }

        private void spreadsheetControl1_DocumentLoaded(object sender, EventArgs e) {
            var workbook = spreadsheetControl1.Document;
            var sheet = workbook.Worksheets[0];
            
            
            chartControl1.DataSource = sheet["B3:D103"].GetDataSource();
            var series = chartControl1.Diagram.Series[0];
            series.ArgumentDataMember = "Column 0";
            series.ValueDataMember = "Column 1";
            series = chartControl1.Diagram.Series[1];
            series.ArgumentDataMember = "Column 0";
            series.ValueDataMember = "Column 2";
        }

        private void spreadsheetControl1_CellValueChanged(object sender, DevExpress.XtraSpreadsheet.SpreadsheetCellEventArgs e) {
            updateLocked = true;
            try {
                if (e.Cell.GetReferenceA1() == "F3")
                    trbMean.Value = e.Cell.Value.NumericValue;
                else if (e.Cell.GetReferenceA1() == "F6")
                    trbStdDev.Value = e.Cell.Value.NumericValue * 100;
            }
            finally {
                updateLocked = false;
            }
        }

        private void trbMean_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            if (!updateLocked)
                UpdateValueAsync(UpdateMean);
        }

        private void trbStdDev_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            if (!updateLocked)
                UpdateValueAsync(UpdateStdDev);
        }

        private void UpdateValueAsync(Action action) {
            if (operation == null || operation.Status == DispatcherOperationStatus.Aborted || operation.Status == DispatcherOperationStatus.Completed)
                operation = Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, action);
        }

        private void UpdateMean() {
            spreadsheetControl1.ActiveWorksheet["F3"].Value = trbMean.Value;
        }

        private void UpdateStdDev() {
            spreadsheetControl1.ActiveWorksheet["F6"].Value = trbStdDev.Value / 100.0;
        }
    }
}
