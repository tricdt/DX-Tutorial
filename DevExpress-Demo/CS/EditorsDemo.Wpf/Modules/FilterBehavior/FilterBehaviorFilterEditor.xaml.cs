using System.Windows.Controls;

namespace EditorsDemo.FilterBehavior {

    public partial class FilterBehaviorFilterEditor : UserControl {
        public FilterBehaviorFilterEditor() {
            InitializeComponent();
            chart.DataSource = new DevAVBranchesSales().GetList();
        }
    }
}
