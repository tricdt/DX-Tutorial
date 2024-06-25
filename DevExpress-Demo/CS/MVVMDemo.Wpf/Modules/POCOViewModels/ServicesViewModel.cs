using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace MVVMDemo.POCOViewModels {
    public class ServicesViewModel {
        public virtual string UserName { get; set; }

        #region !
        IMessageBoxService MessageBoxService { get { return this.GetRequiredService<IMessageBoxService>(); } }

        public void Login() {
            MessageBoxService.ShowMessage("User: " + UserName, "Login", MessageButton.OK, MessageIcon.Information);
        }
        #endregion

        public bool CanLogin() {
            return !string.IsNullOrEmpty(UserName);
        }
    }
}
