using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;

namespace MVVMDemo.ViewModelBaseDemo {
    public class LoginViewModel : ViewModelBase {
        public string UserName {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string Status {
            get { return GetValue<string>(); }
            private set { SetValue(value); }
        }

        [Command]
        public void Login() {
            Status = "User: " + UserName;
        }
        public bool CanLogin() {
            return !string.IsNullOrEmpty(UserName);
        }
    }
}
