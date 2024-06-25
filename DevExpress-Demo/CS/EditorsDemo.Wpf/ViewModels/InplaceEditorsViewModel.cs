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

namespace EditorsDemo {

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
    public class DescriptionAttachedBehavior : DependencyObject {
        static readonly DependencyProperty DescriptionProperty;

        static DescriptionAttachedBehavior() {
            DescriptionProperty = DependencyProperty.RegisterAttached("Description", typeof(object), typeof(DescriptionAttachedBehavior), new FrameworkPropertyMetadata(null, DescriptionChanged));
        }
        public static void SetDescription(DependencyObject d, object value) {
            d.SetValue(DescriptionProperty, value);
        }
        public static object GetDescription(DependencyObject d) {
            return d.GetValue(DescriptionProperty);
        }
        static void DescriptionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var rtb = d as RichTextBox;
            if (rtb == null || rtb.Document == null)
                return;
            rtb.Document.Blocks.Clear();
            var property = e.NewValue as PropertyDescriptor;
            if (property == null)
                return;
            ContentControl control = new ContentControl() { Template = rtb.FindResource(DynamicallyAssignDataEditorsTemplateSelector.GetTemplateName(property) + "Description") as ControlTemplate };
            control.ApplyTemplate();
            ParagraphContainer container = VisualTreeHelper.GetChild(control, 0) as ParagraphContainer;
            rtb.Document.Blocks.Add(container.Paragraph);
        }
    }
}
