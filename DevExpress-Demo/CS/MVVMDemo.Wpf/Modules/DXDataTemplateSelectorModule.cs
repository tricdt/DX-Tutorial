using DevExpress.Xpf.DemoBase;
using MVVMDemo.DXDataTemplateSelector;

namespace MVVMDemo {
    public partial class DXDataTemplateSelectorModule : MVVMDemoModule {
        protected override IShowcaseInfo[] CreateShowcases() {
            const string path = "Modules/DXDataTemplateSelector";
            const string uri = "119428/MVVM-Framework/DXBinding/DXDataTemplateSelector";
            return new ShowcaseInfo[] {
                LoadShowcase("Declarative DataTemplateSelector", uri, 
                    path, new[] { typeof(DeclarativeDataTemplateSelectorView), typeof(UserRoleInfo) }),
                LoadShowcase("Using DXBinding", uri,
                    path, new[] { typeof(UsingDXBindingView), typeof(UserRoleInfo) }),
            };
        }
    }
}
