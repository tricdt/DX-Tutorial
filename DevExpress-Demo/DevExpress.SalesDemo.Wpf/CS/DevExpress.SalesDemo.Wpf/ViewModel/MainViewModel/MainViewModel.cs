using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Windows;

namespace DevExpress.SalesDemo.Wpf.ViewModel {
    public class MainViewModel {
        public static MainViewModel Create() {
            return ViewModelSource.Create(() => new MainViewModel());
        }
        protected MainViewModel() {
            LoginViewModel = LoginViewModel.Create();
            NavigationViewModel = NavigationViewModel.Create();
        }

        public virtual LoginViewModel LoginViewModel { get; protected set; }
        public virtual NavigationViewModel NavigationViewModel { get; protected set; }
        protected virtual IDialogService DialogService { get { return null; } }
        public void ShowLoginView() {
            var vm = LoginViewModel.Clone(LoginViewModel);
            this.GetService<IDialogService>().ShowDialog(MessageButton.OK, ApplicationSettings.Default.MainWindowTitle, vm);
            LoginViewModel = vm;
        }
    }
}
