Imports System.Windows
Imports System.Windows.Markup

Namespace GridDemo

    <ContentProperty("Content")>
    Public Class ValueSelectorItem
        Inherits DependencyObject

        Public Property Content As Object
            Get
                Return GetValue(ContentProperty)
            End Get

            Set(ByVal value As Object)
                SetValue(ContentProperty, value)
            End Set
        End Property

        Public Shared ReadOnly ContentProperty As DependencyProperty = DependencyProperty.Register("Content", GetType(Object), GetType(ValueSelectorItem), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property Tag As Object
            Get
                Return GetValue(TagProperty)
            End Get

            Set(ByVal value As Object)
                SetValue(TagProperty, value)
            End Set
        End Property

        Public Shared ReadOnly TagProperty As DependencyProperty = DependencyProperty.Register("Tag", GetType(Object), GetType(ValueSelectorItem), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property DisplayName As String
            Get
                Return CStr(GetValue(DisplayNameProperty))
            End Get

            Set(ByVal value As String)
                SetValue(DisplayNameProperty, value)
            End Set
        End Property

        Public Shared ReadOnly DisplayNameProperty As DependencyProperty = DependencyProperty.Register("DisplayName", GetType(String), GetType(ValueSelectorItem), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))
    End Class
End Namespace
