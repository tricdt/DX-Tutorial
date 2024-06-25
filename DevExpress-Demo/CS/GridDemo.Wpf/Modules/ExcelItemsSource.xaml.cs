using DevExpress.Spreadsheet;
using DevExpress.Xpf.Spreadsheet;
using System;
using System.Windows;

namespace GridDemo {
    public partial class ExcelItemsSource : GridDemoModule {
        public static readonly Uri SourceUri = new Uri(@"pack://application:,,,/GridDemo;component/Data/Orders.xls");
        public static SpreadsheetDocumentSource Source = new SpreadsheetDocumentSource(Application.GetResourceStream(SourceUri).Stream, DocumentFormat.Xls);

        public ExcelItemsSource() {
            InitializeComponent();
        }
    }
}
