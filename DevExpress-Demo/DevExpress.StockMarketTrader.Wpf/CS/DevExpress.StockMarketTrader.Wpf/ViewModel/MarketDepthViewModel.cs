using System.Collections.Generic;
using DevExpress.Mvvm.POCO;
using DevExpress.StockMarketTrader.Data;
using DevExpress.StockMarketTrader.Wpf.DataSources;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Core;

namespace DevExpress.StockMarketTrader.Wpf.ViewModel {
    public class MarketDepthViewModel {
        public static MarketDepthViewModel Create() {
            return ViewModelSource.Create(() => new MarketDepthViewModel());
        }

        readonly MarketDepthDataSource dataSource;

        public ObservableCollectionCore<DepthData> BidChartDataSource { get { return dataSource.BidChartDataSource; } }
        public ObservableCollectionCore<DepthData> AskChartDataSource { get { return dataSource.AskChartDataSource; } }

        public MarketDepthViewModel() {
            dataSource = new MarketDepthDataSource();
        }

        public void UpdateData(List<TransactionData> sellOrders, List<TransactionData> buyOrders) {
            dataSource.UpdateData(sellOrders, buyOrders);
        }
        public void AfterLoadLayout(object sender) {
            ((ChartControl)sender).Diagram.Series[0].DataSource = BidChartDataSource;
            ((ChartControl)sender).Diagram.Series[1].DataSource = AskChartDataSource;
        }
    }
}
