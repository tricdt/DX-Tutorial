using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Core.FilteringUI;

namespace GridDemo.Filtering {
    public partial class ColumnOperatorTemplateSelector : UserControl {
        public ColumnOperatorTemplateSelector() {
            InitializeComponent();
        }

        #region !
        void OnExcelStyleFilterQueryOperators(object sender, ExcelStyleFilterElementQueryOperatorsEventArgs e) {
            if(e.FieldName == "Quantity") {
                var template = (DataTemplate)FindResource("ternaryTemplate");
                e.Operators[ExcelStyleFilterElementOperatorType.Between].OperandTemplate = template;
                e.Operators[ExcelStyleFilterElementOperatorType.NotBetween].OperandTemplate = template;
            }
        }
        #endregion
    }
}
