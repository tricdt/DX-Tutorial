using DevExpress.Mvvm;
using DevExpress.Xpf.Scheduling;
using System.Collections.ObjectModel;

namespace SchedulingDemo.ViewModels {
    public class WindowsCustomizationDemoViewModel {
        readonly GymData data = new GymData(14);
        protected WindowsCustomizationDemoViewModel() { }

        public virtual ObservableCollection<GymEvent> GymEvents { get { return this.data.GymEvents; } }
        public virtual ObservableCollection<Trainer> Trainers { get { return this.data.Trainers; } }
        public virtual ObservableCollection<Training> Trainings { get { return this.data.Trainings; } } 

        public void InitNewAppointment(AppointmentItem apt) {
            apt.ResourceId = apt.LabelId;
        }
    }
}
