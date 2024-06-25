using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using DevExpress.Data.Filtering;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {
    public partial class QualitativeDataAggregationTab : TabItemModule {
        public QualitativeDataAggregationTab() {
            InitializeComponent();
            this.lbeAggregationFunctions.FilterCriteria = CriteriaOperator.Parse("Not ([Id] in ('Financial', 'Histogram'))");
        }
    }

    public class AggregateFunctionToTitleConverter : MarkupExtension, IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value.GetType() != typeof(AggregateFunction))
                return value;
            AggregateFunction function = (AggregateFunction)value;
            switch (function) {
                case AggregateFunction.None:
                    return "Sale Prices (USD)";
                case AggregateFunction.Average:
                    return "Average Sale Price (USD)";
                case AggregateFunction.Minimum:
                    return "Minimum Sale Price (USD)";
                case AggregateFunction.Maximum:
                    return "Maximum Sale Price (USD)";
                case AggregateFunction.Sum:
                    return "Total Income (USD)";
                case AggregateFunction.Count:
                    return "Sold Items Count";
                case AggregateFunction.Custom:
                    return "Price Standard Deviation";
                default:
                    throw new InvalidEnumArgumentException(string.Format("The {0} value is not supported.", function));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return value;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
