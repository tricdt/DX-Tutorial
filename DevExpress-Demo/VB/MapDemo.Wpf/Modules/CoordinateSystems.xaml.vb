Imports System
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.Windows.Input
Imports System.Xml.Linq
Imports DevExpress.Map
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Map

Namespace MapDemo

    <POCOViewModel>
    Public Class HotelsViewModel
        Inherits BindableBase

        Public Overridable Property CoordinateSystemType As CoordinateSystemType

        Public Overridable Property HotelInfos As ObservableCollection(Of HotelInfo)

        Public Overridable Property SelectedHotel As HotelInfo

        Public Overridable Property ZoomLevel As Double

        Public Overridable Property MinZoomLevel As Integer

        Public Overridable Property MaxZoomLevel As Integer

        Public Overridable Property CenterPoint As CoordPoint

        Public Overridable Property UseSprings As Boolean

        Private mapDefaultPosition As GeoPoint = New GeoPoint(-21.164, -175.127)

        Private geoMinZoomLevel As Integer = 12

        Private geoMaxZoomLevel As Integer = 15

        Private hotelMinZoomLevel As Integer = 2

        Private hotelMaxZoomLevel As Integer = 6

        Private mapDefaultZoomLevel As Double = 13.4

        Public Sub New()
            CoordinateSystemType = CoordinateSystemType.Geo
            MinZoomLevel = geoMinZoomLevel
            MaxZoomLevel = geoMaxZoomLevel
            ZoomLevel = mapDefaultZoomLevel
            CenterPoint = mapDefaultPosition
            HotelInfos = LoadDataFromXML()
            UseSprings = True
        End Sub

        Protected Sub OnSelectedHotelChanged()
            If SelectedHotel Is Nothing Then
                UseSprings = False
                MinZoomLevel = geoMinZoomLevel
                MaxZoomLevel = geoMaxZoomLevel
                CoordinateSystemType = CoordinateSystemType.Geo
                CenterPoint = mapDefaultPosition
                UseSprings = True
                ZoomLevel = mapDefaultZoomLevel
            Else
                UseSprings = False
                MinZoomLevel = hotelMinZoomLevel
                MaxZoomLevel = hotelMaxZoomLevel
                CoordinateSystemType = CoordinateSystemType.Cartesian
                CenterPoint = New CartesianPoint(0, 0)
                UseSprings = True
                ZoomLevel = SelectedHotel.ZoomLevel
            End If
        End Sub

        Private Function LoadDataFromXML() As ObservableCollection(Of HotelInfo)
            Dim document As XDocument = LoadXmlFromResources("/Data/Hotels.xml")
            Dim infos As ObservableCollection(Of HotelInfo) = New ObservableCollection(Of HotelInfo)()
            If document IsNot Nothing Then
                For Each element As XElement In document.Element("Hotels").Elements()
                    Dim hotelInfo As HotelInfo = New HotelInfo()
                    hotelInfo.Name = element.Element("Name").Value
                    hotelInfo.Latitude = Convert.ToDouble(element.Element("Latitude").Value, CultureInfo.InvariantCulture)
                    hotelInfo.Longitude = Convert.ToDouble(element.Element("Longitude").Value, CultureInfo.InvariantCulture)
                    hotelInfo.ShpFileUri = New Uri(element.Element("ShpFileUri").Value, UriKind.RelativeOrAbsolute)
                    hotelInfo.ZoomLevel = Convert.ToInt32(element.Element("ZoomLevel").Value)
                    hotelInfo.ImageUri = New Uri(element.Element("ImageUri").Value, UriKind.RelativeOrAbsolute)
                    hotelInfo.HighlightedImageUri = New Uri(element.Element("HighlightedImageUri").Value, UriKind.RelativeOrAbsolute)
                    infos.Add(hotelInfo)
                Next
            End If

            Return infos
        End Function
    End Class

    Public Partial Class CoordinateSystems
        Inherits MapDemoModule

        Public ReadOnly Property ViewModel As HotelsViewModel
            Get
                Return TryCast(LayoutRoot.DataContext, HotelsViewModel)
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
            LayoutRoot.DataContext = ViewModelSource.Create(Function() New HotelsViewModel())
        End Sub

        Private Sub Label_MouseDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            ViewModel.SelectedHotel = Nothing
        End Sub
    End Class

    Public Class HotelInfo

        Public Property Latitude As Double

        Public Property Longitude As Double

        Public Property ShpFileUri As Uri

        Public Property Name As String

        Public Property ZoomLevel As Integer

        Public Property ImageUri As Uri

        Public Property HighlightedImageUri As Uri
    End Class

    Public Enum CoordinateSystemType
        Geo
        Cartesian
    End Enum
End Namespace
