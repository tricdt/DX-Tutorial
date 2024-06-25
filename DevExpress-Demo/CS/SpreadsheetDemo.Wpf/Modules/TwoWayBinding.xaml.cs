using DevExpress.Spreadsheet;
using System;

namespace SpreadsheetDemo {
    public partial class TwoWayBinding : SpreadsheetDemoModule {
        public TwoWayBinding() {
            InitializeComponent();
        }

        void spreadsheet_DocumentLoaded(object sender, EventArgs e) {
            IWorkbook workbook = this.spreadsheet.Document;
            Worksheet sheet = workbook.Worksheets[0];

            
            Table expenses = sheet.Tables[0];
            RangeDataSourceOptions options = new RangeDataSourceOptions();
            options.PreserveFormulas = true;
            options.SkipHiddenRows = true;
            grid.ItemsSource = expenses.DataRange.GetDataSource(options);
        }
    }
}
