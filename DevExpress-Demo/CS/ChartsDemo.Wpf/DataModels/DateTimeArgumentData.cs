using System;
using System.Collections.Generic;

namespace ChartsDemo {

    public class DateTimeArgumentDataGenerator {
        const int PointCount = 100000;

        public List<DateTimePoint> Generate() {
            double value = 0;
            DateTime argument = DateTime.Now.AddHours(-PointCount);
            Random random = new Random(1);
            var points = new List<DateTimePoint>();
            for (double i = 0; i < PointCount; i++) {
                points.Add(new DateTimePoint(argument.AddHours(i), Math.Abs(value)));
                value += (random.NextDouble() * 10.0 - 5.0);
            }
            return points;
        }
    }


    public class DateTimePoint {
        public DateTime Argument { get; private set; }
        public double Value { get; private set; }

        public DateTimePoint(DateTime argument, double value) {
            Argument = argument;
            Value = value;
        }
    }

}
