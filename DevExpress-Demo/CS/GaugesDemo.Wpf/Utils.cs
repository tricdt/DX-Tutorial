using System;
using System.Windows.Data;
using System.Windows.Markup;
using DevExpress.Utils;

namespace GaugesDemo {
    public class BoolToDefaultBooleanConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }

        #region IValueConvector implementation
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return (bool)value ? DefaultBoolean.True : DefaultBoolean.False;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
}
