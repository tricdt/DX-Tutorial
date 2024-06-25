using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.DataClasses;

namespace GridDemo {
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/DragDropViewModel.(cs)")]
    public partial class DragDrop : GridDemoModule {
        public DragDrop() {
            InitializeComponent();
        }

        void OnDragRecordOver(object sender, DragRecordOverEventArgs e) {
            if(e.IsFromOutside && typeof(Employee).IsAssignableFrom(e.GetRecordType()))
                e.Effects = System.Windows.DragDropEffects.Move;
            e.Handled = true;
        }
    }
}
