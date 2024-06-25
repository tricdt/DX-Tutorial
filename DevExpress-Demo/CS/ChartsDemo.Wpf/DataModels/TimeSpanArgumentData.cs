using System;
using System.Collections.Generic;

namespace ChartsDemo {

    public class TimeSpanArgumentDataGenerator {
        const int PointCount = 40000;

        public List<TimeSpanPoint> Generate(double initialValue, double min, double max) {
            double value = initialValue;
            Random random = new Random(1);
            double threshold = (min - initialValue) / 0.2;
            var points = new List<TimeSpanPoint>();
            for (double i = 0; i < threshold; i++) {
                points.Add(new TimeSpanPoint(TimeSpan.FromSeconds(i), value));
                value += random.NextDouble() - 0.3;
            }
            for (double i = threshold; i < PointCount; i++) {
                points.Add(new TimeSpanPoint(TimeSpan.FromSeconds(i), value));
                value = Math.Max(Math.Min(value + random.NextDouble() - 0.5, max), min);
            }
            return points;
        }
    }


    public class TimeSpanPoint {
        public TimeSpan Argument { get; private set; }
        public double Value { get; private set; }

        public TimeSpanPoint(TimeSpan argument, double value) {
            Argument = argument;
            Value = value;
        }
    }

}
