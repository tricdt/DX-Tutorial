using System;
using System.Collections.Generic;
namespace ChartsDemo {
    public static class LargeDataGenerator {
        static int counter = 0;
        public static List<SimpleDataPoint> GenerateSeriesDataSource(int pointsCount) {
            List<SimpleDataPoint> points = new List<SimpleDataPoint>();
            double value = 0;
            double argument = 0;
            counter++;
            for (int i = 0; i < pointsCount; i++) {
                argument = i;
                value = (float)(                                                                                 Math.Sin(argument      ) +
                                                                                                                 Math.Sin(argument / 100.0) +
                                                                                                            30 * Math.Sin(argument / 1000.0) +
                                                                               1000 * (1 + 0.1 * counter % 10) * Math.Sin(argument / 100000.0 + Math.PI / 6 * counter));
                points.Add(new SimpleDataPoint(argument, value));
            }
            return points;
        }
    }
}
