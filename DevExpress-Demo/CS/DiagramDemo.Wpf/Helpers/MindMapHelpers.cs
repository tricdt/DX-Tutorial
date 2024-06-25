using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Markup;
using DevExpress.Xpf.Diagram;
using DevExpress.Diagram.Core;

namespace DevExpress.Diagram.Demos {
    static class MindMapHelpers {
        public static DiagramItemStyleId GetMindMapStyle(DiagramItem item, DiagramItem parent) {
            if(parent.IncomingConnectors.Count() == 0)
                return OrgChartHelpers.GetStyle(parent.OutgoingConnectors.Count() + 1).GetPaleStyle();
            return parent.ThemeStyleId.GetBrightStyle();
        }
        public static Size GetSize(int itemLevel) {
            switch(itemLevel) {
                case 0:
                    return new Size(310, 250);
                case 1:
                    return new Size(210, 140);
                case 2:
                    return new Size(190, 125);
            }
            return new Size(165, 110);
        }
        public static float GetFontSize(int itemLevel) {
            switch(itemLevel) {
                case 0:
                    return 32;
                case 1:
                    return 24;
                case 2:
                    return 18;
            }
            return 13.5f;
        }
    }
    public class SelectedItemsConverter : MarkupExtension, IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value != null && (value is DiagramShape);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
