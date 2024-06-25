using DevExpress.Xpf.Charts.Sankey;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SankeyDemo {
    public partial class Colorizer : SankeyDemoModule {
        public Colorizer() {
            InitializeComponent();
        }
    }

    public class ColorizerDemoToolTipConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string text = "";
            if(value is SankeyNode) {
                SankeyNode node = (SankeyNode)value;
                if(Math.Round(node.SourceWeight, 3) != 0)
                    text += String.Format("Total import: ${0} billion", Math.Round(node.SourceWeight * 1000, 2));
                if(Math.Round(node.TargetWeight, 3) != 0) {
                    if(!string.IsNullOrEmpty(text))
                        text += Environment.NewLine;
                    text += String.Format("Total export: ${0} billion", Math.Round(node.TargetWeight * 1000, 2));
                }
            } else if(value is SankeyLink) {
                SankeyLink link = (SankeyLink)value;
                text = String.Format("${0} billion", Math.Round(link.TotalWeight * 1000, 2));
            }
            return text;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class ColorizerItemToColorizerConverter : IMultiValueConverter {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture) {
            string colorizerName = value[0].ToString();
            SankeyColorizerBase colorizer;
            if(colorizerName == "Continent Colorizer (Custom)")
                colorizer = new ContinentColorizer() {
                    ContinentCountriesPairs = ContinentCountries.GetContinentCountriesPairs_ColorizerDemo(),
                    ContinentColorPairs = ContinentCountries.GetContinentColorPairs_ColorizerDemo()
                };
            else if(colorizerName == "Gradient Colorizer (Custom)")
                colorizer = GradientColorizer.Create((List<Export>)value[1]);
            else if(colorizerName == "Palette Colorizer")
                colorizer = new SankeyPaletteColorizer();
            else
                colorizer = new SankeyPaletteColorizer() { LinkBrush = new SolidColorBrush(Color.FromRgb(134, 134, 134)) };

            return colorizer;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class NodeAlignmentConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string nodeAlignmentString = value.ToString();
            if(nodeAlignmentString == "Far")
                return SankeyNodeAlignment.Far;
            else if(nodeAlignmentString == "Near")
                return SankeyNodeAlignment.Near;
            else
                return SankeyNodeAlignment.Center;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
