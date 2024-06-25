using DevExpress.Xpf.DemoBase.Helpers;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;

namespace GridDemo {
    [XmlRoot("Countries")]
    public class CountriesData : List<Country> {
        static List<Country> dataSource = null;
        public static List<Country> DataSource {
            get {
                if(dataSource != null)
                    return dataSource;
                XmlSerializer s = new XmlSerializer(typeof(CountriesData));
                Assembly assembly = typeof(CountriesData).Assembly;
                dataSource = (List<Country>)s.Deserialize(assembly.GetManifestResourceStream(DemoHelper.GetPath("GridDemo.Data.", assembly) + "Countries.xml"));
                return dataSource;
            }
        }
    }

    public class Country {
        public string ActualNWindName { get { return NWindName ?? Name; } }
        public string ActualName { get { return Name ?? NWindName; } }
        public string Name { get; set; }
        public string NWindName { get; set; }
        public byte[] Flag { get; set; }
    }
}