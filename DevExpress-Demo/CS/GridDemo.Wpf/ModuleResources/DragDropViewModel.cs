using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Xpf.DemoBase.DataClasses;

namespace GridDemo {
    public class DragDropViewModel {
        public DragDropViewModel() {
            const int threshold = 9;
            ActiveEmployees = new ObservableCollection<Employee>(EmployeesData.DataSource.Skip(threshold));
            NewEmployees = new ObservableCollection<Employee>(EmployeesData.DataSource.Take(threshold));
        }

        public ObservableCollection<Employee> ActiveEmployees { get; set; }
        public ObservableCollection<Employee> NewEmployees { get; set; }
    }
}
