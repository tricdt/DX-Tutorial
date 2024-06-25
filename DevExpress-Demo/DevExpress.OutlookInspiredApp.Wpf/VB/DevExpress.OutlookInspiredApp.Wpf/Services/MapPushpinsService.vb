Imports DevExpress.Mvvm.UI
Imports DevExpress.DevAV.ViewModels
Imports DevExpress.Xpf.Map
Imports System.Collections.Generic
Imports System.Linq

Namespace DevExpress.DevAV.ViewModels

    Public Interface IMapPushpinsService

        Sub Clear()

    End Interface
End Namespace

Namespace DevExpress.DevAV

    Public Class MapPushpinsService
        Inherits ServiceBase
        Implements IMapPushpinsService

        Private ReadOnly Property Map As MapControl
            Get
                Return CType(AssociatedObject, MapControl)
            End Get
        End Property

        Private ReadOnly Property InformationLayers As IEnumerable(Of InformationLayer)
            Get
                Return Map.Layers.Where(Function(l) TypeOf l Is InformationLayer).Cast(Of InformationLayer)()
            End Get
        End Property

        Public Sub Clear() Implements IMapPushpinsService.Clear
            For Each layer In InformationLayers
                layer.ClearResults()
            Next
        End Sub
    End Class
End Namespace
