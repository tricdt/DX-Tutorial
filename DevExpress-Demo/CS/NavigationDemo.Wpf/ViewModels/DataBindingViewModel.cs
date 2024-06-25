using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Xpf.DemoBase.DataClasses;
using NavigationDemo.Utils;

namespace NavigationDemo {
    public class DataBindingViewModel {
        public ReadOnlyCollection<EmployeeDepartment> Departments { get; private set; }
        public virtual object SelectedItem { get; set; }

        public DataBindingViewModel() {
            var departments = EmployeesWithPhotoData.DataSource
                .GroupBy(x => x.GroupName)
                .Select(x => CreateEmployeeDepartment(x.Key, x.Take(10).ToArray()))
                .ToArray();
            Departments = new ReadOnlyCollection<EmployeeDepartment>(departments);
            SelectedItem = Departments[1].Employees[0];
        }

        static EmployeeDepartment CreateEmployeeDepartment(string name, IList<Employee> employees) {
            var image = ImageHelper.GetEmployeeDepartmentImage(name);
            return new EmployeeDepartment(name, image, employees);
        }
    }
    public class EmployeeDepartment {
        public string Name { get; private set; }
        public Uri SvgImage { get; private set; }
        public ReadOnlyCollection<Employee> Employees { get; private set; }

        public EmployeeDepartment(string name, Uri svgImage, IList<Employee> employees) {
            this.Name = name;
            this.SvgImage = svgImage;
            this.Employees = new ReadOnlyCollection<Employee>(employees);
        }

        public override string ToString() {
            return Name;
        }
    }
}
