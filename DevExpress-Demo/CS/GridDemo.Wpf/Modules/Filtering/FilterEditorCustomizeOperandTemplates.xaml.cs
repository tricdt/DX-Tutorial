using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Core.FilteringUI;

namespace GridDemo.Filtering {
    public partial class FilterEditorCustomizeOperandTemplates : UserControl {
        public FilterEditorCustomizeOperandTemplates() {
            InitializeComponent();
        }

        #region !
        void OnQueryOperators(object sender, FilterEditorQueryOperatorsEventArgs e) {
            if(e.FieldName == "Quantity") {
                var template = (DataTemplate)FindResource("ternaryTemplate");
                e.Operators[FilterEditorOperatorType.Between].OperandTemplate = template;
                e.Operators[FilterEditorOperatorType.NotBetween].OperandTemplate = template;
            }
        }
        #endregion
    }
}
