using MVVMDemo.DXBindingDemo;
using DevExpress.Xpf.DemoBase;

namespace MVVMDemo {
    public partial class DXBindingModule : MVVMDemoModule {
        protected override IShowcaseInfo[] CreateShowcases() {
            const string path = "Modules/DXBinding";
            const string uri = "115771/MVVM-Framework/Productivity/DXBinding";
            return new ShowcaseInfo[] {
                LoadShowcase("Binding Expressions", uri, 
                    path, new[] { typeof(BindingExpressionsView),  typeof(BindingExpressionsViewModel) }),
                LoadShowcase("Functions", uri,
                    path, new[] { typeof(FunctionsView),  typeof(FunctionsViewModel), typeof(RegistrationHelper) }),
                LoadShowcase("Binding Sources", uri,
                    path, new[] { typeof(SourcesView) }),
                LoadShowcase("Back Conversion", uri,
                    path, new[] { typeof(BackConversionView),  typeof(BackConversionViewModel) }),

            };
        }
    }
}
