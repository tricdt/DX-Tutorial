using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;

namespace NavigationDemo {
    public class SolutionNodeImageSelector : TreeListNodeImageSelector {
        static SolutionNodeImageSelector() {
            ImageCache = new Dictionary<TypeNode, ImageSource>();
        }
        static readonly Dictionary<TypeNode, ImageSource> ImageCache;
        public override ImageSource Select(DevExpress.Xpf.Grid.TreeList.TreeListRowData rowData) {
            SolutionNode solutionNode = (rowData.Row as SolutionNode);
            if(solutionNode == null)
                return null;
            return GetImageByTypeNode(solutionNode.TypeNode);
        }
        public static ImageSource GetImageByTypeNode(TypeNode typeNode) {
            if(ImageCache.ContainsKey(typeNode))
                return ImageCache[typeNode];
            var extension = new SvgImageSourceExtension() { Uri = new Uri(GetImagePathByTypeNode(typeNode)), Size = new Size(16, 16) };
            var image = (ImageSource)extension.ProvideValue(null);
            ImageCache.Add(typeNode, image);
            return image;
        }
        public static string GetImagePathByTypeNode(TypeNode typeNode) {
            return "pack://application:,,,/NavigationDemo;component/Images/SolutionExplorer/" + typeNode.ToString() + ".svg";
        }
    }
}
