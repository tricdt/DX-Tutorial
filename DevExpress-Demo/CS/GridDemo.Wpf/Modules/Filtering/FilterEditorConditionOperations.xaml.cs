using System.Windows.Controls;
using DevExpress.Xpf.Core.FilteringUI;

namespace GridDemo.Filtering {
    public partial class FilterEditorConditionOperations : UserControl {
        public FilterEditorConditionOperations() {
            InitializeComponent();
        }

        #region !
        void OnQueryConditionOperations(object sender, QueryConditionOperationsEventArgs e) {
            e.AllowRemoveCondition = false;
        }
        #endregion
    }
}
