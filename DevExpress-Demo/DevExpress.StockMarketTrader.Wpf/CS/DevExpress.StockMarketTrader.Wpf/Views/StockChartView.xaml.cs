using DevExpress.Xpf.Charts;
using System.Windows.Controls;

namespace DevExpress.StockMarketTrader.Wpf.Views {
    public partial class StockChartView : UserControl {        
        
        public ChartControl Chart { get { return chartControl; } }

        public StockChartView() {
            InitializeComponent();
        }
    }
}
