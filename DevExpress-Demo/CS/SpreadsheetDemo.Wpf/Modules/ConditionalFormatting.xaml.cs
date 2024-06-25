using System;
using System.Globalization;
using DevExpress.Spreadsheet;
using DevExpress.Xpf.DemoBase;
using DevExpress.XtraSpreadsheet.Demos;

namespace SpreadsheetDemo {
    public partial class ConditionalFormatting : SpreadsheetDemoModule {
        public ConditionalFormatting() {
            InitializeComponent();
        }

        void spreadsheetControl1_DocumentLoaded(object sender, EventArgs e) {
            UpdateConditionalFormatting();
        }
        void OnCheckedChanged(object sender, System.Windows.RoutedEventArgs e) {
            UpdateConditionalFormatting();
        }

        void UpdateConditionalFormatting() {
            if (spreadsheetControl1 == null)
                return;

            spreadsheetControl1.BeginUpdate();
            try {
                Worksheet sheet = spreadsheetControl1.ActiveWorksheet;
                sheet.ConditionalFormattings.Clear();
                if (chkImports.IsChecked == true) {
                    DevExpress.XtraSpreadsheet.Demos.TopTradingPartners.ApplyTopImportsConditionalFormatting(sheet);
                    DevExpress.XtraSpreadsheet.Demos.TopTradingPartners.ApplyImportsYearlyChangeConditionalFormatting(sheet);
                }
                if (chkExports.IsChecked == true) {
                    DevExpress.XtraSpreadsheet.Demos.TopTradingPartners.ApplyTopExportsConditionalFormatting(sheet);
                    DevExpress.XtraSpreadsheet.Demos.TopTradingPartners.ApplyExportsYearlyChangeConditionalFormatting(sheet);
                }
                if (chkBalance.IsChecked == true) {
                    DevExpress.XtraSpreadsheet.Demos.TopTradingPartners.ApplyBalanceTrendConditionalFormatting(sheet);
                    DevExpress.XtraSpreadsheet.Demos.TopTradingPartners.ApplyBalanceChangeConditionalFormatting(sheet);
                }
                if (chkAsiaRegion.IsChecked == true)
                    DevExpress.XtraSpreadsheet.Demos.TopTradingPartners.ApplyAsiaCountriesConditionalFormatting(sheet);
            }
            finally {
                spreadsheetControl1.EndUpdate();
            }
        }
    }
}
