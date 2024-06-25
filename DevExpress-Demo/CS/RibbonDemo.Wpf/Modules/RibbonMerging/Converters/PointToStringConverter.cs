using System;
using System.Windows;
using System.Windows.Data;
namespace RibbonDemo {
    public class PointToStringConverter : IValueConverter {
        public string FormatString { get; set; }
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(!(value is Point))
                return null;
            Point p = (Point)value;
            return String.Format(FormatString ?? "{0},{1}", p.X != -1 ? Math.Round(p.X).ToString() : "", p.X != -1 ? Math.Round(p.Y).ToString() : "");
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
