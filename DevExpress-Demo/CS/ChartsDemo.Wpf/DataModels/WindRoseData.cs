using System.Collections.Generic;
using System.Xml.Serialization;

namespace ChartsDemo {

    [XmlRoot("WindRoseData")]
    public class WindRoseData : List<WindInfo> {
        public static WindRoseData Load() {
            XmlSerializer serializer = XmlSerializer.FromTypes(new[] { typeof(WindRoseData) })[0];
            return (WindRoseData)serializer.Deserialize(DataLoader.LoadFromResources("/Data/WindRoseData.xml"));
        }
    }


    public class WindInfo {
        public string Direction { get; set; }
        public int Speed { get; set; }
    }

}
