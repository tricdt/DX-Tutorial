using DevExpress.Mvvm.POCO;
using System.Collections.Generic;
using DevExpress.StockMarketTrader.Data;
using DevExpress.StockMarketTrader.Wpf.Data;

namespace DevExpress.StockMarketTrader.Wpf.ViewModel {
    public class OpenOrdersViewModel {
        public static OpenOrdersViewModel Create(List<OpenOrderData> orders) {
            return ViewModelSource.Create(() => new OpenOrdersViewModel(orders));
        }

        readonly List<OpenOrderData> ordersDataSource;

        public List<OpenOrderData> OrdersDataSource { get { return ordersDataSource; } }

        public OpenOrdersViewModel(List<OpenOrderData> orders) {
            ordersDataSource = orders;
        }
    }
}
