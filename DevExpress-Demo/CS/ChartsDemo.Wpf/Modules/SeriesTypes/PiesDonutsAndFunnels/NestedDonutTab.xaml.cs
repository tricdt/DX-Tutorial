using System.Windows;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {
    public partial class NestedDonutTab : TabItemModule {
        public NestedDonutTab() {
            InitializeComponent();
        }

        void ChartControlBoundDataChanged(object sender, RoutedEventArgs e) {
            var seriesCollection = this.chart.Diagram.Series;
            if (seriesCollection.Count > 0) {
                foreach (NestedDonutSeries2D series in seriesCollection) {
                    series.ShowInLegend = false;
                    AgePopulation population = series.Points[0].Tag as AgePopulation;
                    if (population != null) series.Group = population.Name;
                }
                seriesCollection[0].ShowInLegend = true;
            }
        }
    }
}
