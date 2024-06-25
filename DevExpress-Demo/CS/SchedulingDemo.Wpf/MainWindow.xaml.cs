using DevExpress.Xpf.DemoBase.Helpers;
using System.Threading.Tasks;

namespace SchedulingDemo {
    public partial class MainWindow : SidebarWindow {
        public MainWindow() {
            InitializeComponent();
        }
        static MainWindow() {
            new TaskFactory().StartNew(() => DevExpress.DemoData.Models.NWindContext.Preload());
        }
    }
}
