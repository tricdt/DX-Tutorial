using System.Windows;
using System.Windows.Controls;

namespace DevExpress.SalesDemo.Wpf.View.Common {
    public partial class PerformanceButtonsView : UserControl {
        public static readonly DependencyProperty LastTimePeriodButtonVisibilityProperty =
            DependencyProperty.Register("LastTimePeriodButtonVisibility", typeof(Visibility), typeof(PerformanceButtonsView), new PropertyMetadata(Visibility.Visible));
        public static readonly DependencyProperty CurrentTimePeriodButtonVisibilityProperty =
            DependencyProperty.Register("CurrentTimePeriodButtonVisibility", typeof(Visibility), typeof(PerformanceButtonsView), new PropertyMetadata(Visibility.Visible));
        public Visibility LastTimePeriodButtonVisibility {
            get { return (Visibility)GetValue(LastTimePeriodButtonVisibilityProperty); }
            set { SetValue(LastTimePeriodButtonVisibilityProperty, value); }
        }
        public Visibility CurrentTimePeriodButtonVisibility {
            get { return (Visibility)GetValue(CurrentTimePeriodButtonVisibilityProperty); }
            set { SetValue(CurrentTimePeriodButtonVisibilityProperty, value); }
        }
        
        public PerformanceButtonsView() {
            InitializeComponent();
        }
    }
}
