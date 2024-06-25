using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm.POCO;
using DevExpress.XtraScheduler;

namespace SchedulingDemo.ViewModels {
    public class TimeRulerDemoViewModel {
        int timeRulerNumber = 0;
        protected TimeRulerDemoViewModel() {
            Start = TeamData.Start;
            Calendars = new ObservableCollection<TeamCalendar>(TeamData.Calendars);
            Appointments = new ObservableCollection<TeamAppointment>(TeamData.AllAppointments);

            TimeRulers = new ObservableCollection<TimeRulerViewModel>();
            AddNewTimeRuler("GMT", TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));
            AddNewTimeRuler("Local", TimeZoneInfo.Local);
        }
        public virtual IEnumerable<TeamCalendar> Calendars { get; protected set; }
        public virtual IEnumerable<TeamAppointment> Appointments { get; protected set; }
        public virtual DateTime Start { get; set; }
        public virtual ObservableCollection<TimeRulerViewModel> TimeRulers { get; protected set; }
        public virtual TimeRulerViewModel SelectedTimeRuler { get; set; }

        public void AddNewTimeRuler(string caption = null) {
            if (String.IsNullOrEmpty(caption))
                caption = "Ruler";
            AddNewTimeRuler(caption + this.timeRulerNumber, TimeZoneInfo.Local);
        }
        public void RemoveSelectedTimeRuler() {
            TimeRulers.Remove(SelectedTimeRuler);
            SelectedTimeRuler = TimeRulers.FirstOrDefault();
        }
        void AddNewTimeRuler(string caption, TimeZoneInfo timeZoneInfo) {
            var res = TimeRulerViewModel.Create();
            res.Caption = caption;
            res.AlwaysShowTimeDesignator = false;
            res.ShowMinutes = false;
            res.TimeZone = timeZoneInfo;
            TimeRulers.Add(res);
            this.timeRulerNumber++;
            SelectedTimeRuler = res;
        }
    }
    public class TimeRulerViewModel {
        public static TimeRulerViewModel Create() {
            return ViewModelSource.Create(() => new TimeRulerViewModel());
        }
        protected TimeRulerViewModel() { }
        public virtual string Caption { get; set; }
        public virtual TimeZoneInfo TimeZone { get; set; }
        public virtual TimeMarkerVisibility TimeMarkerVisibility { get; set; }
        public virtual bool AlwaysShowTimeDesignator { get; set; }
        public virtual bool ShowMinutes { get; set; }
    }
}
