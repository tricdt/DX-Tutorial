using DevExpress.Mvvm.POCO;
using System.Collections.Generic;
using DevExpress.StockMarketTrader.Data;
using DevExpress.StockMarketTrader.Wpf.Data;

namespace DevExpress.StockMarketTrader.Wpf.ViewModel {
    public class OrderHistoryViewModel {
        public static OrderHistoryViewModel Create(List<OrderHistoryData> orders) {
            return ViewModelSource.Create(() => new OrderHistoryViewModel(orders));
        }

        readonly List<OrderHistoryData> ordersDataSource;

        public List<OrderHistoryData> OrdersDataSource { get { return ordersDataSource; } }

        public OrderHistoryViewModel(List<OrderHistoryData> orders) {
            ordersDataSource = orders;
        }
    }
}
