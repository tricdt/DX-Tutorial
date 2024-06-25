using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PropertyGridDemo.Utils {
    public class FormatWrapper {
        public FormatWrapper() { }
        public FormatWrapper(string name, string format) {
            FormatName = name;
            FormatString = format;
        }
        public string FormatName { get; set; }
        public string FormatString { get; set; }
    }
    public class BaseKindHelper<T> {
        public Array GetEnumMemberList() {
            return Enum.GetValues(typeof(T));
        }
    }
    public class ClickModeKindHelper : BaseKindHelper<ClickMode> { }
    public class TextWrappingKindHelper : BaseKindHelper<TextWrapping> { }
    public class ScrollBarVisibilityKindHelper : BaseKindHelper<ScrollBarVisibility> { }
    public class CharacterCasingKindHelper : BaseKindHelper<CharacterCasing> { }
    public class NullableToStringConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        string nullString = "Null";
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value == null)
                return nullString;
            return value.ToString();
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
    public class DecimalToConverter : MarkupExtension, IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            Type target = parameter as Type;
            if(target == null)
                return value;
            return System.Convert.ChangeType(value, target, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return System.Convert.ToDecimal(value);
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
    
    public class IConvertibleConverter : IValueConverter {
        public string ToType { get; set; }
        public string FromType { get; set; }

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            Type target = Type.GetType(ToType, false);
            if(target == null)
                return value;
            return System.Convert.ChangeType(value, target, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            Type target = Type.GetType(FromType, false);
            if (target == null)
                return value;
            return System.Convert.ChangeType(value, target, culture);
        }

        #endregion
    }

     public class ResourceStringToImageSourceConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            var valueString = value as String;
            if (String.IsNullOrEmpty(valueString)) return null;
            var uriPrefix = "pack://application:,,,/PropertyGridDemo;component/";
            var uri = new Uri(String.Format("{0}{1}", uriPrefix, valueString), UriKind.RelativeOrAbsolute);
            return new BitmapImage(uri);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
    }
     public class ResourceStringToImageSourceConverterExtension : MarkupExtension {
         static ResourceStringToImageSourceConverter instance;
         static ResourceStringToImageSourceConverterExtension() {
             instance = new ResourceStringToImageSourceConverter();
         }
         public ResourceStringToImageSourceConverterExtension() { }
         public override object ProvideValue(IServiceProvider serviceProvider) {
             return instance;
         }
     }
}
