using DevExpress.Data;
using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Threading;

namespace GridDemo {
    [POCOViewModel]
    public class RealtimeDataSourceViewModel {
        #region Stocks
        static readonly string[] Names = new[] {
            "ANR", "FE", "GT", "PRGO", "APD", "PPL", "AES", "AVB", "IBM", "GAS", "EFX", "GPC", "ICE", "IVZ", "KO",
            "CCE", "SO", "STI", "BWA", "HRL", "WFM", "LM", "TROW", "K", "EXPE", "PCAR", "TRIP", "WHR", "WMT", "NU",
            "HST", "CVH", "LMT", "MAR", "CVC", "RF", "VMC", "PHM", "MU", "IRM", "AMT", "BXP", "STT", "PBCT", "FISV",
            "BLL", "MTB", "DIS", "LH", "AKAM", "CPB", "MYL", "LIFE", "LEG", "SCG", "CNX", "COL", "MCHP", "GR", "DUK",
            "BAC", "NUE", "UNM", "DLTR", "ABC", "TEG", "RRD", "EQR", "EXC", "BA", "CME", "NTRS", "VTR", "FITB", "PG",
            "KR", "M", "SNI", "ETN", "CLF", "PH", "KEY", "SHW", "HD", "AFL", "TSS", "CMI", "HBAN", "AEP", "BIG", "LTD",
            "ESRX", "GLW", "WPI", "MON", "AAPL", "DF", "T", "CMA", "THC", "LUV", "TXN", "TIE", "PX",
        };
        #endregion

        #region UpdateCountInfo
        public class UpdateCountInfo {
            public UpdateCountInfo(double time, int count) {
                Time = time;
                Count = count;
            }

            public double Time { get; private set; }
            public int Count { get; private set; }
        } 
        #endregion

        DataUpdaterBase dataUpdater;
        readonly DispatcherTimer updateInfoTimer;
        readonly Stopwatch changesPerSecondStopwatch = Stopwatch.StartNew();

        protected RealtimeDataSourceViewModel() {
            UpdateInfo = new ObservableCollection<UpdateCountInfo>();
            updateInfoTimer = new DispatcherTimer(TimeSpan.FromSeconds(0.5), DispatcherPriority.Send, TimerShowCallback, Dispatcher.CurrentDispatcher);
            UseRealTimeSource = true;
            MaxLoad = 34;
            Load = MaxLoad;
        }

        public ObservableCollection<UpdateCountInfo> UpdateInfo { get; private set; }
        public int MaxLoad { get; private set; }

        public virtual object Source { get; set; }

        public virtual int Load { get; set; }
        protected void OnLoadChanged() {
            SetLoad();
        }

        public virtual bool UseRealTimeSource { get; set; }
        protected void OnUseRealTimeSourceChanged() {
            ClearData();
            var list = Names.Select(name => new MarketData(name)).ToList();
            if(UseRealTimeSource) {
                Source = new RealTimeSource { DataSource = list };
                dataUpdater = new RealTimeDataUpdater(list);
            } else {
                Source = list;
                dataUpdater = new BackgroundDataUpdater(list);
            }
            SetLoad();
        }

        public void Dispose() {
            ClearData();
            updateInfoTimer.Stop();
        }


        void SetLoad() {
            dataUpdater.SetLoad(Load, MaxLoad);
        }

        void ClearData() {
            if(dataUpdater != null)
                dataUpdater.Stop();
            if(Source is RealTimeSource) {
                ((RealTimeSource)Source).Dispose();
            }
        }

        int GetChangesPerSecond() {
            var changes = dataUpdater.GetAndResetChangesCount();
            TimeSpan changeReportInterval = changesPerSecondStopwatch.Elapsed;
            changesPerSecondStopwatch.Restart();
            return Convert.ToInt32(changes / changeReportInterval.TotalSeconds);
        }

        void TimerShowCallback(object sender, EventArgs e) {
            var changePerSecond = GetChangesPerSecond();
            UpdateInfo.Add(new UpdateCountInfo(DateTime.Now.TimeOfDay.TotalSeconds, changePerSecond));
            if(UpdateInfo.Count > 10)
                UpdateInfo.RemoveAt(0);
        }
    }
}
