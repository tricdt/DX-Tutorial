Imports System.Windows

Namespace ChartsDemo

    <Markup.ContentProperty("Content")>
    Public Class ValueSelectorItem
        Inherits DependencyObject

        Public Shared ReadOnly ContentProperty As DependencyProperty = DependencyProperty.Register("Content", GetType(Object), GetType(ValueSelectorItem), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Shared ReadOnly DisplayNameProperty As DependencyProperty = DependencyProperty.Register("DisplayName", GetType(String), GetType(ValueSelectorItem), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property Content As Object
            Get
                Return GetValue(ContentProperty)
            End Get

            Set(ByVal value As Object)
                SetValue(ContentProperty, value)
            End Set
        End Property

        Public Property DisplayName As String
            Get
                Return CStr(GetValue(DisplayNameProperty))
            End Get

            Set(ByVal value As String)
                SetValue(DisplayNameProperty, value)
            End Set
        End Property
    End Class
End Namespace
