Imports DevExpress.Xpf.Grid
Imports DevExpress.XtraGrid
Imports System
Imports System.Collections.Generic

Namespace GridDemo

    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/GroupIntervalData.(cs)")>
    Public Partial Class IntervalGrouping
        Inherits GridDemoModule

        Public Sub New()
            InitializeComponent()
            SetInterval("Country", ColumnGroupInterval.Alphabetical)
        End Sub

        Public Sub SetInterval(ByVal fieldName As String, ByVal interval As ColumnGroupInterval)
            Reset()
            grid.Columns(fieldName).GroupInterval = interval
            grid.GroupBy(fieldName)
        End Sub

        Public Sub SetSortMode(ByVal fieldName As String)
            Reset()
            grid.Columns(fieldName).SortMode = ColumnSortMode.Custom
            grid.GroupBy(fieldName)
        End Sub

        Private Sub Reset()
            grid.SortInfo.Clear()
            grid.GroupCount = 0
            For Each column As GridColumn In grid.Columns
                column.GroupInterval = ColumnGroupInterval.Default
                column.SortMode = ColumnSortMode.Default
            Next
        End Sub

        Private Sub grid_CustomColumnGroup(ByVal sender As Object, ByVal e As CustomColumnSortEventArgs)
            Dim x As Double = Math.Floor(Convert.ToDouble(e.Value1) / 10)
            Dim y As Double = Math.Floor(Convert.ToDouble(e.Value2) / 10)
            Dim res As Integer = Comparer(Of Double).Default.Compare(x, y)
            If x > 19 AndAlso y > 19 Then res = 0
            e.Result = res
            e.Handled = True
        End Sub

        Private Sub view_CustomGroupDisplayText(ByVal sender As Object, ByVal e As CustomGroupDisplayTextEventArgs)
            If e.Column.SortMode = ColumnSortMode.Custom Then
                Dim d As Double = Math.Floor(Convert.ToDouble(e.Value) / 10)
                Dim ret As String = String.Format("{0:$0.00} - {1:$0.00} ", d * 10, (d + 1) * 10)
                If d > 19 Then ret = String.Format(">= {0:$0.00} ", d * 10)
                e.DisplayText = ret
            End If
        End Sub
    End Class
End Namespace
