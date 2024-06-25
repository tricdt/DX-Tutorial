using System.Windows.Controls;
using DevExpress.Xpf.Core.FilteringUI;

namespace GridDemo.Filtering {
    public partial class FilterEditorCustomizeOperandValues : UserControl {
        public FilterEditorCustomizeOperandValues() {
            InitializeComponent();
        }

        #region !
        void FilterEditorControl_OnQueryOperands(object sender, QueryOperandsEventArgs e) {
            switch(e.FieldName) {
                case "RequiredDate":
                case "ShippedDate":
                    e.AllowPropertyOperand = true;
                    break;
            }
        }
        #endregion
    }
}
