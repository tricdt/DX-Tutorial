using System;
using System.Collections.Generic;

namespace ChartsDemo {
    public class WeatherInWashington {
        static List<WeatherPoint> data;

        public static List<WeatherPoint> Data {
            get {
                if (data == null)
                    InitCollection();
                return data;
            }
        }

        static void InitCollection() {
            data = new List<WeatherPoint>();
            int lastYear = DateTime.Now.Year - 1;
            data.Add(new WeatherPoint() { Date = new DateTime(lastYear, 7, 10), DayTemperature = 23, NightTemperature = 22, Pressure = 732 });
            data.Add(new WeatherPoint() { Date = new DateTime(lastYear, 7, 11), DayTemperature = 28, NightTemperature = 22, Wind = 5, Pressure = 729 });
            data.Add(new WeatherPoint() { Date = new DateTime(lastYear, 7, 12), DayTemperature = 27, NightTemperature = 22, Wind = 5, Pressure = 734 });
            data.Add(new WeatherPoint() { Date = new DateTime(lastYear, 7, 13), DayTemperature = 29, Wind = 6, Pressure = 734 });
            data.Add(new WeatherPoint() { Date = new DateTime(lastYear, 7, 14), DayTemperature = 28, NightTemperature = 25, Wind = 3, Pressure = 735 });
            data.Add(new WeatherPoint() { Date = new DateTime(lastYear, 7, 15) });
            data.Add(new WeatherPoint() { Date = new DateTime(lastYear, 7, 16), DayTemperature = 29, NightTemperature = 27, Wind = 6, Pressure = 731 });
            data.Add(new WeatherPoint() { Date = new DateTime(lastYear, 7, 17), DayTemperature = 22, NightTemperature = 21, Wind = 5, Pressure = 731 });
            data.Add(new WeatherPoint() { Date = new DateTime(lastYear, 7, 18), DayTemperature = 26, NightTemperature = 19, Wind = 5, Pressure = 734 });
            data.Add(new WeatherPoint() { Date = new DateTime(lastYear, 7, 19), DayTemperature = 27, NightTemperature = 26, Wind = 4, Pressure = 733 });
            data.Add(new WeatherPoint() { Date = new DateTime(lastYear, 7, 20), DayTemperature = 27, NightTemperature = 26, Wind = 3, Pressure = 731 });
            data.Add(new WeatherPoint() { Date = new DateTime(lastYear, 7, 21), DayTemperature = 26, Wind = 7, Pressure = 731 });
            data.Add(new WeatherPoint() { Date = new DateTime(lastYear, 7, 22), DayTemperature = 23, NightTemperature = 23, Wind = 2, Pressure = 735 });
            data.Add(new WeatherPoint() { Date = new DateTime(lastYear, 7, 23), DayTemperature = 25, Pressure = 735 });
            data.Add(new WeatherPoint() { Date = new DateTime(lastYear, 7, 24), DayTemperature = 26, NightTemperature = 26, Pressure = 732 });
            data.Add(new WeatherPoint() { Date = new DateTime(lastYear, 7, 25), DayTemperature = 37, NightTemperature = 27, Wind = 4, Pressure = 729 });
            data.Add(new WeatherPoint() { Date = new DateTime(lastYear, 7, 26), DayTemperature = 30, NightTemperature = 27, Wind = 5, Pressure = 731 });
            data.Add(new WeatherPoint() { Date = new DateTime(lastYear, 7, 27), DayTemperature = 30, NightTemperature = 28, Wind = 4, Pressure = 731 });
            data.Add(new WeatherPoint() { Date = new DateTime(lastYear, 7, 28), DayTemperature = 28, NightTemperature = 28, Wind = 4, Pressure = 731 });
        }

        public class WeatherPoint {
            public DateTime Date { get; set; }
            public double? NightTemperature { get; set; }
            public double? DayTemperature { get; set; }
            public double? Wind { get; set; }
            public double? Pressure { get; set; }

        }
    }
}
