using DevExpress.Data.Filtering;
using System.Windows.Controls;

namespace GridDemo.Filtering {
    public partial class FilterPanel : UserControl {
        public FilterPanel() {
            InitializeComponent();
            grid.AddMRUFilter(CriteriaOperator.Parse("[ShipCity] = 'Bergamo'"));
        }
    }
}
