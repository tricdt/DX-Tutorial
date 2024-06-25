using System;
using System.Collections;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using DevExpress.Xpf.DemoBase;
using DevExpress.Mvvm;
using DevExpress.Xpf.WindowsUI;
using DevExpress.DemoData.Models;

namespace ControlsDemo {
    public class ControlsDemoModule : DemoModule {
    }
    public class SourceConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
    public class UniversalContainerConverter<T> : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value is T ? GetContainer((T)value) : null;
        }
        protected virtual object GetContainer(T value) {
            return null;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return ((UniversalContainer<T>)value).Value;
        }
        #endregion
    }
    public class UniversalContainer<T> {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public T Value { get; set; }
        public override bool Equals(object obj) {
            return obj is UniversalContainer<T> ? object.Equals(Value, ((UniversalContainer<T>)obj).Value) : false;
        }
        public override int GetHashCode() {
            return Value.GetHashCode();
        }
    }
    public enum DockSide {
        Top,
        Bottom
    }
}
