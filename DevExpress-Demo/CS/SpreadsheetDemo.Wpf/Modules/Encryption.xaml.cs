using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Demos;
using DevExpress.Xpf.Editors;
using DevExpress.Mvvm;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.XtraSpreadsheet;
using Microsoft.Win32;
using System.Diagnostics;

namespace SpreadsheetDemo {
    public partial class Encryption : SpreadsheetDemoModule {

        public Encryption() {
            InitializeComponent();
            InitializeEncryptionOptions();
        }

        void InitializeEncryptionOptions() {
            passwordEdit.Text = "test";

            string[] array = Enum.GetNames(typeof(EncryptionType));
            typeEdit.ItemsSource = array;
            typeEdit.SelectedItem = EncryptionType.Strong.ToString();
        }

        void PasswordChanged(object sender, EditValueChangedEventArgs e) {
            spreadsheetControl1.Document.DocumentSettings.Encryption.Password = passwordEdit.Text;
        }

        void TypeChanged(object sender, EditValueChangedEventArgs e) {
            spreadsheetControl1.Document.DocumentSettings.Encryption.Type = (EncryptionType)Enum.Parse(typeof(EncryptionType), typeEdit.Text);
        }

        void EncryptAndSave(object sender, RoutedEventArgs e) {
            string filter = "Excel Workbook files(*.xlsx)|*.xlsx|Excel Binary Workbook(*.xlsb)|*.xlsb|Excel 97-2003 Workbook files(*.xls)|*.xls";
            string fileName = FileSaveHelper.GetSaveFileName(filter, "Document.xlsx");
            if (String.IsNullOrEmpty(fileName))
                return;
            spreadsheetControl1.SaveDocument(fileName);
            if (openFileCheckEditBox.IsChecked.HasValue && openFileCheckEditBox.IsChecked.Value)
                FileOpenHelper.ShowFile(fileName);
        }
    }
}
