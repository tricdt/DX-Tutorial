using System;
using System.Globalization;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {

    [CodeFile("Modules/DataAnalysis/MovingAverageAndRegressionLine.xaml"),
     CodeFile("Modules/DataAnalysis/MovingAverageAndRegressionLine.xaml.(cs)"),
     CodeFile("DataModels/SimpleDataPoint.(cs)"),
     CodeFile("DataModels/PointGenerator.(cs)"),
     NoAutogeneratedCodeFiles]
    public partial class MovingAverageAndRegressionLineDemo : ChartsDemoModule {
        public MovingAverageAndRegressionLineDemo() {
            InitializeComponent();
        }

        void chart_QueryChartCursor(object sender, QueryChartCursorEventArgs e) {
            e.CursorImageOffset = new System.Windows.Point(1000000, 1000000);
        }
    }


    class MovingAverageKindToEnvelopePercentEnabledConverter : ForwardOnlyValueConverter {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value.GetType() != typeof(MovingAverageKind))
                return value;
            if (value.Equals(MovingAverageKind.MovingAverage))
                return false;
            else
                return true;
        }
    }

}
