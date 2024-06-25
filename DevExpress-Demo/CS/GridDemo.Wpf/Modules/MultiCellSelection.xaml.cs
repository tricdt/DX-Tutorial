using DevExpress.Data;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace GridDemo {
    public partial class MultiCellSelection : GridDemoModule {
        public MultiCellSelection() {
            InitializeComponent();
            AssignDataSource();

            SelectCells();
        }
        static readonly string[] dxLogo = new[] {
" dd ",
" dd ",
" ddd",
" d d",
" d d",
" d d",
" d d",
" d d",
" ddd",
" dd ",
" dd ",
"    ",
" x x",
" x x",
" xxx",
" xxx",
"  x ",
"  x ",
"  x ",
" xxx",
" xxx",
" x x",
" x x",
};

  

        void SelectCells() {
            grid.BeginSelection();
            var points = dxLogo.SelectMany((s, y) => {
                return s.Select((c, x) => c != ' ' ? new Point(x, y) : (Point?)null);
            });
            foreach(var point in points.Where(x => x != null)) {
                view.SelectCell(point.Value.Y, view.VisibleColumns[point.Value.X + 1]);
            }
            grid.EndSelection();
        }

        void AssignDataSource() {
            grid.ItemsSource = SalesByYearData.Data;
            grid.Columns["Date"].Fixed = FixedStyle.Left;
            grid.Columns["Date"].SortOrder = ColumnSortOrder.Descending;
            grid.Columns["Date"].ShowInColumnChooser = false;
            List<GridColumn> actualVisibleColumns = new List<GridColumn>();
            foreach(var col in view.VisibleColumns) 
                if(col.FieldName != "Date")
                    actualVisibleColumns.Add(col);            
            for (int i=0; i< actualVisibleColumns.Count; i++) {
                GridColumn column = actualVisibleColumns[i];
                grid.TotalSummary.Add(new GridSummaryItem() { FieldName = column.FieldName, SummaryType = SummaryItemType.Sum, DisplayFormat = "${0:N}" });
                column.EditSettings = new SpinEditSettings() { MaskType = MaskType.Numeric, MaskUseAsDisplayFormat = true, Mask = "c", MaskCulture = new CultureInfo("en-US") };
            }
            view.FocusedRowHandle = 0;
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, new Action(BestFitCore));
        }

        void BestFitCore() {
            view.BestFitColumn(grid.Columns["Date"]);
        }
      

        void Button_Click(object sender, System.Windows.RoutedEventArgs e) {
            SelectCells(true);
        }
        void Button_Click_1(object sender, System.Windows.RoutedEventArgs e) {
            SelectCells(false);
        }

        void SelectCells(bool shouldSelectTopValues) {
            List<KeyValuePair<GridCell, int>> list = new List<KeyValuePair<GridCell, int>>();
            List<GridColumn> actualVisibleColumns = new List<GridColumn>();
            foreach(var col in view.VisibleColumns)
                if(col.FieldName != "Date")
                    actualVisibleColumns.Add(col);
            for(int i = 0; i < grid.VisibleRowCount; i++)
                for(int j = 0; j < actualVisibleColumns.Count; j++)
                    list.Add(new KeyValuePair<GridCell, int>(new GridCell(i, actualVisibleColumns[j]), (int)grid.GetCellValue(i, actualVisibleColumns[j])));
            list.Sort(delegate(KeyValuePair<GridCell, int> x, KeyValuePair<GridCell, int> y) {
                return Compare(x, y, shouldSelectTopValues);
            });

            grid.BeginSelection();
            view.DataControl.UnselectAll();
            for(int i = 0; i < Math.Min(20, list.Count); i++) {
                view.SelectCell(list[list.Count - i - 1].Key.RowHandle, list[list.Count - i - 1].Key.Column);
            }
            grid.EndSelection();
        }

        private static int Compare(KeyValuePair<GridCell, int> x, KeyValuePair<GridCell, int> y, bool shouldSelectTopValues) {
            if(shouldSelectTopValues)
                return Comparer<int>.Default.Compare(x.Value, y.Value);
            else
                return Comparer<int>.Default.Compare(y.Value, x.Value);
        }
    }
}
