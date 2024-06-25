using System.Windows;

namespace ChartsDemo {
    [System.Windows.Markup.ContentProperty("Content")]
    public class ValueSelectorItem : DependencyObject {
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(ValueSelectorItem), new PropertyMetadata(null));
        public static readonly DependencyProperty DisplayNameProperty = DependencyProperty.Register("DisplayName", typeof(string), typeof(ValueSelectorItem), new PropertyMetadata(null));

        public object Content {
            get { return GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        public string DisplayName {
            get { return (string)GetValue(DisplayNameProperty); }
            set { SetValue(DisplayNameProperty, value); }
        }
    }
}
