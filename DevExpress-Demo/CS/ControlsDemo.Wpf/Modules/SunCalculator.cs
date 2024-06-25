using System;

namespace ControlsDemo {
    public static class SunCalculatior {
        // Algorithm source:
        // Almanac for Computers, 1990
        // published by Nautical Almanac Office
        // United States Naval Observatory
        // Washington, DC 20392

        const double Zenith = 90.83;

        public static DateTime? SunRise(DateTime date, double utcOffset, double lat, double lon) {
            return Calculate(date, utcOffset, lat, lon, true);
        }
        public static DateTime? SunSet(DateTime date, double utcOffset, double lat, double lon) {
            return Calculate(date, utcOffset, lat, lon, false);
        }
        static DateTime? Calculate(DateTime date, double utcOffset, double lat, double lon, bool rise) {
            int N = date.DayOfYear;

            double lonHour = lon / 15.0;

            double t = rise ?
                N + ((6.0 - lonHour) / 24.0) :
                N + ((18.0 - lonHour) / 24.0);

            double M = (0.9856 * t) - 3.289;

            double L = M + (1.916 * Math.Sin(DegToRad(M))) + (0.020 * Math.Sin(DegToRad(2.0 * M))) + 282.634;
            L = ToRange(L, 0, 360);

            double RA = RadToDeg(Math.Atan(0.91764 * Math.Tan(DegToRad(L))));
            RA = ToRange(RA, 0, 360);

            double Lquadrant = (Math.Floor(L / 90.0)) * 90.0;
            double RAquadrant = (Math.Floor(RA / 90.0)) * 90.0;
            RA = RA + (Lquadrant - RAquadrant);

            RA = RA / 15.0;

            double sinDec = 0.39782 * Math.Sin(DegToRad(L));
            double cosDec = Math.Cos(Math.Asin(sinDec));

            double cosH = (Math.Cos(DegToRad(Zenith)) - (sinDec * Math.Sin(DegToRad(lat)))) / (cosDec * Math.Cos(DegToRad(lat)));

            if(cosH > 1.0 || cosH < -1.0)
                return null;

            double H = rise ?
                360.0 - RadToDeg(Math.Acos(cosH)) :
                RadToDeg(Math.Acos(cosH));

            H = H / 15.0;

            double _T = H + RA - (0.06571 * t) - 6.622;

            double UT = _T - lonHour;

            UT += utcOffset;

            UT = ToRange(UT, 0, 24);

            return date.Date.AddSeconds(Math.Round(UT * 3600.0));
        }
        static double DegToRad(double angle) {
            return angle * Math.PI / 180.0;
        }
        static double RadToDeg(double angle) {
            return angle * 180.0 / Math.PI;
        }
        static double ToRange(double value, double min, double max) {
            double range = max - min;
            while(value < min)
                value += range;
            while(value >= max)
                value -= range;
            return value;
        }
    }
}
