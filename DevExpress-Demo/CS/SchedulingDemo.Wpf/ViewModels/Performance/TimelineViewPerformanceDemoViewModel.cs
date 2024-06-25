using System;
using System.Collections.ObjectModel;

namespace SchedulingDemo.ViewModels {
    public class TimelineViewPerformanceDemoViewModel {
        static readonly SampleDataGenerator data;
        static TimelineViewPerformanceDemoViewModel() {
            data = new SampleDataGenerator(DateTime.Today.AddDays(-20), true);
            data.SetUp(100, 12, 50);
        }
        protected TimelineViewPerformanceDemoViewModel() {
            AppointmentsPerDays = new ObservableCollection<int>() { 50, 100, 200, 500, 1000 };
            AppointmentsPerDay = data.AppointmentsPerDay;
            DayCount = data.DayCount;
            ResourceCount = data.ResourceCount;
        }

        public virtual ObservableCollection<AppointmentData> Appointments { get { return data.Appointments; } }
        public virtual ObservableCollection<ResourceData> Resources { get { return data.Resources; } }
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
            data.SetUp(DayCount, ResourceCount, AppointmentsPerDay);
        }
    }
}
