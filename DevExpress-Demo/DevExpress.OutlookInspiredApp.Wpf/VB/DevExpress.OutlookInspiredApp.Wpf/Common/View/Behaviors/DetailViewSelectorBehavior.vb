Imports DevExpress.DevAV.ViewModels
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Grid
Imports System.Windows

Namespace DevExpress.DevAV.Common.View

    Public Class DetailViewSelectorBehavior
        Inherits Behavior(Of GridControl)

        Public Sub New()
        End Sub

        Public Shared ReadOnly ViewKindProperty As DependencyProperty = DependencyProperty.Register("ViewKind", GetType(CollectionViewKind), GetType(DetailViewSelectorBehavior), New PropertyMetadata(CollectionViewKind.CardView, Sub(d, e) CType(d, DetailViewSelectorBehavior).UpdateViewKind()))

        Public Property ViewKind As CollectionViewKind
            Get
                Return CType(GetValue(ViewKindProperty), CollectionViewKind)
            End Get

            Set(ByVal value As CollectionViewKind)
                SetValue(ViewKindProperty, value)
            End Set
        End Property

        Public Property CardView As GridViewBase

        Public Property TableView As GridViewBase

        Private Sub UpdateViewKind()
            If AssociatedObject Is Nothing OrElse CardView Is Nothing OrElse TableView Is Nothing OrElse ViewKind = CollectionViewKind.Carousel Then Return
            AssociatedObject.View = If(ViewKind = CollectionViewKind.CardView, CardView, TableView)
        End Sub

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            UpdateViewKind()
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
        End Sub
    End Class
End Namespace
