using System.Windows;

namespace GridDemo {
    public partial class GroupMerging : GridDemoModule {
        public GroupMerging() {
            InitializeComponent();
        }

        void OnGroupByOrderDateAndCountryThenCityMergedClick(object sender, RoutedEventArgs e) {
            grid.ClearGrouping();
            grid.GroupBy("OrderDate");
            GroupByCountryThenCity(true);
        }
        void OnGroupByCountryThenCityMergedClick(object sender, RoutedEventArgs e) {
            grid.ClearGrouping();
            GroupByCountryThenCity(true);
        }
        void OnGroupByCountryThenCityUnmergedClick(object sender, RoutedEventArgs e) {
            grid.ClearGrouping();
            GroupByCountryThenCity(false);
        }
        void OnClearGroupingClick(object sender, RoutedEventArgs e) {
            grid.ClearGrouping();
        }
        void GroupByCountryThenCity(bool groupMerge) {
            grid.GroupBy("Country");
            grid.GroupBy("City", groupMerge);
        }
    }
}
