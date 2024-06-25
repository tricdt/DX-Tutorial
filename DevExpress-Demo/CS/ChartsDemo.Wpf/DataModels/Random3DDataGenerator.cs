using System;
using System.Collections.Generic;

namespace ChartsDemo {

    static class Random3DDataGenerator {
        public static List<Simple3dDataPoint> Generate() {
            NormalRandom rnd = new NormalRandom(1);
            List<Simple3dDataPoint> list = new List<Simple3dDataPoint>(3000);
            for (int i = 0; i < 3; i++) {
                double centerX = 35 * (2 - i);
                double centerY = 40 * i;
                double centerZ = 20;
                for (int j = 0; j < 100; j++) {
                    double x = centerX + (rnd.NextDouble() - 0.5) * 10;
                    double y = centerY + (rnd.NextDouble() - 0.5) * 15;
                    double z = centerZ + (rnd.NextDouble() - 0.5) * 15;
                    list.Add(new Simple3dDataPoint() { Series = "Series " + i, X = x, Y = y, Value = z });
                }
            }
            return list;
        }
    }


    class NormalRandom {
        readonly Random rnd;

        public NormalRandom(int seek) {
            this.rnd = new Random(seek);
        }

        public double NextDouble() {
            double u1 = 1.0 - this.rnd.NextDouble();
            double u2 = 1.0 - this.rnd.NextDouble();
            return Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
        }
    }


    class Simple3dDataPoint {
        public string Series { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Value { get; set; }
    }

}
