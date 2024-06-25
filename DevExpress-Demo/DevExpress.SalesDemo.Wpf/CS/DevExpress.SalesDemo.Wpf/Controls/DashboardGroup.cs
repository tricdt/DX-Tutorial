using System.Windows;
using System.Windows.Controls;

namespace DevExpress.SalesDemo.Wpf.Controls {
    public class DashboardGroup : HeaderedContentControl {
        public static readonly DependencyProperty HeaderMarginProperty =
            DependencyProperty.Register("HeaderMargin", typeof(Thickness), typeof(DashboardGroup), new PropertyMetadata(new Thickness(0)));
        public Thickness HeaderMargin {
            get { return (Thickness)GetValue(HeaderMarginProperty); }
            set { SetValue(HeaderMarginProperty, value); }
        }
        public DashboardGroup() {
            DefaultStyleKey = typeof(DashboardGroup);
        }
    }
}
