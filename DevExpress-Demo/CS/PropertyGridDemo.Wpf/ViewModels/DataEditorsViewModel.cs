using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.DemosHelpers.Grid;
using DevExpress.Xpf.Editors;
using System.ComponentModel.DataAnnotations;
using DevExpress.Data.Helpers;
using System.Windows.Data;
using DevExpress.Mvvm.DataAnnotations;

namespace PropertyGridDemo {

    public class DataEditorsViewModel {
        DynamicallyAssignDataEditorsData data;
        public DynamicallyAssignDataEditorsData Data {
            get {
                if (data == null)
                    data = new DynamicallyAssignDataEditorsData();
                return data;
            }
        }
        public IEnumerable<PropertyDescriptor> Properties { get { return TypeDescriptor.GetProperties(data).Cast<PropertyDescriptor>(); } }
        public static readonly string[] PalleteSizesStatic = new string[] { "4x4", "10x10", "16x16", "20x20", "25x25" };
        public string[] PalleteSizes { get { return PalleteSizesStatic; } }
    }
    public class DynamicallyAssignDataEditorsTemplateSelector : DataTemplateSelector {
        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            FrameworkElement element = (FrameworkElement)container;
            DataTemplate resource = element.TryFindResource(GetTemplateName((PropertyDescriptor)item)) as DataTemplate;
            return resource ?? base.SelectTemplate(item, container);
        }
        public static string GetTemplateName(PropertyDescriptor property) {
            var displayAttribute = (DisplayAttribute)property.Attributes[typeof(DisplayAttribute)];
            return displayAttribute.GetDescription() ?? displayAttribute.GetName();
        }
    }
    public class PropertyDescriptorToDisplayNameConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            var property = (PropertyDescriptor)value;
            var displayAttribute = (DisplayAttribute)property.Attributes[typeof(DisplayAttribute)];
            return displayAttribute.GetName();
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
