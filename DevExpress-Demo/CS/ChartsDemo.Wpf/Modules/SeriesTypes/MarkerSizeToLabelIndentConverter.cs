using System;
using System.Globalization;

namespace ChartsDemo {
    class MarkerSizeToLabelIndentConverter : ForwardOnlyValueConverter {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return ((double)value) / 2;
        }
    }
}
