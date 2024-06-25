using System.Windows;

namespace GridDemo {
    public partial class Grouping : GridDemoModule {
        public Grouping() {
            InitializeComponent();
            GroupByCountryThenCity();
        }
        void OnGroupByCountryThenCityClick(object sender, RoutedEventArgs e) {
            GroupByCountryThenCity();
        }
        void OnGroupByCountryCityOrderDateClick(object sender, RoutedEventArgs e) {
            GroupByCountryThenCity();
            grid.GroupBy("OrderDate");
        }
        void OnGroupByCityThenOrderDateClick(object sender, RoutedEventArgs e) {
            grid.ClearGrouping();
            grid.GroupBy("City");
            grid.GroupBy("OrderDate");
        }
        void OnClearGroupingClick(object sender, RoutedEventArgs e) {
            grid.ClearGrouping();
        }
        void GroupByCountryThenCity() {
            grid.ClearGrouping();
            grid.GroupBy("Country");
            grid.GroupBy("City");
        }
    }
}
