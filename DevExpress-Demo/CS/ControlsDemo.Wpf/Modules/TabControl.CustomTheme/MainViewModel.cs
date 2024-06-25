using DevExpress.DemoData.Models;
using DevExpress.Mvvm.POCO;
using System.Collections.Generic;
using System.Linq;

namespace ControlsDemo.TabControl.CustomTheme {
    public class MainViewModel {
        public static MainViewModel Create() {
            return ViewModelSource.Create(() => new MainViewModel());
        }
        protected MainViewModel() {
            if(this.IsInDesignMode()) return;
            Employees = NWindContext.Create().Employees.ToList();
        }
        
        public virtual List<Employee> Employees { get; protected set; }
    }
}
