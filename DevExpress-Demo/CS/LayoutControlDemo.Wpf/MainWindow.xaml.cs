using System;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpf.DemoBase.Helpers;

namespace LayoutControlDemo {
    public partial class MainWindow : SidebarWindow {
        public MainWindow() {
            InitializeComponent();
        }
        static MainWindow() {
            new TaskFactory().StartNew(() => DevExpress.DemoData.Models.Vehicles.VehiclesContext.Preload());
        }
    }
}
