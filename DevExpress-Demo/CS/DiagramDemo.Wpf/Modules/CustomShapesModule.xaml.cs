using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Diagram.Core;
using DevExpress.Diagram.Demos;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Diagram;

namespace DiagramDemo {
    [CodeFile("Resources/ContentItemsStyles.xaml")]
    public partial class CustomShapesModule : DiagramDemoModule {
        public CustomShapesModule() {
            InitializeComponent();
            InitializeStencils();
            diagramControl.DocumentLoaded += (o, e) => {
                diagramControl.Items
                .Where(item => item.Tag != null)
                .ForEach(item => item.ToolTip = new TextBlock { Text = item.Tag.ToString(), TextAlignment = TextAlignment.Left });
            };
            diagramControl.DocumentSource = DiagramDemoHelper.GetDataFilePath("CustomShapesDocument_WPF.xml");
        }
        DiagramStencil svgStencil;
        DiagramStencil customShapesStencil;
        DiagramStencil contentItemsStencil;

        void InitializeStencils() {
            this.customShapesStencil = DemoHelper.CreateStencilFromFile(DiagramDemoHelper.GetDataFilePath("CustomShapes.xml"), "CustomShapes", "Custom Shapes");
            this.svgStencil = DemoHelper.CreatePredefinedSvgStencil("SvgShapes", "Svg Shapes");
            this.contentItemsStencil = CreateContentItemsStencil();
            this.diagramControl.Stencils = DemoHelper.CreateExtendedStencilCollection(svgStencil, customShapesStencil);
        }
        DiagramStencil CreateContentItemsStencil() {
            DiagramStencil stencil = new DiagramStencil("CustomTools", "Content Item Tools", true);
            stencil.RegisterTool(new FactoryItemTool("Text", () => "Text", diagram => new DiagramContentItem() { CustomStyleId = "formattedTextContentItem" }, new Size(230, 110), true));
            stencil.RegisterTool(new FactoryItemTool("Action", () => "Button", diagram => new DiagramContentItem() { CustomStyleId = "buttonContentItem", }, new Size(230, 80), true));
            return stencil;
        }

        void diagramControl_ItemInitializing(object sender, DiagramItemInitializingEventArgs e) {
            DemoHelper.InitializeSvgShape(svgStencil, e.Item as IDiagramShape);
        }
    }
}
