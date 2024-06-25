using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.PivotGrid;

namespace PivotGridDemo {
    public partial class ChartsIntegration : PivotGridDemoModule {

        public ChartsIntegration() {
            InitializeComponent();
        }

        void PivotGridDemoModule_Loaded(object sender, RoutedEventArgs e) {
            pivotGrid.SetSelectionByFieldValues(false, new object[] { "Beverages" });
            pivotGrid.SetSelectionByFieldValues(false, new object[] { "Condiments" });
            pivotGrid.SetSelectionByFieldValues(false, new object[] { "Confections" });
        }
        void chartControl_BoundDataChanged(object sender, RoutedEventArgs e) {
            Func<PivotCellBaseEventArgs, bool> getShowWarning = x => (x.ColumnValueType == FieldValueType.GrandTotal && !pivotGrid.ChartProvideColumnGrandTotals)
                || (x.RowValueType == FieldValueType.GrandTotal && !pivotGrid.ChartProvideRowGrandTotals);

            var selectedCells = (pivotGrid.MultiSelection.SelectedCells.Count == 0)
                ? new[] { pivotGrid.GetCellInfo(pivotGrid.FocusedCell.X, pivotGrid.FocusedCell.Y) }
                : pivotGrid.MultiSelection.SelectedCells.Select(x => pivotGrid.GetCellInfo(x.X, x.Y));
            warningChart.MaxWidth = gbPivotOptions.ActualWidth;
            warningChart.Visibility = selectedCells.All(getShowWarning) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
