using System;
using System.Globalization;
using System.Windows.Data;
using DevExpress.Xpf.Gauges;

namespace DevExpress.SalesDemo.Wpf.Converters {
    public class DecimalToRangeValueConverter  : IValueConverter{
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            double doubleValue = System.Convert.ToDouble(value);
            RangeValueType valueType = parameter == null ? RangeValueType.Absolute : (RangeValueType)parameter;
            return new RangeValue(doubleValue, valueType);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            RangeValue rv = (RangeValue)value;
            return rv.Value;
        }
    }
}
