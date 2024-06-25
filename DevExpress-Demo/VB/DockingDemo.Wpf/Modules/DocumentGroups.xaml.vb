Imports System
Imports System.Windows
Imports DevExpress.Xpf.Docking
Imports DevExpress.DemoData.Helpers

Namespace DockingDemo

    Public Partial Class DocumentGroups
        Inherits DockingDemoModule

        Public Sub New()
            InitializeComponent()
            If EnvironmentHelper.IsClickOnce Then
                addWindowUriButton.Visibility = Visibility.Collapsed
            End If
        End Sub

        Private i As Integer

        Private Sub addEmptyButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim panel As DocumentPanel = dockManager.DockController.AddDocumentPanel(documentContainer)
            panel.Caption = "Empty Panel " & i
            i += 1
            dockManager.Activate(panel)
        End Sub

        Private Sub addUserControlUriButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim panel As DocumentPanel = dockManager.DockController.AddDocumentPanel(documentContainer, New Uri("/DockingDemo;component/Layouts/ChildUserControl.xaml", UriKind.Relative))
            panel.Caption = "UserControl " & i
            i += 1
            dockManager.Activate(panel)
        End Sub

        Private Sub addWindowUriButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim panel As DocumentPanel = dockManager.DockController.AddDocumentPanel(documentContainer, New Uri("/DockingDemo;component/Layouts/ChildWindow.xaml", UriKind.Relative))
            panel.Caption = "Window " & i
            i += 1
            dockManager.Activate(panel)
        End Sub

        Private Sub addPageUriButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim panel As DocumentPanel = dockManager.DockController.AddDocumentPanel(documentContainer, New Uri("/DockingDemo;component/Layouts/ChildPage.xaml", UriKind.Relative))
            panel.Caption = "Page " & i
            i += 1
            dockManager.Activate(panel)
        End Sub
    End Class
End Namespace
