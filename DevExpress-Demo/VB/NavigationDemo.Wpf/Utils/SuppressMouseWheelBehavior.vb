Imports System.Windows
Imports System.Windows.Input
Imports DevExpress.Mvvm.UI.Interactivity

Namespace NavigationDemo.Utils

    Public Class SuppressMouseWheelBehavior
        Inherits Behavior(Of UIElement)

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.PreviewMouseWheel, AddressOf OnPreviewMouseWheel
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            RemoveHandler AssociatedObject.PreviewMouseWheel, AddressOf OnPreviewMouseWheel
        End Sub

        Private Sub OnPreviewMouseWheel(ByVal sender As Object, ByVal e As MouseWheelEventArgs)
            e.Handled = True
            Dim args = New MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
            args.RoutedEvent = UIElement.MouseWheelEvent
            CType(sender, UIElement).RaiseEvent(args)
        End Sub
    End Class
End Namespace
