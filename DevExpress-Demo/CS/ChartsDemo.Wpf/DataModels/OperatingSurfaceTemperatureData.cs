using System;
using System.Collections.Generic;

namespace ChartsDemo {

    public class OperatingSurfaceTemperatureData {
        const int PointsCount = 250;
        readonly TemperaturePoint maxTemperaturePoint = new TemperaturePoint(TimeSpan.Zero, double.MinValue);
        readonly TemperaturePoint minTemperaturePoint = new TemperaturePoint(TimeSpan.MaxValue, double.MaxValue);
        readonly List<TemperaturePoint> temperaturePoints = new List<TemperaturePoint>(PointsCount);

        public double MinOptimalTemperature { get { return 43; } }
        public double MaxOptimalTemperature { get { return 63; } }
        public double OptimalTemperature { get { return 53; } }
        public TemperaturePoint MaxTemperaturePoint {
            get { return this.maxTemperaturePoint; }
        }
        public TemperaturePoint MinTemperaturePoint {
            get { return this.minTemperaturePoint; }
        }
        public List<TemperaturePoint> TemperaturePoints {
            get { return this.temperaturePoints; }
        }

        public OperatingSurfaceTemperatureData() {
            Random random = new Random(9);
            double preTemperature = 50;
            for (int i = 0; i < PointsCount; i++) {
                TimeSpan time = TimeSpan.FromSeconds(i);
                double temperature = preTemperature + (random.NextDouble() - 0.5) * 10;
                if (temperature > 90)
                    temperature -= 20;
                if (temperature < 20)
                    temperature += 10;
                TemperaturePoint temperaturePoint = new TemperaturePoint(time, temperature);
                if (temperature < this.minTemperaturePoint.Temperature)
                    this.minTemperaturePoint = temperaturePoint;
                if (temperature > this.maxTemperaturePoint.Temperature)
                    this.maxTemperaturePoint = temperaturePoint;
                this.temperaturePoints.Add(temperaturePoint);
                preTemperature = temperature;
            }
        }
    }


    public class TemperaturePoint {
        public TimeSpan TimeStamp { get; private set; }
        public double Temperature { get; private set; }

        internal TemperaturePoint(TimeSpan time, double temperature) {
            TimeStamp = time;
            Temperature = temperature;
        }
    }

}
