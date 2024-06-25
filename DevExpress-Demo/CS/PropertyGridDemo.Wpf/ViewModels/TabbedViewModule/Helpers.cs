using DevExpress.Xpf.Charts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace PropertyGridDemo {
    public class StaticPropertiesExtension : MarkupExtension {
        public Type Type { get; set; }
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return Type.GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public).Select(x => x.GetValue(null, null));
        }
    }
    public class ColorToBrushConverterExtension : MarkupExtension, IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (!(value is Color))
                return null;
            return new SolidColorBrush((Color)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
    public class Bar2DKindToModelConverterExtension : MarkupExtension, IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Bar2DKind bar2DKind = value as Bar2DKind;
            if (bar2DKind != null)
                return Activator.CreateInstance(bar2DKind.Type);
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }    
}
