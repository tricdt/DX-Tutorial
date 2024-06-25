using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace ChartsDemo {

    [XmlRoot("MarsTemperatureData")]
    public class MarsTemperatureData : List<MarsTemperature> {
        public static MarsTemperatureData GetFullData() {
            XmlSerializer serializer = XmlSerializer.FromTypes(new[] { typeof(MarsTemperatureData) })[0];
            return (MarsTemperatureData)serializer.Deserialize(DataLoader.LoadFromResources("/Data/MarsTemperatureData.xml"));          
        }
        public static List<MarsTemperature> GetShortData() {
            MarsTemperatureData fullData = GetFullData();
            return fullData.Take(31).ToList(); 
        }
    }


    public class MarsTemperature {
        public double Sol { get; set; }
        public double Temperature { get; set; }
    }

}
