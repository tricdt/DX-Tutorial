using DevExpress.Data.Filtering;
using DevExpress.DemoData;
using System.Linq;
using System.Windows.Controls;

namespace GridDemo.Filtering {
    public partial class RangeElementRangeControlTemplate : UserControl {
        public RangeElementRangeControlTemplate() {
            InitializeComponent();
            grid.ItemsSource = NWindDataProvider.Invoices.Where(x => x.Quantity < 20).ToList();
        }
    }
}
