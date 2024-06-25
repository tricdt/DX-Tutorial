using MVVMDemo.BindableBaseDemo;
using DevExpress.Xpf.DemoBase;

namespace MVVMDemo {
    public partial class BindableBaseModule : MVVMDemoModule {
        protected override IShowcaseInfo[] CreateShowcases() {
            const string path = "Modules/BindableBase";
            const string uri = "17350/Common-Concepts/MVVM-Framework/ViewModels/BindableBase";
            return new ShowcaseInfo[] {
                LoadShowcase("Bindable Properties", uri, path, new[] { typeof(BindablePropertiesViewModel), typeof(BindablePropertiesView) }),
                LoadShowcase("Bindable Properties via Fields", null, path, new[] { typeof(BindablePropertiesViaFieldsViewModel), typeof(BindablePropertiesViaFieldsView) }),
            };
        }
    }
}
