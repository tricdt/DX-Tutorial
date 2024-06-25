using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace MVVMDemo.POCOViewModels {
    public class LoginViewModel {
        // These properties will be converted to bindable ones
        public virtual string UserName { get; set; }
        public virtual string Status { get; protected set; }

        // LoginCommand will be created for the Login and CanLogin methods: 
        public void Login() {
            Status = "User: " + UserName;
        }
        public bool CanLogin() {
            return !string.IsNullOrEmpty(UserName);
        }

        // We recommend that you do not use public constructors to prevent creating a View Model instance without the ViewModelSource 
        protected LoginViewModel() { }

        // A helper method that uses the ViewModelSource class for creating a LoginViewModel instance
        public static LoginViewModel Create() {
            return ViewModelSource.Create(() => new LoginViewModel());
        }
    }
}
