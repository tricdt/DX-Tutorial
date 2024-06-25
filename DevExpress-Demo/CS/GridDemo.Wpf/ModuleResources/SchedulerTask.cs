using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.DemoData;
using DevExpress.DemoData.Models;

namespace GridDemo {
    public class SchedulerTask {
        public string Subject { get; set; }
        public IssuePriority Priority { get; set; }
        public TimeSpan Duration { get; set; }
        public int TotalHours { get { return (int)Math.Ceiling(Duration.TotalHours); } }
        public string Description { get; set; }
        public string Name { get { return !string.IsNullOrEmpty(Description) ? Description : Subject; } }
        public bool AllDay { get; set; }

        public override string ToString() {
            return Subject;
        }
    }

    public class Appointment {
        public int Id { get; set; }
        public IssuePriority LabelId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public long OwnerId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public bool AllDay { get; set; }
    }

    public class AppointmentType {
        public IssuePriority Priority { get; set; }
        public string Caption { get { return Priority.ToString(); } }
    }

    public class SchedulerTaskProvider {
        public IList<SchedulerTask> Inbox { get; protected set; }
        public IList<Employee> Employees { get; protected set; }
        public IList<AppointmentType> AppointmentTypes { get; protected set; }
        public IList<Appointment> Appointments { get; protected set; }

        public SchedulerTaskProvider() {
            GenerateData();
        }
        void GenerateData() {
            const int appointmentsCount = 4;
            string[] inboxTaskDescriptions = GetAllSubjects().Skip(appointmentsCount).ToArray();
            string[] appointmentsDescriptions = GetAllSubjects().Take(appointmentsCount).ToArray();
            Inbox = GenerateSchedulerTasks(inboxTaskDescriptions);
            Employees = GenerateEmployees();
            Appointments = GenerateAppointments(appointmentsDescriptions, GetOwnerIds(Employees));
            AppointmentTypes = GenerateAppointmentTypes();
        }

        static IEnumerable<string> GetAllSubjects() {
            return OutlookDataGenerator.Subjects;
        }
        static IList<SchedulerTask> GenerateSchedulerTasks(string[] descriptions) {
            var severityRandom = new Random();
            var priorityRandom = new Random();
            var durationRandom = new Random();
            return descriptions.Select(d => CreateSchedulerTask(d, severityRandom, priorityRandom, durationRandom)).ToList();
        }
        static SchedulerTask CreateSchedulerTask(string description, Random severityRandom, Random priorityRandom, Random durationRandom) {
            return new SchedulerTask {
                Subject = GetSubject(description),
                Priority = GetRandomEnumValue<IssuePriority>(priorityRandom),
                Duration = TimeSpan.FromMinutes(durationRandom.Next(1, 5) * 60),
                Description = description
            };
        }
        static string GetSubject(string description) {
            if(!String.IsNullOrWhiteSpace(description)) {
                int charLocation = description.IndexOf('.');
                if(charLocation > 0)
                    return description.Substring(0, charLocation);
            }
            return String.Empty;
        }
        static T GetRandomEnumValue<T>(Random random) {
            Array values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(random.Next(values.Length));
        }
        static IList<Employee> GenerateEmployees() {
            const int resourceCount = 3;
            return NWindDataProvider.Employees.Take(resourceCount).ToList();
        }
        static long[] GetOwnerIds(IEnumerable<Employee> employees) {
            return employees.Select(x => x.EmployeeID).ToArray();
        }
        static IList<AppointmentType> GenerateAppointmentTypes() {
            var appointmentType = new List<AppointmentType>();
            appointmentType.Add(new AppointmentType { Priority = IssuePriority.Low });
            appointmentType.Add(new AppointmentType { Priority = IssuePriority.High });
            appointmentType.Add(new AppointmentType { Priority = IssuePriority.Medium });
            return appointmentType;
        }
        static IList<Appointment> GenerateAppointments(string[] descriptions, long[] ownerIds) {
            var appointments = new List<Appointment>();
            appointments.Add(new Appointment {
                Id = 0,
                Subject = GetSubject(descriptions[0]),
                Description = descriptions[0],
                OwnerId = ownerIds[0],
                LabelId = IssuePriority.Low,
                Start = GetTime(9, 30),
                End = GetTime(10, 30),
            });
            appointments.Add(new Appointment {
                Id = 1,
                Subject = GetSubject(descriptions[1]),
                Description = descriptions[1],
                OwnerId = ownerIds[1],
                LabelId = IssuePriority.Medium,
                Start = GetTime(9, 30),
                End = GetTime(10, 30),
            });
            appointments.Add(new Appointment {
                Id = 2,
                Subject = GetSubject(descriptions[2]),
                Description = descriptions[2],
                OwnerId = ownerIds[1],
                LabelId = IssuePriority.Low,
                Start = GetTime(11, 0),
                End = GetTime(12, 0),
            });
            appointments.Add(new Appointment {
                Id = 3,
                Subject = GetSubject(descriptions[3]),
                Description = descriptions[3],
                OwnerId = ownerIds[2],
                LabelId = IssuePriority.High,
                Start = GetTime(9, 30),
                End = GetTime(12, 0)
            });
            return appointments;
        }
        static DateTime GetTime(int hours, int minutes) {
            return DateTime.Today.AddHours(hours).AddMinutes(minutes);
        }
    }
}
