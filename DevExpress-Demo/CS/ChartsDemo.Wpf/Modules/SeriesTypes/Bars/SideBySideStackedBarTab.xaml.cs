using System.Windows;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {
    public partial class SideBySideStackedBarTab : TabItemModule {
        public SideBySideStackedBarTab() {
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
            foreach (BarSideBySideStackedSeries2D series in this.chart.Diagram.Series) {
                GenderAgeInfo genderAge = (GenderAgeInfo)series.Tag;
                series.StackedGroup = this.lbeGroupBy.SelectedIndex == 0 ? genderAge.Gender : genderAge.Age;
                if ((string)series.StackedGroup == "65 years and older") {
                    series.StackedGroup = "65+ years";
                }
            }
        }
    }
}
