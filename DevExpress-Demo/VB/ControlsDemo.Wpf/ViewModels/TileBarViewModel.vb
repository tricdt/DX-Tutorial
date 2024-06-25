Namespace ControlsDemo

    Public Class TileBarViewModel
        Inherits TileNavBaseViewModel

        Private ReadOnly Property TileBarService As ITileBarService
            Get
                Return ServiceContainer.GetService(Of ITileBarService)()
            End Get
        End Property

        Protected Overrides Sub OnNavigationMessage(ByVal message As NavigationMessage)
            MyBase.OnNavigationMessage(message)
            TileBarService.CloseFlyout()
        End Sub
    End Class
End Namespace
