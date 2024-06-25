using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DevExpress.SalesDemo.Wpf.View {
    public partial class PerformanceBarChartView : UserControl {
        public static readonly DependencyProperty DateBorderMarginProperty =
            DependencyProperty.Register("DateBorderMargin", typeof(Thickness), typeof(PerformanceBarChartView), new PropertyMetadata(new Thickness()));
        public static readonly DependencyProperty SalesVolumesMarginProperty =
            DependencyProperty.Register("SalesVolumesMargin", typeof(Thickness), typeof(PerformanceBarChartView), new PropertyMetadata(new Thickness()));
        public static readonly DependencyProperty AreaAndSalesVolumesBrushProperty =
            DependencyProperty.Register("AreaAndSalesVolumesBrush", typeof(SolidColorBrush), typeof(PerformanceBarChartView), new PropertyMetadata(Brushes.Red));
        public static readonly DependencyProperty ButtonsGridMarginProperty =
            DependencyProperty.Register("ButtonsGridMargin", typeof(Thickness), typeof(PerformanceBarChartView), new PropertyMetadata(new Thickness(0)));
        public static readonly DependencyProperty AxisXMinorCountProperty =
          DependencyProperty.Register("AxisXMinorCount", typeof(int), typeof(PerformanceBarChartView), new PropertyMetadata(1));

        public Thickness DateBorderMargin {
            get { return (Thickness)GetValue(DateBorderMarginProperty); }
            set { SetValue(DateBorderMarginProperty, value); }
        }
        public Thickness SalesVolumesMargin {
            get { return (Thickness)GetValue(SalesVolumesMarginProperty); }
            set { SetValue(DateBorderMarginProperty, value); }
        }
        public SolidColorBrush AreaAndSalesVolumesBrush {
            get { return (SolidColorBrush)GetValue(AreaAndSalesVolumesBrushProperty); }
            set { SetValue(AreaAndSalesVolumesBrushProperty, value); }
        }
        public Thickness ButtonsGridMargin {
            get { return (Thickness)GetValue(ButtonsGridMarginProperty); }
            set { SetValue(ButtonsGridMarginProperty, value); }
        }
        public int AxisXMinorCount {
            get { return (int)GetValue(AxisXMinorCountProperty); }
            set { SetValue(AxisXMinorCountProperty, value); }
        }

        public PerformanceBarChartView() {
            InitializeComponent();
        }
    }
}
