using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/ChartElements/Legends.xaml"),
     CodeFile("Modules/ChartElements/Legends.xaml.(cs)"),
     CodeFile("DataModels/ResourceUsageData.(cs)"),
     NoAutogeneratedCodeFiles]
    public partial class LegendsDemo : ChartsDemoModule {
        public static readonly DependencyProperty ChartPaletteProperty = DependencyProperty.Register("ChartPalette", typeof(Palette), typeof(LegendsDemo), new PropertyMetadata(OnChartPaletteChanged));

        static void OnChartPaletteChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            LegendsDemo demo = d as LegendsDemo;
            if (demo != null)
                demo.ColorizeSeries();
        }

        public LegendsDemo() {
            InitializeComponent();
            Binding myBinding = new Binding("Palette");
            myBinding.Source = this.chart;
            myBinding.Mode = BindingMode.OneWay;
            SetBinding(ChartPaletteProperty, myBinding);
        }
        void ColorizeSeries() {
            SolidColorBrush brush = new SolidColorBrush(this.chart.Palette[0]);
            brush.Freeze();
            ((XYSeries)this.chart.Diagram.Series[3]).Brush = brush;
            brush = new SolidColorBrush(this.chart.Palette[1]);
            brush.Freeze();
            ((XYSeries)this.chart.Diagram.Series[4]).Brush = brush;
            brush = new SolidColorBrush(this.chart.Palette[2]);
            brush.Freeze();
            ((XYSeries)this.chart.Diagram.Series[5]).Brush = brush;
        }
    }

    enum LegendMode {
        Common,
        SeparateInsidePane,
        SeparateOutsidePane
    }
}
