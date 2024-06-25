using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace ChartsDemo {
    public class RealTime3DDataGenerator {
        #region ElevationData
        public class ElevationData {
            int directionX;
            int directionZ;
            int directionY;

            public Point3D Elevation { get; set; }
            public int DirectionX { get { return this.directionX; } }
            public int DirectionZ { get { return this.directionZ; } }
            public int DirectionY { get { return this.directionY; } }

            public ElevationData(Point3D elevation) {
                Elevation = elevation;
                this.directionX = 1;
                this.directionZ = 1;
                this.directionY = 1;
            }
            public void UpdateDirection(int sideLength) {
                if (Elevation.X < 0 || Elevation.X >= sideLength) this.directionX *= -1;
                if (Elevation.Z < 0 || Elevation.Z >= sideLength) this.directionZ *= -1;
                if (Elevation.Y < -sideLength / 3 || Elevation.Y > sideLength / 3) this.directionY *= -1;
            }
        }
        #endregion

        const double OffsetFactor = Math.PI * 3d / 2d;
        const double RadiusFactor = 10d / Math.PI;
        const int ElevationsCount = 25;

        readonly Random rnd = new Random(0);
        readonly ElevationData[] vertices;
        int sensitivityDirection = 1;
        double sensitivity;

        public int Size { get; set; }

        public RealTime3DDataGenerator() {
            this.vertices = new ElevationData[ElevationsCount];
        }

        void NextElevations() {
            double k = Size / 66d; 
            for (int i = 0; i < ElevationsCount; i++) {
                this.vertices[i].UpdateDirection(Size);
                double dx = k * this.vertices[i].DirectionX;
                double dz = k * this.vertices[i].DirectionZ;
                double dy = k * this.vertices[i].DirectionY;
                double x = this.vertices[i].Elevation.X + dx;
                double z = this.vertices[i].Elevation.Z + dz;
                double y = this.vertices[i].Elevation.Y + dy;
                this.vertices[i].Elevation = new Point3D(x, y, z);
            }
        }
        void NextSensitivity() {
            if (this.sensitivity < Size * 0.15)
                this.sensitivityDirection = 1;
            if (this.sensitivity > Size * 0.6)
                this.sensitivityDirection = -1;
            this.sensitivity += this.sensitivityDirection * 0.2;
        }

        public void RecreateElevations() {
            this.sensitivity = Size * 0.5;
            for (int i = 0; i < ElevationsCount; i++) {
                Point3D elevation = new Point3D(this.rnd.Next(0, Size), this.rnd.Next(-Size / 3, Size / 3), this.rnd.Next(0, Size));
                this.vertices[i] = new ElevationData(elevation);
            }
        }
        public IEnumerable<object> GenerateArguments() {
            List<object> arguments = new List<object>();
            for (int i = 0; i < Size; i++)
                arguments.Add(i);
            return arguments;
        }
        public IEnumerable<double> GenerateValues() {
            NextElevations();
            NextSensitivity();
            double[] values = new double[Size * Size];
            for (int j = 0; j < this.vertices.Length; j++) {
                double dataX = this.vertices[j].Elevation.X;
                double dataZ = this.vertices[j].Elevation.Z;
                double dataY = this.vertices[j].Elevation.Y;
                int counter = 0;
                for (int x = 0; x < Size; x++) {
                    for (int z = 0; z < Size; z++) {
                        double dx = dataX - x;
                        double dz = dataZ - z;
                        double distance = Math.Sqrt(dx * dx + dz * dz);
                        if (distance <= this.sensitivity) {
                            double percent = 1.0 - distance / this.sensitivity;
                            double coef = dataY - values[counter];
                            double elevate = coef * (Math.Sin(percent * RadiusFactor + OffsetFactor) + 1.0) / 2;
                            values[counter] += elevate;
                        }
                        counter++;
                    }
                }
            }
            return values;
        }
    }

}
