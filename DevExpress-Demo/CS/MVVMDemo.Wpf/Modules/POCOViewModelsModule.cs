using MVVMDemo.POCOViewModels;
using DevExpress.Xpf.DemoBase;

namespace MVVMDemo {
    public partial class POCOViewModelsModule : MVVMDemoModule {
        protected override IShowcaseInfo[] CreateShowcases() {
            const string path = "Modules/POCOViewModels";
            const string uriPrefix = "17352/Common-Concepts/MVVM-Framework/ViewModels/POCO-ViewModels#";
            return new ShowcaseInfo[] {
                LoadShowcase("Overview", uriPrefix + "Basics", path, new[] { typeof(LoginViewModel), typeof(LoginView), typeof(LoginViewModel_RuntimeGeneratedCode) }),
                LoadShowcase("Bindable Properties", uriPrefix + "BindableProperties", path, new[] { typeof(PropertiesViewModel), typeof(PropertiesView), typeof(PropertiesViewModel_RuntimeGeneratedCode) }),
                LoadShowcase("Commands", uriPrefix + "Commands", path, new[] { typeof(CommandsViewModel), typeof(CommandsView), typeof(CommandsViewModel_RuntimeGeneratedCode) }),
                LoadShowcase("Services", uriPrefix + "Services", path, new[] { typeof(ServicesViewModel), typeof(ServicesView), typeof(ServicesViewModel_RuntimeGeneratedCode) }),
                LoadShowcase("IDataErrorInfo", uriPrefix + "IDataErrorInfo", path, new[] { typeof(DataErrorInfoViewModel), typeof(DataErrorInfoView), typeof(DataErrorInfoViewModel_RuntimeGeneratedCode) }),
                LoadShowcase("Dependent Properties", null, path, new[] { typeof(DependentPropertiesViewModel), typeof(DependentPropertiesView), typeof(DependentPropertiesViewModel_RuntimeGeneratedCode) }),
                LoadShowcase("Change Notifications", null, path, new[] { typeof(ChangeNotificationsViewModel), typeof(ChangeNotificationsView), typeof(ChangeNotificationsViewModel_RuntimeGeneratedCode) }),
            };
        }
    }
}
