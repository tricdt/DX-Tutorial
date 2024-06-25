using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Grid;

namespace GridDemo {
    [DevExpress.Xpf.DemoBase.CodeFile("Controls/SalesByEmployeeData.cs")]
    [DevExpress.Xpf.DemoBase.CodeFile("Controls/ColumnDescription.cs")]
    public partial class FixedBands : GridDemoModule {
        public FixedBands() {
            InitializeComponent();
        }
    }

    public class ColumnTemplateSelector : DataTemplateSelector {
        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            ColumnDescription column = (ColumnDescription)item;
            var grid = ((GridControlBand)container).View.DataControl;
            if (string.IsNullOrEmpty(column.StyleKey))
                return (DataTemplate)grid.FindResource("columnTemplate");
            return (DataTemplate)grid.FindResource(column.StyleKey);  
        }
    }
}
