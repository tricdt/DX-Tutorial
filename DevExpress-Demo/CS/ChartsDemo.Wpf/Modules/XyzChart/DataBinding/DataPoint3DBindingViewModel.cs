using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace ChartsDemo {

    public class DataPoint3DBindingViewModel {
        static List<Function3DItemData> CreateFunctionItemsData() {
            var functions = new Func<double, double, Point3D>[]
                { CalculateFirstValue, CalculateSecondValue, CalculateThirdValue, CalculateFourthValue, CalculateFifthValue };
            var images = Enumerable.Range(1, 5).Select(x => Chart3DUtils.CreateFunctionImage(x));
            return functions.Zip(images, (func, image) => new Function3DItemData { Image = image, Points = CreatePoints(func) }).ToList();
        }
        static double Sinc(double x) {
            return x != 0 ? Math.Sin(x) : 1;
        }
        static Point3D CalculateFirstValue(double x, double y) {
            x *= UnitFactor;
            y *= UnitFactor;
            double value = Sinc(Math.Sin(Math.Pow(Math.Pow(x, 6) + Math.Pow(y, 6), 1.0d / 6.0d))) * 5;
            return new Point3D(x, y, value);
        }
        static Point3D CalculateSecondValue(double x, double y) {
            x *= UnitFactor;
            y *= UnitFactor;
            double square = Math.Sqrt(x * x + y * y);
            double value = square * Sinc(square) * 0.2;
            return new Point3D(x, y, value);
        }
        static Point3D CalculateThirdValue(double x, double y) {
            x *= UnitFactor / 2;
            y *= UnitFactor / 2;
            double value = Math.Sin(x) * Math.Cos(y) * 2;
            return new Point3D(x, y, value);
        }
        static Point3D CalculateFourthValue(double x, double y) {
            double theta = Math.Atan2(y, x);
            double r = x * x + y * y;
            double z = Math.Exp(-r) * Math.Sin(2 * Math.PI * Math.Sqrt(r)) * Math.Cos(3 * theta);
            return new Point3D(x, y, z);
        }
        static Point3D CalculateFifthValue(double x, double y) {
            x *= 3;
            y *= 3;
            double z = Math.Sin(x * y);
            return new Point3D(x, y, z);
        }
        static List<Point3D> CreatePoints(Func<double, double, Point3D> valueCalculator) {
            List<Point3D> points = new List<Point3D>();
            for (double x = -1; x < 1; x += 0.017)
                for (double y = -1; y < 1; y += 0.017)
                    points.Add(valueCalculator(x, y));
            return points;
        }

        public static DataPoint3DBindingViewModel Create() {
            return ViewModelSource.Create(() => new DataPoint3DBindingViewModel());
        }

        const double UnitFactor = 15;

        public List<Function3DItemData> FunctionItemsData { get; private set; }
        public virtual Function3DItemData Function { get; set; }

        protected DataPoint3DBindingViewModel() {
            FunctionItemsData = CreateFunctionItemsData();
            Function = FunctionItemsData[3];
        }
    }


    public class Function3DItemData : BindableBase {
        List<Point3D> points;
        BitmapImage image;

        public List<Point3D> Points {
            get { return this.points; }
            set { SetProperty(ref this.points, value, () => Points); }
        }
        public BitmapImage Image {
            get { return this.image; }
            set { SetProperty(ref this.image, value, () => Image); }
        }
    }


    public static class Chart3DUtils {
        public static Size3D CreateSize3D(double x, double y, double z) {
            return new Size3D(x, y, z);
        }
        public static BitmapImage CreateFunctionImage(int index) {
            string uriStr = string.Format(@"/ChartsDemo;component/Images/Functions/{0}.png", index);
            return new BitmapImage(new Uri(uriStr, UriKind.RelativeOrAbsolute));
        }
    }

}
