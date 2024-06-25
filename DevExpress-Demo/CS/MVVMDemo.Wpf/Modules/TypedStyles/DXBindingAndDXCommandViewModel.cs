using DevExpress.Mvvm;
using System.Windows;

namespace MVVMDemo.TypedStylesDemo {
    public class DXBindingAndDXCommandViewModel : BindableBase {
        public string FirstName {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string LastName {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public void Register() {
            MessageBox.Show("Registered");
        }
        public DXBindingAndDXCommandViewModel() {
            FirstName = "Gregory";
            LastName = "Price";
        }
    }
}
