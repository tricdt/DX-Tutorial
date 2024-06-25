using DevExpress.Mvvm.POCO;
using DevExpress.SalesDemo.Wpf.Helpers;
using System.Windows.Media.Imaging;

namespace DevExpress.SalesDemo.Wpf.ViewModel {
    public class LoginViewModel {
        public static LoginViewModel Create() {
            return ViewModelSource.Create(() => new LoginViewModel());
        }
        public static LoginViewModel Clone(LoginViewModel vm) {
            var res = Create();
            res.UserName = vm.UserName;
            return res;
        }
        protected LoginViewModel() {
            Icon = ResourceImageHelper.GetResourceImage("User.png");
            UserName = "John Smith";
        }

        public virtual BitmapImage Icon { get; protected set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
    }
}
