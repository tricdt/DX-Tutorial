using System;
using System.Windows;
using System.Windows.Data;

namespace GridDemo {
    public class AutoFilterConditionVisibilityConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            string strValue = (string)value;
            return (strValue == "ToId" || strValue == "HasAttachment" || strValue == "Sent") ? Visibility.Collapsed : Visibility.Visible;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
