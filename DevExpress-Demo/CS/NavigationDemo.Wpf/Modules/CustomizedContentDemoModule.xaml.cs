using DevExpress.Xpf.DemoBase;

namespace NavigationDemo {
    [CodeFile("ViewModels/CustomizedContentViewModel.(cs)")]
    public partial class CustomizedContentDemoModule : AccordionDemoModule {
        public CustomizedContentDemoModule() {
            InitializeComponent();
        }
    }
    public enum StockIndicatorMode {
        Delta,
        DeltaChange
    }
}
