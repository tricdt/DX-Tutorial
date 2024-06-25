using System.Collections;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DevExpress.Diagram.Core;
using DevExpress.Utils;

namespace DevExpress.Diagram.Demos {
    static class DemoHelper {
        public static DiagramStencil CreatePredefinedSvgStencil(string stencilId, string stencilName, bool visible = false) {
            DiagramStencil stencil = new DiagramStencil(stencilId, stencilName, visible);
            InitializePredefinedStencil(stencil);
            return stencil;
        }
        static void InitializePredefinedStencil(DiagramStencil stencil) {
            const string directoryPath = "images/officeplan";
            var assembly = typeof(DemoHelper).Assembly;
            var filePaths = AssemblyHelper.GetResources(assembly)
                .OfType<DictionaryEntry>()
                .Select(x => (string)x.Key)
                .Where(x => x.StartsWith(directoryPath))
                .OrderBy(x => x);
            foreach(var filePath in filePaths) {
                string fileName = Regex.Match(filePath.Replace("%20", " "), @"(?<=\/)[A-z0-9 ]*(?=\.svg)").Value;
                string shapeId = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(fileName);
                var stream = AssemblyHelper.GetResourceStream(assembly, filePath, true);
                var sd = ShapeDescription.CreateSvgShape(shapeId, shapeId, stream, false);
                stencil.RegisterShape(sd);
            }
        }
        public static void InitializeSvgShape(DiagramStencil stencil, IDiagramShape shape) {
            if(shape != null && stencil.ContainsShape(shape.Shape)) {
                shape.CanEdit = false;
            }
        }
        public static DiagramStencil CreateStencilFromFile(string fileName, string stencilId, string stencilName, bool visible = false) {
            DiagramStencil customShapesStencil = null;
            using(var stream = File.OpenRead(fileName)) {
                customShapesStencil = DiagramStencil.Create(stencilId, stencilName, stream, s => s, visible);
            }
            return customShapesStencil;
        }
        public static DiagramStencilCollection CreateExtendedStencilCollection(params DiagramStencil[] exStencils) {
            return new DiagramStencilCollection(exStencils.Concat(DiagramToolboxRegistrator.Stencils));
        }
    }
}
