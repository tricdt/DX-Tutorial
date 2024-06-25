using System;
using System.Collections.Generic;
using System.Windows.Threading;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {
    public class RealTime3DChartDemoViewModel {
        const int Interval = 40;

        readonly RealTime3DDataGenerator dataGenerator;
        readonly DispatcherTimer timer;
        bool inProcess = true;

        public virtual IEnumerable<double> Values { get; set; }
        public virtual FillStyleBase FillStyle { get; set; }
        public virtual int Size { get; set; }
        public virtual bool IsTimerEnabled { get; set; }
        public virtual IEnumerable<object> ArgumentsX { get; protected set; }
        public virtual IEnumerable<object> ArgumentsY { get; protected set; }
        public virtual double MinValue { get; protected set; }
        public virtual double MaxValue { get; protected set; }

        public RealTime3DChartDemoViewModel() {
            this.dataGenerator = new RealTime3DDataGenerator();
            this.timer = CreateTimer();
            IsTimerEnabled = true;
            Size = 50;
        }

        public void DisableProcess() {
            this.inProcess = this.timer.IsEnabled;
            IsTimerEnabled = false;
        }
        public void RestoreProcess() {
            IsTimerEnabled = this.inProcess;
        }
        public void ToggleIsTimerEnabled() {
            IsTimerEnabled = !IsTimerEnabled;
        }

        protected void OnIsTimerEnabledChanged() {
            this.timer.IsEnabled = IsTimerEnabled;
        }
        protected void OnSizeChanged() {
            var oldIsEnabled = this.timer.IsEnabled;
            this.timer.IsEnabled = false;

            this.dataGenerator.Size = Size + 1;
            MinValue = -this.dataGenerator.Size / 3 - 1.5;
            MaxValue = this.dataGenerator.Size / 3 + 1.5;
            UpdateDataSource();
            UpdateValues();

            this.timer.IsEnabled = oldIsEnabled;
        }

        DispatcherTimer CreateTimer() {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(Interval);
            timer.Tick += (_d, _e) => UpdateValues();
            return timer;
        }
        FillStyleBase CreateGradientFillStyle() {
            GradientFillStyle style = new GradientFillStyle();
            style.ColorStops.Add(new ColorStop() { Offset = new Unit(MaxValue) });
            style.ColorStops.Add(new ColorStop() { Offset = new Unit(0) });
            style.ColorStops.Add(new ColorStop() { Offset = new Unit(MinValue) });
            return style;
        }
        void UpdateDataSource() {
            IEnumerable<object> arguments = this.dataGenerator.GenerateArguments();
            ArgumentsX = arguments;
            ArgumentsY = arguments;
            this.dataGenerator.RecreateElevations();
            FillStyle = CreateGradientFillStyle();
        }
        void UpdateValues() {
            Values = this.dataGenerator.GenerateValues();
        }
    }
}
