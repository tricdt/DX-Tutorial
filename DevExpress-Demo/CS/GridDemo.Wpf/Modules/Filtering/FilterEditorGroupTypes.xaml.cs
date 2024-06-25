using System.Windows.Controls;
using DevExpress.Xpf.Core.FilteringUI;

namespace GridDemo.Filtering {
    public partial class FilterEditorGroupTypes : UserControl {
        public FilterEditorGroupTypes() {
            InitializeComponent();
        }

        #region !
        void OnQueryGroupTypes(object sender, QueryGroupTypesEventArgs e) {
            e.AllowNotOr = false;
            e.AllowOr = false;
        }
        #endregion
    }
}
