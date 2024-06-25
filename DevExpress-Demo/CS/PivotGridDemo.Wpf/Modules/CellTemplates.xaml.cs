namespace PivotGridDemo {
    public partial class CellTemplates : PivotGridDemoModule {
        public CellTemplates() {
            InitializeComponent();
        }
    }

    public enum CellTemplateType { None, ShareOnly, ValueAndShare }
}
