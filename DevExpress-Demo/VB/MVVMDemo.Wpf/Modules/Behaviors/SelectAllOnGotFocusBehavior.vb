Imports DevExpress.Mvvm.UI.Interactivity
Imports System.Windows.Controls

Namespace MVVMDemo.Behaviors

    Public NotInheritable Class SelectAllOnGotFocusBehavior
        Inherits Behavior(Of TextBox)

        Private ReadOnly Property TextBox As TextBox
            Get
                Return AssociatedObject
            End Get
        End Property

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler TextBox.GotFocus, AddressOf OnGotFocus
        End Sub

        Protected Overrides Sub OnDetaching()
            RemoveHandler TextBox.GotFocus, AddressOf OnGotFocus
            MyBase.OnDetaching()
        End Sub

        Private Sub OnGotFocus(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            TextBox.SelectAll()
        End Sub
    End Class
End Namespace
