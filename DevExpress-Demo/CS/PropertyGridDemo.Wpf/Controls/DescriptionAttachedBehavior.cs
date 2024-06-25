using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DevExpress.Xpf.DemoBase.DemosHelpers.Grid;

namespace PropertyGridDemo {
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
            if(rtb == null || rtb.Document == null)
                return;
            rtb.Document.Blocks.Clear();
            var property = e.NewValue as PropertyDescriptor;
            if(property == null)
                return;
            ContentControl control = new ContentControl() { Template = rtb.FindResource(DynamicallyAssignDataEditorsTemplateSelector.GetTemplateName(property) + "Description") as ControlTemplate };
            control.ApplyTemplate();
            ParagraphContainer container = VisualTreeHelper.GetChild(control, 0) as ParagraphContainer;
            rtb.Document.Blocks.Add(container.Paragraph);
        }
    }
}
