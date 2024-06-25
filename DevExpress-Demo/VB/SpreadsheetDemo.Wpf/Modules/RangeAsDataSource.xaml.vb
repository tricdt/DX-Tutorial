Imports System
Imports System.Windows.Threading

Namespace SpreadsheetDemo

    Public Partial Class RangeAsDataSource
        Inherits SpreadsheetDemoModule

        Private updateLocked As Boolean = False

        Private operation As DispatcherOperation = Nothing

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub spreadsheetControl1_DocumentLoaded(ByVal sender As Object, ByVal e As EventArgs)
            Dim workbook = spreadsheetControl1.Document
            Dim sheet = workbook.Worksheets(0)
            chartControl1.DataSource = sheet("B3:D103").GetDataSource()
            Dim series = chartControl1.Diagram.Series(0)
            series.ArgumentDataMember = "Column 0"
            series.ValueDataMember = "Column 1"
            series = chartControl1.Diagram.Series(1)
            series.ArgumentDataMember = "Column 0"
            series.ValueDataMember = "Column 2"
        End Sub

        Private Sub spreadsheetControl1_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraSpreadsheet.SpreadsheetCellEventArgs)
            updateLocked = True
            Try
                If Equals(e.Cell.GetReferenceA1(), "F3") Then
                    trbMean.Value = e.Cell.Value.NumericValue
                ElseIf Equals(e.Cell.GetReferenceA1(), "F6") Then
                    trbStdDev.Value = e.Cell.Value.NumericValue * 100
                End If
            Finally
                updateLocked = False
            End Try
        End Sub

        Private Sub trbMean_EditValueChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            If Not updateLocked Then UpdateValueAsync(New Action(AddressOf UpdateMean))
        End Sub

        Private Sub trbStdDev_EditValueChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            If Not updateLocked Then UpdateValueAsync(New Action(AddressOf UpdateStdDev))
        End Sub

        Private Sub UpdateValueAsync(ByVal action As Action)
            If operation Is Nothing OrElse operation.Status = DispatcherOperationStatus.Aborted OrElse operation.Status = DispatcherOperationStatus.Completed Then operation = Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, action)
        End Sub

        Private Sub UpdateMean()
            spreadsheetControl1.ActiveWorksheet("F3").Value = trbMean.Value
        End Sub

        Private Sub UpdateStdDev()
            spreadsheetControl1.ActiveWorksheet("F6").Value = trbStdDev.Value / 100.0
        End Sub
    End Class
End Namespace
