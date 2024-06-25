using MVVMDemo.AsyncCommands;
using DevExpress.Xpf.DemoBase;

namespace MVVMDemo {
    public partial class AsyncCommandsModule : MVVMDemoModule {
        protected override IShowcaseInfo[] CreateShowcases() {
            const string path = "Modules/AsyncCommands";
            const string uri = "17354/Common-Concepts/MVVM-Framework/Commands/Asynchronous-Commands";
            return new ShowcaseInfo[] {
                LoadShowcase("Asynchronous Delegate Commands", uri, path, new[] { typeof(AsyncDelegateCommandsViewModel), typeof(AsyncDelegateCommandsView) }),
                LoadShowcase("Asynchronous POCO Commands", uri, path, new[] { typeof(AsyncPOCOCommandsViewModel), typeof(AsyncPOCOCommandsView), typeof(AsyncPOCOCommandsViewModel_RuntimeGeneratedCode) }),
            };
        }
    }
}
