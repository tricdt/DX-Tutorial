using System;
using System.Windows.Threading;

namespace GaugesDemo {
    public class EqualizerViewModel {
        public virtual double Frequency32 { get; protected set; }
        public virtual double Frequency64 { get; protected set; }
        public virtual double Frequency125 { get; protected set; }
        public virtual double Frequency250 { get; protected set; }
        public virtual double Frequency500 { get; protected set; }
        public virtual double Frequency1K { get; protected set; }
        public virtual double Frequency2K { get; protected set; }
        public virtual double Frequency4K { get; protected set; }
        public virtual double Frequency8K { get; protected set; }
        public virtual double Frequency16K { get; protected set; }

        const int MaxValue = 100;

        readonly Random random = new Random();
        readonly DispatcherTimer timer = new DispatcherTimer();

        protected EqualizerViewModel() {
            timer.Interval = TimeSpan.FromMilliseconds(500);
            UpdateData();
        }
        void UpdateData() {
            Frequency32 = random.Next(MaxValue);
            Frequency64 = random.Next(MaxValue);
            Frequency125 = random.Next(MaxValue);
            Frequency250 = random.Next(MaxValue);
            Frequency500 = random.Next(MaxValue);
            Frequency1K = random.Next(MaxValue);
            Frequency2K = random.Next(MaxValue);
            Frequency4K = random.Next(MaxValue);
            Frequency8K = random.Next(MaxValue);
            Frequency16K = random.Next(MaxValue);
        }
        void OnTimedEvent(object source, EventArgs e) {
            UpdateData();
        }

        public void Start() {
            timer.Tick += OnTimedEvent;
            timer.Start();
        }
        public void Stop() {
            timer.Stop();
            timer.Tick -= OnTimedEvent;
        }
    }
}
