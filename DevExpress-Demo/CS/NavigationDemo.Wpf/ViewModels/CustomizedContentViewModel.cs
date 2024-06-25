using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Threading;
using System.Xml.Serialization;
using DevExpress.Internal;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.DemoBase.Helpers;

namespace NavigationDemo {
    [POCOViewModel]
    public class CustomizedContentViewModel {
        const int HistoryLength = 20;
        const int UpdateInterval = 1000;
        Random random = new Random();

        public IEnumerable<StockItemInfo> Data { get; set; }

        public CustomizedContentViewModel() {
            var data = StockItems.GetData().Select(x => StockItemInfo.Create(x.CompanyName, x.Price, x.Volume, x.LowPrice, x.HighPrice)).ToList();
            foreach(var stockItem in data)
                for(int i = 0; i < HistoryLength; i++)
                    UpdatePrice(stockItem);
            Data = data;
        }

        DispatcherTimer timer;
        public void OnLoaded() {
            timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(UpdateInterval) };
            timer.Tick += timer_Tick;
            timer.Start();
        }
        public void OnUnloaded() {
            if(timer != null) {
                timer.Stop();
                timer.Tick -= timer_Tick;
                timer = null;
            }
        }
        void timer_Tick(object sender, EventArgs e) {
            foreach(var stockItem in Data)
                UpdatePrice(stockItem);
        }

        void UpdatePrice(StockItemInfo stockItem) {
            double newDelta = random.NextDouble() * 0.5 - 0.25;
            if(stockItem.Price + newDelta <= 0)
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
            stockItem.Volume += Convert.ToInt32(random.NextDouble() * stockItem.Volume * 0.005 - 0.0025);
            UpdatePriceHistory(stockItem);
        }
        void UpdatePriceHistory(StockItemInfo stockItem) {
            List<double> newPriceHistory = new List<double>(new double[HistoryLength]);
            for(int i = 1; i < HistoryLength; i++)
                newPriceHistory[i - 1] = i < stockItem.PriceHistory.Count ? stockItem.PriceHistory[i] : 0;
            newPriceHistory[HistoryLength - 1] = stockItem.Price;
            stockItem.PriceHistory = newPriceHistory;
        }
    }

    [XmlRoot("StockItems")]
    public class StockItems : List<StockItem> {
        public static List<StockItem> GetData() {
            XmlSerializer s = new XmlSerializer(typeof(StockItems));
            List<StockItem> result = null;
            var path = DataDirectoryHelper.GetFile("StockSource.xml", DataDirectoryHelper.DataFolderName);
            using(FileStream fs = File.OpenRead(path))
                result = (List<StockItem>)s.Deserialize(fs);
            return result;
        }
    }

    public class StockItem {
        public string CompanyName { get; set; }
        public double Price { get; set; }
        public int Volume { get; set; }
        public double LowPrice { get; set; }
        public double HighPrice { get; set; }
    }
    [POCOViewModel]
    public class StockItemInfo {
        public static StockItemInfo Create(string companyName, double price, int volume, double lowPrice, double highPrice) {
            var itemInfo = ViewModelSource.Create(() => new StockItemInfo());
            itemInfo.CompanyName = companyName;
            itemInfo.Price = price;
            itemInfo.Volume = volume;
            itemInfo.LowPrice = lowPrice;
            itemInfo.HighPrice = highPrice;
            return itemInfo;
        }

        public virtual string CompanyName { get; set; }
        public virtual double Price { get; set; }
        public virtual int Volume { get; set; }
        public virtual double LowPrice { get; set; }
        public virtual double HighPrice { get; set; }
        public virtual double DeltaPrice { get; set; }
        public virtual double DeltaPricePercent { get; set; }
        public virtual int DeltaChange { get; set; }

        protected StockItemInfo() {
            PriceHistory = new List<double>();
        }
        public virtual List<double> PriceHistory { get; set; }
    }
}
