using DevExpress.Xpf.Grid;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;

namespace GridDemo {
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/GroupIntervalData.(cs)")]
    public partial class IntervalGrouping : GridDemoModule {
        public IntervalGrouping() {
            InitializeComponent();
            SetInterval("Country", ColumnGroupInterval.Alphabetical);
        }
        public void SetInterval(string fieldName, ColumnGroupInterval interval) {
            Reset();
            grid.Columns[fieldName].GroupInterval = interval;
            grid.GroupBy(fieldName);
        }
        public void SetSortMode(string fieldName) {
            Reset();
            grid.Columns[fieldName].SortMode = ColumnSortMode.Custom;
            grid.GroupBy(fieldName);
        }
        void Reset() {
            grid.SortInfo.Clear();
            grid.GroupCount = 0;
            foreach(GridColumn column in grid.Columns) {
                column.GroupInterval = ColumnGroupInterval.Default;
                column.SortMode = ColumnSortMode.Default;
            }
        }
        void grid_CustomColumnGroup(object sender, CustomColumnSortEventArgs e) {
            double x = Math.Floor(Convert.ToDouble(e.Value1) / 10);
            double y = Math.Floor(Convert.ToDouble(e.Value2) / 10);
            int res = Comparer<double>.Default.Compare(x, y);
            if(x > 19 && y > 19) res = 0;
            e.Result = res;
            e.Handled = true;
        }
        void view_CustomGroupDisplayText(object sender, CustomGroupDisplayTextEventArgs e) {
            if(e.Column.SortMode == ColumnSortMode.Custom) {
                double d = Math.Floor(Convert.ToDouble(e.Value) / 10);
                string ret = string.Format("{0:$0.00} - {1:$0.00} ", d * 10, (d + 1) * 10);
                if(d > 19) ret = string.Format(">= {0:$0.00} ", d * 10);
                e.DisplayText = ret;
            }
        }
    }
}
