Imports System.Windows.Controls

Namespace ProductsDemo.Modules

    Public Partial Class SpreadsheetModule
        Inherits UserControl

        Const FileName As String = "LoanCalculator.xlsx"

        Public Sub New()
            Me.InitializeComponent()
            Dim filePath As String = Utils.GetRelativePath(FileName)
            If String.IsNullOrEmpty(filePath) Then Return
            Me.spreadsheetControl.LoadDocument(filePath)
        End Sub
    End Class
End Namespace
