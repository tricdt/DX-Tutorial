using System.Windows;

namespace MapDemo {
    public enum ProviderName {
        Bing,
        Osm,
        None
    }
    public class CopyrightControl : VisibleControl {
        public static readonly DependencyProperty ProviderNameProperty = DependencyProperty.Register("ProviderName",
            typeof(ProviderName), typeof(CopyrightControl), new PropertyMetadata(ProviderName.None));

        public ProviderName ProviderName {
            get { return (ProviderName)GetValue(ProviderNameProperty); }
            set { SetValue(ProviderNameProperty, value); }
        }

        public CopyrightControl() {
            DefaultStyleKey = typeof(CopyrightControl);
        }
    }
}
