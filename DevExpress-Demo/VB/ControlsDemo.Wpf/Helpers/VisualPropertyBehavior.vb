Imports DevExpress.Mvvm.UI.Interactivity
Imports System.Windows
Imports System.Windows.Media

Namespace ControlsDemo

    Public Class VisualPropertyBehavior
        Inherits Behavior(Of Visual)

        Public Property Visual As Visual
            Get
                Return CType(GetValue(VisualProperty), Visual)
            End Get

            Set(ByVal value As Visual)
                SetValue(VisualProperty, value)
            End Set
        End Property

        Public Shared ReadOnly VisualProperty As DependencyProperty = DependencyProperty.Register("Visual", GetType(Visual), GetType(VisualPropertyBehavior), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            Visual = AssociatedObject
            AddHandler CType(AssociatedObject, FrameworkElement).Unloaded, AddressOf VisualPropertyBehavior_Unloaded
        End Sub

        Private Sub VisualPropertyBehavior_Unloaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Visual = Nothing
            RemoveHandler CType(AssociatedObject, FrameworkElement).Unloaded, AddressOf VisualPropertyBehavior_Unloaded
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            Visual = Nothing
        End Sub
    End Class
End Namespace
