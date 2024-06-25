using MVVMDemo.ViewModelBaseDemo;
using DevExpress.Xpf.DemoBase;

namespace MVVMDemo {
    public partial class ViewModelBaseModule : MVVMDemoModule {
        protected override IShowcaseInfo[] CreateShowcases() {
            const string path = "Modules/ViewModelBase";
            const string uri = "17351/Common-Concepts/MVVM-Framework/ViewModels/ViewModelBase";
            return new ShowcaseInfo[] {
                LoadShowcase("Overview", uri, path, new[] { typeof(LoginViewModel), typeof(LoginView) }),
                LoadShowcase("Automatic Commands Creation", uri + "#Feature5", path, new[] { typeof(AutoCommandsViewModel), typeof(AutoCommandsView) }),
                LoadShowcase("Manual Commands Creation", uri + "#Feature5", path, new[] { typeof(ManualCommandsViewModel), typeof(ManualCommandsView) }),
                LoadShowcase("Services", uri + "#Feature2", path, new[] { typeof(ServicesViewModel), typeof(ServicesView) }),
                LoadShowcase("IDataErrorInfo", null, path, new[] { typeof(DataErrorInfoViewModel), typeof(DataErrorInfoView) }),
            };
        }
    }
}
