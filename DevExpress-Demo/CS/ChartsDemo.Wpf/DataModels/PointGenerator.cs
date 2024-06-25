using System;
using System.Collections.Generic;

namespace ChartsDemo {
    class PointGenerator {
        const double startValue = 0;

        public static List<SimpleDataPoint> Generate() {
            Random random1 = new Random(2);
            Random random2 = new Random(3);
            double previousValue = startValue;
            List<SimpleDataPoint> list = new List<SimpleDataPoint>() { new SimpleDataPoint(0, previousValue) };
            double x = -2000;
            while (x < 2000) {
                double value = previousValue + random1.Next(-98, 100);
                double pointValue = value - 3000;
                list.Add(new SimpleDataPoint(x, pointValue));
                previousValue = value;
                x += random2.NextDouble() * 3;
            }
            return list;
        }

        public static IEnumerable<NumericPoint> GenerateCluster(Random random, int xPlus, int xMinus, int yPlus, int yMinus, int count) {
            List<NumericPoint> points = new List<NumericPoint>();
            int deltaX = xMinus - xPlus;
            int deltaY = yMinus - yPlus;
            int centerX = xMinus / 2 + xPlus / 2;
            int centerY = yMinus / 2 + yPlus / 2;
            for (int i = 0; i < count; i++) {
                int half = i / 2 + 1;
                double ratio = Math.Max(2.1, (double)count / half);
                int xOffset = (int)(deltaX / ratio);
                int yOffset = (int)(deltaY / ratio);
                int delta = xMinus - xOffset - centerX;
                int rx, ry;
                do {
                    rx = random.Next(xPlus + xOffset, xMinus - xOffset);
                    ry = random.Next(yPlus + yOffset, yMinus - yOffset);
                }
                while (delta * delta < Math.Pow((centerX - rx), 2) + Math.Pow((centerY - ry), 2));
                points.Add(new NumericPoint(rx, ry));
            }
            return points;
        }
    }
}
