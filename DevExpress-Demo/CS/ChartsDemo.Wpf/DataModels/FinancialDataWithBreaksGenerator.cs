using System;
using System.Collections.Generic;

namespace ChartsDemo {

    class FinancialDataWithBreaksGenerator {
        const double StartPrice = 26;
        const double MaxPrice = 100;
        const double MinPrice = 5;
        const int StartWorkingHour = 8;
        const int EndWorkingHour = 18;
        const int Holiday1Day = 1;
        const int Holiday1Month = 1;
        const int Holiday2Day = 1;
        const int Holiday2Month = 5;
        const DayOfWeek Weekend1 = DayOfWeek.Saturday;
        const DayOfWeek Weekend2 = DayOfWeek.Sunday;

        bool GeneratePoint(DateTime dateTime, double previousClose, Random random, out double newPreviousClose, out FinancialDataPoint point) {
            if (dateTime.Hour < StartWorkingHour
                || dateTime.Hour >= EndWorkingHour
                || dateTime.DayOfWeek == Weekend1
                || dateTime.DayOfWeek == Weekend2
                || (dateTime.Day == Holiday1Day && dateTime.Month == Holiday1Month)
                || (dateTime.Day == Holiday2Day && dateTime.Month == Holiday2Month)) {
                newPreviousClose = double.NaN;
                point = new FinancialDataPoint();
                return false;
            }
            double open;
            if (dateTime.Hour == StartWorkingHour)
                open = Math.Round(previousClose + (random.NextDouble() - 0.5) / 2d, 2);
            else
                open = Math.Round(previousClose, 2);
            double close = Math.Round(open + (random.NextDouble() - 0.5) / 5d, 2);
            if (close > MaxPrice)
                close = Math.Round(0.8 * close, 2);
            if (close <= MinPrice)
                close = Math.Round(2 * MinPrice - close, 2);
            double high = Math.Round(Math.Max(open, close) + random.NextDouble() / 5d, 2);
            double low = Math.Round(Math.Min(open, close) - random.NextDouble() / 5d, 2);
            double volume = Math.Round((random.NextDouble() + 0.1) * 1000d, 2);
            newPreviousClose = close;
            point = new FinancialDataPoint(dateTime, open, high, low, close, volume);
            return true;
        }

        internal List<FinancialDataPoint> Generate() {
            Random random = new Random(28);
            List<FinancialDataPoint> points = new List<FinancialDataPoint>();
            int startYear = DateTime.Now.Year - 3;
            DateTime currentDateTime = new DateTime(startYear, 1, 2, 8, 0, 0);
            DateTime endDateTime = new DateTime(startYear + 3, 1, 1, 0, 0, 0);
            double previousClose = StartPrice;
            while (currentDateTime < endDateTime) {
                FinancialDataPoint point = new FinancialDataPoint();
                double newPreviousClose;
                bool generated = GeneratePoint(currentDateTime, previousClose, random, out newPreviousClose, out point);
                if (generated) {
                    previousClose = newPreviousClose;
                    points.Add(point);
                }
                currentDateTime = currentDateTime.AddHours(1);
            }
            return points;
        }
    }

}
