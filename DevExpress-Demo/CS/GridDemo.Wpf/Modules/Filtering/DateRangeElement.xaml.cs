using DevExpress.Data.Filtering;
using System;
using System.Windows.Controls;

namespace GridDemo.Filtering {
    public partial class DateRangeElement : UserControl {
        public DateRangeElement() {
            InitializeComponent();
            grid.FilterCriteria = new OperandProperty("OrderDate") >= DateTime.Today.AddDays(-5)
                & new OperandProperty("OrderDate") < DateTime.Today.AddDays(1);
        }
    }
}
