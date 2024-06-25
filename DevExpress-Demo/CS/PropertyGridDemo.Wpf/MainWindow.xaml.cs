using System;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.DemoData.Models;
using DevExpress.Xpf.DemoBase.Helpers;

namespace PropertyGridDemo {
    public partial class MainWindow : SidebarWindow {
        public MainWindow() {
            InitializeComponent();
        }
        static MainWindow() {
            new TaskFactory().StartNew(() => DevExpress.DemoData.Models.NWindContext.Preload());
        }
    }
}
