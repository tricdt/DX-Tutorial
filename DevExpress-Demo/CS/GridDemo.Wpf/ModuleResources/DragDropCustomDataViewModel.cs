using System.Collections.ObjectModel;
using DevExpress.DemoData.Models;

namespace GridDemo {
    public class DragDropCustomDataViewModel {
        public DragDropCustomDataViewModel() {
            var provider = new SchedulerTaskProvider();
            Inbox = new ObservableCollection<SchedulerTask>(provider.Inbox);
            Appointments = new ObservableCollection<Appointment>(provider.Appointments);
            Employees = new ObservableCollection<Employee>(provider.Employees);
            AppointmentTypes = new ObservableCollection<AppointmentType>(provider.AppointmentTypes);
        }
        public ObservableCollection<SchedulerTask> Inbox { get; private set; }
        public ObservableCollection<Appointment> Appointments { get; private set; }
        public ObservableCollection<Employee> Employees { get; private set; }
        public ObservableCollection<AppointmentType> AppointmentTypes { get; private set; }
    }
}
