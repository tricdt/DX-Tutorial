using System;
using DevExpress.Xpf.Ribbon;
using System.Windows.Controls;
using System.Globalization;

namespace ProductsDemo.Modules {
    public partial class SpreadsheetModule : UserControl {
        const string FileName = "LoanCalculator.xlsx";

        public SpreadsheetModule() {
            InitializeComponent();
            string filePath = Utils.GetRelativePath(FileName);
            if (String.IsNullOrEmpty(filePath))
                return;
            this.spreadsheetControl.LoadDocument(filePath);
        }        
    }


}
