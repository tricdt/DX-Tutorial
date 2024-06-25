using System.Windows;

namespace GridDemo {
    class RowTemplateSelectorItem : DependencyObject {
        public DataTemplate DetailTemplate {
            get { return (DataTemplate)GetValue(DetailTemplateProperty); }
            set { SetValue(DetailTemplateProperty, value); }
        }
        public static readonly DependencyProperty DetailTemplateProperty =
            DependencyProperty.Register("DetailTemplate", typeof(DataTemplate), typeof(RowTemplateSelectorItem), new PropertyMetadata(null));

        public Style RowStyle {
            get { return (Style)GetValue(RowStyleProperty); }
            set { SetValue(RowStyleProperty, value); }
        }
        public static readonly DependencyProperty RowStyleProperty =
            DependencyProperty.Register("RowStyle", typeof(Style), typeof(RowTemplateSelectorItem), new PropertyMetadata(null));

        public string DisplayName {
            get { return (string)GetValue(DisplayNameProperty); }
            set { SetValue(DisplayNameProperty, value); }
        }
        public static readonly DependencyProperty DisplayNameProperty =
            DependencyProperty.Register("DisplayName", typeof(string), typeof(RowTemplateSelectorItem), new PropertyMetadata(null));
    }
}
