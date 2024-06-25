using System.Windows;
using DevExpress.Data.Filtering;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {
    public partial class TimeSpanDataAggregationTab : TabItemModule {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title",
            typeof(string), typeof(TimeSpanDataAggregationTab), null);

        XYDiagram2D Diagram {
            get { return chart.Diagram as XYDiagram2D; }
        }

        public string Title {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public TimeSpanDataAggregationTab() {
            InitializeComponent();
            this.lbeAggregationFunctions.FilterCriteria = CriteriaOperator.Parse("Not ([Id] in ('None', 'Financial', 'Histogram', 'Sum', 'Count', 'Custom'))");
        }
        internal static double ConvertCelsiusToFahrenheit(double value) {
            return value * 1.8 + 32;
        }
        void chart_AxisScaleChanged(object sender, AxisScaleChangedEventArgs e) {
            Axis2D axisX = Diagram.AxisX;
            Axis2D axisY = Diagram.AxisY;
            if (e.Axis == axisX) {
                TimeSpanScaleChangedEventArgs args = e as TimeSpanScaleChangedEventArgs;
                if (args != null && args.MeasureUnitChange.IsChanged)
                    Title = "Measure Unit: " + args.MeasureUnitChange.NewValue;
            }
            else if (e.Axis == axisY) {
                Diagram.SecondaryAxesY[0].WholeRange.SetMinMaxValues(ConvertCelsiusToFahrenheit(axisY.ActualVisualRange.ActualMinValueInternal),
                    ConvertCelsiusToFahrenheit(axisY.ActualVisualRange.ActualMaxValueInternal));
            }
        }
        void Chart_CustomDrawCrosshair(object sender, CustomDrawCrosshairEventArgs e) {
            if (e.CrosshairElementGroups.Count > 0 && e.CrosshairElementGroups[0].CrosshairElements.Count > 0) {
                foreach (CrosshairElement element in e.CrosshairElementGroups[0].CrosshairElements) {
                    if (element.LabelElement != null && element.SeriesPoint != null)
                        element.LabelElement.Text += string.Format(", {0:0}°F", ConvertCelsiusToFahrenheit(element.SeriesPoint.Value));
                }
            }
        }
    }
}
