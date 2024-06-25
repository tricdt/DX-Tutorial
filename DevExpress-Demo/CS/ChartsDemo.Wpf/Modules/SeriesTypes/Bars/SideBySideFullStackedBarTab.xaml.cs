using System.Windows;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {
    public partial class SideBySideFullStackedBarTab : TabItemModule {
        public SideBySideFullStackedBarTab() {
            InitializeComponent();
        }
        void chart_BoundDataChanged(object sender, RoutedEventArgs e) {
            GroupSeries();
        }
        void lbeGroupBy_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            GroupSeries();
            this.chart.Animate();
        }
        void GroupSeries() {
            foreach (BarSideBySideFullStackedSeries2D series in this.chart.Diagram.Series) {
                GenderAgeInfo genderAge = (GenderAgeInfo)series.Tag;
                series.StackedGroup = this.lbeGroupBy.SelectedIndex == 0 ? genderAge.Gender : genderAge.Age;
            }
        }
    }
}
