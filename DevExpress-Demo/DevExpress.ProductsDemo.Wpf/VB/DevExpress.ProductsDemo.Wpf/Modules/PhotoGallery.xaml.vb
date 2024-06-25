Imports System
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Input
Imports System.Windows.Media.Imaging
Imports System.Xml.Linq
Imports DevExpress.Map
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Map

Namespace ProductsDemo.Modules

    Public Partial Class PhotoGallery
        Inherits System.Windows.Controls.UserControl

        Public Sub New()
            Me.InitializeComponent()
            Dim viewModel As ProductsDemo.Modules.CitiesViewModel = New ProductsDemo.Modules.CitiesViewModel(Me.map, TryCast(Me.Resources("citySmallIconTemplate"), System.Windows.DataTemplate))
            Me.LayoutRoot.DataContext = viewModel
            Me.placePointer.Content = viewModel
        End Sub

        Private Sub GalleryItemClick(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Dim item As ProductsDemo.PhotoGalleryItemControl = TryCast(sender, ProductsDemo.PhotoGalleryItemControl)
            If item IsNot Nothing Then Me.ViewModel.SelectedPlace = TryCast(item.DataContext, ProductsDemo.PlaceInfo)
        End Sub

        Private Sub OnGalleryClose(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.ViewModel.SelectedCity = Nothing
        End Sub

        Private Sub OnBackClick(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.ViewModel.SelectedCity = Nothing
        End Sub

        Private Sub photoGallery_MouseLeftButtonUp(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)
            Me.ViewModel.SelectedCity = Nothing
        End Sub

        Private Sub placeControl_ShowNextSight(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.ViewModel.ShowNextSight()
        End Sub

        Private Sub placeControl_ShowPreviousSight(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.ViewModel.ShowPrevSight()
        End Sub

        Private ReadOnly Property ViewModel As CitiesViewModel
            Get
                Return TryCast(Me.LayoutRoot.DataContext, ProductsDemo.Modules.CitiesViewModel)
            End Get
        End Property
    End Class

    Public Class CitiesViewModel
        Inherits DevExpress.Mvvm.BindableBase

        Const MaxZoomLevelInWorldView As Integer = 7

        Const MinZoomLevelInWorldView As Integer = 5

        Const MinZoomLevelInCityView As Integer = 15

        Const MaxZoomLevelInCityView As Integer = 18

        Private citiesCore As System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Xpf.Map.MapCustomElement) = Nothing

        Private selectedCityCore As ProductsDemo.CityInfo = Nothing

        Private selectedPlaceCore As ProductsDemo.PlaceInfo = Nothing

        Private viewTypeCore As ProductsDemo.PhotoGalleryViewType = ProductsDemo.PhotoGalleryViewType.Map

        Private centerPointCore As DevExpress.Map.CoordPoint = New DevExpress.Xpf.Map.GeoPoint(47.5, 2)

        Private cityPointCore As System.Windows.Point = New System.Windows.Point(0, 0)

        Private zoomLevelCore As Double = 5

        Private minZoomLevelField As Integer = ProductsDemo.Modules.CitiesViewModel.MinZoomLevelInWorldView

        Private maxZoomLevelField As Integer = ProductsDemo.Modules.CitiesViewModel.MaxZoomLevelInWorldView

        Private citySmallIconsCore As System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Xpf.Map.MapCustomElement) = Nothing

        Public Property Cities As ObservableCollection(Of DevExpress.Xpf.Map.MapCustomElement)
            Get
                Return Me.citiesCore
            End Get

            Private Set(ByVal value As ObservableCollection(Of DevExpress.Xpf.Map.MapCustomElement))
                SetProperty(Me.citiesCore, value, "Cities")
            End Set
        End Property

        Public Property CitySmallIcons As ObservableCollection(Of DevExpress.Xpf.Map.MapCustomElement)
            Get
                Return Me.citySmallIconsCore
            End Get

            Private Set(ByVal value As ObservableCollection(Of DevExpress.Xpf.Map.MapCustomElement))
                SetProperty(Me.citySmallIconsCore, value, "CitySmallIcons")
            End Set
        End Property

        Public Property SelectedCity As CityInfo
            Get
                Return Me.selectedCityCore
            End Get

            Set(ByVal value As CityInfo)
                SetProperty(Me.selectedCityCore, value, "SelectedCity", New Global.System.Action(AddressOf Me.SelectedItemPropertyChanged))
            End Set
        End Property

        Public Property SelectedPlace As PlaceInfo
            Get
                Return Me.selectedPlaceCore
            End Get

            Set(ByVal value As PlaceInfo)
                SetProperty(Me.selectedPlaceCore, value, "SelectedPlace", New Global.System.Action(AddressOf Me.SelectedItemPropertyChanged))
            End Set
        End Property

        Public Property ViewType As PhotoGalleryViewType
            Get
                Return Me.viewTypeCore
            End Get

            Set(ByVal value As PhotoGalleryViewType)
                SetProperty(Me.viewTypeCore, value, "ViewType", New Global.System.Action(AddressOf Me.ViewTypePropertyChanged))
            End Set
        End Property

        Public Property CenterPoint As CoordPoint
            Get
                Return Me.centerPointCore
            End Get

            Set(ByVal value As CoordPoint)
                SetProperty(Me.centerPointCore, value, "CenterPoint")
            End Set
        End Property

        Public Property CityPoint As Point
            Get
                Return Me.cityPointCore
            End Get

            Set(ByVal value As Point)
                SetProperty(Me.cityPointCore, value, "CityPoint")
            End Set
        End Property

        Public Property ZoomLevel As Double
            Get
                Return Me.zoomLevelCore
            End Get

            Set(ByVal value As Double)
                SetProperty(Me.zoomLevelCore, value, "ZoomLevel")
            End Set
        End Property

        Public Property MinZoomLevel As Integer
            Get
                Return Me.minZoomLevelField
            End Get

            Set(ByVal value As Integer)
                SetProperty(Me.minZoomLevelField, value, "MinZoomLevel")
            End Set
        End Property

        Public Property MaxZoomLevel As Integer
            Get
                Return Me.maxZoomLevelField
            End Get

            Set(ByVal value As Integer)
                SetProperty(Me.maxZoomLevelField, value, "MaxZoomLevel")
            End Set
        End Property

        Private Sub SelectedItemPropertyChanged()
            Me.UpdateViewType()
        End Sub

        Private Sub ViewTypePropertyChanged()
            Me.Update()
        End Sub

        Private ReadOnly map As DevExpress.Xpf.Map.MapControl

        Private ReadOnly Property Layer As LayerBase
            Get
                Return If(Me.map.Layers.Count > 0, Me.map.Layers(0), Nothing)
            End Get
        End Property

        Public Sub New(ByVal map As DevExpress.Xpf.Map.MapControl, ByVal citySmallIconTemplate As System.Windows.DataTemplate)
            Me.map = map
            Me.Cities = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Xpf.Map.MapCustomElement)()
            Me.CitySmallIcons = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Xpf.Map.MapCustomElement)()
            Me.LoadDataFromXML(citySmallIconTemplate)
        End Sub

        Private Sub LoadDataFromXML(ByVal citySmallIconTemplate As System.Windows.DataTemplate)
            Dim fileName As String = ProductsDemo.Utils.GetRelativePath("CitiesData.xml")
            Dim document As System.Xml.Linq.XDocument = ProductsDemo.DataLoader.LoadXmlFromFile(fileName)
            If document IsNot Nothing Then
                For Each element As System.Xml.Linq.XElement In document.Element(CType(("Cities"), System.Xml.Linq.XName)).Elements()
                    Dim cityName As String = element.Element(CType(("CityName"), System.Xml.Linq.XName)).Value
                    Dim cityLocation As DevExpress.Xpf.Map.GeoPoint = New DevExpress.Xpf.Map.GeoPoint(System.Convert.ToDouble(element.Element(CType(("Latitude"), System.Xml.Linq.XName)).Value, System.Globalization.CultureInfo.InvariantCulture), System.Convert.ToDouble(element.Element(CType(("Longitude"), System.Xml.Linq.XName)).Value, System.Globalization.CultureInfo.InvariantCulture))
                    Dim cityInfo As ProductsDemo.CityInfo = New ProductsDemo.CityInfo(cityName, cityLocation)
                    For Each placeElement As System.Xml.Linq.XElement In element.Element(CType(("Places"), System.Xml.Linq.XName)).Elements()
                        Dim placeName As String = placeElement.Element(CType(("Name"), System.Xml.Linq.XName)).Value
                        Dim placeLocation As DevExpress.Xpf.Map.GeoPoint = New DevExpress.Xpf.Map.GeoPoint(System.Convert.ToDouble(placeElement.Element(CType(("Latitude"), System.Xml.Linq.XName)).Value, System.Globalization.CultureInfo.InvariantCulture), System.Convert.ToDouble(placeElement.Element(CType(("Longitude"), System.Xml.Linq.XName)).Value, System.Globalization.CultureInfo.InvariantCulture))
                        Dim placeDescription As String = placeElement.Element(CType(("Description"), System.Xml.Linq.XName)).Value
                        Dim placeImageUri As System.Uri = New System.Uri(placeElement.Element(CType(("ImageUri"), System.Xml.Linq.XName)).Value, System.UriKind.RelativeOrAbsolute)
                        cityInfo.Places.Add(New ProductsDemo.PlaceInfo(placeName, cityName, placeLocation, placeDescription, New System.Windows.Media.Imaging.BitmapImage(placeImageUri)))
                    Next

                    Dim binding As System.Windows.Data.Binding = New System.Windows.Data.Binding("ViewType") With {.Source = Me, .Converter = New ProductsDemo.ViewTypeToBoolConverter(), .ConverterParameter = ProductsDemo.PhotoGalleryViewType.Map}
                    Dim city As ProductsDemo.CityInformationControl = New ProductsDemo.CityInformationControl() With {.CityInfo = cityInfo}
                    city.SetBinding(ProductsDemo.CityInformationControl.VisibleProperty, binding)
                    Dim mapItem As DevExpress.Xpf.Map.MapCustomElement = New DevExpress.Xpf.Map.MapCustomElement() With {.Content = city, .Location = cityLocation}
                    AddHandler mapItem.MouseLeftButtonUp, New System.Windows.Input.MouseButtonEventHandler(AddressOf Me.OnMouseLeftButtonUp)
                    AddHandler mapItem.MouseLeftButtonDown, New System.Windows.Input.MouseButtonEventHandler(AddressOf Me.OnMouseLeftButtonDown)
                    AddHandler city.TouchDown, AddressOf Me.OnCityTouchDown
                    Me.Cities.Add(mapItem)
                    Me.CitySmallIcons.Add(New DevExpress.Xpf.Map.MapCustomElement() With {.Content = cityInfo, .ContentTemplate = citySmallIconTemplate, .Location = cityInfo.Location})
                Next
            End If
        End Sub

        Private Sub UpdateViewType()
            If Me.SelectedCity IsNot Nothing Then
                Me.ViewType = If(Me.SelectedPlace IsNot Nothing, ProductsDemo.PhotoGalleryViewType.Detail, ProductsDemo.PhotoGalleryViewType.Gallery)
            Else
                Me.ViewType = ProductsDemo.PhotoGalleryViewType.Map
            End If
        End Sub

        Private Sub Update()
            Select Case Me.ViewType
                Case ProductsDemo.PhotoGalleryViewType.Map
_Select0_CaseProductsDemo_PhotoGalleryViewType_Map:
                    Me.MinZoomLevel = ProductsDemo.Modules.CitiesViewModel.MinZoomLevelInWorldView
                    Me.MaxZoomLevel = ProductsDemo.Modules.CitiesViewModel.MaxZoomLevelInWorldView
                    Me.ZoomLevel = 5
                Case ProductsDemo.PhotoGalleryViewType.Gallery
                    Me.MinZoomLevel = ProductsDemo.Modules.CitiesViewModel.MinZoomLevelInWorldView
                    Me.MaxZoomLevel = ProductsDemo.Modules.CitiesViewModel.MaxZoomLevelInWorldView
                    Me.ZoomLevel = 5
                    Me.CityPoint = Me.Layer.GeoToScreenPoint(Me.SelectedCity.Location)
                Case ProductsDemo.PhotoGalleryViewType.Detail
                    Me.MinZoomLevel = ProductsDemo.Modules.CitiesViewModel.MinZoomLevelInCityView
                    Me.MaxZoomLevel = ProductsDemo.Modules.CitiesViewModel.MaxZoomLevelInCityView
                    Me.ZoomLevel = 17
                    Me.CenterPoint = Me.SelectedPlace.Location
                Case Else
                    GoTo _Select0_CaseProductsDemo_PhotoGalleryViewType_Map
            End Select
        End Sub

        Private Sub SelectCity(ByVal cityInfo As ProductsDemo.CityInfo)
            Me.SelectedPlace = Nothing
            Me.SelectedCity = cityInfo
            Me.UpdateViewType()
        End Sub

        Private Sub OnMouseLeftButtonUp(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)
            Dim element As DevExpress.Xpf.Map.MapCustomElement = TryCast(sender, DevExpress.Xpf.Map.MapCustomElement)
            If element IsNot Nothing Then
                Me.SelectCity(CType(element.Content, ProductsDemo.CityInformationControl).CityInfo)
                e.Handled = True
            End If
        End Sub

        Private Sub OnMouseLeftButtonDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)
            If TypeOf sender Is DevExpress.Xpf.Map.MapCustomElement Then e.Handled = True
        End Sub

        Private Sub OnCityTouchDown(ByVal sender As Object, ByVal e As System.Windows.Input.TouchEventArgs)
            Me.SelectCity(CType(sender, ProductsDemo.CityInformationControl).CityInfo)
        End Sub

        Public Sub ShowNextSight()
            If Me.SelectedCity IsNot Nothing AndAlso Me.SelectedPlace IsNot Nothing Then
                Dim index As Integer = Me.SelectedCity.Places.IndexOf(Me.SelectedPlace) + 1
                Me.SelectedPlace = If(index < Me.SelectedCity.Places.Count, Me.SelectedCity.Places(index), Me.SelectedCity.Places(0))
                Me.CenterPoint = Me.SelectedPlace.Location
            End If
        End Sub

        Public Sub ShowPrevSight()
            If Me.SelectedCity IsNot Nothing AndAlso Me.SelectedPlace IsNot Nothing Then
                Dim index As Integer = Me.SelectedCity.Places.IndexOf(Me.SelectedPlace) - 1
                Me.SelectedPlace = If(index < 0, Me.SelectedCity.Places(Me.SelectedCity.Places.Count - 1), Me.SelectedCity.Places(index))
                Me.CenterPoint = Me.SelectedPlace.Location
            End If
        End Sub
    End Class
End Namespace
