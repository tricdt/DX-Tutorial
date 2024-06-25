Imports System.Windows.Media.Imaging
Imports DevExpress.Xpf.Map

Namespace ProductsDemo

    Public Class PlaceInfo

        Private ReadOnly nameField As String

        Private ReadOnly cityNameField As String

        Private ReadOnly locationField As GeoPoint

        Private ReadOnly descriptionField As String

        Private ReadOnly imageSourceField As BitmapImage

        Public ReadOnly Property Name As String
            Get
                Return nameField
            End Get
        End Property

        Public ReadOnly Property CityName As String
            Get
                Return cityNameField
            End Get
        End Property

        Public ReadOnly Property Location As GeoPoint
            Get
                Return locationField
            End Get
        End Property

        Public ReadOnly Property Description As String
            Get
                Return descriptionField
            End Get
        End Property

        Public ReadOnly Property ImageSource As BitmapImage
            Get
                Return imageSourceField
            End Get
        End Property

        Public Sub New(ByVal name As String, ByVal cityName As String, ByVal location As GeoPoint, ByVal description As String, ByVal imageSource As BitmapImage)
            nameField = name
            cityNameField = cityName
            locationField = location
            descriptionField = description
            imageSourceField = imageSource
        End Sub
    End Class
End Namespace
