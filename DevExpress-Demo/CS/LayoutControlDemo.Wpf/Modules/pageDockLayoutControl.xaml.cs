using System.Windows;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.LayoutControl;

namespace LayoutControlDemo {
    public partial class pageDockLayoutControl : LayoutControlDemoModule {
        private int _LastClientItemZIndex;

        public pageDockLayoutControl() {
            InitializeComponent();
        }

        private void DockRadioButton_Click(object sender, RoutedEventArgs e) {
            var item = (FrameworkElement)PropertiesControl.SelectedItem;
            if (DockLayoutControl.GetDock(item) == DevExpress.Xpf.LayoutControl.Dock.None ||
                DockLayoutControl.GetDock(item) == DevExpress.Xpf.LayoutControl.Dock.Client) {
                item.SetZIndex(++_LastClientItemZIndex);
            } else
                item.SetZIndex(0);
            if (DockLayoutControl.GetDock(item) == DevExpress.Xpf.LayoutControl.Dock.Client) {
                item.ClearValue(WidthProperty);
                item.ClearValue(HeightProperty);
            }
        }
    }
}
