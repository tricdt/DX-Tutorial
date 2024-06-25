using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace BarsDemo {
    public static class DXImageHelper {
        public static ImageSource GetDXImage(string dxImageName) {
            if(dxImageName.EndsWith(".svg")) {
                return new DXImageExtension(dxImageName).ProvideValue(null) as ImageSource;
            } else {
                DXImageExtension extension = new DXImageExtension();
                extension.Image = new DXImageConverter().ConvertFromString(dxImageName) as DXImageInfo;
                return extension.ProvideValue(null) as ImageSource;
            }
        }
    }
    public class CommandTemplateSelector : DataTemplateSelector {
        public DataTemplate SubItemTemplate { get; set; }
        public DataTemplate ItemTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            if (item != null && item is BarButtonInfo) {
                if (item is GroupBarButtonInfo)
                    return SubItemTemplate;
                else
                    return ItemTemplate;
            }
            return null;
        }
    }
    public class DXImageConverterExtension : MarkupExtension, IValueConverter {
        public DXImageConverterExtension() { }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) { return DXImageHelper.GetDXImage((string)value); }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { throw new NotImplementedException(); }
        public override object ProvideValue(IServiceProvider serviceProvider) { return this; }
    }
}
