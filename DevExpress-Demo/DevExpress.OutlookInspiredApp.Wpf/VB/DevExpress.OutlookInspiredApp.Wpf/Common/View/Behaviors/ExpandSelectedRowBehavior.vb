Imports System.Windows
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Grid

Namespace DevExpress.DevAV.Common.View

    Public Class ExpandSelectedRowBehavior
        Inherits Behavior(Of GridControl)

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.Loaded, AddressOf OnAssociatedObjectLoaded
            AddHandler AssociatedObject.ItemsSourceChanged, AddressOf OnAssociatedObjectItemsSourceChanged
            AddHandler AssociatedObject.FilterChanged, AddressOf OnAssociatedObjectFilterChanged
            SelectAndExpandRow()
        End Sub

        Private Sub OnAssociatedObjectFilterChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            SelectAndExpandRow()
        End Sub

        Private Sub OnAssociatedObjectItemsSourceChanged(ByVal sender As Object, ByVal e As ItemsSourceChangedEventArgs)
            SelectAndExpandRow()
        End Sub

        Private Sub OnAssociatedObjectLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            SelectAndExpandRow()
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            RemoveHandler AssociatedObject.Loaded, AddressOf OnAssociatedObjectLoaded
            RemoveHandler AssociatedObject.ItemsSourceChanged, AddressOf OnAssociatedObjectItemsSourceChanged
            RemoveHandler AssociatedObject.FilterChanged, AddressOf OnAssociatedObjectFilterChanged
        End Sub

        Private Sub SelectAndExpandRow(ByVal Optional rowHandle As Integer = 0)
            If Not AssociatedObject.IsLoaded Then Return
            If AssociatedObject.View IsNot Nothing Then AssociatedObject.View.FocusedRowHandle = rowHandle
            AssociatedObject.ExpandMasterRow(rowHandle)
        End Sub
    End Class
End Namespace
