using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Data;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraScheduler;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Media;
using DevExpress.Xpf.Editors;
using System.Collections.Generic;
using GridDemo;
using DevExpress.Mvvm.UI.Interactivity;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Diagnostics;
using System.Reflection;
using DevExpress.Xpf.Core;
using System.Globalization;
using SchedulerDateTimeHelper = DevExpress.XtraScheduler.Native.DateTimeHelper;

namespace EditorsDemo.Utils {
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
    public static class ImageHelper {
        public static ImageSource GetSvgImage(string imageName) {
            var extension = new SvgImageSourceExtension() { Uri = new Uri(string.Format("pack://application:,,,/EditorsDemo;component/Images/{0}.svg", imageName)), Size = new System.Windows.Size(16, 16) };
            return (ImageSource)extension.ProvideValue(null);
        }
    }
    public class NullableToStringConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        string nullString = "Null";
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value == null)
                return nullString;
            return value.ToString();
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
    public class DecimalToConverter : MarkupExtension, IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            Type target = parameter as Type;
            if (target == null)
                return value;
            return System.Convert.ChangeType(value, target, culture);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
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

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            Type target = Type.GetType(ToType, false);
            if (target == null)
                return value;
            return System.Convert.ChangeType(value, target, culture);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            Type target = Type.GetType(FromType, false);
            if (target == null)
                return value;
            return System.Convert.ChangeType(value, target, culture);
        }

        #endregion
    }

    public class CategoryDataToImageSourceConverter : BytesToImageSourceConverter {
        static Dictionary<string, object> cachedImages = new Dictionary<string, object>();

        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            CategoryData categoryData = value as CategoryData;
            if (value == null) return null;
            if (cachedImages.ContainsKey(categoryData.Name)) {
                return cachedImages[categoryData.Name];
            }
            else {
                object image = base.Convert(categoryData.Picture, targetType, parameter, culture);
                cachedImages.Add(categoryData.Name, image);
                return image;
            }
        }
    }
    public class CategoryNameToImageSourceConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var category = (string)value;
            if(string.IsNullOrEmpty(category))
                return null;
            category = category.Replace(" ", "").Replace("/", "");
            return ImageHelper.GetSvgImage(@"Products/" + category);
        }
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
