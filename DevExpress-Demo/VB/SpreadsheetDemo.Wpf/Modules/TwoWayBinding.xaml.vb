Imports DevExpress.Spreadsheet
Imports System

Namespace SpreadsheetDemo

    Public Partial Class TwoWayBinding
        Inherits SpreadsheetDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub spreadsheet_DocumentLoaded(ByVal sender As Object, ByVal e As EventArgs)
            Dim workbook As IWorkbook = spreadsheet.Document
            Dim sheet As Worksheet = workbook.Worksheets(0)
            Dim expenses As Table = sheet.Tables(0)
            Dim options As RangeDataSourceOptions = New RangeDataSourceOptions()
            options.PreserveFormulas = True
            options.SkipHiddenRows = True
            grid.ItemsSource = expenses.DataRange.GetDataSource(options)
        End Sub
    End Class
End Namespace
