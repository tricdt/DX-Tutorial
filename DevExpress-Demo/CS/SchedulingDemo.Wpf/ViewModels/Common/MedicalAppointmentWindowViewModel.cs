using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Scheduling;
using System.Collections.ObjectModel;
using System.Linq;

namespace SchedulingDemo {
    public class MedicalAppointmentWindowViewModel : AppointmentWindowViewModel {
        public static MedicalAppointmentWindowViewModel Create(AppointmentItem appointmentItem, SchedulerControl scheduler) {
            return ViewModelSource.Create(() => new MedicalAppointmentWindowViewModel(appointmentItem, scheduler));
        }
        protected MedicalAppointmentWindowViewModel(AppointmentItem appointmentItem, SchedulerControl scheduler) : base(appointmentItem, scheduler) {
            patient = Patients.FirstOrDefault(x => x.Id.Equals(CustomFields["PatientId"]));;
        }

        public ObservableCollection<Patient> Patients { get { return ReceptionDeskData.Patients; } }
        public ObservableCollection<AppointmentLocation> Locations { get { return ReceptionDeskData.AppointmentLocations; } }

        Patient patient;
        [BindableProperty]
        public virtual Patient Patient {
            get { return patient; }
            set {
                Patient newPatient = value;
                if (patient == newPatient)
                    return;
                patient = newPatient;
                CustomFields["PatientId"] = newPatient.Id;
                Subject = newPatient.Name;
            }
        }
     }
}
