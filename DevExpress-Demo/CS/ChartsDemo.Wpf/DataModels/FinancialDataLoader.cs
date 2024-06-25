using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace ChartsDemo {
    public class FinancialDataLoader {
        readonly ImageSource positiveDynamic = new BitmapImage(new Uri("/ChartsDemo;component/Images/ArrowUp.png", UriKind.Relative));
        readonly ImageSource negativeDynamic = new BitmapImage(new Uri("/ChartsDemo;component/Images/ArrowDown.png", UriKind.Relative));
        readonly ImageSource zeroDynamic = new BitmapImage(new Uri("/ChartsDemo;component/Images/ZeroDynamic.png", UriKind.Relative));

        StockDynamic GetStockDynamic(decimal previousPointValue, decimal currentPointValue) {
            if (previousPointValue < currentPointValue)
                return new StockDynamic(new SolidColorBrush(Color.FromArgb(255, 63, 171, 0)), this.positiveDynamic);
            else if (previousPointValue > currentPointValue)
                return new StockDynamic(new SolidColorBrush(Color.FromArgb(255, 213, 50, 35)), this.negativeDynamic);
            else
                return new StockDynamic(new SolidColorBrush(Color.FromArgb(255, 161, 161, 161)), this.zeroDynamic);
        }
        StockDataPoint ReadDataPointFromXML(XElement element) {
            StockDataPoint point = new StockDataPoint() {
                TradeDate = DateTime.ParseExact(element.Element("Date").Value, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Open = Convert.ToDecimal(element.Element("Open").Value, CultureInfo.InvariantCulture),
                Close = Convert.ToDecimal(element.Element("Close").Value, CultureInfo.InvariantCulture),
                Low = Convert.ToDecimal(element.Element("Low").Value, CultureInfo.InvariantCulture),
                High = Convert.ToDecimal(element.Element("High").Value, CultureInfo.InvariantCulture),
                ToolTipData = new ToolTipStockData()
            };
            point.ToolTipData.Owner = point;
            return point;
        }

        public List<StockDataPoint> GetGoogleStockData() {
            XDocument document = DataLoader.LoadXmlFromResources("/Data/GoogleStockData.xml");
            List<StockDataPoint> result = new List<StockDataPoint>();
            if (document != null) {
                IEnumerable<XElement> elements = document.Element("StockPrices").Elements("StockPrice").Reverse();
                StockDataPoint previousPoint = ReadDataPointFromXML(elements.ElementAt(0));
                foreach (XElement element in elements) {
                    StockDataPoint point = ReadDataPointFromXML(element);
                    point.ToolTipData.OpenDynamic = GetStockDynamic(previousPoint.Open, point.Open).ImageSource;
                    point.ToolTipData.CloseDynamic = GetStockDynamic(previousPoint.Close, point.Close).ImageSource;
                    point.ToolTipData.HighDynamic = GetStockDynamic(previousPoint.High, point.High).ImageSource;
                    point.ToolTipData.LowDynamic = GetStockDynamic(previousPoint.Low, point.Low).ImageSource;
                    point.ToolTipData.OpenFontBrush = GetStockDynamic(previousPoint.Open, point.Open).Brush;
                    point.ToolTipData.CloseFontBrush = GetStockDynamic(previousPoint.Close, point.Close).Brush;
                    point.ToolTipData.HighFontBrush = GetStockDynamic(previousPoint.High, point.High).Brush;
                    point.ToolTipData.LowFontBrush = GetStockDynamic(previousPoint.Low, point.Low).Brush;
                    result.Add(point);
                    previousPoint = point;
                }
            }
            return result;
        }
    }


    public class StockDataPoint {
        public ToolTipStockData ToolTipData { get; set; }
        public DateTime TradeDate { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
    }

    public class StockDynamic {
        public Brush Brush { get; private set; }
        public ImageSource ImageSource { get; private set; }

        public StockDynamic(Brush brush, ImageSource imageSource) {
            Brush = brush;
            ImageSource = imageSource;
        }
    }

    public class ToolTipStockData {
        public StockDataPoint Owner { get; set; }
        public ImageSource HighDynamic { get; set; }
        public ImageSource LowDynamic { get; set; }
        public ImageSource OpenDynamic { get; set; }
        public ImageSource CloseDynamic { get; set; }
        public Brush HighFontBrush { get; set; }
        public Brush LowFontBrush { get; set; }
        public Brush OpenFontBrush { get; set; }
        public Brush CloseFontBrush { get; set; }
    }
}
