Imports DevExpress.DevAV.ViewModels
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Navigation
Imports System

Namespace DevExpress.DevAV

    Public Class FilterItemClickBehavior
        Inherits Behavior(Of TileBarItem)

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.Click, AddressOf AssociatedObject_Click
        End Sub

        Private Sub AssociatedObject_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim filterItem = TryCast(AssociatedObject.DataContext, FilterItem)
            If filterItem Is Nothing OrElse Equals(filterItem.Name, Nothing) Then Return
            Log(String.Format("OutlookInspiredApp: Change Filter: {0}", filterItem.Name))
        End Sub

        Protected Overrides Sub OnDetaching()
            RemoveHandler AssociatedObject.Click, AddressOf AssociatedObject_Click
            MyBase.OnDetaching()
        End Sub
    End Class
End Namespace
