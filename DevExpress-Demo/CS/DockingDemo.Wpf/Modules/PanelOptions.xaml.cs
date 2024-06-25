using DevExpress.Xpf.Bars;
using DevExpress.Xpf.DemoBase;

namespace DockingDemo {
    public partial class PanelOptions : DockingDemoModule {
        public PanelOptions() {
            InitializeComponent();
        }
        void bHome_ItemClick(object sender, ItemClickEventArgs e) {
            NavigateHomePage();
        }
        void bAbout_ItemClick(object sender, ItemClickEventArgs e) {
            ShowAbout();
        }
    }
}
