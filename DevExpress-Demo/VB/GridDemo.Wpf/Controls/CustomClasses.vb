Imports DevExpress.DemoData.Models
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Utils
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls

Namespace GridDemo

    Public Class GridSummaryList
        Inherits List(Of GridSummaryItem)

    End Class

    Public Class NameTextControl
        Inherits Control

        Public Shared ReadOnly NameValueProperty As DependencyProperty = DependencyProperty.Register("NameValue", GetType(String), GetType(NameTextControl), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Shared ReadOnly TextValueProperty As DependencyProperty = DependencyProperty.Register("TextValue", GetType(String), GetType(NameTextControl), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property NameValue As String
            Get
                Return CStr(GetValue(NameValueProperty))
            End Get

            Set(ByVal value As String)
                SetValue(NameValueProperty, value)
            End Set
        End Property

        Public Property TextValue As String
            Get
                Return CStr(GetValue(TextValueProperty))
            End Get

            Set(ByVal value As String)
                SetValue(TextValueProperty, value)
            End Set
        End Property

        Public Sub New()
            SetDefaultStyleKey(GetType(NameTextControl))
        End Sub
    End Class

    Public Class HintControl
        Inherits ContentControl

        Public Sub New()
            SetDefaultStyleKey(GetType(HintControl))
        End Sub
    End Class

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
