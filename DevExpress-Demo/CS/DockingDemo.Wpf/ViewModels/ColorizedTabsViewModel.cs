using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using DevExpress.DemoData.Models;
using DevExpress.Mvvm.POCO;
using DockingDemo.Helpers;

namespace DockingDemo.ViewModels {
    public class ColorizedTabsViewModel {
        public static ColorizedTabsViewModel Create() {
            return ViewModelSource.Create(() => new ColorizedTabsViewModel());
        }
        protected ColorizedTabsViewModel() {
            if(this.IsInDesignMode()) return;
            InitDataSource();
        }
        
        public virtual List<EmployeeWrapper> Employees { get; protected set; }
        void InitDataSource() {
            Employees = NWindContext.Create().Employees.ToList().Select(x => EmployeeWrapper.Create(x)).ToList();
            int i = 0;
            foreach(var employee in Employees) {
                employee.BackgroundColor = ColorPalette.GetColor(i++);
            }
        }

    }
    public class EmployeeWrapper {
        public static EmployeeWrapper Create(DevExpress.DemoData.Models.Employee employee) {
            return ViewModelSource.Create(() => new EmployeeWrapper(employee));
        }
        protected EmployeeWrapper(DevExpress.DemoData.Models.Employee employee) {
            Employee = employee;
        }

        public DevExpress.DemoData.Models.Employee Employee { get; private set; }

        public virtual Color BackgroundColor { get; set; }
    }
}
