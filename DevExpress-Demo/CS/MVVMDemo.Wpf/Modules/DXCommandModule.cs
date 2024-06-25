using MVVMDemo.DXCommandDemo;
using DevExpress.Xpf.DemoBase;

namespace MVVMDemo {
    public partial class DXCommandModule : MVVMDemoModule {
        protected override IShowcaseInfo[] CreateShowcases() {
            const string path = "Modules/DXCommand";
            const string uri = "115776/MVVM-Framework/Productivity/DXCommand";
            return new ShowcaseInfo[] {
                LoadShowcase("Binding command to methods", uri, 
                    path, new[] { typeof(BindingCommandToMethodsView),  typeof(BindingCommandToMethodsViewModel) }),
                LoadShowcase("Using values from other controls", uri,
                    path, new[] { typeof(ValuesFromControlsView),  typeof(ValuesFromControlsViewModel) }),
                LoadShowcase("Using command parameter", uri,
                    path, new[] { typeof(CommandParameterView),  typeof(CommandParameterViewModel) }),
                LoadShowcase("Call multiple methods", uri,
                    path, new[] { typeof(MultipleCallsView),  typeof(MultipleCallsViewModel) }),
            };
        }
    }
}
