using System.Collections.Generic;
using System.Xml.Serialization;

namespace ChartsDemo {

    [XmlRoot("Countries")]
    public class CountriesAreaData : List<Country> {
        static int CompareCountriesByArea(Country country1, Country country2) {
            return country2.Area.CompareTo(country1.Area);
        }

        public static CountriesAreaData Load() {
            XmlSerializer serializer = XmlSerializer.FromTypes(new[] { typeof(CountriesAreaData) })[0];
            CountriesAreaData data = (CountriesAreaData)serializer.Deserialize(DataLoader.LoadFromResources("/Data/Countries.xml"));
            data.Sort(CompareCountriesByArea);
            return data;
        }
    }


    public class Country {
        public double Area { get; set; }
        public string Name { get; set; }
        public string OfficialName { get; set; }
    }

}
