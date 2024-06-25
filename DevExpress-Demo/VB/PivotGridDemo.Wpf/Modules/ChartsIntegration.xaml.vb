Imports System
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.PivotGrid

Namespace PivotGridDemo

    Public Partial Class ChartsIntegration
        Inherits PivotGridDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub PivotGridDemoModule_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            pivotGrid.SetSelectionByFieldValues(False, New Object() {"Beverages"})
            pivotGrid.SetSelectionByFieldValues(False, New Object() {"Condiments"})
            pivotGrid.SetSelectionByFieldValues(False, New Object() {"Confections"})
        End Sub

        Private Sub chartControl_BoundDataChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim getShowWarning As Func(Of PivotCellBaseEventArgs, Boolean) = Function(x) x.ColumnValueType = FieldValueType.GrandTotal AndAlso Not pivotGrid.ChartProvideColumnGrandTotals OrElse x.RowValueType = FieldValueType.GrandTotal AndAlso Not pivotGrid.ChartProvideRowGrandTotals
            Dim selectedCells = If(pivotGrid.MultiSelection.SelectedCells.Count = 0, {pivotGrid.GetCellInfo(pivotGrid.FocusedCell.X, pivotGrid.FocusedCell.Y)}, pivotGrid.MultiSelection.SelectedCells.Select(Function(x) pivotGrid.GetCellInfo(x.X, x.Y)))
            warningChart.MaxWidth = gbPivotOptions.ActualWidth
            warningChart.Visibility = If(selectedCells.All(getShowWarning), Visibility.Visible, Visibility.Collapsed)
        End Sub
    End Class
End Namespace
