using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SchedulingDemo.ViewModels {
    public class ReceptionDeskDemoViewModel {
        protected ReceptionDeskDemoViewModel() {
            SelectedDoctors = new List<object>();
            SelectedDoctors.AddRange(Doctors.Take(5));
        }

        public DateTime StartDate { get { return ReceptionDeskData.BaseDate; } }
        public virtual ObservableCollection<Doctor> Doctors { get { return ReceptionDeskData.Doctors; } }
        public virtual List<object> SelectedDoctors { get; set; }
        public virtual ObservableCollection<MedicalAppointment> MedicalAppointments { get { return ReceptionDeskData.Appointments; } }
        public virtual ObservableCollection<HospitalDepartment> HospitalDepartments { get { return ReceptionDeskData.Departments; } }
        public virtual ObservableCollection<Patient> Patients { get { return ReceptionDeskData.Patients; } }
        public virtual ObservableCollection<AppointmentLocation> Locations { get { return ReceptionDeskData.AppointmentLocations; } }
        public virtual ObservableCollection<PaymentState> PaymentStates { get { return ReceptionDeskData.PaymentStates; } }
    }
}
