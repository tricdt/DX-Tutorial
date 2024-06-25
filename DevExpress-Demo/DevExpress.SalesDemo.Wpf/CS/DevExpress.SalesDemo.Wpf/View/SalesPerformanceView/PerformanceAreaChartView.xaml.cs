using DevExpress.Xpf.Charts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DevExpress.SalesDemo.Wpf.View {
    public partial class PerformanceAreaChartView : UserControl {
        public static readonly DependencyProperty DateBorderMarginProperty =
            DependencyProperty.Register("DateBorderMargin", typeof(Thickness), typeof(PerformanceAreaChartView), new PropertyMetadata(new Thickness()));
        public static readonly DependencyProperty SalesVolumesMarginProperty =
            DependencyProperty.Register("SalesVolumesMargin", typeof(Thickness), typeof(PerformanceAreaChartView), new PropertyMetadata(new Thickness()));
        public static readonly DependencyProperty AreaAndSalesVolumesBrushProperty =
            DependencyProperty.Register("AreaAndSalesVolumesBrush", typeof(SolidColorBrush), typeof(PerformanceAreaChartView), new PropertyMetadata(Brushes.Red));
        public static readonly DependencyProperty ButtonsGridMarginProperty =
            DependencyProperty.Register("ButtonsGridMargin", typeof(Thickness), typeof(PerformanceAreaChartView), new PropertyMetadata(new Thickness(0)));
        public static readonly DependencyProperty AxisXMinorCountProperty =
            DependencyProperty.Register("AxisXMinorCount", typeof(int), typeof(PerformanceAreaChartView), new PropertyMetadata(1));
        public static readonly DependencyProperty AxisXGridSpacingProperty =
            DependencyProperty.Register("AxisXGridSpacing", typeof(double), typeof(PerformanceAreaChartView), new PropertyMetadata(1d));
        public static readonly DependencyProperty AreaSeriesCrosshairLabelPatternProperty =
            DependencyProperty.Register("AreaSeriesCrosshairLabelPattern", typeof(string), typeof(PerformanceAreaChartView), new PropertyMetadata(""));
        public static readonly DependencyProperty DateTimeGridAlignmentProperty =
            DependencyProperty.Register("DateTimeGridAlignment", typeof(DateTimeGridAlignment), typeof(PerformanceAreaChartView), new PropertyMetadata(DateTimeGridAlignment.Day));
        public static readonly DependencyProperty DateTimeMeasureUnitProperty =
            DependencyProperty.Register("DateTimeMeasureUnit", typeof(DateTimeMeasureUnit), typeof(PerformanceAreaChartView), new PropertyMetadata(DateTimeMeasureUnit.Hour));

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
        public double AxisXGridSpacing {
            get { return (double)GetValue(AxisXGridSpacingProperty); }
            set { SetValue(AxisXGridSpacingProperty, value); }
        }
        public string AreaSeriesCrosshairLabelPattern {
            get { return (string)GetValue(AreaSeriesCrosshairLabelPatternProperty); }
            set { SetValue(AreaSeriesCrosshairLabelPatternProperty, value); }
        }
        public DateTimeGridAlignment DateTimeGridAlignment {
            get { return (DateTimeGridAlignment)GetValue(DateTimeGridAlignmentProperty); }
            set { SetValue(DateTimeGridAlignmentProperty, value); }
        }
        public DateTimeMeasureUnit DateTimeMeasureUnit {
            get { return (DateTimeMeasureUnit)GetValue(DateTimeMeasureUnitProperty); }
            set { SetValue(DateTimeMeasureUnitProperty, value); }
        }

        public PerformanceAreaChartView() {
            InitializeComponent();
        }
    }
}
