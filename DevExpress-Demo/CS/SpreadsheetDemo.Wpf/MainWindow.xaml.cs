using System;
using System.Linq;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.Helpers;

namespace SpreadsheetDemo {
    public partial class MainWindow : SidebarWindow {
        public MainWindow() {
            InitializeComponent();
            SetValue(ScrollBarExtensions.AllowMiddleMouseScrollingProperty, true);
        }
    }
}
