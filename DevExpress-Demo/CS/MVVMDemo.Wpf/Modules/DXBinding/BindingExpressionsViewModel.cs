using System.Windows;

namespace MVVMDemo.DXBindingDemo {
    public class BindingExpressionsViewModel {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

        public virtual bool IsReadonly { get; set; }

        public virtual string UserName { get; set; }
        public virtual bool AcceptTerms{ get; set; }
        public void Register() {
            MessageBox.Show("Registered");
        }

        protected BindingExpressionsViewModel() {
            FirstName = "Gregory";
            LastName = "Price";
        }

        
    }
}
