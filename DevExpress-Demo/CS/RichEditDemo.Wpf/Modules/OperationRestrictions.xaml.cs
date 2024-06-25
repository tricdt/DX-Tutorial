using DevExpress.Xpf.PropertyGrid;

namespace RichEditDemo {
    public partial class OperationRestrictions : RichEditDemoModule {
        public OperationRestrictions() {
            InitializeComponent();
            this.propertyGridControl.SelectedObject = new RichEditOptionsProvider(this.richEdit.Options);
        }

        void PropertyGridControl_CellValueChanged(object sender, CellValueChangedEventArgs args) {
            if (string.IsNullOrEmpty(RichEditControl.DocumentSaveOptions.CurrentFileName))
                return;
            if (args.Row.FullPath.Contains("DocumentCapabilities"))
                RichEditControl.LoadDocument(RichEditControl.DocumentSaveOptions.CurrentFileName);
        }
    }
}
