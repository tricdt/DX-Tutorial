using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Resources;

namespace ChartsDemo {

    class RealTimeFinancialDataGenerator {
        const double MinPrice = 50;
        const int InitialDataPointsCount = 18000;
        const int MaxPointsCount = InitialDataPointsCount;
        const int PeriodMilliseconds = 41;

        readonly ObservableCollection<FinancialDataPoint> dataSource = new ObservableCollection<FinancialDataPoint>();
        readonly Random random = new Random(3);
        readonly List<FinancialDataPoint> buffer = new List<FinancialDataPoint>();
        readonly object bufferSync = new object();
        FinancialDataPoint prevPoint;
        bool generatingEnabled = false;
        Thread generatingThread;
        int prevMinute = -1;
        int volumeIndex = 0;
        FinancialDataPoint currentAggregatingPoint;

        public ObservableCollection<FinancialDataPoint> DataSource {
            get { return this.dataSource; }
        }
        public DateTime LastArgument {
            get { return this.prevPoint.DateTimeStamp; }
        }

        FinancialDataPoint GeneratePoint(DateTime argument, FinancialDataPoint locPrevPoint, double volume) {
            double priceDelta = (this.random.NextDouble() - 0.5) / 1000;
            double close = locPrevPoint.Close + priceDelta;
            if (close <= MinPrice)
                close = 2 * MinPrice - close;
            double open = locPrevPoint.Close;
            double high = Math.Max(open, close) + (this.random.NextDouble()) / 4000d;
            double low = Math.Min(open, close) - (this.random.NextDouble()) / 4000d;
            return new FinancialDataPoint(argument, open, high, low, close, volume);
        }

        void GeneratingLoop() {
            DateTime timeStamp = DateTime.Now;
            while (this.generatingEnabled) {
                DateTime newTimeStamp = timeStamp.AddMilliseconds(PeriodMilliseconds);
                TimeSpan span = newTimeStamp - DateTime.Now;
                if (span.Ticks > 0)
                    Thread.Sleep((int)span.TotalMilliseconds);
                timeStamp = newTimeStamp;
                AddPoint(timeStamp);
            }
        }
        void AddPoint(DateTime timeStamp) {
            if (this.volumeIndex > this.dataSource.Count - 2)
                this.volumeIndex = -1;
            if (timeStamp.Minute != this.prevMinute) {
                this.volumeIndex++;
                this.prevMinute = timeStamp.Minute;
            }
            FinancialDataPoint point = GeneratePoint(timeStamp, this.prevPoint, this.dataSource[this.volumeIndex].Volume / 2000d);
            if (this.currentAggregatingPoint.DateTimeStamp.Minute == timeStamp.Minute) {
                this.currentAggregatingPoint.Close = point.Close;
                this.currentAggregatingPoint.High = Math.Max(this.currentAggregatingPoint.High, point.High);
                this.currentAggregatingPoint.Low = Math.Min(this.currentAggregatingPoint.Low, point.Low);
                this.currentAggregatingPoint.Volume += point.Volume;
                lock (this.bufferSync) {
                    if (this.buffer.Count > 0)
                        this.buffer[this.buffer.Count - 1] = this.currentAggregatingPoint;
                    else
                        this.buffer.Add(this.currentAggregatingPoint);
                }
            }
            else {
                lock (this.bufferSync) {
                    this.currentAggregatingPoint = point;
                    this.buffer.Add(point);
                    if (this.buffer.Count > MaxPointsCount)
                        this.buffer.RemoveAt(0);
                }
            }
            this.prevPoint = point;
        }
        bool TheSameMinute(DateTime dt1, DateTime dt2) {
            return (dt1 - DateTime.MinValue).TotalMinutes == (dt2 - DateTime.MinValue).TotalMinutes;
        }

        internal void ReadInitialData() {
            DateTime stamp = DateTime.Now;
            Uri uri = new Uri("/ChartsDemo;component/Data/InitialFinData.csv", UriKind.RelativeOrAbsolute);
            StreamResourceInfo info = Application.GetResourceStream(uri);
            StreamReader reader = new StreamReader(info.Stream);
            for (int i = 0; i < InitialDataPointsCount; i++) {
                if (!reader.EndOfStream) {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');
                    FinancialDataPoint point = new FinancialDataPoint();
                    stamp = stamp.AddMinutes(-1);
                    if (stamp.DayOfWeek == DayOfWeek.Sunday)
                        stamp = stamp.AddDays(-2);
                    point.DateTimeStamp = stamp;
                    point.Open = double.Parse(values[0], CultureInfo.InvariantCulture);
                    point.High = double.Parse(values[1], CultureInfo.InvariantCulture);
                    point.Low = double.Parse(values[2], CultureInfo.InvariantCulture);
                    point.Close = double.Parse(values[3], CultureInfo.InvariantCulture);
                    point.Volume = double.Parse(values[4], CultureInfo.InvariantCulture);
                    this.dataSource.Insert(0, point);
                }
            }
            this.prevPoint = this.dataSource.Last();
            this.currentAggregatingPoint = this.prevPoint;
        }
        internal void UpdateDataSource() {
            List<FinancialDataPoint> tempBuffer;
            lock (this.bufferSync) {
                tempBuffer = new List<FinancialDataPoint>(this.buffer);
                this.buffer.Clear();
            }
            if (tempBuffer.Count == 0)
                return;
            if (TheSameMinute(tempBuffer[0].DateTimeStamp, this.dataSource[this.dataSource.Count - 1].DateTimeStamp)) {
                this.dataSource[this.dataSource.Count - 1] = tempBuffer[0];
            }
            else {
                this.dataSource.Add(tempBuffer[0]);
            }
            if (tempBuffer.Count > 1)
                for (int i = 1; i < tempBuffer.Count; i++)
                    this.dataSource.Add(tempBuffer[i]);
            int overflow = this.dataSource.Count - MaxPointsCount;
            for (int i = 0; i < overflow; i++)
                this.dataSource.RemoveAt(0);
        }
        internal void Start() {
            if (this.generatingThread == null)
                this.generatingThread = new Thread(new ThreadStart(GeneratingLoop));
            this.generatingEnabled = true;
            this.generatingThread.Start();
        }
        public void Stop() {
            this.generatingEnabled = false;
            if (this.generatingThread != null)
                this.generatingThread.Join();
            this.generatingThread = null;
        }
    }

}
