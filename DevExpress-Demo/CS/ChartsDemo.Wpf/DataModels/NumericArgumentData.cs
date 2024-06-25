using System;
using System.Collections.Generic;

namespace ChartsDemo {
    public class NumericArgumentDataGenerator {
        const int PointCount = 500000;

        public List<NumericPoint> Generate() {
            double value = 0;
            double argument = 1;
            Random random = new Random(0);
            var points = new List<NumericPoint>();
            for (int i = 0; i < PointCount; i++) {
                points.Add(new NumericPoint(argument, value));
                value += (random.NextDouble() * 10.0 - 5.0);
                argument++;
            }
            return points;
        }
    }

    public class NumericPoint {
        public double Argument { get; private set; }
        public double Value { get; private set; }

        public NumericPoint(double argument, double value) {
            Argument = argument;
            Value = value;
        }
    }

}
