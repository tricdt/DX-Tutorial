using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading;

namespace ChartsDemo {

    class SensorDataGenerator {
        const int DataGenerationIntervalMilliseconds = 15;
        const int InitialDataPointsCount = 10000;

        readonly List<SensorIndicationItem> buffer = new List<SensorIndicationItem>();
        int counter;

        readonly RealTimeDataCollection dataSource = new RealTimeDataCollection();
        bool generatingEnabled = false;
        Thread generatingThread;
        readonly Random random = new Random(1);
        readonly object sync = new object();
        double yAddition1 = 0;
        double yAddition2 = 0;
        double yAddition3 = 0;
        double yAddition4 = 0;
        double yAddition5 = 0;
        double yAddition6 = 0;
        double yAddition7 = 0;
        double yAddition8 = 0;

        void AddPoint(DateTime timeStamp) {
            SensorIndicationItem point = CreatePoint(timeStamp);
            lock (sync) {
                buffer.Add(point);
            }
        }

        SensorIndicationItem CreatePoint(DateTime timeStamp) {
            counter++;
            double arg = timeStamp.ToOADate();
            arg = arg * 250000d;
            if (counter % random.Next(300, 500) == 0)
                yAddition1 += random.Next(10, 20) * Math.Sign(random.NextDouble() - 0.5);
            if (yAddition1 < -30)
                yAddition1 += 10;
            if (yAddition1 > 30)
                yAddition1 -= 10;
            double indication1 = 5 * Math.Sin(5d / 2d * Math.Cos(arg)) + 100 + (random.NextDouble() - 0.5) * 5 + yAddition1;
            if (counter % random.Next(100, 300) == 0)
                yAddition2 += random.Next(10, 20) * Math.Sign(random.NextDouble() - 0.5);
            if (yAddition2 < -30)
                yAddition2 += 10;
            if (yAddition2 > 30)
                yAddition2 -= 10;
            double indication2 = 4 * Math.Sin(7 * Math.Cos(arg - 1.5)) + 90 + (random.NextDouble() - 0.5) * 7 + yAddition2;

            if (counter % random.Next(100, 500) == 0)
                yAddition3 += random.Next(10, 20) * Math.Sign(random.NextDouble() - 0.5);
            if (yAddition3 < -30)
                yAddition3 += 10;
            if (yAddition3 > 30)
                yAddition3 -= 10;
            double indication3 = 10 * (Math.Sin(arg) + Math.Sin(arg / 1.2d) + Math.Sin(arg / 1.5d)) + 100 + random.NextDouble() * 12 + yAddition3;
            if (counter % random.Next(50, 100) == 0)
                yAddition4 += random.Next(10, 20) * Math.Sign(random.NextDouble() - 0.5);
            if (yAddition4 < -30)
                yAddition4 += 10;
            if (yAddition4 > 30)
                yAddition4 -= 10;
            double indication4 = 10 * (Math.Cos(arg + 1.5) + Math.Sin(arg / 1.2d + 0.5) + Math.Cos(arg / 1.5d + 0.3)) + 120 + random.NextDouble() * 15 + yAddition4;

            if (counter % random.Next(300, 400) == 0)
                yAddition5 += random.Next(10, 20) * Math.Sign(random.NextDouble() - 0.5);
            if (yAddition5 < -30)
                yAddition5 += 10;
            if (yAddition5 > 30)
                yAddition5 -= 10;
            double indication5 = 15 * Math.Cos(Math.Tan(arg + random.NextDouble() / 10)) + 500 + random.NextDouble() * 15 + yAddition5;
            if (counter % random.Next(400, 1000) == 0)
                yAddition6 += random.Next(10, 20) * Math.Sign(random.NextDouble() - 0.5);
            if (yAddition6 < -30)
                yAddition6 += 10;
            if (yAddition6 > 30)
                yAddition6 -= 10;
            double indication6 = 20 * Math.Sin(Math.Tan(arg + 1)) + 450 + random.NextDouble() * 9 + yAddition6;

            if (counter % random.Next(200, 300) == 0)
                yAddition7 += random.Next(10, 20) * Math.Sign(random.NextDouble() - 0.5);
            if (yAddition7 < -30)
                yAddition7 += 10;
            if (yAddition7 > 30)
                yAddition7 -= 10;
            double indication7 = 30 * Math.Abs(Math.Sin(Math.Tan(arg + 1))) + Math.Cos(arg) + Math.Sin(arg) + 750 + random.NextDouble() * 15 + yAddition7;
            if (counter % random.Next(300, 350) == 0)
                yAddition8 += random.Next(10, 20) * Math.Sign(random.NextDouble() - 0.5);
            if (yAddition8 < -30)
                yAddition8 += 10;
            if (yAddition8 > 30)
                yAddition8 -= 10;
            double indication8 = 30 * (1 - Math.Cos(arg)) / random.Next(1, 5) + 700 + random.NextDouble() * 15 + yAddition8;

            return new SensorIndicationItem(timeStamp, indication1, indication2, indication3, indication4, indication5, indication6, indication7, indication8);
        }
        void GeneratingLoop() {
            DateTime timeStamp = DateTime.Now;
            while (generatingEnabled) {
                DateTime newTimeStamp = timeStamp.AddMilliseconds(DataGenerationIntervalMilliseconds);
                TimeSpan span = newTimeStamp - DateTime.Now;
                if (span.Ticks > 0)
                    Thread.Sleep((int)span.TotalMilliseconds);
                timeStamp = newTimeStamp;
                AddPoint(timeStamp);
            }
        }

