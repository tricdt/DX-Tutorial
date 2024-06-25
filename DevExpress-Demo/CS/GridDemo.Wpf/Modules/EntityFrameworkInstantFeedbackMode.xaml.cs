namespace GridDemo {
#if !DXCORE3
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/InstantFeedbackModeViewModelBase.(cs)")]
    [DevExpress.Xpf.DemoBase.CodeFile("Modules/EntityFrameworkServerMode/EntityFrameworkInstantFeedbackModeViewModel.(cs)")]
    [DevExpress.Xpf.DemoBase.CodeFile("Modules/EntityFrameworkServerMode/OutlookDataContext.(cs)")]
    public partial class EntityFrameworkInstantFeedbackMode : GridDemoModule {
#else
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/InstantFeedbackModeViewModelBase.(cs)")]
    [DevExpress.Xpf.DemoBase.CodeFile("Modules/EntityFrameworkServerMode/EntityFrameworkInstantFeedbackModeViewModel.(cs)")]
    [DevExpress.Xpf.DemoBase.CodeFile("Modules/EntityFrameworkServerMode/OutlookDataContextNetCore.(cs)")]
    public partial class EntityFrameworkInstantFeedbackMode : GridDemoModule {
#endif
        public EntityFrameworkInstantFeedbackMode() {
            InitializeComponent();
            ModuleUnloaded += (s, e) => {
                grid.ItemsSource = null;
                instantFeedbackDataSource.Dispose();
            };
        }
    }
}
