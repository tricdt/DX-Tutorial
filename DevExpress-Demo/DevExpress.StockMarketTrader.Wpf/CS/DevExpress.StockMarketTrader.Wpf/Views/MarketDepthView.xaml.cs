using DevExpress.StockMarketTrader.Wpf.ViewModel;
using DevExpress.Xpf.Charts;
using System.Windows.Controls;

namespace DevExpress.StockMarketTrader.Wpf.Views {
    public partial class MarketDepthView : UserControl {

        public ChartControl Chart { get { return null; } }

        public MarketDepthView() {
            InitializeComponent();
            DataContext = new MarketDepthViewModel();
        }
    }
}
