Imports DevExpress.Data
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Editors.Settings
Imports DevExpress.Xpf.Grid
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.Globalization
Imports System.Linq

Namespace GridDemo

    Public Partial Class MultiCellSelection
        Inherits GridDemo.GridDemoModule

        Public Sub New()
            Me.InitializeComponent()
            Me.AssignDataSource()
            Me.SelectCells()
        End Sub

        Private Shared ReadOnly dxLogo As String() = {" dd ", " dd ", " ddd", " d d", " d d", " d d", " d d", " d d", " ddd", " dd ", " dd ", "    ", " x x", " x x", " xxx", " xxx", "  x ", "  x ", "  x ", " xxx", " xxx", " x x", " x x"}

        Private Sub SelectCells()
            Me.grid.BeginSelection()
            Dim points = GridDemo.MultiCellSelection.dxLogo.SelectMany(Function(s, y) s.[Select](Function(c, x) If(c <> " "c, New System.Drawing.Point(x, y), CType(Nothing, System.Drawing.Point?))))
            For Each point In points.Where(Function(x) x IsNot Nothing)
                Me.view.SelectCell(point.Value.Y, Me.view.VisibleColumns(point.Value.X + 1))
            Next

            Me.grid.EndSelection()
        End Sub

        Private Sub AssignDataSource()
            Me.grid.ItemsSource = GridDemo.SalesByYearData.Data
            Me.grid.Columns(CStr(("Date"))).Fixed = DevExpress.Xpf.Grid.FixedStyle.Left
            Me.grid.Columns(CStr(("Date"))).SortOrder = DevExpress.Data.ColumnSortOrder.Descending
            Me.grid.Columns(CStr(("Date"))).ShowInColumnChooser = False
            Dim actualVisibleColumns As System.Collections.Generic.List(Of DevExpress.Xpf.Grid.GridColumn) = New System.Collections.Generic.List(Of DevExpress.Xpf.Grid.GridColumn)()
            For Each col In Me.view.VisibleColumns
                If Not Equals(col.FieldName, "Date") Then actualVisibleColumns.Add(col)
            Next

            For i As Integer = 0 To actualVisibleColumns.Count - 1
                Dim column As DevExpress.Xpf.Grid.GridColumn = actualVisibleColumns(i)
                Me.grid.TotalSummary.Add(New DevExpress.Xpf.Grid.GridSummaryItem() With {.FieldName = column.FieldName, .SummaryType = DevExpress.Data.SummaryItemType.Sum, .DisplayFormat = "${0:N}"})
                column.EditSettings = New DevExpress.Xpf.Editors.Settings.SpinEditSettings() With {.MaskType = DevExpress.Xpf.Editors.MaskType.Numeric, .MaskUseAsDisplayFormat = True, .Mask = "c", .MaskCulture = New System.Globalization.CultureInfo("en-US")}
            Next

            Me.view.FocusedRowHandle = 0
            Me.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, New System.Action(AddressOf Me.BestFitCore))
        End Sub

        Private Sub BestFitCore()
            Me.view.BestFitColumn(Me.grid.Columns("Date"))
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.SelectCells(True)
        End Sub

        Private Sub Button_Click_1(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.SelectCells(False)
        End Sub

        Private Sub SelectCells(ByVal shouldSelectTopValues As Boolean)
            Dim list As System.Collections.Generic.List(Of System.Collections.Generic.KeyValuePair(Of DevExpress.Xpf.Grid.GridCell, Integer)) = New System.Collections.Generic.List(Of System.Collections.Generic.KeyValuePair(Of DevExpress.Xpf.Grid.GridCell, Integer))()
            Dim actualVisibleColumns As System.Collections.Generic.List(Of DevExpress.Xpf.Grid.GridColumn) = New System.Collections.Generic.List(Of DevExpress.Xpf.Grid.GridColumn)()
            For Each col In Me.view.VisibleColumns
                If Not Equals(col.FieldName, "Date") Then actualVisibleColumns.Add(col)
            Next

            For i As Integer = 0 To Me.grid.VisibleRowCount - 1
                For j As Integer = 0 To actualVisibleColumns.Count - 1
                    list.Add(New System.Collections.Generic.KeyValuePair(Of DevExpress.Xpf.Grid.GridCell, Integer)(New DevExpress.Xpf.Grid.GridCell(i, actualVisibleColumns(j)), CInt(Me.grid.GetCellValue(i, actualVisibleColumns(j)))))
                Next
            Next

            list.Sort(Function(ByVal x As System.Collections.Generic.KeyValuePair(Of DevExpress.Xpf.Grid.GridCell, Integer), ByVal y As System.Collections.Generic.KeyValuePair(Of DevExpress.Xpf.Grid.GridCell, Integer)) GridDemo.MultiCellSelection.Compare(x, y, shouldSelectTopValues))
            Me.grid.BeginSelection()
            Me.view.DataControl.UnselectAll()
            For i As Integer = 0 To System.Math.Min(20, list.Count) - 1
                Me.view.SelectCell(list(CInt((list.Count - i - 1))).Key.RowHandle, list(CInt((list.Count - i - 1))).Key.Column)
            Next

            Me.grid.EndSelection()
        End Sub

        Private Shared Function Compare(ByVal x As System.Collections.Generic.KeyValuePair(Of DevExpress.Xpf.Grid.GridCell, Integer), ByVal y As System.Collections.Generic.KeyValuePair(Of DevExpress.Xpf.Grid.GridCell, Integer), ByVal shouldSelectTopValues As Boolean) As Integer
            If shouldSelectTopValues Then
                Return System.Collections.Generic.Comparer(Of Integer).[Default].Compare(x.Value, y.Value)
            Else
                Return System.Collections.Generic.Comparer(Of Integer).[Default].Compare(y.Value, x.Value)
            End If
        End Function
    End Class
End Namespace
