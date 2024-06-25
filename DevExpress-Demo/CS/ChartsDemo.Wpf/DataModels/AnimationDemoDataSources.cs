using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;

namespace ChartsDemo {
    public class AnimationDataPointSources {
        class GoogleStockData {
            public DataTable ShortData {
                get { return GetData().AsEnumerable().Reverse().Take(30).Reverse().CopyToDataTable(); }
            }
            DataTable LoadDataTableFromXml(string fileName, string tableName) {
                Uri uri = new Uri(@"pack://application:,,,/ChartsDemo;component/Data/" + fileName);
                Stream xmlStream = Application.GetResourceStream(uri).Stream;
                DataSet xmlDataSet = new DataSet();
                xmlDataSet.ReadXml(xmlStream);
                xmlStream.Close();
                return xmlDataSet.Tables[tableName];
            }
            DataTable GetData() {
                return LoadDataTableFromXml("GoogleStockData.xml", "StockPrice");
            }
        }

        List<List<DataPoint>> dataSource;
        List<List<DataPoint>> dataSource2;
        List<List<DataPoint>> pieDataSource;
        List<List<DataPoint>> nestedDonutDataSource;
        List<List<DataPoint>> barDataSource;
        List<List<DataPoint>> barDataSource2;
        List<List<DataPoint>> scatterDataSource;
        List<List<DataPoint>> funnelDataSource;
        List<List<PolarDataPoint>> polarDataSource;
        List<List<PolarDataPoint>> polarRangeDataSource;
        List<List<DataPoint>> bubbleDataSource;
        List<List<DataPoint>> rangeDataSource;
        List<List<DataPoint>> rangeDataSource2;
        List<DataTable> financialDataSource;
        List<List<BoxPlotDataPoint>> boxPlotDataSource;

        public List<List<DataPoint>> DataSource { get { return this.dataSource ?? (this.dataSource = CreateDataSource()); } }
        public List<List<DataPoint>> DataSource2 { get { return this.dataSource2 ?? (this.dataSource2 = DataSource.Take(1).ToList()); } }
        public List<List<DataPoint>> PieDataSource { get { return this.pieDataSource ?? (this.pieDataSource = CreatePieDataSource().ToList()); } }
        public List<List<DataPoint>> NestedDonutDataSource { get { return this.nestedDonutDataSource ?? (this.nestedDonutDataSource = CreatePieDataSource().Concat(CreatePieDataSource()).ToList()); } }
        public List<List<DataPoint>> BarDataSource { get { return this.barDataSource ?? (this.barDataSource = CreateBarDataSource()); } }
        public List<List<DataPoint>> BarDataSource2 { get { return this.barDataSource2 ?? (this.barDataSource2 = BarDataSource.Take(3).ToList()); } }
        public List<List<DataPoint>> ScatterDataSource { get { return this.scatterDataSource ?? (this.scatterDataSource = CreateScatterDataSource().ToList()); } }
        public List<List<DataPoint>> FunnelDataSource { get { return this.funnelDataSource ?? (this.funnelDataSource = CreateFunnelDataSource().ToList()); } }
        public List<List<PolarDataPoint>> PolarDataSource { get { return this.polarDataSource ?? (this.polarDataSource = CreatePolarDataSource().ToList()); } }
        public List<List<PolarDataPoint>> PolarRangeDataSource { get { return this.polarRangeDataSource ?? (this.polarRangeDataSource = CreatePolarRangeDataSource().ToList()); } }
        public List<List<DataPoint>> BubbleDataSource { get { return this.bubbleDataSource ?? (this.bubbleDataSource = CreateBubbleDataSource().ToList()); } }
        public List<List<DataPoint>> RangeDataSource { get { return this.rangeDataSource ?? (this.rangeDataSource = CreateRangeDataSource().ToList()); } }
        public List<List<DataPoint>> RangeDataSource2 { get { return this.rangeDataSource2 ?? (this.rangeDataSource2 = RangeDataSource.Take(1).ToList()); } }
        public List<DataTable> FinancialDataSource { get { return this.financialDataSource ?? (this.financialDataSource = CreateFinancialDataSource().ToList()); } }
        public List<List<BoxPlotDataPoint>> BoxPlotDataSource { get { return this.boxPlotDataSource ?? (this.boxPlotDataSource = CreateBoxPlotDataSource()); } }


