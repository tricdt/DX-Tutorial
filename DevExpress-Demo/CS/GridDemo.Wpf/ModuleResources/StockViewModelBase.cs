using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Threading;
using System.Xml.Serialization;

namespace GridDemo {
    [POCOViewModel]
    public abstract class StockViewModelBase {
        const int HistoryLength = 20;
        const int UpdateInterval = 1000;

        protected readonly Random Random = new Random();

        DispatcherTimer timer;

        public StockViewModelBase() {
            ObservableCollectionCore<StockItem> data = StockItems.GetData();
            foreach(StockItem stockItem in data) {
                for(int i = 0; i < HistoryLength; i++)
                    UpdatePrice(stockItem, true);
            }
            Data = data;
        }

        public ObservableCollectionCore<StockItem> Data { get; set; }

        public void StartUpdate() {
            timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(UpdateInterval) };
            timer.Tick += timer_Tick;
            timer.Start();
        }
        public void StopUpdate() {
            timer.Stop();
            timer.Tick -= timer_Tick;
            timer = null;
        }

        void timer_Tick(object sender, EventArgs e) {
            Data.BeginUpdate();
            foreach(StockItem stockItem in Data)
                UpdatePrice(stockItem, false);
            Data.EndUpdate();
        }
        void UpdatePrice(StockItem stockItem, bool dataGeneration) {
            double newDelta = GetDeltaPrice(stockItem.Price, dataGeneration);
            if (stockItem.Price + newDelta <= 0)
                newDelta = 0;
            if(Math.Sign(stockItem.DeltaPrice) == Math.Sign(newDelta))
                stockItem.DeltaChange = 0;
            else
                stockItem.DeltaChange = Math.Sign(newDelta);
            stockItem.DeltaPrice = newDelta;
            stockItem.Price += stockItem.DeltaPrice;
            if(stockItem.Price < stockItem.LowPrice)
                stockItem.LowPrice = stockItem.Price;
            if(stockItem.Price > stockItem.HighPrice)
                stockItem.HighPrice = stockItem.Price;
            stockItem.DeltaPricePercent = stockItem.DeltaPrice / stockItem.Price;
            stockItem.Volume += Convert.ToInt32(Random.NextDouble() * stockItem.Volume * 0.005 - 0.0025);
            UpdatePriceHistory(stockItem);
        }
        void UpdatePriceHistory(StockItem stockItem) {
            List<double> newPriceHistory = new List<double>(new double[HistoryLength]);
            for(int i = 1; i < HistoryLength; i++)
                newPriceHistory[i - 1] = i < stockItem.PriceHistory.Count ? stockItem.PriceHistory[i] : 0;
            newPriceHistory[HistoryLength - 1] = stockItem.Price;
            stockItem.PriceHistory = newPriceHistory;
        }

        protected abstract double GetDeltaPrice(double currentPrice, bool dataGeneration);
    }

    [XmlRoot("StockItems")]
    public class StockItems : ObservableCollectionCore<StockItem> {
        public static ObservableCollectionCore<StockItem> GetData() {
            XmlSerializer s = new XmlSerializer(typeof(StockItems));
            Assembly assembly = typeof(StockItems).Assembly;
            return (ObservableCollectionCore<StockItem>)s.Deserialize(assembly.GetManifestResourceStream(DemoHelper.GetPath("GridDemo.Data.", assembly) + "StockSource.xml"));
        }
    }

    public class StockItem {
        public string CompanyName { get; set; }
        public double Price { get; set; }
        public int Volume { get; set; }
        public double LowPrice { get; set; }
        public double HighPrice { get; set; }
        public double DeltaPrice { get; set; }
        public double DeltaPricePercent { get; set; }
        public int DeltaChange { get; set; }

        public StockItem() {
            PriceHistory = new List<double>();
        }
        public List<double> PriceHistory { get; set; }
    }
}
