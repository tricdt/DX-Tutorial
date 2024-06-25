using DevExpress.Diagram.Core;
using DevExpress.Xpf.Diagram;
using System;
using System.Windows;
using System.Linq;
using DevExpress.Mvvm.Native;
using DevExpress.Diagram.Core.Native;

namespace DevExpress.Diagram.Demos {
    public static class OrgChartHelpers {
        readonly static DiagramItemStyleId[] styles = DiagramShapeStyleId.Styles.ToArray();
        public static DiagramItemStyleId GetStyleID(DiagramItem item) {
            return GetStyle(GetItemLevel(item));
        }
        public static DiagramItemStyleId GetStyle(int id) {
            switch(id % 7) {
                case 0:
                    return DiagramShapeStyleId.Balanced1;
                case 1:
                    return DiagramShapeStyleId.Balanced2;
                case 2:
                    return DiagramShapeStyleId.Balanced3;
                case 3:
                    return DiagramShapeStyleId.Balanced4;
                case 4:
                    return DiagramShapeStyleId.Balanced5;
                case 5:
                    return DiagramShapeStyleId.Balanced6;
                case 6:
                    return DiagramShapeStyleId.Balanced7;
            }
            return DiagramShapeStyleId.Variant1;
        }
        public static DiagramItemStyleId GetBrightStyle(this DiagramItemStyleId styleId) {
            var index = Array.IndexOf(styles, styleId);
            if(index == -1)
                return DiagramShapeStyleId.Variant1;
            return styles[index > 10 ? index - 7 : index];
        }
        public static DiagramItemStyleId GetPaleStyle(this DiagramItemStyleId styleId) {
            var index = Array.IndexOf(styles, styleId);
            if(index == -1)
                return DiagramShapeStyleId.Variant1;
            return styles[index < 39 ? index + 7 : index];
        }
        public static int GetItemLevel(DiagramItem item) {
            var level = 0;
            var parent = GetParent(item);
            while(parent != null) {
                level++;
                parent = GetParent(parent);
            }
            return level;
        }
        static DiagramItem GetParent(DiagramItem item) {
            var incomingConnector = item.IncomingConnectors.FirstOrDefault();
            return incomingConnector == null ? null : (DiagramItem)incomingConnector.BeginItem;
        }
    }
}
