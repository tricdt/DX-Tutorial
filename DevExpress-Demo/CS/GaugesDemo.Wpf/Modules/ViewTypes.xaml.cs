using System;
using System.Windows.Threading;

namespace GaugesDemo {
    public partial class ViewTypes : GaugesDemoModule {
        public ViewTypes() {
            InitializeComponent();
        }
    }

    public class CurrentTimeViewModel {
        readonly DispatcherTimer timer = new DispatcherTimer();

        public virtual string CurrentTime { get; protected set; }

        protected CurrentTimeViewModel() {
            UpdateTime();
            timer.Interval = TimeSpan.FromSeconds(1);
        }
        void OnTimedEvent(object source, EventArgs e) {
            UpdateTime();
        }
        void UpdateTime() {
            CurrentTime = string.Format("{0:H:mm:ss}", DateTime.Now);
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
