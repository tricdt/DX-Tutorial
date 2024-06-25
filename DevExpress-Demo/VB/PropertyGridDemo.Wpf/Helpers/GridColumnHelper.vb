Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Mvvm.UI.Interactivity

Namespace PropertyGridDemo

    Public Class GridColumnHelper
        Inherits Behavior(Of Grid)

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.SizeChanged, AddressOf AssociatedObject_SizeChanged
        End Sub

        Private Sub AssociatedObject_SizeChanged(ByVal sender As Object, ByVal e As SizeChangedEventArgs)
            AssociatedObject.ColumnDefinitions(1).Width = New GridLength(e.NewSize.Width / 3, GridUnitType.Pixel)
        End Sub
    End Class
End Namespace
