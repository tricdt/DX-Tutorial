Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Media.Imaging
Imports System.Xml.Linq
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Map
Imports DevExpress.Map

Namespace MapDemo

    Public Partial Class PhotoGallery
        Inherits MapDemoModule

        Private ReadOnly Property ViewModel As CitiesViewModel
            Get
                Return TryCast(LayoutRoot.DataContext, CitiesViewModel)
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
            Dim viewModel As CitiesViewModel = ViewModelSource.Create(Function() New CitiesViewModel(map))
            LayoutRoot.DataContext = viewModel
            placePointer.Content = viewModel
        End Sub

        Private Sub GalleryItemClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim item As PhotoGalleryItemControl = TryCast(sender, PhotoGalleryItemControl)
            If item IsNot Nothing Then ViewModel.SelectedPlace = TryCast(item.DataContext, PlaceInfo)
        End Sub

        Private Sub OnGalleryClose(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ViewModel.SelectedCity = Nothing
        End Sub

        Private Sub OnBackClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ViewModel.SelectedCity = Nothing
        End Sub

        Private Sub photoGallery_MouseLeftButtonUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            ViewModel.SelectedCity = Nothing
        End Sub

        Private Sub placeControl_ShowNextSight(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ViewModel.ShowNextSight()
        End Sub

        Private Sub placeControl_ShowPreviousSight(ByVal sender As Object, ByVal e As RoutedEventArgs)
            ViewModel.ShowPrevSight()
        End Sub
    End Class

    Public Enum ViewType
        Map
        Gallery
        Detail
    End Enum

    <POCOViewModel>
    Public Class CitiesViewModel
        Inherits BindableBase

        Const InitialZoomLevel As Integer = 5

        Const MaxZoomLevelInWorldView As Integer = 7

        Const MinZoomLevelInWorldView As Integer = 5

        Const MinZoomLevelInCityView As Integer = 15

        Const MaxZoomLevelInCityView As Integer = 18

        Public Overridable Property Cities As ObservableCollection(Of CityInfo)

        Public Overridable Property CitiesMini As ObservableCollection(Of CityInfo)

        Public Overridable Property SelectedCity As CityInfo

        Protected Sub OnSelectedCityChanged()
            SelectedPlace = Nothing
            UpdateViewType()
        End Sub

        Public Overridable Property SelectedPlace As PlaceInfo

        Protected Sub OnSelectedPlaceChanged()
            UpdateViewType()
        End Sub

        Public Overridable Property ViewType As ViewType

        Public Overridable Property CenterPoint As CoordPoint

        Public Overridable Property CityPoint As Point

        Public Overridable Property ZoomLevel As Double

        Public Overridable Property MinZoomLevel As Integer

        Public Overridable Property MaxZoomLevel As Integer

        Private ReadOnly map As MapControl

        Private ReadOnly Property Layer As LayerBase
            Get
                Return If(map.Layers.Count > 0, map.Layers(0), Nothing)
            End Get
        End Property

        Public Sub New(ByVal map As MapControl)
            Me.map = map
            Cities = New ObservableCollection(Of CityInfo)()
            CitiesMini = New ObservableCollection(Of CityInfo)()
            ZoomLevel = InitialZoomLevel
            MinZoomLevel = MinZoomLevelInWorldView
            MaxZoomLevel = MaxZoomLevelInWorldView
            ViewType = ViewType.Map
            CenterPoint = New GeoPoint(47.5, 2)
            CityPoint = New Point(0, 0)
            LoadDataFromXML()
        End Sub

        Private Sub LoadDataFromXML()
            Dim document As XDocument = LoadXmlFromResources("/Data/CitiesData.xml")
            If document IsNot Nothing Then
                For Each element As XElement In document.Element("Cities").Elements()
                    Dim cityName As String = element.Element("CityName").Value
                    Dim cityLocation As GeoPoint = New GeoPoint(Convert.ToDouble(element.Element("Latitude").Value, CultureInfo.InvariantCulture), Convert.ToDouble(element.Element("Longitude").Value, CultureInfo.InvariantCulture))
                    Dim cityInfo As CityInfo = New CityInfo(cityName, cityLocation)
                    For Each placeElement As XElement In element.Element("Places").Elements()
                        Dim placeName As String = placeElement.Element("Name").Value
                        Dim placeLocation As GeoPoint = New GeoPoint(Convert.ToDouble(placeElement.Element("Latitude").Value, CultureInfo.InvariantCulture), Convert.ToDouble(placeElement.Element("Longitude").Value, CultureInfo.InvariantCulture))
                        Dim placeDescription As String = placeElement.Element("Description").Value
                        Dim placeImageUri As Uri = New Uri(placeElement.Element("ImageUri").Value, UriKind.RelativeOrAbsolute)
                        cityInfo.Places.Add(New PlaceInfo(placeName, cityName, placeLocation, placeDescription, New BitmapImage(placeImageUri)))
                    Next

                    Cities.Add(cityInfo)
                    CitiesMini.Add(cityInfo)
                Next
            End If
        End Sub

        Private Sub UpdateViewType()
            If SelectedCity IsNot Nothing Then
                ViewType = If(SelectedPlace IsNot Nothing, ViewType.Detail, ViewType.Gallery)
            Else
                ViewType = ViewType.Map
            End If
        End Sub

        Protected Sub OnViewTypeChanged()
            Select Case ViewType
                Case ViewType.Map
_Select0_CaseMapDemo_ViewType_Map:
                    MinZoomLevel = MinZoomLevelInWorldView
                    MaxZoomLevel = MaxZoomLevelInWorldView
                    ZoomLevel = 5
                Case ViewType.Gallery
                    MinZoomLevel = MinZoomLevelInWorldView
                    MaxZoomLevel = MaxZoomLevelInWorldView
                    ZoomLevel = 5
                    CityPoint = Layer.GeoToScreenPoint(SelectedCity.Location)
                Case ViewType.Detail
                    MinZoomLevel = MinZoomLevelInCityView
                    MaxZoomLevel = MaxZoomLevelInCityView
                    ZoomLevel = 17
                    CenterPoint = SelectedPlace.Location
                Case Else
                    GoTo _Select0_CaseMapDemo_ViewType_Map
            End Select
        End Sub

        Public Sub ShowNextSight()
            If SelectedCity IsNot Nothing AndAlso SelectedPlace IsNot Nothing Then
                Dim index As Integer = SelectedCity.Places.IndexOf(SelectedPlace) + 1
                SelectedPlace = If(index < SelectedCity.Places.Count, SelectedCity.Places(index), SelectedCity.Places(0))
                CenterPoint = SelectedPlace.Location
            End If
        End Sub

        Public Sub ShowPrevSight()
            If SelectedCity IsNot Nothing AndAlso SelectedPlace IsNot Nothing Then
                Dim index As Integer = SelectedCity.Places.IndexOf(SelectedPlace) - 1
                SelectedPlace = If(index < 0, SelectedCity.Places(SelectedCity.Places.Count - 1), SelectedCity.Places(index))
                CenterPoint = SelectedPlace.Location
            End If
        End Sub
    End Class

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
