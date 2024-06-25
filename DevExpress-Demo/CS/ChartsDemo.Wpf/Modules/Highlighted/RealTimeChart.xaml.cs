using System;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/Highlighted/RealTimeChart.xaml"),
     CodeFile("Modules/Highlighted/RealTimeChart.xaml.(cs)"),
     CodeFile("DataModels/SensorDataGenerator.(cs)"),
     CodeFile("Modules/Highlighted/RealTimeChartViewModel.(cs)"),
     NoAutogeneratedCodeFiles]
    public partial class RealTimeChartDemo : ChartsDemoModule {
        RealTimeChartViewModel ViewModel { get { return DataContext as RealTimeChartViewModel; } }
        public override ChartControl ModuleChartControl { get { return chart; } }

        public RealTimeChartDemo() {
            InitializeComponent();
            Loaded += (s, e) => SetVisualRange();
            Unloaded += (s, e) => { ViewModel.Unload(); };
        }
        public override void OnStopModule() {
            ViewModel.PauseTimer ();
        }
        public override void OnStartModule() {
            ViewModel.ResumeTimer();
        }
        
        public void SetVisualRange() {
            AxisX2D axisX = ((XYDiagram2D)chart.Diagram).ActualAxisX;
            DateTime wholeRangeMax = (DateTime)axisX.ActualWholeRange.ActualMaxValue;
            axisX.ActualVisualRange.SetMinMaxValues(wholeRangeMax.AddSeconds(-14), wholeRangeMax);
        }
    }
}