        List<List<DataPoint>> CreateGroups(string[] arguments, List<double[]> groups) {
            return groups
                .Select(x => x.Zip(arguments, (value, argument) => new DataPoint(argument: argument, value: value)).ToList())
                .ToList();
        }
        List<List<DataPoint>> CreateDataSource() {
            var args = new[] { "A", "B", "C", "D", "E", "F" };
            var group0 = new double[] { 15, 13, 7, 5, 23, 21 };
            var group1 = new double[] { 8, 12, 4, 9, 15, 19 };
            var group2 = new double[] { 3, 10, 6, 6, 8, 10 };
            return CreateGroups(args, new List<double[]> { group0, group1, group2 });
        }
        IEnumerable<List<DataPoint>> CreatePieDataSource() {
            var dataSource = new List<DataPoint>();
            var random = new Random(0);
            for (int i = 0; i < 16; i++)
                dataSource.Add(new DataPoint("1", random.NextDouble() * 3 + 1));
            yield return dataSource;
        }
        List<List<DataPoint>> CreateBarDataSource() {
            var dataSource = new List<DataPoint>();
            var args = new[] { "A", "B", "C", "D", "E", "F" };
            var group0 = new double[] { 1, 2, 5, -2, -2.1, -2.4 };
            var group1 = new double[] { 3, 10, 6, -3, -3.2, -3.8 };
            var group2 = new double[] { 8, 12, 7, -1.5, -1, -0.7 };
            var group3 = new double[] { 15, 13, 4, -1.3, -0.6, -4 };
            return CreateGroups(args, new List<double[]> { group0, group1, group2, group3 });
        }
        IEnumerable<List<DataPoint>> CreateScatterDataSource() {
            yield return new List<DataPoint> {
                new DataPoint("A", 15),
                new DataPoint("B", 11),
                new DataPoint("C", 7),
                new DataPoint("D", 9),
                new DataPoint("C", 23),
                new DataPoint("B", 21)
            };
        }
        IEnumerable<List<DataPoint>> CreateFunnelDataSource() {
            yield return new List<DataPoint> {
                new DataPoint("Visited a Website", 9152),
                new DataPoint("Downloaded a Trial", 6870),
                new DataPoint("Contacted to Support", 5121),
                new DataPoint("Subscribed", 2224),
                new DataPoint("Renewed", 1670)
            };
        }
        IEnumerable<List<PolarDataPoint>> CreatePolarDataSource() {
            var random = new Random(0);
            yield return Enumerable.Range(0, 7)
                .Select(x => new PolarDataPoint(Math.Floor(random.NextDouble() * 360), Math.Floor(random.NextDouble() * 25)))
                .ToList();
        }
        IEnumerable<List<PolarDataPoint>> CreatePolarRangeDataSource() {
            var random = new Random(0);
            var pointsCount = 6;
            yield return Enumerable.Range(0, 7)
                .Select(x => new PolarDataPoint(
                    Math.Floor(x / (double)pointsCount * 360),
                    Math.Floor(random.NextDouble() * 10 + 30),
                    Math.Floor(random.NextDouble() * 10 + 10)))
                .ToList();
        }
        IEnumerable<List<DataPoint>> CreateBubbleDataSource() {
            yield return new List<DataPoint> {
                new DataPoint("A", value: 10, weight: 5.9),
                new DataPoint("B", value: 5, weight: 4.9),
                new DataPoint("C", value: 2, weight: 4.6),
                new DataPoint("D", value: 5, weight: 3),
                new DataPoint("E", value: 2, weight: 2.9),
                new DataPoint("F", value: 4, weight: 2.8),
                new DataPoint("G", value: 2, weight: 2.6),
                new DataPoint("H", value: 3, weight: 2.5),
                new DataPoint("I", value: 4, weight: 2.4),
            };
        }
        List<List<BoxPlotDataPoint>> CreateBoxPlotDataSource() {
            List<BoxPlotDataPoint> result = new List<BoxPlotDataPoint>();
            double value = 20;
            Random rnd = new Random(1);
            char argChar = 'A';
            for (int i = 0; i < 10; i++) {
                argChar = (char)((int)argChar + 1);
                string arg = argChar.ToString();
                result.Add(GenerateBoxPlotDataPoint(arg, ref value, rnd));
            }
            return new List<List<BoxPlotDataPoint>>() { result };
        }
        BoxPlotDataPoint GenerateBoxPlotDataPoint(string arg, ref double value, Random rnd) {
            BoxPlotDataPoint dataPoint = new BoxPlotDataPoint();
            dataPoint.Argument = arg;
            value += rnd.Next(-5, 6);
            dataPoint.Min = value;
            dataPoint.Quartile1 = value + rnd.Next(1, 5);
            dataPoint.Median = dataPoint.Quartile1 + rnd.Next(4, 8);
            dataPoint.Mean = dataPoint.Median + rnd.Next(-3, 3);
            dataPoint.Quartile3 = dataPoint.Mean + rnd.Next(4, 9);
            dataPoint.Max = dataPoint.Quartile3 + rnd.Next(1, 5);
            dataPoint.Outliers = new double[3];
            dataPoint.Outliers[0] = dataPoint.Min - rnd.Next(1, 3);
            dataPoint.Outliers[1] = dataPoint.Max + rnd.Next(1, 3);
            dataPoint.Outliers[2] = dataPoint.Max + rnd.Next(4, 10);
            return dataPoint;
        }
        IEnumerable<List<DataPoint>> CreateRangeDataSource() {
            yield return new List<DataPoint> {
                new DataPoint("A", 3, 13),
                new DataPoint("B", 5, 8),
                new DataPoint("C", 2, 9),
                new DataPoint("D", -2, -8),
                new DataPoint("E", -1, -6),
                new DataPoint("F", -3, -7),
            };
            yield return new List<DataPoint> {
                new DataPoint("A", 5, 15),
                new DataPoint("B", 3, 11),
                new DataPoint("C", 6, 11),
                new DataPoint("D", -1, -9),
                new DataPoint("E", -3, -9),
                new DataPoint("F", -2, -6),
            };
        }
        IEnumerable<DataTable> CreateFinancialDataSource() {
            yield return new GoogleStockData().ShortData;
        }
    }


    public class DataPoint {
        public string Argument { get; set; }
        public double Value { get; set; }
        public double Value2 { get; set; }
        public double Weight { get; set; }

        public DataPoint(string argument, double value, double value2 = 0, double weight = 0) {
            Argument = argument;
            Value = value;
            Value2 = value2;
            Weight = weight;
        }
    }


    public class PolarDataPoint {
        public double Argument { get; set; }
        public double Value { get; set; }
        public double Value2 { get; set; }

        public PolarDataPoint(double argument, double value, double value2 = 0) {
            Argument = argument;
            Value = value;
            Value2 = value2;
        }
    }


    public class BoxPlotDataPoint {
        public string Argument { get; set; }
        public double Min { get; set; }
        public double Quartile1 { get; set; }
        public double Median { get; set; }
        public double Quartile3 { get; set; }
        public double Max { get; set; }
        public double Mean { get; set; }
        public double[] Outliers { get; set; }
    }
}
