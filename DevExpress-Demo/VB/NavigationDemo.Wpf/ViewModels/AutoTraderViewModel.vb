Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.DemoData.Models.Vehicles
Imports DevExpress.Mvvm.POCO

Namespace NavigationDemo

    Public Class AutoTraderViewModel

        Private _Vehicles As IEnumerable(Of DevExpress.DemoData.Models.Vehicles.Model), _Trademarks As IEnumerable(Of DevExpress.DemoData.Models.Vehicles.Trademark), _TransmissionTypes As IEnumerable(Of DevExpress.DemoData.Models.Vehicles.TransmissionType), _BodyStyles As IEnumerable(Of DevExpress.DemoData.Models.Vehicles.BodyStyle), _DoorTypes As Integer()

        Public Property Vehicles As IEnumerable(Of Model)
            Get
                Return _Vehicles
            End Get

            Private Set(ByVal value As IEnumerable(Of Model))
                _Vehicles = value
            End Set
        End Property

        Public Property Trademarks As IEnumerable(Of Trademark)
            Get
                Return _Trademarks
            End Get

            Private Set(ByVal value As IEnumerable(Of Trademark))
                _Trademarks = value
            End Set
        End Property

        Public Property TransmissionTypes As IEnumerable(Of TransmissionType)
            Get
                Return _TransmissionTypes
            End Get

            Private Set(ByVal value As IEnumerable(Of TransmissionType))
                _TransmissionTypes = value
            End Set
        End Property

        Public Property BodyStyles As IEnumerable(Of BodyStyle)
            Get
                Return _BodyStyles
            End Get

            Private Set(ByVal value As IEnumerable(Of BodyStyle))
                _BodyStyles = value
            End Set
        End Property

        Public Property DoorTypes As Integer()
            Get
                Return _DoorTypes
            End Get

            Private Set(ByVal value As Integer())
                _DoorTypes = value
            End Set
        End Property

        Public Sub New()
            If IsInDesignMode() Then Return
            Dim context = New VehiclesContext()
            Vehicles = context.Models.ToList()
            Trademarks = context.Trademarks.ToList()
            TransmissionTypes = context.TransmissionTypes.ToList()
            BodyStyles = context.BodyStyles.ToList()
            DoorTypes = New Integer() {2, 3, 4}
        End Sub
    End Class
End Namespace
