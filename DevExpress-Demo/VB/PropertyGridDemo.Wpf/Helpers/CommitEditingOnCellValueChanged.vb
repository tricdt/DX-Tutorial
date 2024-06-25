Imports DevExpress.Xpf.Grid
Imports DevExpress.Mvvm.UI.Interactivity

Namespace PropertyGridDemo

    Public Class CommitEditingOnCellValueChanged
        Inherits Behavior(Of GridViewBase)

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.CellValueChanged, AddressOf AssociatedObject_CellValueChanged
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            RemoveHandler AssociatedObject.CellValueChanged, AddressOf AssociatedObject_CellValueChanged
        End Sub

        Private Sub AssociatedObject_CellValueChanged(ByVal sender As Object, ByVal e As CellValueChangedEventArgs)
            Dim view = CType(sender, DataViewBase)
            view.CommitEditing()
        End Sub
    End Class
End Namespace
