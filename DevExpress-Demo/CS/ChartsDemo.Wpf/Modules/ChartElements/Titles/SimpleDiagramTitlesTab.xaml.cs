using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using DevExpress.Xpf.Charts;
using DevExpress.XtraPrinting.Native;

namespace ChartsDemo {
    public partial class SimpleDiagramTitlesTab : TabItemModule {
        public SimpleDiagramTitlesTab() {
            InitializeComponent();
        }

        void chart_BoundDataChanged(object sender, RoutedEventArgs e) {
            foreach (PieSeries2D series in this.chart.Diagram.Series)
                series.Titles[0].Content = series.DisplayName;
            if (this.chart.Diagram.Series.Count > 0)
                this.chart.Diagram.Series[0].ShowInLegend = true;
        }
        void Hyperlink_Click(object sender, RoutedEventArgs e) {
            Hyperlink source = sender as Hyperlink;
            if (source != null) {
                ProcessLaunchHelper.StartConfirmed(source.NavigateUri.ToString());
            }
        }
    }
}
