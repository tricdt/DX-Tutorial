using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ProductsDemo {
    public class ViewTypeToBoolConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(targetType == typeof(bool) && value is PhotoGalleryViewType && parameter is PhotoGalleryViewType)
                return (PhotoGalleryViewType)value == (PhotoGalleryViewType)parameter;
            return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value is bool)
                if(targetType == typeof(PhotoGalleryViewType)) {
                    return (bool)value ? PhotoGalleryViewType.Gallery : PhotoGalleryViewType.Map;
                }
            return null;
        }
    }
    public class ViewTypeToVisibilityConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (targetType == typeof(Visibility) && value is PhotoGalleryViewType && parameter is PhotoGalleryViewType)
                return (PhotoGalleryViewType)value == (PhotoGalleryViewType)parameter ? Visibility.Visible : Visibility.Hidden;
            return Visibility.Hidden;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is Visibility)
                if (targetType == typeof(PhotoGalleryViewType)) {
                    return (Visibility)value == Visibility.Visible ? PhotoGalleryViewType.Gallery : PhotoGalleryViewType.Map;
                }
            return null;
        }
    }
    public enum PhotoGalleryViewType {
        Map,
        Gallery,
        Detail
    }
}
