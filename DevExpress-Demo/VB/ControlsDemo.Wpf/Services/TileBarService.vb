Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Navigation

Namespace ControlsDemo

    Public Interface ITileBarService

        Sub CloseFlyout()

    End Interface

    Public Class TileBarService
        Inherits ServiceBase
        Implements ITileBarService

        Public Sub CloseFlyout() Implements ITileBarService.CloseFlyout
            Dim tileBar As TileBar = TryCast(AssociatedObject, TileBar)
            If tileBar IsNot Nothing Then tileBar.CloseFlyout()
        End Sub
    End Class
End Namespace
