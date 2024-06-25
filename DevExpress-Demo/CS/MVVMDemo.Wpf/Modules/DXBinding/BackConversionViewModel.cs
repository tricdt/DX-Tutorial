using DevExpress.Mvvm;
using System.Linq;
using System.Windows;

namespace MVVMDemo.DXBindingDemo {
    public class BackConversionViewModel {
        public virtual bool IsEnabled { get; set; }

        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        protected BackConversionViewModel() {
            FirstName = "Gregory";
            LastName = "Price";
        }
    }
}
