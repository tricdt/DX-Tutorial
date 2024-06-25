using DevExpress.Xpf.Scheduling;
using DevExpress.Xpf.Scheduling.Internal;
using DevExpress.XtraScheduler.Xml;
using System;
using System.Collections.ObjectModel;

namespace SchedulingDemo.ViewModels {
    public class TimeZonesDemoViewModel {
        public virtual ObservableCollection<AppointmentEntity> UtcAppointments { get; protected set; }
        public virtual ObservableCollection<AppointmentEntity> StorageAppointments { get; protected set; }
        public virtual TimeZoneInfo StorageTimeZone { get; set; }
        public virtual ObservableCollection<AppointmentEntity> Appointments { get; protected set; }

        public TimeZonesDemoViewModel() {
            UtcAppointments = new ObservableCollection<AppointmentEntity>(CreateApts(TimeZoneInfo.Utc));
            Appointments = new ObservableCollection<AppointmentEntity>(CreateApts(null));
        }
        protected void OnStorageTimeZoneChanged() {
            StorageAppointments = new ObservableCollection<AppointmentEntity>(CreateApts(StorageTimeZone));
        }

        AppointmentEntity[] CreateApts(TimeZoneInfo dbTimeZone) {
            var tz1 = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            var tz2 = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            var tz3 = TimeZoneInfo.FindSystemTimeZoneById("AUS Central Standard Time");

            var apt1 = new AppointmentEntity();
            apt1.Subject = "Regular apt";
            apt1.AppointmentType = 0;
            apt1.Start = Convert(DateTime.Today.AddDays(1).AddHours(10), dbTimeZone ?? tz1);
            apt1.End = Convert(DateTime.Today.AddDays(1).AddHours(11), dbTimeZone ?? tz1);
            apt1.TimeZoneId = tz1.Id;

            var apt2 = new AppointmentEntity();
            apt2.Subject = "All day apt";
            apt2.AppointmentType = 0;
            apt2.AllDay = true;
            apt2.Start = DateTime.Today;
            apt2.End = DateTime.Today.AddDays(1);
            apt2.TimeZoneId = tz2.Id;

            var recInfo = RecurrenceBuilder.Daily(Convert(DateTime.Today.AddHours(13), dbTimeZone ?? tz3), 10).Build();
            var apt3 = new AppointmentEntity();
            apt3.Subject = "Pattern apt";
            apt3.AppointmentType = 1;
            apt3.Start = Convert(DateTime.Today.AddHours(13), dbTimeZone ?? tz3);
            apt3.End = Convert(DateTime.Today.AddHours(14), dbTimeZone ?? tz3);
            apt3.RecurrenceInfo = recInfo.ToXml();
            apt3.TimeZoneId = tz3.Id;

            var apt4 = new AppointmentEntity();
            apt4.Subject = "Changed Occurrence";
            apt4.AppointmentType = 3;
            apt4.Start = Convert(DateTime.Today.AddDays(1).AddHours(14), dbTimeZone ?? tz3);
            apt4.End = Convert(DateTime.Today.AddDays(1).AddHours(15), dbTimeZone ?? tz3);
            apt4.RecurrenceInfo = new PatternReferenceXmlPersistenceHelper(new PatternReference(recInfo.Id, 1)).ToXml();
            apt4.TimeZoneId = tz3.Id;
            apt4.ReminderInfo = ReminderXmlHelper.ToXml(new[] { new ReminderItem() {
                TimeBeforeStart = TimeSpan.FromMinutes(30),
                AlertTime = apt4.Start - TimeSpan.FromMinutes(30)
            } }, null, (dbTimeZone ?? tz3).Id);

            return new[] { apt1, apt2, apt3, apt4 };
        }
        DateTime Convert(DateTime x, TimeZoneInfo timeZone) {
            return TimeZoneInfo.ConvertTime(x, timeZone);
        }
    }
}
