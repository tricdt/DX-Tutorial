using System;
using System.Windows;
using DevExpress.Xpf.Docking;
using DevExpress.DemoData.Helpers;

namespace DockingDemo {
    public partial class DocumentGroups : DockingDemoModule {
        public DocumentGroups() {
            InitializeComponent();
            if(EnvironmentHelper.IsClickOnce) {
                addWindowUriButton.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        int i;
        void addEmptyButton_Click(object sender, RoutedEventArgs e) {
            DocumentPanel panel = dockManager.DockController.AddDocumentPanel(documentContainer);
            panel.Caption = "Empty Panel " + i;
            i += 1;
            dockManager.Activate(panel);
        }
        void addUserControlUriButton_Click(object sender, RoutedEventArgs e) {
            DocumentPanel panel = dockManager.DockController.AddDocumentPanel(
                documentContainer, new Uri("/DockingDemo;component/Layouts/ChildUserControl.xaml", UriKind.Relative));
            panel.Caption = "UserControl " + i;
            i += 1;
            dockManager.Activate(panel);
        }
        void addWindowUriButton_Click(object sender, RoutedEventArgs e) {
            DocumentPanel panel = dockManager.DockController.AddDocumentPanel(
                documentContainer, new Uri("/DockingDemo;component/Layouts/ChildWindow.xaml", UriKind.Relative));
            panel.Caption = "Window " + i;
            i += 1;
            dockManager.Activate(panel);
        }
        void addPageUriButton_Click(object sender, RoutedEventArgs e) {
            DocumentPanel panel = dockManager.DockController.AddDocumentPanel(
                documentContainer, new Uri("/DockingDemo;component/Layouts/ChildPage.xaml", UriKind.Relative));
            panel.Caption = "Page " + i;
            i += 1;
            dockManager.Activate(panel);
        }
    }
}
