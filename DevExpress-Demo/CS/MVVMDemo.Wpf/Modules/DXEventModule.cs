using MVVMDemo.DXEventDemo;
using DevExpress.Xpf.DemoBase;

namespace MVVMDemo {
    public partial class DXEventModule : MVVMDemoModule {
        protected override IShowcaseInfo[] CreateShowcases() {
            const string path = "Modules/DXEvent";
            const string uri = "115778/MVVM-Framework/Productivity/DXEvent";
            return new ShowcaseInfo[] {
                LoadShowcase("Binding event to method", uri, 
                    path, new[] { typeof(BindingEventToMethodView),  typeof(BindingEventToMethodViewModel) }),
                LoadShowcase("Call multiple methods", uri,
                    path, new[] { typeof(MultipleCallsView) }),
                LoadShowcase("Using sender and event arguments", uri,
                    path, new[] { typeof(EventArgsView),  typeof(EventArgsViewModel) }),
            };
        }
    }
}
