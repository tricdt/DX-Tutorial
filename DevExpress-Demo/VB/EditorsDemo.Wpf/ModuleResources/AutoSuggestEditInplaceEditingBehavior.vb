Imports System.Linq
Imports DevExpress.DemoData.Models
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Editors

Namespace EditorsDemo.ModuleResources

    Public Class AutoSuggestEditInplaceEditingBehavior
        Inherits Behavior(Of AutoSuggestEdit)

        Private ReadOnly context As NWindContext = NWindContext.Create()

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.QuerySubmitted, AddressOf AssociatedObjectOnQuerySubmitted
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            RemoveHandler AssociatedObject.QuerySubmitted, AddressOf AssociatedObjectOnQuerySubmitted
        End Sub

        Private Sub AssociatedObjectOnQuerySubmitted(ByVal sender As Object, ByVal e As AutoSuggestEditQuerySubmittedEventArgs)
            AssociatedObject.ItemsSource = If(String.IsNullOrEmpty(e.Text), context.Products.ToArray(), context.Products.Where(Function(x) x.ProductName.StartsWith(e.Text)).ToArray())
        End Sub
    End Class
End Namespace
