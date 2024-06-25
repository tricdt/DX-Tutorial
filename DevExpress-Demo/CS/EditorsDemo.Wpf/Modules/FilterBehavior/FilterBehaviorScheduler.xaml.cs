using System.Windows.Controls;

namespace EditorsDemo.FilterBehavior {
    public partial class FilterBehaviorScheduler : UserControl {
        public FilterBehaviorScheduler() {
            InitializeComponent();
            scheduler.DataSource.AppointmentsSource = AppointmentsGenerator.CreateAppointments();
        }
    }
}
