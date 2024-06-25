using System.Windows;
using DevExpress.Xpf.Diagram;
using DevExpress.Diagram.Core;
using DevExpress.Diagram.Demos;
using DevExpress.Diagram.Core.Routing;

namespace DiagramDemo {
    public partial class MindMapTreeLayoutModule : LayoutModuleBase {
        protected override DiagramControl Diagram { get { return diagramControl; } }

        public MindMapTreeLayoutModule() {
            InitializeComponent();
            diagramControl.LoadDemoData("MindMapTreeLayoutDiagram.xml");
        }
        protected override void RelayoutDiagramCore() {
            diagramControl.ApplyMindMapTreeLayout();
        }
        void CreateChild(object sender, RoutedEventArgs e) {
            var parent = diagramControl.PrimarySelection as DiagramItem;
            var size = MindMapHelpers.GetSize(OrgChartHelpers.GetItemLevel(parent) + 1);
            var child = new DiagramShape() { Shape = BasicShapes.Ellipse, Width = size.Width, Height = size.Height, FontSize = MindMapHelpers.GetFontSize(OrgChartHelpers.GetItemLevel(parent) + 1), Content = "New Item" };
            child.ThemeStyleId = MindMapHelpers.GetMindMapStyle(child, parent);
            diagramControl.Items.Add(child);
            diagramControl.Items.Add(new DiagramConnector() { Type = ConnectorType.Curved, ThemeStyleId = child.ThemeStyleId, StrokeThickness = 3, BeginItem = parent, EndItem = child });
            RelayoutDiagramCore();
        }
    }
}
