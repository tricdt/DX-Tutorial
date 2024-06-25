Imports System.Windows
Imports System.Windows.Controls

Namespace EditorsDemo

    Public Partial Class FlyoutControlModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub OpenInnerFlyout(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim element As FrameworkElement = TryCast(sender, FrameworkElement)
            If element Is Nothing Then Return
            flyoutControl.PlacementTarget = LayoutRoot
            flyoutControl.Style = GetFlyoutStyle(element.Name)
            flyoutControl.IsOpen = True
        End Sub

        Private Sub OpenFlyout(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim element As FrameworkElement = TryCast(sender, FrameworkElement)
            If element Is Nothing Then Return
            flyoutControl.PlacementTarget = element
            flyoutControl.Style = GetFlyoutStyle(element.Name)
            flyoutControl.IsOpen = True
        End Sub

        Private Function GetFlyoutStyle(ByVal key As String) As Style
            Return TryCast(Resources(key), Style)
        End Function
    End Class
End Namespace
