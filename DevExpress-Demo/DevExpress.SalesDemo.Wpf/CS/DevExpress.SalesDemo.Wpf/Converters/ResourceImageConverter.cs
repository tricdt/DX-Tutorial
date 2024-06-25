using DevExpress.SalesDemo.Wpf.Helpers;
using System;
using System.Globalization;
using System.Windows.Data;

namespace DevExpress.SalesDemo.Wpf.Converters {
    public class ResourceImageConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string image = parameter as string ?? value as string;
            if(image == null)
                throw new NullReferenceException("parameter");
            return ResourceImageHelper.GetResourceImage(image);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
