using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace MVVMDemo.Services.Navigation {
    public class DetailViewModel : ISupportParameter {
        public virtual PersonInfo Person { get; set; }

        object _Parameter;
        object ISupportParameter.Parameter {
            get { return _Parameter; }
            set {
                _Parameter = value;
                Person = (PersonInfo)_Parameter;
            }
        }

        INavigationService NavigationService { get { return this.GetRequiredService<INavigationService>(); } }
        public void Back() {
            NavigationService.GoBack();
        }
    }
}
