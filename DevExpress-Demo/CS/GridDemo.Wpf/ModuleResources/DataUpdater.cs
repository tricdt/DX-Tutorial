using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace GridDemo {
    #region DataUpdaterBase
    public abstract class DataUpdaterBase {
        readonly Random rndRow = new Random();
        readonly IList<MarketData> list;
        int changeCount = 0;
        protected bool NeedStop { get; private set; }

        public DataUpdaterBase(IList<MarketData> list) {
            this.list = list;
        }
        public int GetAndResetChangesCount() {
            var result = changeCount;
            changeCount = 0;
            return result;
        }
        public void Stop() {
            this.NeedStop = true;
        }
        public abstract void SetLoad(int loadValue, int maxLoadValue);
        protected void RandomlyUpdateOneRow() {
            int row = rndRow.Next(0, list.Count);
            list[row].Update();
            changeCount++;
        }
    }
    #endregion

    #region RealTimeDataUpdater
    public class RealTimeDataUpdater : DataUpdaterBase {
        public RealTimeDataUpdater(IList<MarketData> list) : base(list) {
            System.Threading.Tasks.Task.Factory.StartNew(UpdateData, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }
        void UpdateData() {
            Stopwatch watch = Stopwatch.StartNew();
            do {
                watch.Restart();
                RandomlyUpdateOneRow();

                int sleep = (int)Math.Floor(1000.0 * Convert.ToDouble((interEventDelay - watch.ElapsedTicks) / Stopwatch.Frequency));
                if(sleep > 0)
                    Thread.Sleep(sleep);
                while(interEventDelay - watch.ElapsedTicks > 0) {
                    Thread.Sleep(0);
                }
                watch.Stop();
            } while(!NeedStop);
        }

        int interEventDelay;
        public override void SetLoad(int loadValue, int maxLoadValue) {
            double pow = (maxLoadValue - loadValue + 3) * 0.5;
            interEventDelay = (int)Math.Pow(2.0, pow);
        }

    }
    #endregion

    #region BackgroundDataUpdater
    public class BackgroundDataUpdater : DataUpdaterBase {
        DispatcherTimer timer;
        public BackgroundDataUpdater(IList<MarketData> list) : base(list) {
            timer = new DispatcherTimer(DispatcherPriority.Background);
            timer.Tick += OnTimerTick;
            timer.Interval = TimeSpan.FromMilliseconds(0);
            timer.Start();

        }
        void OnTimerTick(object sender, EventArgs e) {
            if(NeedStop) {
                timer.Stop();
                return;
            }
            for(int i = 0; i < 2; i++) {
                RandomlyUpdateOneRow();
            }
        }
        public override void SetLoad(int loadValue, int maxLoadValue) {
            timer.Interval = TimeSpan.FromMilliseconds((maxLoadValue - loadValue) * 2);
        }
    } 
    #endregion
}
