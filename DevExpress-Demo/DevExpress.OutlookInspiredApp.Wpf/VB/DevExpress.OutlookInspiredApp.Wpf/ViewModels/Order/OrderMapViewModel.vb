Imports System.ComponentModel
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Map
Imports System.IO
Imports DevExpress.DevAV.Reports

Namespace DevExpress.DevAV.ViewModels

    Public Class OrderMapViewModel
        Inherits NavigatorMapViewModel(Of CustomerStore)

        Private _Order As Order

        Public Shared Overloads Function Create(ByVal order As Order) As OrderMapViewModel
            Return ViewModelSource.Create(Function() New OrderMapViewModel(order))
        End Function

        Protected Sub New(ByVal order As Order)
            MyBase.New(order.Store, AddressHelper.DevAVHomeOffice.ToString(), New GeoPoint(AddressHelper.DevAVHomeOffice.Latitude, AddressHelper.DevAVHomeOffice.Longitude), order.Store.Address.ToString(), New GeoPoint(order.Store.Address.Latitude, order.Store.Address.Longitude), Nothing)
            Me.Order = order
        End Sub

        Public Property Order As Order
            Get
                Return _Order
            End Get

            Protected Set(ByVal value As Order)
                _Order = value
            End Set
        End Property

        Public Overridable Property ShipmentText As String

        Public Overridable Property PdfStream As Stream

        Public Overrides Sub OnLoaded()
            MyBase.OnLoaded()
            CreateShippingDetailPdf()
        End Sub

        Public Overridable Sub OuUnloaded()
            PdfStream.Dispose()
        End Sub

        Private Sub CreateShippingDetailPdf()
            PdfStream = New MemoryStream()
            Call ReportFactory.ShippingDetail(Order).ExportToPdf(PdfStream)
            ShipmentText = GetShipmentText()
        End Sub

        Private Function GetShipmentText() As String
            Select Case Order.ShipmentStatus
                Case ShipmentStatus.Received
                    Return "Shipment Received"
                Case ShipmentStatus.Transit
                    Return "Shipment in Transit"
            End Select

            Return "Awaiting shipment"
        End Function
    End Class
End Namespace
