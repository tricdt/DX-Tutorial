using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace ChartsDemo {
    abstract class ForwardOnlyValueConverter : MarkupExtension, IValueConverter {
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return value;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
