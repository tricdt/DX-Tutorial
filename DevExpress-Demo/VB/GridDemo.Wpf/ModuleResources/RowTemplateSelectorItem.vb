Imports System.Windows

Namespace GridDemo

    Friend Class RowTemplateSelectorItem
        Inherits DependencyObject

        Public Property DetailTemplate As DataTemplate
            Get
                Return CType(GetValue(DetailTemplateProperty), DataTemplate)
            End Get

            Set(ByVal value As DataTemplate)
                SetValue(DetailTemplateProperty, value)
            End Set
        End Property

        Public Shared ReadOnly DetailTemplateProperty As DependencyProperty = DependencyProperty.Register("DetailTemplate", GetType(DataTemplate), GetType(RowTemplateSelectorItem), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property RowStyle As Style
            Get
                Return CType(GetValue(RowStyleProperty), Style)
            End Get

            Set(ByVal value As Style)
                SetValue(RowStyleProperty, value)
            End Set
        End Property

        Public Shared ReadOnly RowStyleProperty As DependencyProperty = DependencyProperty.Register("RowStyle", GetType(Style), GetType(RowTemplateSelectorItem), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property DisplayName As String
            Get
                Return CStr(GetValue(DisplayNameProperty))
            End Get

            Set(ByVal value As String)
                SetValue(DisplayNameProperty, value)
            End Set
        End Property

        Public Shared ReadOnly DisplayNameProperty As DependencyProperty = DependencyProperty.Register("DisplayName", GetType(String), GetType(RowTemplateSelectorItem), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))
    End Class
End Namespace
