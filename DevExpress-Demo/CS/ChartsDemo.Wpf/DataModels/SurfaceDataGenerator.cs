using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace ChartsDemo {
    public static class SurfaceDataGenerator {
        public static List<Point3D> Generate() {
            List<Point3D> points = new List<Point3D>();
            for (double j = -25.0; j < 25.0; j += 0.75) {
                for (double i = -15.0; i < 15.0; i += 0.75) {
                    double x = 2 + i + Math.Sin(j / 4.0) * 6;
                    double y = 1 + j + Math.Cos(i / 4.0);
                    double z = 5.5 * Math.Sin(i / 3.0) * Math.Sin(j / 3.0);
                    points.Add(new Point3D(x, y, z));
                }
            }
            return points;
        }
    }
}
