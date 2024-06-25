using DevExpress.Mvvm.POCO;
using System.Collections.Generic;
using DevExpress.StockMarketTrader.Wpf.Data;

namespace DevExpress.StockMarketTrader.Wpf.ViewModel {
    public class TradeHistoryViewModel {
        public static TradeHistoryViewModel Create(List<TradeHistoryData> trades) {
            return ViewModelSource.Create(() => new TradeHistoryViewModel(trades));
        }

        readonly List<TradeHistoryData> tradesDataSource;

        public List<TradeHistoryData> TradesDataSource { get { return tradesDataSource; } }

        public TradeHistoryViewModel(List<TradeHistoryData> trades) {
            tradesDataSource = trades;
        }
    }
}
