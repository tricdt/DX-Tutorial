using System;
using System.Collections.Generic;
using System.Linq;

namespace SchedulingDemo.ViewModels {
    public class DayViewDemoViewModel {
        public virtual DateTime Start { get; set; }
        public IEnumerable<WorkCalendar> Calendars { get { return WorkData.Calendars; } }
        public IEnumerable<WorkAppointment> Appointments { get { return WorkData.Appointments; } }

        public DayViewDemoViewModel() {
            Start = WorkData.TodayStart;
            Init();
        }
        void Init() {
            WorkData.Calendars.ToList().ForEach(x => x.IsVisible = false);
            WorkData.CalendarsSupport.ToList().ForEach(x => x.IsVisible = true);
        }
    }
}
