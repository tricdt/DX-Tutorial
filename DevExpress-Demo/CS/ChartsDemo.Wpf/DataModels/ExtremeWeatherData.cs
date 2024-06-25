using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;

namespace ChartsDemo {
    class ExtremeWeatherData {
        List<WeatherRecord> Load(string fileName) {
            List<WeatherRecord> items = new List<WeatherRecord>();
            XDocument weatherDocument = DataLoader.LoadXmlFromResources("/Data/DailyWeather/" + fileName);
            foreach (XElement element in weatherDocument.Root.Elements("Weather")) {
                items.Add(WeatherRecord.Load(element));
            }
            return items;
        }
        public List<WeatherRecord> LoadDeathValleyData() {
            return Load("DeathValley.xml");
        }
        public List<WeatherRecord> LoadVostokStationData() {
            return Load("VostokStation.xml");
        }
    }

    public class WeatherRecord {
        public static WeatherRecord Load(XElement element) {
            CultureInfo culture = CultureInfo.InvariantCulture;
            DateTime _date = DateTime.Parse(element.Attribute("Date").Value, culture);
            double min = double.Parse(element.Attribute("Min").Value, culture);
            double max = double.Parse(element.Attribute("Max").Value, culture);
            double avg = double.Parse(element.Attribute("Avg").Value, culture);
            return new WeatherRecord(_date, max, avg, min);
        }

        DateTime date;

        public double MinValue { get; private set; }
        public double MaxValue { get; private set; }
        public double AvgValue { get; private set; }
        public DateTime Date { get { return this.date; } private set { this.date = value; } }

        WeatherRecord(DateTime _date, double maxValue, double avgValue, double minValue) {
            Date = _date;
            MaxValue = maxValue;
            AvgValue = avgValue;
            MinValue = minValue;
        }
    }
}
