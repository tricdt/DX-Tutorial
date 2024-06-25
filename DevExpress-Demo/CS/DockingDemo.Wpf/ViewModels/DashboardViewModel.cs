using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DevExpress.Map.Native;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Map;
using DockingDemo.Helpers;

namespace DockingDemo.ViewModels {
    public class DashboardViewModel {
        int stateIndex = 0;
        public DashboardViewModel() {
            var generator = new SalesPerformanceDataGenerator(DataLoader.LoadSales());
            generator.Generate();
            KeyMetrics = generator.KeyMetrics;
            MonthlySales = generator.MonthlySales;
            TotalSales = generator.TotalSales;
            DeltaInfos = DeltaInfo.GetAllInfo();
            states = MonthlySales.Select(x => x.State).Distinct().ToList();
            State = states.FirstOrDefault();
        }
        public IEnumerable<KeyMetricsItem> KeyMetrics { get; set; }
        public IEnumerable<MonthlySalesItem> MonthlySales { get; set; }
        public IEnumerable<TotalSalesItem> TotalSales { get; set; }
        public virtual IEnumerable<MonthlySalesItem> CurrentMonthlySales { get; set; }
        public virtual IEnumerable<TotalSalesItem> CurrentTotalSales { get; set; }
        public List<DeltaInfo> DeltaInfos { get; set; }
        public virtual IEnumerable<ProductData> TopSales { get; set; }
        public virtual IEnumerable<CategoryData> CategoriesData { get; set; }
        public string MapTitle { get { return "Sales by State - " + State; } }

        List<string> states;
        string dataState;
        string state;
        public string State {
            get { return state; }
            set {
                state = value;
                dataState = GetStateByIndex();
                OnStateChanged();
                this.RaisePropertyChanged(x => MapTitle);
            }
        }
        MapPath selectedState;
        public MapPath SelectedState {
            get { return selectedState; }
            set {
                selectedState = value;
                State = (value as IMapItemCore).Text;
            }
        }
        protected void OnCurrentMonthlySalesChanged(IEnumerable<MonthlySalesItem> oldValue) {
            TopSales = CurrentMonthlySales.GroupBy(x => x.Product).
                Select(x => new ProductData { Product = x.Key, Total = x.Sum(y => y.Revenue),
                    Sales = x.Select(y => new SaleData { Price = y.Revenue, Date = y.CurrentDate })
                }).
                OrderBy(x => x.Total, ListSortDirection.Descending).Take(5).ToList();
            CategoriesData = CurrentMonthlySales.GroupBy(x => x.Category).
                Select(x => new CategoryData { Category = x.Key, Revenue = x.Sum(y => y.UnitsSoldTarget) });
        }
        void OnStateChanged() {
            CurrentMonthlySales = MonthlySales.Where(x => x.State == dataState);
            CurrentTotalSales = TotalSales.Where(x => x.State == dataState);
        }
        string GetStateByIndex() {
            if(states.Contains(state))
                return state;
            if(stateIndex >= states.Count)
                stateIndex = 0;
            return states[stateIndex++];
        }
    }
    public class DeltaInfo {
        public DeltaInfo(string delta, string summary, string name) {
            Delta = "+$" + delta;
            Summary = "$" + summary;
            Name = name;
        }
        public string Delta { get; set; }
        public string Summary { get; set; }
        public string Name { get; set; }
        public static List<DeltaInfo> GetAllInfo() {
            var result = new List<DeltaInfo>();
            result.Add(new DeltaInfo("6.24M", "159M", "Revenue YTD"));
            result.Add(new DeltaInfo("15.3K", "30.6M", "Expenses YTD"));
            result.Add(new DeltaInfo("6.23K", "129M", "Profit YTD"));
            result.Add(new DeltaInfo("114K", "955K", "Avg Order Size"));
            result.Add(new DeltaInfo("15.8K", "207K", "New Customers"));
            result.Add(new DeltaInfo("523K", "6.31M", "Market Share"));
            return result;
        }
    }
    public class ProductData {
        public string Product { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<SaleData> Sales {get; set;}
    }
    public class SaleData {
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
    public class CategoryData {
        public string Category { get; set; }
        public decimal Revenue { get; set; }
    }
}
