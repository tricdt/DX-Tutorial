using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using DevExpress.Internal;

namespace NavigationDemo {
    [XmlRoot("Countries")]
    public class CountriesData : List<Country> {
        static List<Country> dataSource = null;
        public static List<Country> DataSource {
            get {
                if(dataSource != null)
                    return dataSource;
                XmlSerializer s = new XmlSerializer(typeof(CountriesData));
                var path = DataDirectoryHelper.GetFile("Countries.xml", DataDirectoryHelper.DataFolderName);
                using(FileStream fs = File.OpenRead(path))
                    dataSource = (List<Country>)s.Deserialize(fs);
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
