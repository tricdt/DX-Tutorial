using DevExpress.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace SchedulingDemo.ViewModels {
    public class FilteringDemoViewModel {
        protected FilteringDemoViewModel() {
            if(ViewModelBase.IsInDesignMode)
                return;
            foreach(var x in ReceptionDeskData.Doctors)
                x.IsVisible = false;
            foreach(var x in ReceptionDeskData.DepartmentPrimaryCare.Doctors)
                x.IsVisible = true;
            foreach(var x in ReceptionDeskData.DepartmentOphthalmology.Doctors)
                x.IsVisible = true;
        }

        public DateTime StartDate { get { return ReceptionDeskData.BaseDate; } }
        public virtual ObservableCollection<Doctor> Doctors { get { return ReceptionDeskData.Doctors; } }
        public virtual ObservableCollection<MedicalAppointment> MedicalAppointments { get { return ReceptionDeskData.Appointments; } }
        public virtual ObservableCollection<HospitalDepartment> HospitalDepartments { get { return ReceptionDeskData.Departments; } }
        public virtual ObservableCollection<Patient> Patients { get { return ReceptionDeskData.Patients; } }
        public virtual ObservableCollection<AppointmentLocation> Locations { get { return ReceptionDeskData.AppointmentLocations; } }
        public virtual ObservableCollection<PaymentState> PaymentStates { get { return ReceptionDeskData.PaymentStates; } }
    }
}
