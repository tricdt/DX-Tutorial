using DevExpress.Mvvm;
using System;
using System.Globalization;

namespace GanttDemo.Examples {
    #region !
    public class CustomDateTimeRangeFormatter : IFormatProvider, ICustomFormatter {
        public string Format(string format, object arg, IFormatProvider formatProvider) {
            if(arg is DateTimeRange) {
                var range = (DateTimeRange)arg;
                var culture = CultureInfo.CurrentCulture;
                int weekNum = culture.Calendar.GetWeekOfYear(
                    range.Start,
                    culture.DateTimeFormat.CalendarWeekRule,
                    culture.DateTimeFormat.FirstDayOfWeek);
                return string.Format("Week {0}", weekNum);
            }
            return null;
        }

        public object GetFormat(Type formatType) {
            if(formatType == typeof(ICustomFormatter))
                return this;
            return CultureInfo.CurrentCulture.GetFormat(formatType); ;
        }
    }
    #endregion
}
