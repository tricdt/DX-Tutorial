using System;
using System.Collections.Generic;

namespace DevExpress.Demos.DayAndNightLineCalculator {
    public static class DayAndNightLineCalculator {
        static double[] CalculateCoordinates(double mjt, double t0, double[] eclipticalCoords) {
            double x = eclipticalCoords[0];
            double y = eclipticalCoords[1];
            double z = eclipticalCoords[2];

            double a1 = 24110.54841;
            double a2 = 8640184.812;
            double a3 = 0.093104;
            double a4 = 0.0000062;
            double s0 = a1 + a2 * t0 + a3 * t0 * t0 - a4 * t0 * t0 * t0;
            double nSec = (mjt - (int)mjt) * 86400.0;
            double ut1 = nSec * 366.2422 / 365.2422;
            s0 = Degree2Rad(((s0 + ut1) / 3600.0 * 15.0) % 360.0);
            double x0 = Math.Cos(s0) * x + Math.Sin(s0) * y;
            double y0 = -Math.Sin(s0) * x + Math.Cos(s0) * y;
            double[] result = new double[3];
            result[0] = Rad2Degree(Math.Atan2(y0, x0));
            result[1] = Rad2Degree(Math.Atan2(z, Math.Sqrt(x0 * x0 + y0 * y0)));
            result[2] = z;
            return result;
        }
        static double CalcJulianDateTime(DateTime dt, double ut) {
            double mon = dt.Month > 2 ? dt.Month : dt.Month + 12;
            double yr = dt.Month > 2 ? dt.Year : dt.Year - 1;
            double var = 365.0 * yr - 679004.0;
            double b = (int)(yr / 400) - (int)(yr / 100) + (int)(yr / 4);
            double res = var + b + (int)(306001.0 * (mon + 1) / 10000) + dt.Day;
            return res + ut / 24.0;
        }
        static double[] CalculateSunEclipticalCoordinates(double ut, double t0) {
            double m = (357.528 + 35999.05 * t0 + 0.04107 * ut) % 360.0;
            double l = 280.46 + 36000.772 * t0 + 0.04107 * ut;
            l = (l + (1.915 - 0.0048 * t0) * Math.Sin(Degree2Rad(m)) + 0.02 * Math.Sin(2.0 * Degree2Rad(m))) % 360;
            double axisTilt = Degree2Rad(23.439281);
            double[] result = new double[3];
            result[0] = Math.Cos(Degree2Rad(l));
            result[1] = Math.Sin(Degree2Rad(l)) * Math.Cos(axisTilt);
            result[2] = Math.Sin(Degree2Rad(l)) * Math.Sin(axisTilt);
            return result;
        }
        static double CalculateUniversalTime(DateTime dt) {
            return dt.Hour + (double)dt.Minute / 60.0 + (double)dt.Second / 3600.0;
        }
        static double Degree2Rad(double value) {
            return value * Math.PI / 180.0;
        }
        static double Rad2Degree(double value) {
            return value * 180.0 / Math.PI;
        }
        public static double[] CalculateSunPosition(DateTime date) {
            double ut = CalculateUniversalTime(date);
            double mjt = CalcJulianDateTime(date, ut);

            double t0 = ((int)mjt - 51544.5) / 36525.0;
            double[] eclipticalCoords = CalculateSunEclipticalCoordinates(ut, t0);
            return CalculateCoordinates(mjt, t0, eclipticalCoords);
        }
        public static IList<double> GetDayAndNightLineLatitudes(double centerLat, double centerLon, double step) {
            IList<double> result = new List<double>();
            double ct = -1.0 / Math.Tan(Degree2Rad(centerLat));
            for (double lon = -180; lon <= 180; lon += step) {
                double t = ct * Math.Cos(Degree2Rad(lon) - Degree2Rad(centerLon));
                double lat = Rad2Degree(Math.Atan(t));
                result.Add(lat);
            }
            return result;
        }
        public static bool CalculateIsNorthNight(double[] sunPosition) {
            double lat = Math.PI / 2.0;
            double lon = -Math.PI;
            double x = Math.Cos(lon) * Math.Cos(lat);
            double y = Math.Sin(lon) * Math.Cos(lat);
            double z = Math.Sin(lat);
            return x * sunPosition[0] + y * sunPosition[1] + z * sunPosition[2] < 0;
        }
    }
}
