using System;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.Helpers;

namespace RichEditDemo {
    public partial class MainWindow : SidebarWindow {
        public MainWindow() {
            InitializeComponent();
            SetValue(ScrollBarExtensions.AllowMiddleMouseScrollingProperty, true);
        }
        static MainWindow() {
            new TaskFactory().StartNew(() => DevExpress.DemoData.Models.NWindContext.Preload());
        }
    }
}
