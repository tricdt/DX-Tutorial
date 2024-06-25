Imports System.Windows

Namespace MapDemo

    Public Enum ProviderName
        Bing
        Osm
        None
    End Enum

    Public Class CopyrightControl
        Inherits VisibleControl

        Public Shared ReadOnly ProviderNameProperty As DependencyProperty = DependencyProperty.Register("ProviderName", GetType(ProviderName), GetType(CopyrightControl), New PropertyMetadata(ProviderName.None))

        Public Property ProviderName As ProviderName
            Get
                Return CType(GetValue(ProviderNameProperty), ProviderName)
            End Get

            Set(ByVal value As ProviderName)
                SetValue(ProviderNameProperty, value)
            End Set
        End Property

        Public Sub New()
            DefaultStyleKey = GetType(CopyrightControl)
        End Sub
    End Class
End Namespace
