using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Office;
using DevExpress.Spreadsheet;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Spreadsheet;
using DevExpress.Xpf.Core;

namespace SpreadsheetDemo {
    public partial class FirstLook : SpreadsheetDemoModule {

        public FirstLook() {
            InitializeComponent();
        }

        private void spreadsheetControl1_InvalidFormatException(object sender, SpreadsheetInvalidFormatExceptionEventArgs e) {
            ThemedMessageBox.Show(
                "Error",
                string.Format("Cannot open the file '{0}' because the file format or file extension is not valid.\n" +
                "Verify that file has not been corrupted and that the file extension matches the format of the file.", e.SourceUri),
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void spreadsheetControl1_DocumentClosing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (spreadsheetControl1.Modified) {
                string currentFileName = spreadsheetControl1.Options.Save.CurrentFileName;
                string message = !string.IsNullOrEmpty(currentFileName) ?
                    string.Format("Do you want to save the changes you made for '{0}'?", currentFileName) :
                    "Do you want to save the changes?";
                MessageBoxResult result = ThemedMessageBox.Show("Warning", message, MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                    spreadsheetControl1.SaveDocument();
                e.Cancel = result == MessageBoxResult.Cancel;
            }
        }
    }
}
