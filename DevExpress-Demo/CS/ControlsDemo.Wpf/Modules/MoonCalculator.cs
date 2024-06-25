using System;

namespace ControlsDemo {
    public static class MoonCalculator {
        static double arc = 206264.8062;
        static double Frac(double x) {
            x = x - Math.Floor(x);
            return x < 0 ? x + 1 : x;
        }
        static double CalculateSun(double t) {
            double m = Math.PI * 2 * Frac(0.993133 + 99.997361 * t);
            double dl = 6893.0 * Math.Sin(m) + 72.0 * Math.Sin(2 * m);
            double l = Math.PI * 2 * Frac(0.7859453 + m / 2 / Math.PI + (6191.2 * t + dl) / 1296E3);
            return l * 180 / Math.PI;
        }
        static void CalculateMoon(double t, out double l, out double b) {
            double l0 = Frac(0.606433 + 1336.855225 * t);
            double p2 = Math.PI * 2;
            l = p2 * Frac(0.374897 + 1325.552410 * t);
            double ls = p2 * Frac(0.993133 + 99.997361 * t);
            double d = p2 * Frac(0.827361 + 1236.853086 * t);
            double f = p2 * Frac(0.259086 + 1342.227825 * t);
            double dl = +22640 * Math.Sin(l) - 4586 * Math.Sin(l - 2 * d) + 2370 * Math.Sin(2 * d) + 769 * Math.Sin(2 * l)
                - 668 * Math.Sin(ls) - 412 * Math.Sin(2 * f) - 212 * Math.Sin(2 * l - 2 * d) - 206 * Math.Sin(l + ls - 2 * d)
                + 192 * Math.Sin(l + 2 * d) - 165 * Math.Sin(ls - 2 * d) - 125 * Math.Sin(d) - 110 * Math.Sin(l + ls)
                + 148 * Math.Sin(l - ls) - 55 * Math.Sin(2 * f - 2 * d);
            double s = f + (dl + 412 * Math.Sin(2 * f) + 541 * Math.Sin(ls)) / arc;
            double h = f - 2 * d;
            double n = -526 * Math.Sin(h) + 44 * Math.Sin(l + h) - 31 * Math.Sin(-l + h) - 23 * Math.Sin(ls + h)
                + 11 * Math.Sin(-ls + h) - 25 * Math.Sin(-2 * l + f) + 21 * Math.Sin(-l + f);
            l = p2 * Frac(l0 + dl / 1296E3);
            l = l * 180.0 / Math.PI;
            b = (18520.0 * Math.Sin(s) + n) / arc;
            b = b * 180.0 / Math.PI;

        }
        public static double GetPhase(DateTime currentDate) {
            double t = (currentDate - new DateTime(2000, 1, 1)).Duration().TotalDays / 36525;
            double ls = CalculateSun(t);
            double l, b;
            CalculateMoon(t, out l, out b);
            double cgam = Math.Cos((ls - l) * Math.PI / 180) * Math.Cos(b * Math.PI / 180);
            double sgam = Math.Sqrt(1.0 - cgam * cgam);
            double s = Math.Atan2(sgam, (0.0025 - cgam)) * 180.0 / Math.PI;
            t = ls - l;
            t = t < 0 ? t + 360 : t;
            if(t < 180)
                return -Math.Pow(Math.Cos(s / 2 * Math.PI / 180), 2);
            return Math.Pow(Math.Cos(s / 2 * Math.PI / 180), 2);
        }
    }
}
