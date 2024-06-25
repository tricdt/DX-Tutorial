using System;
using DevExpress.Spreadsheet;
using DevExpress.XtraSpreadsheet.Services;
using DevExpress.XtraSpreadsheet.Commands;

namespace SpreadsheetDemo {
    public partial class HeaderFooter : SpreadsheetDemoModule {
        public HeaderFooter() {
            InitializeComponent();
        }

        private void btnPageSetup_Click(object sender, System.Windows.RoutedEventArgs e) {
            ISpreadsheetCommandFactoryService service = (ISpreadsheetCommandFactoryService)spreadsheetControl1.GetService(typeof(ISpreadsheetCommandFactoryService));
            SpreadsheetCommand command = service.CreateCommand(SpreadsheetCommandId.PageSetupHeaderFooter);
            command.ForceExecute(command.CreateDefaultCommandUIState());
        }

        private void btnPrintPreview_Click(object sender, System.Windows.RoutedEventArgs e) {
            ISpreadsheetCommandFactoryService service = (ISpreadsheetCommandFactoryService)spreadsheetControl1.GetService(typeof(ISpreadsheetCommandFactoryService));
            SpreadsheetCommand command = service.CreateCommand(SpreadsheetCommandId.FilePrintPreview);
            command.ForceExecute(command.CreateDefaultCommandUIState());
        }
    }
}
