using System.Windows.Controls;

namespace EditorsDemo.FilterBehavior {

    public partial class FilterBehaviorFilteringPanel : UserControl {
        public FilterBehaviorFilteringPanel() {
            InitializeComponent();
            chart.DataSource = new DevAVBranchesSales().GetList();
        }
    }
}
