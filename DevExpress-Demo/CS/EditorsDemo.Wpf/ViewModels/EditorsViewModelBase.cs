using System.Collections.Generic;
using DevExpress.DemoData;
using DevExpress.DemoData.Models;
using DevExpress.Mvvm;
using DevExpress.Xpf.DemoBase.DataClasses;
using Employee = DevExpress.Xpf.DemoBase.DataClasses.Employee;

namespace EditorsDemo {
    public abstract class EditorsViewModelBase : ViewModelBase {

        protected static NWindDataLoader DataLoader;
        static EditorsViewModelBase() {
            DataLoader = new NWindDataLoader();
        }

        public EditorsViewModelBase() {
            Employees = new List<Employee>(EmployeesWithPhotoData.DataSource);
            Products = new List<Product>((IList<Product>)DataLoader.Products);
        }
        public IList<Employee> Employees { get; private set; }
        public IList<Product> Products { get; private set; }


    }
}
