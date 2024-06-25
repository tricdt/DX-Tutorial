using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using DevExpress.Xpf.Core.Internal;

namespace NavigationDemo.Utils {
    public class CollectionConverter<T> : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value == null)
                return Enumerable.Empty<T>();
            return ((List<object>)value).Cast<T>().ToList();
        }
    }
    public class Int32CollectionConverter : CollectionConverter<int> { }
    public class Int64CollectionConverter : CollectionConverter<long> { }
    public class DeltaValueToImageSourceConverter : MarkupExtension, IMultiValueConverter {
        [TypeConverter(typeof(SvgImageSourceConverter))]
        public ImageSource NegativeImageSource { get; set; }
        [TypeConverter(typeof(SvgImageSourceConverter))]
        public ImageSource PositiveImageSource { get; set; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            var delta = (double)values[0];
            var deltaChange = (int)values[1];
            var mode = (StockIndicatorMode)values[2];
            if((mode == StockIndicatorMode.Delta && delta < 0) || (mode == StockIndicatorMode.DeltaChange && deltaChange < 0)) return NegativeImageSource;
            if((mode == StockIndicatorMode.Delta && delta > 0) || (mode == StockIndicatorMode.DeltaChange && deltaChange > 0)) return PositiveImageSource;
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
    public class FilterStringConverter : MarkupExtension, IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var item = (FilterItem)value;
            return item == null ? string.Empty : item.FilterString;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
    public class NavigationWidthConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        public double StartupWidth { get; set; }
        bool isExpanded = true;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            isExpanded = (bool)value;
            return isExpanded ? new GridLength(StartupWidth) : GridLength.Auto;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(isExpanded)
                StartupWidth = ((GridLength)value).Value;
            return Binding.DoNothing;
        }

    }
}
