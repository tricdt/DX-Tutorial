using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttDemo
{
    public static class DateTimeHelper {
        public static IEnumerable<DateTime> GetHolidays(DateTime startDate, DateTime endDate) {
            if(startDate > endDate)
                yield break;

            var date = startDate.Date;
            while(date <= endDate) {
                if(IsUSHoliday(date))
                    yield return date;
                date = date.AddDays(1);
            }
        }
        static bool IsUSHoliday(DateTime date) {
            int nthWeekDay = (int)(Math.Ceiling(date.Day / 7.0d));
            DayOfWeek dayName = date.DayOfWeek;
            bool isThursday = dayName == DayOfWeek.Thursday;
            bool isFriday = dayName == DayOfWeek.Friday;
            bool isMonday = dayName == DayOfWeek.Monday;
            bool isWeekend = dayName == DayOfWeek.Saturday || dayName == DayOfWeek.Sunday;

            
            if((date.Month == 12 && date.Day == 31 && isFriday) ||
                (date.Month == 1 && date.Day == 1 && !isWeekend) ||
                (date.Month == 1 && date.Day == 2 && isMonday)) return true;

            
            if(date.Month == 1 && isMonday && nthWeekDay == 3)
                return true;

            
            if(date.Month == 2 && isMonday && nthWeekDay == 3)
                return true;

            
            if(date.Month == 5 && isMonday && date.AddDays(7).Month == 6)
                return true;

            
            if((date.Month == 7 && date.Day == 3 && isFriday) ||
                (date.Month == 7 && date.Day == 4 && !isWeekend) ||
                (date.Month == 7 && date.Day == 5 && isMonday))
                return true;

            
            if(date.Month == 9 && isMonday && nthWeekDay == 1)
                return true;

            
            if(date.Month == 10 && isMonday && nthWeekDay == 2)
                return true;

            
            if((date.Month == 11 && date.Day == 10 && isFriday) ||
                (date.Month == 11 && date.Day == 11 && !isWeekend) ||
                (date.Month == 11 && date.Day == 12 && isMonday))
                return true;

            
            if(date.Month == 11 && isThursday && nthWeekDay == 4)
                return true;

            
            if((date.Month == 12 && date.Day == 24 && isFriday) ||
                (date.Month == 12 && date.Day == 25 && !isWeekend) ||
                (date.Month == 12 && date.Day == 26 && isMonday))
                return true;

            return false;
        }
    }
}
