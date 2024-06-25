using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Data;
using System.Globalization;
using DevExpress.Diagram.Core;
using DevExpress.Diagram.Core.Layout;

namespace DiagramDemo.Utils {
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
    public class PositionXYToPointConverter : MarkupExtension, IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            var positions = Array.ConvertAll(values, o => System.Convert.ToDouble(o));
            return new Point(positions[0], positions[1]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            var point = (Point)value;
            return new object[] { (int)point.X, (int)point.Y };
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
