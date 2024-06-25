using System;
using System.Collections.Generic;

namespace DevExpress.SalesDemo.Model {
    public static class DataSource {
        static IDataProvider instance;
        public static IDataProvider GetDataProvider() {
            if(instance == null)
                EnsureDataProvider();
            return instance;
        }
        public static void EnsureDataProvider() {
            if(instance != null) return;
            string dbPath = DevExpress.DemoData.Helpers.DataFilesHelper.FindFile(@"sales.mdb", "Data");
            System.IO.File.SetAttributes(dbPath, System.IO.FileAttributes.Normal);
            var generator = new DevExpress.Demos.SalesDBGenerator.SalesGenerator();
            using(ProgressTracker.Instance.StartTracking(generator)) {
                generator.Run(OleDataProvider.GetConnectionString(dbPath));
                instance = new OleDataProvider(dbPath);
            }
        }
        public static IObservable<int> Progress {
            get { return ((IObservable<int>)ProgressTracker.Instance); }
        }
        #region IObservable
        class ProgressTracker : IObservable<int> {
            internal static ProgressTracker Instance = new ProgressTracker();
            IList<IObserver<int>> observers;
            ProgressTracker() {
                observers = new List<IObserver<int>>();
            }
            public IDisposable StartTracking(IDataGenerator generator) {
                return new TrackingContext(generator, this);
            }
            IDisposable IObservable<int>.Subscribe(IObserver<int> observer) {
                return new Subscription(this, observer);
            }
            void generator_Start(object sender, ProgressEventArgs e) {
                foreach(IObserver<int> observer in observers)
                    observer.OnNext(e.Progress);
            }
            void generator_Complete(object sender, ProgressEventArgs e) {
                foreach(IObserver<int> observer in observers)
                    observer.OnCompleted();
            }
            void generator_Progress(object sender, ProgressEventArgs e) {
                foreach(IObserver<int> observer in observers)
                    observer.OnNext(e.Progress);
            }
            class TrackingContext : IDisposable {
                IDataGenerator generator;
                ProgressTracker tracker;
                public TrackingContext(IDataGenerator generator, ProgressTracker tracker) {
                    this.generator = generator;
                    this.tracker = tracker;
                    generator.GenerationStart += OnGenerationStart;
                    generator.GenerationComplete += OnGenerationComplete;
                    generator.GenerationProgress += OnGenerationProgress;
                }
                void IDisposable.Dispose() {
                    generator.GenerationStart -= OnGenerationStart;
                    generator.GenerationComplete -= OnGenerationComplete;
                    generator.GenerationProgress -= OnGenerationProgress;
                }
                void OnGenerationStart(object sender, ProgressEventArgs e) {
                    tracker.generator_Start(sender, e);
                }
                void OnGenerationComplete(object sender, ProgressEventArgs e) {
                    tracker.generator_Complete(sender, e);
                }
                void OnGenerationProgress(object sender, ProgressEventArgs e) {
                    tracker.generator_Progress(sender, e);
                }
            }
            class Subscription : IDisposable {
                IObserver<int> observer;
                ProgressTracker tracker;
                public Subscription(ProgressTracker tracker, IObserver<int> observer) {
                    if(!tracker.observers.Contains(observer))
                        tracker.observers.Add(observer);
                    this.tracker = tracker;
                    this.observer = observer;
                }
                void IDisposable.Dispose() {
                    tracker.observers.Remove(observer);
                }
            }
        }
        #endregion IObservable
    }
    //
    public class SalesGroup {
        public string GroupName {
            get;
            set;
        }
        public decimal TotalCost {
            get;
            set;
        }
        public int Units {
            get;
            set;
        }
        public DateTime StartOfPeriod {
            get;
            set;
        }
        public DateTime EndOfPeriod {
            get;
            set;
        }
        public SalesGroup() { }
        public SalesGroup(string groupName, decimal totalCost, int unitsSold, DateTime startOfPeriod, DateTime endOfPeriod) {
            GroupName = groupName;
            TotalCost = totalCost;
            Units = unitsSold;
            StartOfPeriod = startOfPeriod;
            EndOfPeriod = endOfPeriod;
        }
    }
    public enum GroupingPeriod {
        Hour,
        Day,
        All,
        None
    }
}
