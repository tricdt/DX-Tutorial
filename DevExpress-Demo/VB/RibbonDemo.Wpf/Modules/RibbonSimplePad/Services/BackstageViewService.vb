Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Ribbon

Namespace RibbonDemo

    Public Interface IBackstageViewService

        Sub Close()

    End Interface

    Public Class BackstageViewService
        Inherits ServiceBase
        Implements IBackstageViewService

        Private ReadOnly Property Ribbon As RibbonControl
            Get
                Return TryCast(AssociatedObject, RibbonControl)
            End Get
        End Property

        Public Sub Close() Implements IBackstageViewService.Close
            If Ribbon IsNot Nothing OrElse Ribbon.MergedParent IsNot Nothing Then
                Ribbon.MergedParent.CloseApplicationMenu()
            End If
        End Sub
    End Class
End Namespace
