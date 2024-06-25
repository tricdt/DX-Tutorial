using ControlsDemo.Helpers;
using DevExpress.DemoData.Models;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace ControlsDemo.TabControl.ColorizedTabs {
    public class MainViewModel {
        public static MainViewModel Create() {
            return ViewModelSource.Create(() => new MainViewModel());
        }
        protected MainViewModel() {
            if(this.IsInDesignMode()) return;
            InitDataSource();
        }
        
        public virtual List<EmployeeWrapper> Employees { get; protected set; }
        void InitDataSource() {
            Employees =
                  NWindContext.Create().Employees.ToList()
                  .Select(x => EmployeeWrapper.Create(x)).ToList();

            int i = 0;
            foreach(var employee in Employees) {
                employee.BackgroundColor = ColorPalette.GetColor(i, 100);
                employee.BackgroundColor = ColorHelper.Brightness(employee.BackgroundColor, 0.3);
                employee.BackgroundColorOpacity = 180;
                i++;
            }
        }

    }
    public class EmployeeWrapper {
        public static EmployeeWrapper Create(Employee employee) {
            return ViewModelSource.Create(() => new EmployeeWrapper(employee));
        }
        protected EmployeeWrapper(Employee employee) {
            Employee = employee;
        }

        public Employee Employee { get; private set; }

        [BindableProperty(OnPropertyChangedMethodName = "UpdateColors")]
        public virtual Color BackgroundColor { get; set; }
        [BindableProperty(OnPropertyChangedMethodName = "UpdateColors")]
        public virtual byte BackgroundColorOpacity { get; set; }

        protected void UpdateColors() {
            var background = BackgroundColor;
            background.A = BackgroundColorOpacity;
            BackgroundColor = background;
        }
    }
}
