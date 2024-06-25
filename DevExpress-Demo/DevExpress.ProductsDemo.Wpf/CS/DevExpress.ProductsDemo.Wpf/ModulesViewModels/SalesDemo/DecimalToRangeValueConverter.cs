using DevExpress.Xpf.Gauges;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ProductsDemo {
    public class DecimalToRangeValueConverter : IValueConverter {
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
