using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System.Windows;

namespace MVVMDemo.POCOViewModels {
    public class ChangeNotificationsViewModel {
        [BindableProperty(OnPropertyChangedMethodName = "OnNameChanged")]
        public virtual string FirstName { get; set; }

        [BindableProperty(OnPropertyChangedMethodName = "OnNameChanged")]
        public virtual string LastName { get; set; }

        public string FullName { get { return FirstName + " " + LastName; } }

        protected void OnNameChanged() {
            this.RaisePropertyChanged(x => x.FullName);
            this.RaiseCanExecuteChanged(x => x.Register());
        }
        
        [Command(UseCommandManager = false)]
        public void Register() {
            MessageBox.Show("Registered");
        }
        public bool CanRegister() {
            return !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName);
        }
    }
}
