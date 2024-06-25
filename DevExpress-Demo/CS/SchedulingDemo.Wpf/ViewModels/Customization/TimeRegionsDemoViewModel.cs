using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Scheduling;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SchedulingDemo.ViewModels {
    public class TimeRegionsDemoViewModel {        
        public const string AnyResource = "(Any)";

        readonly TimeSpan regionDuration = TimeSpan.FromHours(3);        

        DateTime regionStart;
        int timeRegionNumber = 0;

        public virtual DateTime Start { get; set; }
        public IEnumerable<WorkCalendar> Calendars { get { return WorkData.Calendars; } }
        public IEnumerable<WorkAppointment> Appointments { get { return WorkData.Appointments; } }
        public virtual ObservableCollection<TimeRegionViewModel> TimeRegions { get; protected set; }
        public virtual TimeRegionViewModel SelectedTimeRegion { get; set; }
        public virtual IList<string> Resources { get; protected set; }

        public TimeRegionsDemoViewModel() {
            Start = WorkData.TodayStart;
            TimeRegions = new ObservableCollection<TimeRegionViewModel>();
            regionStart = Start.AddHours(9);

            Init();
        }
        void Init() {            
            WorkData.Calendars.ToList()
                .ForEach(x => x.IsVisible = false);
            WorkData.CalendarConferenceRoom.IsVisible = true;
            WorkData.CalendarTrainingRoom.IsVisible = true;
            Resources = WorkData.CalendarsRooms.Select(x => x.Name).ToList();
            Resources.Insert(0, AnyResource);

            DateTime customRegionStart = Start.AddDays(1).AddHours(9);
            AddCustomTimeRegion("Predefined TimeRegion1", customRegionStart, customRegionStart.AddMinutes(45), DefaultBrushNames.TimeRegion1Hatch);
            customRegionStart = Start.AddDays(1).AddHours(13);
            AddCustomTimeRegion("Predefined TimeRegion2", customRegionStart, customRegionStart.AddMinutes(15), DefaultBrushNames.TimeRegion8Dotted);
            customRegionStart = Start.AddDays(2).AddHours(11);
            AddCustomTimeRegion("Predefined TimeRegion3", customRegionStart, customRegionStart.AddMinutes(75), DefaultBrushNames.TimeRegion2Hatch, new long[] { 1 });
            customRegionStart = Start.AddDays(3).AddHours(10);
            AddCustomTimeRegion("Predefined TimeRegion4", customRegionStart, customRegionStart.AddHours(5), DefaultBrushNames.TimeRegion3Hatch);
            SelectedTimeRegion = TimeRegions[0];
        }

        void AddCustomTimeRegion(string caption, DateTime start, DateTime end, string brushName, IEnumerable<long> resourceIds = null) {
            var res = TimeRegionViewModel.Create();
            res.Caption = caption;
            res.Start = start;
            res.End = end;
            res.BrushName = brushName;
            res.ResourceIds = resourceIds;
            TimeRegions.Add(res);
        }
        void AddNewTimeRegionInternal(string caption) {
            var res = TimeRegionViewModel.Create();
            res.Caption = caption;
            res.Start = regionStart;
            res.End = regionStart + regionDuration;
            res.BrushName = DefaultBrushNames.TimeRegions[timeRegionNumber % DefaultBrushNames.TimeRegions.Length];
            TimeRegions.Add(res);

            timeRegionNumber++;
            regionStart += regionDuration;

            SelectedTimeRegion = res;
        }
        public void AddNewTimeRegion(string caption = null) {
            if (String.IsNullOrEmpty(caption))
                caption = "TimeRegion";
            AddNewTimeRegionInternal(caption + this.timeRegionNumber);
        }
        public void RemoveSelectedTimeRegion() {
            TimeRegions.Remove(SelectedTimeRegion);
            SelectedTimeRegion = TimeRegions.FirstOrDefault();
        }
    }

    public class TimeRegionViewModel {
        public static TimeRegionViewModel Create() {
            return ViewModelSource.Create(() => new TimeRegionViewModel());
        }
        protected TimeRegionViewModel() { }
        public virtual string Caption { get; set; }
        public virtual DateTime Start { get; set; }
        public virtual DateTime End { get; set; }
        public virtual string BrushName { get; set; }
        public virtual string BorderBrushName { get { return DefaultBrushNames.Resources[DefaultBrushNames.TimeRegions.IndexOf(x => x == BrushName) % DefaultBrushNames.Resources.Length]; } }
        public virtual IEnumerable<long> ResourceIds { get; set; }
    }
}
