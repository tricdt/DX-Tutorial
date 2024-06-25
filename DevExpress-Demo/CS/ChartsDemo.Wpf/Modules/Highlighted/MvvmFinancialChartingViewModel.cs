using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {
    public class MvvmFinancialChartingViewModel : IChartDemoViewModel {
        public static MvvmFinancialChartingViewModel Create() {
            return ViewModelSource.Create(() => new MvvmFinancialChartingViewModel());
        }

        const int InitialPointsOnScreen = 60;
        const int MaxPointsOnScreen = 90;

        readonly DispatcherTimer timer = ChartsDemoModule.CreateTimer();
        readonly RealTimeFinancialDataGenerator generator;

        public ObservableCollection<FinancialDataPoint> StockData { get; private set; }
        public List<IndicatorItem> AllIndicators { get; private set; }
        public List<IndicatorItem> SeparatePaneIndicators { get; private set; }
        public double SideMarginsValue { get; private set; }

        public virtual DateTime VisualXRangeMin { get; set; }
        public virtual DateTime VisualXRangeMax { get; set; }
        public virtual DateTimeMeasureUnit AxisXMeasureUnit { get; set; }
        public virtual int AxisXMeasureUnitMultiplier { get; set; }
        public virtual double CurrentPrice { get; set; }
        public virtual double PriceAxisYSideMargin { get; set; }
        public virtual double VolumeAxisYSideMargin { get; set; }
        public virtual string AxisXLabelPattern { get; set; }

        protected MvvmFinancialChartingViewModel() {
            this.generator = new RealTimeFinancialDataGenerator();
            this.generator.ReadInitialData();
            StockData = this.generator.DataSource;
            this.timer.Tick += Timer_Tick;
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 41);
            AllIndicators = new List<IndicatorItem>() {
                IndicatorItem.Create(IndicatorType.BollingerBands, "Bollinger Bands", "BB (20, 2)")
            };
            List<double> cciLevels = new List<double>() { -100, 100 };
            List<double> wrLevels = new List<double>() { -80, -20 };
            SeparatePaneIndicators = new List<IndicatorItem>() {
                IndicatorItem.Create(IndicatorType.CommodityChannelIndex, "Commodity Channel Index", "CCI (14)", cciLevels, -150, 150),
                IndicatorItem.Create(IndicatorType.WilliamsR, "Williams %R", "%R (14)", wrLevels, -100, 0),
            };
            AllIndicators.AddRange(SeparatePaneIndicators);
            SideMarginsValue = 2;
            SetInitialState();
        }

        void Timer_Tick(object sender, EventArgs e) {
            UpdateDataSource();
        }
        void UpdateAxisXVisibility() {
            bool lastVisibleIndicatorFound = false;
            for (int i = SeparatePaneIndicators.Count - 1; i > -1; i--) {
                IndicatorItem indicatorItem = SeparatePaneIndicators[i];
                if (!lastVisibleIndicatorFound) {
                    indicatorItem.ShowXAxis = true;
                    lastVisibleIndicatorFound = true;
                }
                else {
                    indicatorItem.ShowXAxis = false;
                }
            }
        }
        void UpdateVisualRangeAndSideMargins(DateTimeMeasureUnit unit, int multiplier) {
            double priceAxisYSideMargin = 0;
            double volumeAxisYSideMargin = 0;
            DateTime visualXRangeMax = new DateTime();
            DateTime visualXRangeMin = new DateTime();
            string axisXLabelPattern = "";
            switch (unit) {
                case DateTimeMeasureUnit.Minute:
                    priceAxisYSideMargin = 0.03 * multiplier;
                    volumeAxisYSideMargin = 500 * multiplier;
                    visualXRangeMax = StockData.Last().DateTimeStamp.AddMinutes(Math.Max(5, multiplier) * SideMarginsValue);
                    visualXRangeMin = visualXRangeMax.AddMinutes(-InitialPointsOnScreen * Math.Max(5, multiplier));
                    axisXLabelPattern = "{A:g}";
                    break;
                case DateTimeMeasureUnit.Hour:
                    priceAxisYSideMargin = 1.8;
                    volumeAxisYSideMargin = 30000;
                    visualXRangeMax = StockData.Last().DateTimeStamp.AddHours(multiplier * SideMarginsValue);
                    visualXRangeMin = visualXRangeMax.AddHours(-InitialPointsOnScreen * multiplier);
                    axisXLabelPattern = "{A:d}";
                    break;
            }
            PriceAxisYSideMargin = priceAxisYSideMargin;
            VolumeAxisYSideMargin = volumeAxisYSideMargin;
            VisualXRangeMax = visualXRangeMax;
            VisualXRangeMin = visualXRangeMin;
            AxisXLabelPattern = axisXLabelPattern;
        }

        public void SetInitialState() {
            VisualXRangeMax = new DateTime(); 
            VisualXRangeMin = new DateTime();
            AxisXMeasureUnit = DateTimeMeasureUnit.Minute;
            AxisXMeasureUnitMultiplier = 5;
            this.generator.Start();
            UpdateAxisXVisibility();
        }
        public void CheckVisualRangeOnZoom(XYDiagram2DBeforeZoomEventArgs e) {
            if (!(e.Axis is AxisX2D))
                return;
            if (e.NewRange.Max - e.NewRange.Min > MaxPointsOnScreen)
                e.Cancel = true;
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
        }
        public void UpdateDataSource() {
            if (this.generator != null)
                this.generator.UpdateDataSource();
            CurrentPrice = this.generator.DataSource.Last().Close;
        }
        protected void OnAxisXMeasureUnitChanged() {
            UpdateVisualRangeAndSideMargins(AxisXMeasureUnit, AxisXMeasureUnitMultiplier);
        }
        protected void OnAxisXMeasureUnitMultiplierChanged() {
            UpdateVisualRangeAndSideMargins(AxisXMeasureUnit, AxisXMeasureUnitMultiplier);
        }
    }

    public class IndicatorItem {
        public IndicatorType Type { get; private set; }
        public string Name { get; private set; }
        public string LegendText { get; private set; }
        public virtual bool ShowXAxis { get; set; }
        public List<double> SignalLineValues { get; private set; }
        public double? YRangeMin { get; private set; }
        public double? YRangeMax { get; private set; }

        protected IndicatorItem(IndicatorType indicatorType, string indicatorName, string indicatorLegendText, List<double> signalLines, double? yAxisRangeMin, double? yAxisRangeMax) {
            Type = indicatorType;
            Name = indicatorName;
            LegendText = indicatorLegendText;
            if (signalLines == null)
                SignalLineValues = new List<double>();
            else
                SignalLineValues = signalLines;
            YRangeMin = yAxisRangeMin;
            YRangeMax = yAxisRangeMax;
        }

        public static IndicatorItem Create(IndicatorType indicatorType, string indicatorName, string indicatorLegendText, List<double> signalLines = null, double? yAxisRangeMin = null, double? yAxisRangeMax = null) {
            return ViewModelSource.Create(() => new IndicatorItem(indicatorType, indicatorName, indicatorLegendText, signalLines, yAxisRangeMin, yAxisRangeMax));
        }
    }


    public enum IndicatorType {
        CommodityChannelIndex,
        WilliamsR,
        BollingerBands,
    }
}
