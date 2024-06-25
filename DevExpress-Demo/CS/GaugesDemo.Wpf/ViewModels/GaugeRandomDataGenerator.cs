using System;
using System.Windows.Threading;
using DevExpress.Mvvm;

namespace GaugesDemo {
    public class GaugeRandomDataGenerator : BindableBase {
        public double NeedleValue {
            get { return GetProperty(() => NeedleValue); }
            set { SetProperty(() => NeedleValue, value); }
        }
        public double RangeBarValue {
            get { return GetProperty(() => RangeBarValue); }
            set { SetProperty(() => RangeBarValue, value); }
        }
        public double MarkerValue {
            get { return GetProperty(() => MarkerValue); }
            set { SetProperty(() => MarkerValue, value); }
        }
        public double LevelBarValue {
            get { return GetProperty(() => LevelBarValue); }
            set { SetProperty(() => LevelBarValue, value); }
        }

        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public double UpdateInterval {
            get { return updateTimer.Interval.TotalMilliseconds; }
            set { updateTimer.Interval = TimeSpan.FromMilliseconds(value); }
        }

        readonly Random random = new Random();
        readonly DispatcherTimer updateTimer = new DispatcherTimer();

        double NextValue() {
            return MinValue + (MaxValue - MinValue) * random.NextDouble();
        }
        void OnTimerTick(object sender, EventArgs e) {
            NeedleValue = NextValue();
            RangeBarValue = NextValue();
            MarkerValue = NextValue();
            LevelBarValue = NextValue();
        }
        public void Start() {
            updateTimer.Tick += OnTimerTick;
            updateTimer.Start();
        }
        public void Stop() {
            updateTimer.Stop();
            updateTimer.Tick -= OnTimerTick;
        }
    }
}
