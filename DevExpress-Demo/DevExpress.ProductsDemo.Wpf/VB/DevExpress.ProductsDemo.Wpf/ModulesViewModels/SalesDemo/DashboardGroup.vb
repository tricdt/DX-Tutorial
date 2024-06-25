Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Markup

Namespace ProductsDemo

    <ContentProperty("Content")>
    Public Class DashboardGroup
        Inherits Control

#Region "Dependency Properties"
        Public Shared ReadOnly ContentProperty As DependencyProperty = DependencyProperty.Register("Content", GetType(Object), GetType(DashboardGroup), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Shared ReadOnly HeaderTextProperty As DependencyProperty = DependencyProperty.Register("HeaderText", GetType(String), GetType(DashboardGroup), New PropertyMetadata(String.Empty))

        Public Shared ReadOnly HeaderMarginProperty As DependencyProperty = DependencyProperty.Register("HeaderMargin", GetType(Thickness), GetType(DashboardGroup), New PropertyMetadata(New Thickness(0)))

#End Region
        Public Property Content As Object
            Get
                Return GetValue(ContentProperty)
            End Get

            Set(ByVal value As Object)
                SetValue(ContentProperty, value)
            End Set
        End Property

        Public Property HeaderText As String
            Get
                Return CStr(GetValue(HeaderTextProperty))
            End Get

            Set(ByVal value As String)
                SetValue(HeaderTextProperty, value)
            End Set
        End Property

        Public Property HeaderMargin As Thickness
            Get
                Return CType(GetValue(HeaderMarginProperty), Thickness)
            End Get

            Set(ByVal value As Thickness)
                SetValue(HeaderMarginProperty, value)
            End Set
        End Property

        Public Sub New()
            DefaultStyleKey = GetType(DashboardGroup)
        End Sub
    End Class
End Namespace
