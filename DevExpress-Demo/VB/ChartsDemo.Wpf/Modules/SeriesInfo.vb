Imports System.Windows
Imports System.Windows.Markup

Namespace ChartsDemo

    <ContentProperty("SeriesTemplate")>
    Public Class SeriesInfo
        Inherits DependencyObject

        Public Shared ReadOnly DataSourceProperty As DependencyProperty = DependencyProperty.Register("DataSource", GetType(Object), GetType(SeriesInfo), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Shared ReadOnly ContentProperty As DependencyProperty = DependencyProperty.Register("Content", GetType(String), GetType(SeriesInfo), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Shared ReadOnly SeriesTemplateProperty As DependencyProperty = DependencyProperty.Register("SeriesTemplate", GetType(DataTemplate), GetType(SeriesInfo), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Shared ReadOnly DiagramTypeProperty As DependencyProperty = DependencyProperty.Register("DiagramType", GetType(DiagramType), GetType(SeriesInfo), New PropertyMetadata(DiagramType.XY))

        Public Property Content As String
            Get
                Return CStr(GetValue(ContentProperty))
            End Get

            Set(ByVal value As String)
                SetValue(ContentProperty, value)
            End Set
        End Property

        Public Property DataSource As Object
            Get
                Return GetValue(DataSourceProperty)
            End Get

            Set(ByVal value As Object)
                SetValue(DataSourceProperty, value)
            End Set
        End Property

        Public Property SeriesTemplate As DataTemplate
            Get
                Return CType(GetValue(SeriesTemplateProperty), DataTemplate)
            End Get

            Set(ByVal value As DataTemplate)
                SetValue(SeriesTemplateProperty, value)
            End Set
        End Property

        Public Property DiagramType As DiagramType
            Get
                Return CType(GetValue(DiagramTypeProperty), DiagramType)
            End Get

            Set(ByVal value As DiagramType)
                SetValue(DiagramTypeProperty, value)
            End Set
        End Property
    End Class

    Public Enum DiagramType
        XY
        Simple
        Radar
        Polar
        XY3D
    End Enum
End Namespace
