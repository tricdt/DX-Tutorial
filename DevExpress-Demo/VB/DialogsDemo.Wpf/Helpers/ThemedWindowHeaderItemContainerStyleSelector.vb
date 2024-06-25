Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Core

Namespace DialogsDemo

    Public Class ThemedWindowHeaderItemContainerStyleSelector
        Inherits StyleSelector

        Public Overrides Function SelectStyle(ByVal item As Object, ByVal container As DependencyObject) As Style
            Return If(SelectStyle(TryCast(item, BaseHeaderItemModel), TryCast(container, HeaderItemControl)), MyBase.SelectStyle(item, container))
        End Function

        Protected Overloads Function SelectStyle(ByVal item As BaseHeaderItemModel, ByVal container As HeaderItemControl) As Style
            Return TryCast(container.TryFindResource(item.ResourceKey), Style)
        End Function
    End Class
End Namespace
