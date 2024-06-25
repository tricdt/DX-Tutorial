using MVVMDemo.EnumItemsSource;
using DevExpress.Xpf.DemoBase;

namespace MVVMDemo {
    public partial class EnumItemsSourceModule : MVVMDemoModule {
        protected override IShowcaseInfo[] CreateShowcases() {
            const string path = "Modules/EnumItemsSource";
            const string uri = null;
            return new ShowcaseInfo[] {
                LoadShowcase("ListBoxEdit", uri,
                    path, new[] { typeof(ListBoxEditView), typeof(UserRole) }),
                LoadShowcase("ComboBoxEdit", uri,
                    path, new[] { typeof(ComboBoxEditView), typeof(UserRole) }),
                LoadShowcase("ComboBox column in GridControl", uri,
                    path, new[] { typeof(GridControlView), typeof(UserRoleInfo), typeof(UserRole) }),

            };
        }
    }
}
