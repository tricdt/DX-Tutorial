using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ProductsDemo {
    public static class DateTimeUtils {
        public static DateTimeRange GetDayRange(DateTime date) {
            DateTime startOfDate = new DateTime(date.Year, date.Month, date.Day);
            DateTime endOfToday = startOfDate.AddDays(1).AddTicks(-1);
            return new DateTimeRange(startOfDate, endOfToday);
        }
        public static DateTimeRange GetLastWeekRange() {
            DayOfWeek firstDay = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            DateTime today = DateTime.Today;
            DateTime startOfWeek = today;
            while(startOfWeek.DayOfWeek != firstDay) {
                startOfWeek = startOfWeek.AddDays(-1);
            }
            DateTime endOfWeek = startOfWeek.AddDays(7).AddTicks(-1);
            return new DateTimeRange(startOfWeek, endOfWeek);
        }
        public static DateTimeRange GetMonthRange(DateTime date) {
            DateTime startOfMonth = new DateTime(date.Year, date.Month, 1);
            int daysInCurrentMonth = DateTime.DaysInMonth(date.Year, date.Month);
            DateTime endOfMonth = startOfMonth.AddDays(daysInCurrentMonth).AddTicks(-1);
            return new DateTimeRange(startOfMonth, endOfMonth);
        }
        public static DateTimeRange GetYtdRange() {
            DateTime today = DateTime.Today;
            DateTime startOfYear = new DateTime(today.Year, 1, 1);
            DateTime endOfYear = new DateTime(today.Year, 12, 31);
            return new DateTimeRange(startOfYear, endOfYear);
        }
        public static DateTimeRange GetOneYearRange() {
            return new DateTimeRange(DateTime.Today.AddYears(-1), DateTime.Today);
        }

        public static DateTimeRange GetLastYearRange() {
            DateTime dateInLastYear = DateTime.Today.AddYears(-1);
            DateTime startOfYear = new DateTime(dateInLastYear.Year, 1, 1);
            DateTime endOfYear = startOfYear.AddYears(1).AddTicks(-1);
            return new DateTimeRange(startOfYear, endOfYear);
        }
        public static int GetLastYear() {
            return DateTime.Today.AddYears(-1).Year;
        }
        public static object GetCurrentYear() {
            return DateTime.Now.Year;
        }
    }

    public struct DateTimeRange {
        DateTime start;
        DateTime end;

        public DateTime Start {
            get { return start; }
        }
        public DateTime End {
            get { return end; }
        }

        public DateTimeRange(DateTime start, DateTime end) {
            this.start = start;
            this.end = end;
        }
    }

    public struct DecimalRange {
        decimal start;
        decimal end;

        public decimal Start {
            get { return start; }
        }
        public decimal End {
            get { return end; }
        }

        public DecimalRange(decimal start, decimal end) {
            this.start = start;
            this.end = end;
        }
    }

    public static class SalesRangeProvider {
        public static DecimalRange GetBadSalesRange() {
            return new DecimalRange(0, 200000000M);
        }
        public static DecimalRange GetNormalSalesRange() {
            return new DecimalRange(0, 400000000M);
        }
        public static DecimalRange GetGoodSalesRange() {
            return new DecimalRange(0, 600000000M);
        }
    }
}
