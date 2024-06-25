using DevExpress.Diagram.Core;
using DevExpress.Diagram.Demos;

namespace DiagramDemo {
    public partial class FloorPlanModule : DiagramDemoModule {
        public FloorPlanModule() {
            InitializeComponent();
            InitializeStencils();
            diagramControl.DocumentSource = DiagramDemoHelper.GetDataFilePath("OfficePlan.xml");
        }
        DiagramStencil homeObjectsStencil;

        void InitializeStencils() {
            this.homeObjectsStencil = DemoHelper.CreatePredefinedSvgStencil("HomeObjects", "Home Objects", true);
            diagramControl.Stencils = DemoHelper.CreateExtendedStencilCollection(homeObjectsStencil);
        }
        void diagramControl_ItemInitializing(object sender, DevExpress.Xpf.Diagram.DiagramItemInitializingEventArgs e) {
            DemoHelper.InitializeSvgShape(homeObjectsStencil, e.Item as IDiagramShape);
        }
    }
}
