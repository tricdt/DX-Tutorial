using System;
using System.Collections.Generic;

namespace SchedulingDemo.ViewModels {
    public class OneMonthViewDemoViewModel {
        public virtual DateTime Start { get; set; }
        public IEnumerable<EventCalendar> Calendars { get { return EventDataHelper.Calendars; } }
        public IEnumerable<EventData> Appointments { get { return EventDataHelper.Events; } }

        public OneMonthViewDemoViewModel() {
            Start = EventDataHelper.Start;
        }
    }
}
