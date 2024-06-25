using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ChartsDemo {

    [XmlRoot("TemperatureData")]
    public class TemperatureData : List<TemperaturesInfo> {
        public static TemperatureData Load(){
            XmlSerializer serializer = XmlSerializer.FromTypes(new[] { typeof(TemperatureData) })[0];
            return (TemperatureData)serializer.Deserialize(DataLoader.LoadFromResources("/Data/LondonTemperature.xml"));
        }
    }


    public class TemperaturesInfo {
        public string Description { get; set; }
        public List<TemperatureInfo> Items { get; set; }
    }


    public class TemperatureInfo {
        public DateTime Date { get; set; }
        public int Temperature { get; set; }
    }

}
