using System.Windows;
using DevExpress.Xpf.Editors;

namespace LayoutControlDemo {
    public partial class pageFlowLayoutControl : LayoutControlDemoModule {
        public pageFlowLayoutControl() {
            InitializeComponent();
        }
        void StretchContentCheckBox_Checked(object sender, RoutedEventArgs e) {
            foreach (var child in layoutItems.GetLogicalChildren(false))
                child.Width = double.NaN;
        }
        void AllowLayerSizing_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            layoutItems.ShowLayerSeparators = (bool)e.NewValue;
            layoutItems.AllowLayerSizing = (bool)e.NewValue;
        }
    }
}
