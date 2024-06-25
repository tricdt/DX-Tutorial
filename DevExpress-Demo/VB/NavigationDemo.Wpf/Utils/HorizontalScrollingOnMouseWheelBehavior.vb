Imports System.Windows.Controls.Primitives
Imports System.Windows.Input
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.Editors

Namespace NavigationDemo.Utils

    Public Class HorizontalScrollingOnMouseWheelBehavior
        Inherits Behavior(Of ListBoxEdit)

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.PreviewMouseWheel, AddressOf OnPreviewMouseWheel
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            RemoveHandler AssociatedObject.PreviewMouseWheel, AddressOf OnPreviewMouseWheel
        End Sub

        Private Sub OnPreviewMouseWheel(ByVal sender As Object, ByVal e As MouseWheelEventArgs)
            Dim scrollBar = CType(LayoutHelper.FindElementByName(AssociatedObject, "PART_HorizontalScrollBar"), ScrollBar)
            If e.Delta > 0 Then
                Call ScrollBar.LineLeftCommand.Execute(Nothing, scrollBar)
            ElseIf e.Delta < 0 Then
                Call ScrollBar.LineRightCommand.Execute(Nothing, scrollBar)
            End If
        End Sub
    End Class
End Namespace
