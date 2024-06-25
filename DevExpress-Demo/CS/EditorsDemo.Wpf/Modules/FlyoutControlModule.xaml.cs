using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EditorsDemo {
    public partial class FlyoutControlModule : EditorsDemoModule {
        public FlyoutControlModule() {
            this.InitializeComponent();
        }

        void OpenInnerFlyout(object sender, RoutedEventArgs e) {
            FrameworkElement element = sender as FrameworkElement;
            if (element == null)
                return;
            flyoutControl.PlacementTarget = LayoutRoot;
            flyoutControl.Style = GetFlyoutStyle(element.Name);
            flyoutControl.IsOpen = true;
        }
        void OpenFlyout(object sender, RoutedEventArgs e) {
            FrameworkElement element = sender as FrameworkElement;
            if (element == null)
                return;
            flyoutControl.PlacementTarget = element;
            flyoutControl.Style = GetFlyoutStyle(element.Name);
            flyoutControl.IsOpen = true;
        }
        Style GetFlyoutStyle(string key) {
            return Resources[key] as Style;
        }
    }
}
