using System;
using System.Collections.ObjectModel;

namespace SchedulingDemo.ViewModels {
    public class MonthViewPerformanceDemoViewModel {
        static readonly SampleDataGenerator data;
        static MonthViewPerformanceDemoViewModel() {
            data = new SampleDataGenerator(DateTime.Today.AddDays(-200), true);
            data.SetUp(500, 10, 30);
        }
        protected MonthViewPerformanceDemoViewModel() {
            AppointmentsPerDays = new ObservableCollection<int>() { 10, 30, 50, 100 };
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
