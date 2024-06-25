Imports System.Windows
Imports System.Windows.Controls

Namespace DevExpress.SalesDemo.Wpf.Controls

    Public Class DashboardGroup
        Inherits HeaderedContentControl

        Public Shared ReadOnly HeaderMarginProperty As DependencyProperty = DependencyProperty.Register("HeaderMargin", GetType(Thickness), GetType(DashboardGroup), New PropertyMetadata(New Thickness(0)))

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
