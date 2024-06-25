using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.Reflection;

namespace DevExpress.VideoRent.Wpf.Helpers {
    public class InvertThicknessConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Thickness? tn = value as Thickness?;
            if(tn == null) return new Thickness(0.0);
            Thickness t = (Thickness)tn;
            return new Thickness(-t.Left, -t.Top, -t.Right, -t.Bottom);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
    public class IntToDecimalConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            int? i = value as int?;
            return i == null ? 0.0M : (decimal)(int)value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            decimal? d = value as decimal?;
            return d == null ? 0 : (int)(decimal)d;
        }
    }
    public class IntToDoubleConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            int? i = value as int?;
            return i == null ? 0.0 : (double)(int)value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            double? d = value as double?;
            return d == null ? 0 : (int)(double)d;
        }
    }
    public class AndMultiConverter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            bool ret = true;
            foreach(object obj in values) {
                bool? value = obj as bool?;
                if(value == null || !(bool)value) {
                    ret = false;
                    break;
                }
            }
            return ret;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
    public class SummConteverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            double? dn = value as double?;
            double d = dn == null ? 0.0 : (double)dn;
            double p = (double)parameter;
            return d + p;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
    public class TitleIntInfoConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string title = (string)parameter;
            int? intInfo = value as int?;
            return intInfo == null || intInfo == 0 ? title : string.Format("{0} ({1})", title, intInfo);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
    public class StringFormatMultiConverter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            return string.Format((string)parameter, values);
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
    public class StringFormatConverter : IValueConverter {
        public IValueConverter InnerConverter { get; set; }
        public object InnerConverterParameter { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(InnerConverter != null)
                value = InnerConverter.Convert(value, typeof(object), InnerConverterParameter, culture);
            if(value == null) return string.Empty;
            return string.Format((string)parameter, value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
    public class DateConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            DateTime? date = value as DateTime?;
            if(date == null) return string.Empty;
            return string.Format("{0:d}", date);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
    public class TimeConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value == null) return null;
            return ((TimeSpan)value).ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value == null) return null;
            return DateTime.Parse((string)value).TimeOfDay;
        }
    }
    public class EmptyConverter : IValueConverter {
        public int Id { get; set; }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value;
        }
    }
    public class BoolInverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return !(bool)value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return !(bool)value;
        }
    }
    public class BrushToColorConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            SolidColorBrush brush = value as SolidColorBrush;
            if(brush == null) return Colors.Transparent;
            return brush.Color;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
    public class NullContentToVisibilityConverter : IValueConverter {
        public bool Inverted { get; set; }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value == null) return Inverted ? Visibility.Visible : Visibility.Collapsed;
            if(value is string) {
                string row = (string)value;
                if(string.IsNullOrEmpty(row)) return Inverted ? Visibility.Visible : Visibility.Collapsed;
            }
            return Inverted ? Visibility.Collapsed : Visibility.Visible;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
    }
    public class NullContentToBoolConverter : IValueConverter {
        public bool Inverted { get; set; }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value == null) return Inverted;
            if(value is string) {
                string row = (string)value;
                if(string.IsNullOrEmpty(row)) return Inverted;
            }
            if(value is IList) {
                IList list = value as IList;
                if(list.Count == 0) return Inverted;
            }
            return !Inverted;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
    }
    public class DoubleToIntConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(targetType != typeof(int))
                throw new InvalidOperationException();
            return (int)((double)value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class SelectConverter : DependencyObject, IValueConverter {
        #region Dependency Properties
        public static readonly DependencyProperty Value0Property;
        public static readonly DependencyProperty Value1Property;
        public static readonly DependencyProperty DefaultValueProperty;
        static SelectConverter() {
            Type ownerType = typeof(SelectConverter);
            Value0Property = DependencyProperty.Register("Value0", typeof(object), ownerType, new PropertyMetadata(null));
            Value1Property = DependencyProperty.Register("Value1", typeof(object), ownerType, new PropertyMetadata(null));
            DefaultValueProperty = DependencyProperty.Register("DefaultValue", typeof(object), ownerType, new PropertyMetadata(null));
        }
        #endregion

        public object Key0 { get; set; }
        public object Key1 { get; set; }
        public object Value0 { get { return GetValue(Value0Property); } set { SetValue(Value0Property, value); } }
        public object Value1 { get { return GetValue(Value1Property); } set { SetValue(Value1Property, value); } }
        public object DefaultValue { get { return GetValue(DefaultValueProperty); } set { SetValue(DefaultValueProperty, value); } }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(Equal(Key0, value)) return Value0;
            if(Equal(Key1, value)) return Value1;
            return DefaultValue;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if(Equals(Value0, value)) return Key0;
            if(Equals(Value1, value)) return Key1;
            return DependencyProperty.UnsetValue;
        }
        public bool Equal(object key, object value) {
            if(key == null) return value == null;
            return key.Equals(value);
        }
    }
    public class BoolToAnyConverter : DependencyObject, IValueConverter {
        #region Dependency Properties
        public static readonly DependencyProperty TrueValueProperty;
        public static readonly DependencyProperty FalseValueProperty;
        static BoolToAnyConverter() {
            Type ownerType = typeof(BoolToAnyConverter);
            TrueValueProperty = DependencyProperty.Register("TrueValue", typeof(object), ownerType, new PropertyMetadata(null));
            FalseValueProperty = DependencyProperty.Register("FalseValue", typeof(object), ownerType, new PropertyMetadata(null));
        }
        #endregion

        public object TrueValue { get { return GetValue(TrueValueProperty); } set { SetValue(TrueValueProperty, value); } }
        public object FalseValue { get { return GetValue(FalseValueProperty); } set { SetValue(FalseValueProperty, value); } }
        public bool Invert { get; set; }
        public IValueConverter InnerConverter { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(InnerConverter != null)
                value = InnerConverter.Convert(value, typeof(bool), parameter, culture);
            bool? b = value as bool?;
            return (b != null && (bool)b) ^ Invert ? TrueValue : FalseValue;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if(TrueValue == null) return (value == null) ^ Invert;
            bool b = TrueValue.Equals(value) ^ Invert;
            return InnerConverter == null ? b : InnerConverter.ConvertBack(b, targetType, parameter, culture);
        }
    }
    public class AnyToBoolConverter : DependencyObject, IValueConverter {
        #region Dependency Properties
        public static readonly DependencyProperty TrueValueProperty;
        public static readonly DependencyProperty FalseValueProperty;
        static AnyToBoolConverter() {
            Type ownerType = typeof(AnyToBoolConverter);
            TrueValueProperty = DependencyProperty.Register("TrueValue", typeof(object), ownerType, new PropertyMetadata(null));
            FalseValueProperty = DependencyProperty.Register("FalseValue", typeof(object), ownerType, new PropertyMetadata(null));
        }
        #endregion

        public object TrueValue { get { return GetValue(TrueValueProperty); } set { SetValue(TrueValueProperty, value); } }
        public object FalseValue { get { return GetValue(FalseValueProperty); } set { SetValue(FalseValueProperty, value); } }
        public bool Invert { get; set; }
        public IValueConverter InnerConverter { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(TrueValue == null) return (value == null) ^ Invert;
            bool b = TrueValue.Equals(value) ^ Invert;
            return InnerConverter == null ? b : InnerConverter.Convert(b, targetType, parameter, culture);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if(InnerConverter != null)
                value = InnerConverter.ConvertBack(value, typeof(bool), parameter, culture);
            bool? b = value as bool?;
            return (b != null && (bool)b) ^ Invert ? TrueValue : FalseValue;
        }
    }
    public class BoolToVisibilityConverter : IValueConverter {
        public bool Invert { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            bool? b = value as bool?;
            return (b != null && (bool)b) ^ Invert ? Visibility.Visible : Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            Visibility? v = value as Visibility?;
            return (v != null && (Visibility)v == Visibility.Visible) ^ Invert;
        }
    }
    public class ImageSourceToImageConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            ImageSource imageSource = value as ImageSource;
            if(imageSource == null) return null;
            return new Image() { Source = imageSource };
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            Image image = value as Image;
            if(image == null) return null;
            return image.Source;
        }
    }
    public class StringEmptyToNullConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string s = value as string;
            if(string.IsNullOrEmpty(s)) return null;
            return s;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            string s = value as string;
            return s == null ? string.Empty : s;
        }
    }
    public class ContentToControlConverter : IValueConverter {
        public bool AllowEmptyContent { get; set; }

        public ContentToControlConverter() {
            AllowEmptyContent = false;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(!AllowEmptyContent && IsEmpty(value)) return null;
            DataTemplate contentTemplate = (DataTemplate)parameter;
            return new ContentPresenter() { Content = value, ContentTemplate = contentTemplate };
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
        static bool IsEmpty(object content) {
            if(content == null) return true;
            string s = content as string;
            if(s != null && s.Length == 0) return true;
            return false;
        }
    }
    public class StringSplitConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string v = value as string;
            if(v == null) return null;
            string s = (string)parameter;
            char c = s[0];
            int i = int.Parse(s.Substring(1));
            string[] parts = v.Split(c);
            return i < parts.Length ? parts[i] : string.Empty;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}
