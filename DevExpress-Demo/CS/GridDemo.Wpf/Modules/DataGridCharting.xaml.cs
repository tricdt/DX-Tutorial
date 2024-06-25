using System;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace CommonChartsDemo {
    [CodeFile("ModuleResources/DataGridCharting/SalesProductData.(cs)")]
    public partial class DataGridCharting : CommonChartsDemoModule {
        public DataGridCharting() {
            InitializeComponent();
        }
    }
    public enum AggregateType {
        Average = 1,
        Min = 2,
        Max = 3,
        Sum = 4
    }
}
