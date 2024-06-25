using DevExpress.Xpf.Diagram;
using System;
using System.Linq;

namespace DiagramDemo {
    public partial class OrgChartLayoutModule : LayoutModuleBase {
        protected override DiagramControl Diagram { get { return diagramControl; } }
        public OrgChartLayoutModule() {
            InitializeComponent();
            diagramControl.LoadDemoData("OrgChartLayoutDiagram.xml");
        }
        protected override void RelayoutDiagramCore() {
            diagramControl.ApplyOrgChartLayout();
        }
    }
}
