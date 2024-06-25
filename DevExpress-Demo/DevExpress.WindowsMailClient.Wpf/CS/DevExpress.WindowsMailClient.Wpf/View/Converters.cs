using System;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Markup;
using System.Windows.Media;
using DevExpress.Data;

namespace DevExpress.WindowsMailClient.Wpf.View {
    public class NameToColorConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string name = value as string;
            if (string.IsNullOrEmpty(name))
                return Colors.Black;
            byte color = (byte)(Math.Abs(name.GetHashCode()) % 5);
            switch (color) {
                case 0:
                    return (Color)ColorConverter.ConvertFromString("#FF31669C");
                case 1:
                    return (Color)ColorConverter.ConvertFromString("#FF598A7A");
                case 2:
                    return (Color)ColorConverter.ConvertFromString("#FFCCA464");
                case 3:
                    return (Color)ColorConverter.ConvertFromString("#FFE06B4C");
                case 4:
                    return (Color)ColorConverter.ConvertFromString("#FF558AC0");
                default:
                    return Colors.Black;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class OrderNameConverter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            if (values.Length != 2)
                return null;
            if (!(values[1] is ColumnSortOrder))
                return null;
            ColumnSortOrder order = (ColumnSortOrder)values[1];
            if (values[0].ToString() == "Received") {
                switch (order) {
                    case ColumnSortOrder.Ascending:
                        return "Oldest";
                    case ColumnSortOrder.Descending:
                        return "Newest";
                    default:
                        return null;
                }
            }
            switch (order) {
                case ColumnSortOrder.Ascending:
                    return "Ascending";
                case ColumnSortOrder.Descending:
                    return "Descending";
                default:
                    return null;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class DateTimeFormatConverter : MarkupExtension, IValueConverter  {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(!(value is DateTime)) return string.Empty;
            var dateTime = (DateTime)value;
            return dateTime.ToString(DateTime.Now.Day == dateTime.Day ? "t" : "d");
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
