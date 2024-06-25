using System.Windows.Controls;
using DevExpress.Xpf.Core.FilteringUI;

namespace GridDemo.Filtering {
    public partial class FilterEditorGroupOperations : UserControl {
        public FilterEditorGroupOperations() {
            InitializeComponent();
        }

        #region !
        void OnQueryGroupOperations(object sender, QueryGroupOperationsEventArgs e) {
            e.AllowAddCustomExpression = false;
        }
        #endregion
    }
}
