using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.Helpers;

namespace PdfViewerDemo {
    public partial class MainWindow : SidebarWindow {
        public MainWindow() {
            InitializeComponent();
            SetValue(ScrollBarExtensions.AllowMiddleMouseScrollingProperty, true);
        }
    }
}
