Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Spreadsheet
Imports DevExpress.Xpf.Core

Namespace SpreadsheetDemo

    Public Partial Class FirstLook
        Inherits SpreadsheetDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub spreadsheetControl1_InvalidFormatException(ByVal sender As Object, ByVal e As SpreadsheetInvalidFormatExceptionEventArgs)
            ThemedMessageBox.Show("Error", String.Format("Cannot open the file '{0}' because the file format or file extension is not valid." & Microsoft.VisualBasic.Constants.vbLf & "Verify that file has not been corrupted and that the file extension matches the format of the file.", e.SourceUri), MessageBoxButton.OK, MessageBoxImage.Error)
        End Sub

        Private Sub spreadsheetControl1_DocumentClosing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
            If spreadsheetControl1.Modified Then
                Dim currentFileName As String = spreadsheetControl1.Options.Save.CurrentFileName
                Dim message As String = If(Not String.IsNullOrEmpty(currentFileName), String.Format("Do you want to save the changes you made for '{0}'?", currentFileName), "Do you want to save the changes?")
                Dim result As MessageBoxResult = ThemedMessageBox.Show("Warning", message, MessageBoxButton.YesNoCancel, MessageBoxImage.Warning)
                If result = MessageBoxResult.Yes Then spreadsheetControl1.SaveDocument()
                e.Cancel = result = MessageBoxResult.Cancel
            End If
        End Sub
    End Class
End Namespace
