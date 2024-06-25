using MVVMDemo.Commands;
using DevExpress.Xpf.DemoBase;

namespace MVVMDemo {
    public partial class CommandsModule : MVVMDemoModule {
        protected override IShowcaseInfo[] CreateShowcases() {
            const string path = "Modules/Commands";
            const string uri = "17353/Common-Concepts/MVVM-Framework/Commands/Delegate-Commands";
            const string viewModelBaseUri = "17351/Common-Concepts/MVVM-Framework/ViewModels/ViewModelBase";
            const string pocoUri = "17352/Common-Concepts/MVVM-Framework/ViewModels/POCO-ViewModels#";
            return new ShowcaseInfo[] {
                LoadShowcase("Delegate Commands", uri, path, new[] { typeof(DelegateCommandsViewModel), typeof(DelegateCommandsView) }),
                LoadShowcase("Delegate Commands Change Notifications", uri, path, new[] { typeof(ChangeNotificationsViewModel), typeof(ChangeNotificationsView) }),
                LoadShowcase("Automatic Commands Creation", viewModelBaseUri + "#Feature5", path, new[] { typeof(AutoCommandsViewModel), typeof(AutoCommandsView) }),
                LoadShowcase("Automatic Commands Change Notifications", viewModelBaseUri + "#Feature5", path, new[] { typeof(AutoCommandsChangeNotificationsViewModel), typeof(AutoCommandsChangeNotificationsView) }),
                LoadShowcase("POCO Commands", pocoUri + "Commands", path, new[] { typeof(POCOCommandsViewModel), typeof(POCOCommandsView), typeof(POCOCommandsViewModel_RuntimeGeneratedCode) }),
                LoadShowcase("POCO Commands Change Notifications", pocoUri + "Commands", path, new[] { typeof(POCOCommandsChangeNotificationsViewModel), typeof(POCOCommandsChangeNotificationsView), typeof(POCOCommandsChangeNotificationsViewModel_RuntimeGeneratedCode) }),
            };
        }
    }
}
