Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Core.Serialization
Imports DevExpress.Xpf.Docking

Namespace DockingDemo

    Public Interface IDockLayoutManagerSerializationService

        Sub Serialize(ByVal path As Object)

        Sub Deserialize(ByVal path As Object)

    End Interface

    Public Class DockLayoutManagerSerializationService
        Inherits ServiceBase
        Implements IDockLayoutManagerSerializationService

        Private ReadOnly Property DockLayoutManager As DockLayoutManager
            Get
                Return TryCast(AssociatedObject, DockLayoutManager)
            End Get
        End Property

        Private Sub Deserialize(ByVal path As Object) Implements IDockLayoutManagerSerializationService.Deserialize
            Call DXSerializer.Deserialize(AssociatedObject, path, AssociatedObject.GetType().Name, Nothing)
        End Sub

        Private Sub Serialize(ByVal path As Object) Implements IDockLayoutManagerSerializationService.Serialize
            Call DXSerializer.Serialize(AssociatedObject, path, AssociatedObject.GetType().Name, Nothing)
        End Sub
    End Class
End Namespace
