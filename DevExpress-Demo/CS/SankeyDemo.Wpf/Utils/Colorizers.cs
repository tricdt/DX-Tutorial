using DevExpress.Xpf.Charts.Sankey;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace SankeyDemo {
    public class ContinentColorizer : SankeyColorizerBase {
        Dictionary<string, Color> continentColorPairs = new Dictionary<string, Color>();
        Dictionary<string, List<string>> continentCountriesPairs = new Dictionary<string, List<string>>();

        public Dictionary<string, Color> ContinentColorPairs {
            get { return continentColorPairs; }
            set { continentColorPairs = value; }
        }
        public Dictionary<string, List<string>> ContinentCountriesPairs {
            get { return continentCountriesPairs; }
            set { continentCountriesPairs = value; }
        }

        public override Color GetLinkSourceColor(SankeyLink link) {
            var country = link.SourceNode.Tag.ToString();
            string continent = GetContinentByCountry(country);
            if(continent != "" && continentColorPairs.ContainsKey(continent))
                return continentColorPairs[continent];
            return new Color();
        }
        public override Color GetLinkTargetColor(SankeyLink link) {
            var country = link.TargetNode.Tag.ToString();
            string continent = GetContinentByCountry(country);
            if(continent != "" && continentColorPairs.ContainsKey(continent))
                return continentColorPairs[continent];
            return new Color();
        }
        public override Color GetNodeColor(SankeyNode info) {
            var country = info.Tag.ToString();
            string continent = GetContinentByCountry(country);
            if(continent != "" && continentColorPairs.ContainsKey(continent))
                return continentColorPairs[continent];

            return new Color();
        }

        string GetContinentByCountry(string country) {
            if(continentColorPairs.ContainsKey(country))
                return country;
            foreach(var continentCountryPairs in continentCountriesPairs)
                if(continentCountryPairs.Value.Contains(country))
                    return continentCountryPairs.Key;
            return "";
        }
        
    }

    class GradientColorizer : SankeyColorizerBase {
        public static GradientColorizer Create(List<Export> dataSource) {
            GradientColorizer colorizer = new GradientColorizer();
            if(dataSource.Count != 0) {
                Dictionary<string, double> exportCountryValues = new Dictionary<string, double>();
                Dictionary<string, double> importCountryValues = new Dictionary<string, double>();
                foreach(var export in dataSource) {
                    if(!exportCountryValues.ContainsKey(export.Exporter))
                        exportCountryValues.Add(export.Exporter, 0);
                    exportCountryValues[export.Exporter] += export.Sum;

                    if(!importCountryValues.ContainsKey(export.Importer))
                        importCountryValues.Add(export.Importer, 0);
                    importCountryValues[export.Importer] += export.Sum;
                }
                var values = exportCountryValues.Values.ToList();
                values.AddRange(importCountryValues.Values);
                colorizer.MinValue = values.Min();
                colorizer.MaxValue = values.Max();
            }
            return colorizer;
        }

        public double MinValue { get; set; }
        public double MaxValue { get; set; }

        GradientColorizer() { }

        Color GetGradientColor(double percent) {
            percent = double.IsNaN(percent) ? 1 : percent;
            Color color1 = Color.FromArgb(255, 96, 181, 204);
            Color color2 = Color.FromArgb(255, 230, 108, 125);

            double resultRed = color1.R * (1.0 - percent) + color2.R * percent;
            double resultGreen = color1.G * (1.0 - percent) + color2.G * percent;
            double resultBlue = color1.B * (1.0 - percent) + color2.B * percent;
            return Color.FromRgb((byte)resultRed, (byte)resultGreen, (byte)resultBlue);
        }

        public override Color GetLinkSourceColor(SankeyLink link) {
            return GetGradientColor((link.SourceNode.TotalWeight - MinValue) / (MaxValue - MinValue));
        }
        public override Color GetLinkTargetColor(SankeyLink link) {
            return GetGradientColor((link.TargetNode.TotalWeight - MinValue) / (MaxValue - MinValue));
        }
        public override Color GetNodeColor(SankeyNode node) {
            return GetGradientColor((node.TotalWeight - MinValue) / (MaxValue - MinValue));
        }
    }

    public static class ContinentCountries {
        public static Dictionary<string, List<string>> GetContinentCountriesPairs_ColorizerDemo() {
            Dictionary<string, List<string>> continentCountriesPairs = new Dictionary<string, List<string>>();
            continentCountriesPairs.Add("North America", new List<string>() {
                "United States",
                "Canada",
                "Mexico",
            });
            continentCountriesPairs.Add("Asia", new List<string>() {
                "China",
                "Japan",
                "South Korea",
            });
            continentCountriesPairs.Add("South America", new List<string>() {
                "Brazil",
                "Argentina",
            });
            continentCountriesPairs.Add("Australia", new List<string>() {
                "Australia",
            });
            continentCountriesPairs.Add("Europe", new List<string>() {
                "Netherlands",
                "Germany",
                "United Kingdom",
                "Italy",
                "France",
                "Spain",
                "United Kingdom",
            });
            return continentCountriesPairs;
        }
        public static Dictionary<string, Color> GetContinentColorPairs_ColorizerDemo() {
            var continentColorPairs = new Dictionary<string, Color>();
            continentColorPairs.Add("Asia", Color.FromRgb(245, 86, 74));
            continentColorPairs.Add("North America", Color.FromRgb(29, 178, 245));
            continentColorPairs.Add("South America", Color.FromRgb(151, 201, 92));
            continentColorPairs.Add("Australia", Color.FromRgb(198, 144, 83));
            continentColorPairs.Add("Europe", Color.FromRgb(255, 199, 32));
            return continentColorPairs;
        }
        public static Dictionary<string, List<string>> GetContinentCountriesPairs_InteractionDemo() {
            Dictionary<string, List<string>> continentCountriesPairs = new Dictionary<string, List<string>>();
            continentCountriesPairs.Add("North America", new List<string>() {
                "United States",
                "Canada",
                "Mexico",
            });
            continentCountriesPairs.Add("South and Central America", new List<string>() {
                "Brazil",
                "Argentina",
            });
            continentCountriesPairs.Add("Australasia", new List<string>() {
                "Australia",
            });
            continentCountriesPairs.Add("Europe", new List<string>() {
                "Netherlands",
                "Germany",
                "United Kingdom",
                "Italy",
                "France",
                "Spain",
                "United Kingdom",
            });
            continentCountriesPairs.Add("Asia Pacific", new List<string>() {
                "China",
                "Japan",
                "South Korea",
                "India",
                "Singapore",
                "Other Asia Pacific",
            });
            continentCountriesPairs.Add("Middle East", new List<string>() {
                "Saudi Arabia",
                "UAE",
                "United Arab Emirates",
                "Kuwait",
                "Iraq",
                
            });
            continentCountriesPairs.Add("CIS", new List<string>() {
                "Russia",
                "Other CIS",
            });
            return continentCountriesPairs;
        }
        public static Dictionary<string, Color> GetContinentColorPairs_InteractionDemo() {
            var continentColorPairs = new Dictionary<string, Color>();
            continentColorPairs = new Dictionary<string, Color>();
            continentColorPairs.Add("North America", Color.FromRgb(29, 178, 245));
            continentColorPairs.Add("South and Central America", Color.FromRgb(151, 201, 92));
            continentColorPairs.Add("Europe", Color.FromRgb(255, 199, 32));
            continentColorPairs.Add("CIS", Color.FromRgb(44, 115, 255));
            continentColorPairs.Add("Middle East", Color.FromRgb(186, 85, 211));
            continentColorPairs.Add("West Africa", Color.FromRgb(255, 126, 32));
            continentColorPairs.Add("North Africa", Color.FromRgb(51, 204, 170));
            continentColorPairs.Add("Africa", Color.FromRgb(103, 113, 220));
            continentColorPairs.Add("Australasia", Color.FromRgb(198, 144, 83));
            continentColorPairs.Add("Asia Pacific", Color.FromRgb(245, 86, 74));
            return continentColorPairs;
        }
    }
}
