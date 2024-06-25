using System;

namespace DevExpress.SalesDemo.Model {
    public class SalesForecastMaker {
        public static decimal GetForecast(decimal currentSales, decimal elapsedPeriodPart) {
            return elapsedPeriodPart == 0 ? currentSales : currentSales / elapsedPeriodPart;
        }
        public static decimal GetYtdForecast(decimal currentSales) {
            DateTimeRange ytdRange = DateTimeUtils.GetYtdRange();
            decimal wholeIntervalLength = ytdRange.End.Ticks - ytdRange.Start.Ticks;
            decimal elapsedTimeIntervalLength = DateTime.Today.Ticks - ytdRange.Start.Ticks;
            decimal elapsedPeriodPart = wholeIntervalLength == 0 ? 0 : elapsedTimeIntervalLength / wholeIntervalLength;
            return GetForecast(currentSales, elapsedPeriodPart);
        }
    }
}
