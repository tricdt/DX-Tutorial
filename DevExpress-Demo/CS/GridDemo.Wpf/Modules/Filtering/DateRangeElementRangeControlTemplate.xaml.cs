using DevExpress.Data.Filtering;
using DevExpress.DemoData;
using System;
using System.Linq;
using System.Windows.Controls;

namespace GridDemo.Filtering {
    public partial class DateRangeElementRangeControlTemplate : UserControl {
        public DateRangeElementRangeControlTemplate() {
            InitializeComponent();
            grid.ItemsSource = NWindDataProvider.InvoicesUpToDate.Where(x => x.OrderDate.HasValue && x.OrderDate.Value >= DateTime.Today.AddDays(-20));
            grid.FilterCriteria = new OperandProperty("OrderDate") >= DateTime.Today.AddDays(-5)
                & new OperandProperty("OrderDate") < DateTime.Today.AddDays(1);
        }
    }
}
