using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Media3D;

namespace ChartsDemo {

    class Bar3DDataGenerator {
        #region Point3DComparer
        class Point3DComparer : IEqualityComparer<Point3D> {
            const double DefaultEps = 10e-12;
            readonly double eps = DefaultEps;

            public Point3DComparer() {
            }

            public Point3DComparer(double eps) {
                this.eps = eps;
            }

            public bool Equals(Point3D p1, Point3D p2) {
                return Math.Abs(p1.X - p2.X) < this.eps && Math.Abs(p1.Y - p2.Y) < this.eps && Math.Abs(p1.Z - p2.Z) < this.eps;
            }
            public int GetHashCode(Point3D e1) {
                return e1.GetHashCode();
            }
        }
        #endregion

        const int PointsCount = 300;
        const int MaxValue = 200;

        public List<Point3D> Generate() {
            var random = new Random(1);
            var data = new List<Point3D>();
            for (int i = 0; i < PointsCount; i++) {
                Point3D point;
                do {
                    double x = random.NextDouble() * MaxValue * 2;
                    double y = random.NextDouble() * MaxValue * 2;
                    double z = random.NextDouble() * MaxValue;
                    point = new Point3D(x, y, z);
                } while (data.Contains(point, new Point3DComparer(1.0)));
                data.Add(point);
            }
            return data;
        }
    }

}
