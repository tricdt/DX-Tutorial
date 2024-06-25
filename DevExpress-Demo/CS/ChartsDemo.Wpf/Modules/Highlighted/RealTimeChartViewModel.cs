using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Threading;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {
    [POCOViewModel]
    public class RealTimeChartViewModel : IChartDemoViewModel {
        public static RealTimeChartViewModel Create() {
            return ViewModelSource.Create(() => new RealTimeChartViewModel());
        }

        const double MaxVisualRangeLengthMilliseconds = 50000d;
        readonly DispatcherTimer timer = ChartsDemoModule.CreateTimer();
        readonly SensorDataGenerator generator;

        public ObservableCollection<SensorIndicationItem> DataSource { get { return this.generator.DataSource; } }

        public virtual string FPS { get; protected set; }
        public virtual Palette Palette { get; set; }
        public virtual SolidColorBrush SensorSet1Brush { get; protected set; }
        public virtual SolidColorBrush SensorSet2Brush { get; protected set; }
        public List<MeasurableQuantityPaneViewModel> Panes { get; private set; }
        public List<SensorDataSeriesViewModel> SensorDataSeries { get; private set; }

        int framesCount = 0;
        DateTime lastFpsUpdate = DateTime.Now;

        protected RealTimeChartViewModel() {
            this.generator = new SensorDataGenerator();
            this.generator.GenerateInitialData();
            this.generator.Start();
            this.timer.Tick += Timer_Tick;
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 15);
            CompositionTarget.Rendering += CompositionTarget_Rendering;
            Panes = new List<MeasurableQuantityPaneViewModel> {
                new MeasurableQuantityPaneViewModel("Temperature", false),
                new MeasurableQuantityPaneViewModel("Pressure", false),
                new MeasurableQuantityPaneViewModel("Power", false),
                new MeasurableQuantityPaneViewModel("Intensity", true)
            };
            SensorDataSeries = new List<SensorDataSeriesViewModel> {
                SensorDataSeriesViewModel.Create("SensorIndication1", Panes[0]),
                SensorDataSeriesViewModel.Create("SensorIndication2", Panes[0]),
                SensorDataSeriesViewModel.Create("SensorIndication3", Panes[1]),
                SensorDataSeriesViewModel.Create("SensorIndication4", Panes[1]),
                SensorDataSeriesViewModel.Create("SensorIndication5", Panes[2]),
                SensorDataSeriesViewModel.Create("SensorIndication6", Panes[2]),
                SensorDataSeriesViewModel.Create("SensorIndication7", Panes[3]),
                SensorDataSeriesViewModel.Create("SensorIndication8", Panes[3]),
            };
            Palette = new OfficePalette();
        }
        void Timer_Tick(object sender, EventArgs e) {
            UpdateDataSource();
            if ((DateTime.Now - lastFpsUpdate).TotalSeconds >= 1) {
                FPS = "FPS: " + framesCount.ToString();
                lastFpsUpdate = DateTime.Now;
                framesCount = 0;
            }
        }
        void CompositionTarget_Rendering(object sender, EventArgs e) {
            framesCount++;
        }
        protected void OnPaletteChanged() {
            if (Palette == null)
                return;
            SensorSet1Brush = new SolidColorBrush(Palette[0]);
            SensorSet2Brush = new SolidColorBrush(Palette[1]);
            SensorSet1Brush.Freeze();
            SensorSet2Brush.Freeze();
            for (int i = 0; i < SensorDataSeries.Count; i++)
                if (i % 2 == 0)
                    SensorDataSeries[i].Brush = SensorSet1Brush;
                else
                    SensorDataSeries[i].Brush = SensorSet2Brush;
        }
        public void PauseTimer() {
            this.timer.Stop();
        }
        public void ResumeTimer() {
            this.timer.Start();
        }
        public void Unload() {
            this.timer.Stop();
            this.generator.Stop();
            this.timer.Tick -= Timer_Tick;
            CompositionTarget.Rendering -= CompositionTarget_Rendering;
        }
        public void LimitZoom(XYDiagram2DBeforeZoomEventArgs e) {
            if (!(e.Axis is AxisX2D))
                return;
            if (e.NewRange.Max - e.NewRange.Min > MaxVisualRangeLengthMilliseconds)
                e.Cancel = true;
        }
        public void UpdateDataSource() {
            if (this.generator != null)
                this.generator.UpdateDataSource();
        }
    }

    public class MeasurableQuantityPaneViewModel {
        public string Name {
            get;
            private set;
        }
        public bool AxisXVisible {
            get;
            private set;
        }

        public MeasurableQuantityPaneViewModel(string name, bool scrollBarVisible) {
            Name = name;
            AxisXVisible = scrollBarVisible;
        }
    }

    public class SensorDataSeriesViewModel {
        public static SensorDataSeriesViewModel Create(string valueDataMember, MeasurableQuantityPaneViewModel pane) {
            return ViewModelSource.Create(() => new SensorDataSeriesViewModel(valueDataMember, pane));
        }

        public virtual SolidColorBrush Brush {
            get;
            set;
        }
        public string ValueDataMember {
            get;
            private set;
        }
        public MeasurableQuantityPaneViewModel Pane {
            get;
            private set;
        }

        protected SensorDataSeriesViewModel(string valueDataMember, MeasurableQuantityPaneViewModel pane) {
            ValueDataMember = valueDataMember;
            Pane = pane;
        }
    }
}
