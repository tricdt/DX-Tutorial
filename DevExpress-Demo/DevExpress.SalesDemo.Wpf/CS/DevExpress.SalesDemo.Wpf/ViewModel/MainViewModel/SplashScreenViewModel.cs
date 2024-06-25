using DevExpress.Mvvm.POCO;

namespace DevExpress.SalesDemo.Wpf.ViewModel {
    public class ProgressSplashScreenViewModel {
        public static ProgressSplashScreenViewModel Create(string caption, string copyright) {
            return ViewModelSource.Create(() => new ProgressSplashScreenViewModel(caption, copyright));
        }
        protected ProgressSplashScreenViewModel(string caption, string copyright) {
            Caption = caption;
            Copyright = copyright;
        }

        public string Caption { get; private set; }
        public string Copyright { get; private set; }
    }
}
