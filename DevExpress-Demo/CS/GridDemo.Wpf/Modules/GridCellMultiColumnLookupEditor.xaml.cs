using DevExpress.Xpf.Grid.LookUp;
using System;
using System.Windows.Threading;

namespace GridDemo {
    
    
    
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/GridCellMultiColumnLookupEditorResources.xaml")]
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/GridCellMultiColumnLookupEditorClasses.(cs)")]
    public partial class GridCellMultiColumnLookupEditor : GridDemoModule {
        public GridCellMultiColumnLookupEditor() {
            InitializeComponent();
        }
        protected override void Show() {
            base.Show();
            ShowLookUp();
        }
        protected override void Clear() {
            view.CloseEditor();
            base.Clear();
        }
        void ShowLookUp() {
            grid.CurrentColumn = grid.Columns["CustomerID"];
            view.ShowEditor();
            Dispatcher.BeginInvoke(new Action(() => {
                LookUpEdit lookUpEdit = view.ActiveEditor as LookUpEdit;
                if(lookUpEdit != null)
                    lookUpEdit.ShowPopup();
            }), DispatcherPriority.ApplicationIdle);
        }
    }
}
