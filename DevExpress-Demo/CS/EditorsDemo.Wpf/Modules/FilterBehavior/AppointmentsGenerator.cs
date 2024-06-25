using System;
using System.Collections.Generic;
using GridDemo;

namespace EditorsDemo.FilterBehavior {
    public class AppointmentsGenerator {
        public static List<Appointment> CreateAppointments() {
            return new AppointmentsGenerator().Generate();
        }
        List<Appointment> appointments;
        PersonGenerator generator;
        int labelCount = 11;
        List<Appointment> Generate() {
            generator = new PersonGenerator();
            appointments = new List<Appointment>();
            AddAppointmentLayer(1, 0);
            AddAppointmentLayer(2, 0);
            AddAppointmentLayer(1, 1);
            AddAppointmentLayer(2, 1);
            return appointments;
        }
        void AddAppointmentLayer(int hours, int day) {
            for(int i = 0; i < 24; i += hours) {
                var person = generator.CreatePerson();
                appointments.Add(new Appointment() {
                    Subject = person.FullName,
                    Location = person.City,
                    Description = person.JobTitle,
                    Start = DateTime.Today.AddDays(day).AddHours(i),
                    End = DateTime.Today.AddDays(day).AddHours(i + hours),
                    LabelId = (i + hours * (10 + 4 * day)) % labelCount
                });
            }
        }
    }
    public class Appointment {
        public string Subject { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public object LabelId { get; set; }
    }
}
