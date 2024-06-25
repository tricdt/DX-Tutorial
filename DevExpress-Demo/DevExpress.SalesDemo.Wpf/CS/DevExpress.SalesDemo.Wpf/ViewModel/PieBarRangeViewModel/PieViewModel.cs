using DevExpress.Mvvm.POCO;

namespace DevExpress.SalesDemo.Wpf.ViewModel {
    public class PieViewModel : BarViewModel {
        public new static PieViewModel Create() {
            return ViewModelSource.Create(() => new PieViewModel());
        }
        protected PieViewModel() { }
    }
}
