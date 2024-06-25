using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace CommonDemo.TabControl.WebBrowser {
    public class UrlToImageConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value == null) return null;
            Uri baseUri = (Uri)value;
            return new BitmapImage(new Uri(baseUri.AbsoluteUri + "favicon.ico"));
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
