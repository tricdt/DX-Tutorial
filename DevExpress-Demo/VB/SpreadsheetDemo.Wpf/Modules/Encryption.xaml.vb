Imports System
Imports System.Windows
Imports DevExpress.Spreadsheet
Imports DevExpress.Xpf.Editors
Imports System.Diagnostics

Namespace SpreadsheetDemo

    Public Partial Class Encryption
        Inherits SpreadsheetDemoModule

        Public Sub New()
            InitializeComponent()
            InitializeEncryptionOptions()
        End Sub

        Private Sub InitializeEncryptionOptions()
            passwordEdit.Text = "test"
            Dim array As String() = [Enum].GetNames(GetType(EncryptionType))
            typeEdit.ItemsSource = array
            typeEdit.SelectedItem = EncryptionType.Strong.ToString()
        End Sub

        Private Sub PasswordChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            spreadsheetControl1.Document.DocumentSettings.Encryption.Password = passwordEdit.Text
        End Sub

        Private Sub TypeChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
            spreadsheetControl1.Document.DocumentSettings.Encryption.Type = CType([Enum].Parse(GetType(EncryptionType), typeEdit.Text), EncryptionType)
        End Sub

        Private Sub EncryptAndSave(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim filter As String = "Excel Workbook files(*.xlsx)|*.xlsx|Excel Binary Workbook(*.xlsb)|*.xlsb|Excel 97-2003 Workbook files(*.xls)|*.xls"
            Dim fileName As String = FileSaveHelper.GetSaveFileName(filter, "Document.xlsx")
            If String.IsNullOrEmpty(fileName) Then Return
            spreadsheetControl1.SaveDocument(fileName)
            If openFileCheckEditBox.IsChecked.HasValue AndAlso openFileCheckEditBox.IsChecked.Value Then FileOpenHelper.ShowFile(fileName)
        End Sub
    End Class
End Namespace
