using System;
using System.Linq;
using System.Windows.Data;

namespace ControlsDemo {
    public class ImageNameConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value == null) return null;
            return value.ToString().Split('/').Last();
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}