        internal void GenerateInitialData() {
            DateTime baseTimeStamp = DateTime.Now.AddMilliseconds(-InitialDataPointsCount * DataGenerationIntervalMilliseconds);
            DateTime argument = baseTimeStamp;
            for (int i = 0; i < InitialDataPointsCount - 1; i++) {
                argument = argument.AddMilliseconds(DataGenerationIntervalMilliseconds);
                SensorIndicationItem point = CreatePoint(argument);
                dataSource.Add(point);
            }
        }
        internal void Start() {
            if (generatingThread == null)
                generatingThread = new Thread(new ThreadStart(GeneratingLoop));
            generatingEnabled = true;
            generatingThread.Start();
        }
        internal void Stop() {
            generatingEnabled = false;
            if (generatingThread != null)
                generatingThread.Join();
            generatingThread = null;
        }
        internal void UpdateDataSource() {
            lock (sync) {
                dataSource.AddRange(buffer);
                if (dataSource.Count > InitialDataPointsCount)
                    dataSource.RemoveRangeAt(0, buffer.Count);
                buffer.Clear();
            }
        }

        public RealTimeDataCollection DataSource {
            get { return this.dataSource; }
        }
    }


    public class SensorIndicationItem {
        internal SensorIndicationItem(DateTime timeStamp, double sensorIndication1,
                                                        double sensorIndication2,
                                                        double sensorIndication3,
                                                        double sensorIndication4,
                                                        double sensorIndication5,
                                                        double sensorIndication6,
                                                        double sensorIndication7,
                                                        double sensorIndication8

            ) {
            TimeStamp = timeStamp;
            SensorIndication1 = sensorIndication1;
            SensorIndication2 = sensorIndication2;
            SensorIndication3 = sensorIndication3;
            SensorIndication4 = sensorIndication4;
            SensorIndication5 = sensorIndication5;
            SensorIndication6 = sensorIndication6;
            SensorIndication7 = sensorIndication7;
            SensorIndication8 = sensorIndication8;
        }

        public double SensorIndication1 { get; private set; }
        public double SensorIndication2 { get; private set; }
        public double SensorIndication3 { get; private set; }
        public double SensorIndication4 { get; private set; }
        public double SensorIndication5 { get; private set; }
        public double SensorIndication6 { get; private set; }
        public double SensorIndication7 { get; private set; }
        public double SensorIndication8 { get; private set; }
        public DateTime TimeStamp { get; private set; }
    }


    public class RealTimeDataCollection : ObservableCollection<SensorIndicationItem> {
        public void AddRange(IList<SensorIndicationItem> items) {
            foreach (SensorIndicationItem item in items)
                Items.Add(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (IList)items, Items.Count - items.Count));
        }
        public void RemoveRangeAt(int startingIndex, int count) {
            var removedItems = new List<SensorIndicationItem>(count);
            for (int i = 0; i < count; i++) {
                removedItems.Add(Items[startingIndex]);
                Items.RemoveAt(startingIndex);
            }
            if (count > 0)
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedItems, startingIndex));
        }
    }

}
