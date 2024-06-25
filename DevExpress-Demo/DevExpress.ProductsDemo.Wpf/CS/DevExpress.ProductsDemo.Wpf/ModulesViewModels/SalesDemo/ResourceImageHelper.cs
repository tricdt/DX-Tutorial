using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ProductsDemo {
    public static class ResourceImageHelper {
        public static BitmapImage GetResourceImage(string image) {
            BitmapImage res = new BitmapImage();
            res.BeginInit();
            res.StreamSource = ModelAssemblyHelper.GetResourceStream(image);
            res.EndInit();
            res.Freeze();
            return res;
        }
    }
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
