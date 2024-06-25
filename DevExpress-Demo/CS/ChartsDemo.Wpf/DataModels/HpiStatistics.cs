using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;

namespace ChartsDemo {
    static class HPIStatistics {
        public static List<HpiStatisticsItem> Load() {
            XDocument document = DataLoader.LoadXmlFromResources("/Data/HPI.xml");
            List<HpiStatisticsItem> result = new List<HpiStatisticsItem>();
            if (document != null) {
                foreach (XElement element in document.Element("G20HPIs").Elements()) {
                    string country = element.Element("Country").Value;
                    double population = int.Parse(element.Element("Population").Value) / 1000000.0d;
                    double hpi = Convert.ToDouble(element.Element("HPI").Value, CultureInfo.InvariantCulture);
                    decimal product = Convert.ToDecimal(element.Element("Product").Value, CultureInfo.InvariantCulture);
                    result.Add(new HpiStatisticsItem(country, population, hpi, product, string.Format("{0:n2}", hpi)));
                }
            }
            return result;
        }
    }

    public class HpiStatisticsItem {
        public string Country { get; private set; }
        public double Population { get; private set; }
        public double HPI { get; private set; }
        public string HPIHint { get; private set; }
        public decimal Product { get; private set; }

        public HpiStatisticsItem(string country, double population, double hpi, decimal product, string hpiHint) {
            Country = country;
            Population = population;
            HPI = hpi;
            Product = product;
            HPIHint = hpiHint;
        }
    }
}
