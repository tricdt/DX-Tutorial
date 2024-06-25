Imports System
Imports DevExpress.Xpf.Spreadsheet
Imports System.Windows

Namespace SpreadsheetDemo

    Public Partial Class PivotTable
        Inherits SpreadsheetDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub ApplyOptions()
            Dim options As SpreadsheetPivotTableFieldListOptions = spreadsheetControl1.Options.PivotTableFieldList
            options.StartPosition = SpreadsheetPivotTableFieldListStartPosition.ManualScreen
            options.StartSize = New Size(416, 601)
            options.StartLocation = CalculateStartLocation(options.StartSize.Width)
        End Sub

        Private Function CalculateStartLocation(ByVal width As Double) As Point
            Dim result As Point = spreadsheetControl1.PointToScreen(New Point())
            result.X /= spreadsheetControl1.DpiX / 96.0
            result.X += ActualWidth - width
            result.Y /= spreadsheetControl1.DpiY / 96.0
            Return result
        End Function

        Private Sub PivotTable_Unloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            spreadsheetControl1.Selection = spreadsheetControl1.ActiveWorksheet("A1")
        End Sub

        Private Sub SpreadsheetControl1_DocumentLoaded(ByVal sender As Object, ByVal e As EventArgs)
            ApplyOptions()
            spreadsheetControl1.Document.PivotCaches.RefreshAll()
            spreadsheetControl1.Selection = spreadsheetControl1.ActiveWorksheet("C3")
        End Sub
    End Class
End Namespace
