using System.Windows;
using DevExpress.Data.Filtering;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {
    public partial class NumericDataAggregationTab : TabItemModule {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title",
            typeof(string), typeof(NumericDataAggregationTab), null);

        public string Title {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public NumericDataAggregationTab() {
            InitializeComponent();
            this.lbeAggregationFunctions.FilterCriteria = CriteriaOperator.Parse("Not ([Id] in ('None', 'Financial', 'Histogram'))");
        }

        void chart_AxisScaleChanged(object sender, AxisScaleChangedEventArgs e) {
            AxisBase axisX = ((XYDiagram2D)this.chart.Diagram).AxisX;
            if (e.Axis != axisX)
                return;
            NumericScaleChangedEventArgs numericArgs = e as NumericScaleChangedEventArgs;
            if (numericArgs == null)
                return;
            if (numericArgs.MeasureUnitChange.IsChanged) {
                Title = "Measure Unit: " + numericArgs.MeasureUnitChange.NewValue;
            }
        }
    }
}
