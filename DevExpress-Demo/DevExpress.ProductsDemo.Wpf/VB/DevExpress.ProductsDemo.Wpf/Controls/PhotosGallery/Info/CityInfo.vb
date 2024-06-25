Imports System.Collections.Generic
Imports DevExpress.Xpf.Map

Namespace ProductsDemo

    Public Class CityInfo

        Private ReadOnly nameField As String

        Private ReadOnly locationField As GeoPoint

        Private ReadOnly placesField As List(Of PlaceInfo) = New List(Of PlaceInfo)()

        Public ReadOnly Property Name As String
            Get
                Return nameField
            End Get
        End Property

        Public ReadOnly Property Location As GeoPoint
            Get
                Return locationField
            End Get
        End Property

        Public ReadOnly Property Places As List(Of PlaceInfo)
            Get
                Return placesField
            End Get
        End Property

        Public Sub New(ByVal name As String, ByVal location As GeoPoint)
            nameField = name
            locationField = location
        End Sub
    End Class
End Namespace
