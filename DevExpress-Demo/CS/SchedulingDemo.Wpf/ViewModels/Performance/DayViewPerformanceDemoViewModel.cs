using System;
using System.Collections.ObjectModel;

namespace SchedulingDemo.ViewModels {
    public class DayViewPerformanceDemoViewModel {
        static readonly SampleDataGenerator data;
        static DayViewPerformanceDemoViewModel() {
            data = new SampleDataGenerator();
            data.SetUp(10, 15, 500);
        }
        protected DayViewPerformanceDemoViewModel() {
            AppointmentsPerDays = new ObservableCollection<int>() { 50, 100, 200, 500, 1000 };
            AppointmentsPerDay = data.AppointmentsPerDay;
            DayCount = 2;
            ResourceCount = data.ResourceCount;
            Resources = data.Resources;
        }

        public virtual ObservableCollection<AppointmentData> Appointments { get; set; }
        public virtual ObservableCollection<ResourceData> Resources { get; set; }
        public virtual ObservableCollection<int> AppointmentsPerDays { get; protected set; }
        public virtual int AppointmentsPerDay { get; set; }
        public virtual int DayCount { get; set; }
        public virtual int ResourceCount { get; set; }

        protected void OnAppointmentsPerDayChanged() {
            SetUp();
        }
        protected void OnDayCountChanged() {
            SetUp();
        }
        protected void OnResourceCountChanged() {
            SetUp();
        }
        void SetUp() {
            Resources = null;
            Appointments = null;
            data.SetUp(Math.Max(data.DayCount, DayCount), ResourceCount, AppointmentsPerDay);
            Resources = data.Resources;
            Appointments = data.Appointments;
        }
    }
}
