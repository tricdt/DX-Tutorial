using DevExpress.Xpf.Charts.Sankey;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SankeyDemo {
    public partial class CustomNodeOrder : SankeyDemoModule {
        public CustomNodeOrder() {
            InitializeComponent();
        }
    }

    public class CustomNodeOrderDemoToolTipHeaderConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value is SankeyNode) {
                SankeyNode node = (SankeyNode)value;
                string prefix = "";
                switch(node.Level) {
                    case 0:
                        prefix = "Company";
                        break;
                    case 1:
                        prefix = "Product Category";
                        break;
                    case 2:
                        prefix = "Ship mode";
                        break;
                    case 3:
                        prefix = "Customer Country";
                        break;
                }
                return prefix + ": " + node.Tag.ToString();
            } else if(value is SankeyLink)
                return ((SankeyLink)value).SourceNode.Tag.ToString() + " → " + ((SankeyLink)value).TargetNode.Tag.ToString();
            return string.Empty;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class CustomNodeOrderDemoToolTipContentConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string text = "";
            NumberFormatInfo format = CultureInfo.InvariantCulture.NumberFormat;
            if(value is SankeyNode) {
                SankeyNode node = (SankeyNode)value;
                text = "$" + node.TotalWeight.ToString("#,0.00", format);
            } else if(value is SankeyLink) {
                SankeyLink link = (SankeyLink)value;
                text = "$" + link.TotalWeight.ToString("#,0.00", format);
            }
            return text;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class NodeComparerConverter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            string comparerName = values[0].ToString();
            AscendingOrDescendingNodeComparer comparer;
            if(comparerName == "Total Weight")
                comparer = new TotalWeightComparer();
            else if(comparerName == "Output Link Count")
                comparer = new OutputLinkCountComparer();
            else
                comparer = new NodeNameComparer();
            comparer.Ascending = false;
            if(values[1].ToString() == "Ascending")
                comparer.Ascending = true;

            return comparer;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
