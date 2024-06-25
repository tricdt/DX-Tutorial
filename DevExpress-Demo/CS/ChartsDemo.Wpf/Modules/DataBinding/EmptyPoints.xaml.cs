using System;
using System.Globalization;
using System.Windows.Media;
using System.Windows.Threading;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;

namespace ChartsDemo {
    [CodeFile("Modules/DataBinding/EmptyPoints.xaml"),
     CodeFile("Modules/DataBinding/EmptyPoints.xaml.(cs)"),
     CodeFile("Modules/SeriesInfo.(cs)"),
     CodeFile("DataModels/EmptyPointsDemoData.(cs)"),
     NoAutogeneratedCodeFiles]
    public partial class EmptyPointsDemo : ChartsDemoModule {
        public EmptyPointsDemo() {
            InitializeComponent();
        }
        void lbSeriesGroup_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            Dispatcher.BeginInvoke(new Action(this.chart.Animate), DispatcherPriority.Background);
        }
    }

    class EmptyPointBrushConverter : ForwardOnlyValueConverter {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value != null && value is Color) {
                Color color = (Color)value;
                SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(100, color.R, color.G, color.B));
                brush.Freeze();
                return brush;
            }
            return value;
        }
    }
}