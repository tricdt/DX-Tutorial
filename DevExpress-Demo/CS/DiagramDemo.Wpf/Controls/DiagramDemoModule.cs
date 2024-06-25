using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using DevExpress.Diagram.Core;
using DevExpress.Internal;
using DevExpress.Utils;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Diagram;
using DevExpress.Xpf.Ribbon;
using DiagramDemo;

namespace DiagramDemo {
    public class DiagramDemoModule : DemoModule {
        protected virtual bool NeedChangeEditorsTheme { get { return false; } }
        protected virtual void LoadDemoData() {
        }
        protected override bool ShowApplicationButton() {
            var designerControl = this.FindName("diagramControl") as DiagramDesignerControl;
            var customRibbon = this.FindName("ribbonControl") as RibbonControl;
            return designerControl != null || (customRibbon != null && customRibbon.ApplicationMenu != null);
        }
    }
    public static class DiagramControlDemoExtensions {
        public static void LoadDemoData(this DiagramControl diagramControl, string dataSourceName) {
            diagramControl.LoadDocument(DiagramDemoHelper.GetDataFilePath(dataSourceName));
        }
    }
    public static class DiagramDemoHelper {
        public static string GetDataFilePath(string relativePath) {
            return DataDirectoryHelper.GetFile("Diagram\\" + relativePath, DataDirectoryHelper.DataFolderName);
        }
        public static Point GetCircleDiagramItemPosition(double radius, double phase, Point diagramCenter, Size itemSize) {
            var point = GetCartesianPointByPolarPoint(radius, phase);
            double offsetX = diagramCenter.X - itemSize.Width / 2d;
            double offsetY = diagramCenter.Y - itemSize.Height / 2d;
            point.Offset(offsetX, offsetY);
            return point;
        }
        public static Point GetCartesianPointByPolarPoint(double magnitude, double phase) {
            double x = magnitude * Math.Cos(phase);
            double y = magnitude * Math.Sin(phase);
            return new Point(x, y);
        }
        public static void LayoutCircleDiagramItems(DiagramItem[] items, Size pageSize, double radius) {
            double phase = -Math.PI / 2d;
            double phaseDelta = 2 * Math.PI / items.Length;
            double centerX = pageSize.Width / 2d;
            double centerY = pageSize.Height / 2d;
            Point center = new Point(centerX, centerY);
            foreach(var item in items) {
                item.Position = DiagramDemoHelper.GetCircleDiagramItemPosition(radius, phase, center, item.DesiredSize);
                phase += phaseDelta;
            }
        }
    }
    public class DiagramContentItemTool : MarkupExtension {
        public string ToolId { get; set; }
        public string ToolName { get; set; }
        public string CustomStyleId { get; set; }
        public Size DefaultSize { get; set; }
        public bool IsQuick { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return new FactoryItemTool(ToolId, () => ToolName, diagram => CreateItem(), DefaultSize, IsQuick);
        }
        protected virtual DiagramItem CreateItem() {
            return new DiagramContentItem { CustomStyleId = CustomStyleId };
        }
    }
}

namespace DevExpress.Diagram.Demos {
    public static class DiagramDemoFileHelper {
        public static Stream GetDataStream(string fileName) {
            string path = DiagramDemoHelper.GetDataFilePath(fileName);
            return File.OpenRead(path);
        }
        public static Stream GetResourceStream(string path) {
            var assembly = typeof(DiagramDemoFileHelper).Assembly;
            return AssemblyHelper.GetResourceStream(assembly, path, true);
        }
    }
}
