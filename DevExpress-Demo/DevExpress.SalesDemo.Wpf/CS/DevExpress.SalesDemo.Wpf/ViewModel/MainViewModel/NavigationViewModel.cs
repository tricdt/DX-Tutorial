using DevExpress.Mvvm.POCO;

namespace DevExpress.SalesDemo.Wpf.ViewModel {
    public class NavigationViewModel {
        public static NavigationViewModel Create() {
            return ViewModelSource.Create(() => new NavigationViewModel());
        }
        protected NavigationViewModel() { }
    }
}
