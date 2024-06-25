using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml.Serialization;
using DevExpress.Xpf.DemoBase;
using System.ComponentModel;
using DevExpress.Xpf.DemoBase.Helpers;
using System.Reflection;

namespace EditorsDemo {
    [XmlRoot("Countries")]
    public class CountriesData : List<Country> {
        static List<Country> dataSource = null;
        public static List<Country> DataSource {
            get {
                if (dataSource != null)
                    return dataSource;
                XmlSerializer s = new XmlSerializer(typeof(CountriesData));
                Assembly assembly = Assembly.GetExecutingAssembly();
                dataSource = (List<Country>)s.Deserialize(assembly.GetManifestResourceStream(DemoHelper.GetPath("EditorsDemo.Data.", assembly) + "Countries.xml"));
                return dataSource;
            }
        }
    }

    public class Country {
        public string ActualName { get { return NWindName ?? Name; } }
        public string Name { get; set; }
        public string NWindName { get; set; }
        public byte[] Flag { get; set; }
    }
}
