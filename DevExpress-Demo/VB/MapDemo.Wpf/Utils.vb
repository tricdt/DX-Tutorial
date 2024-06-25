Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Resources
Imports System.Windows
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Xml.Linq
Imports DevExpress.Map
Imports DevExpress.Xpf.Map

Namespace MapDemo

    Public Enum TemperatureScale
        Fahrenheit
        Celsius
    End Enum

    Public Class DemoValuesProvider

        Public ReadOnly Property DevexpressBingKey As String
            Get
                Return BingKey
            End Get
        End Property

        Public ReadOnly Property BingMapKinds As IEnumerable(Of BingMapKind)
            Get
                Return New BingMapKind() {BingMapKind.Area, BingMapKind.Road, BingMapKind.Hybrid, BingMapKind.RoadLight, BingMapKind.RoadGray, BingMapKind.RoadDark}
            End Get
        End Property

        Public ReadOnly Property OSMBaseLayers As IEnumerable(Of OpenStreetMapKind)
            Get
                Return New OpenStreetMapKind() {OpenStreetMapKind.Basic, OpenStreetMapKind.CycleMap, OpenStreetMapKind.Hot, OpenStreetMapKind.Transport}
            End Get
        End Property

        Public ReadOnly Property OSMOverlayLayers As IEnumerable(Of Object)
            Get
                Return New Object() {"None", OpenStreetMapKind.SeaMarks, OpenStreetMapKind.HikingRoutes, OpenStreetMapKind.CyclingRoutes, OpenStreetMapKind.PublicTransport}
            End Get
        End Property

        Public ReadOnly Property ShapeMapTypes As IEnumerable(Of String)
            Get
                Return New String() {"GDP", "Population", "Political"}
            End Get
        End Property

        Public ReadOnly Property ShapefileMapTypes As IEnumerable(Of String)
            Get
                Return New String() {"World", "Africa", "South America", "North America", "Australia", "Eurasia"}
            End Get
        End Property

        Public ReadOnly Property TemperatureUnit As IEnumerable(Of TemperatureScale)
            Get
                Return New TemperatureScale() {TemperatureScale.Celsius, TemperatureScale.Fahrenheit}
            End Get
        End Property

        Public ReadOnly Property BubbleMarkerTypes As IEnumerable(Of MarkerType)
            Get
                Return New MarkerType() {MarkerType.Circle, MarkerType.Cross, MarkerType.Diamond, MarkerType.Hexagon, MarkerType.InvertedTriangle, MarkerType.Triangle, MarkerType.Pentagon, MarkerType.Plus, MarkerType.Square, MarkerType.Star5, MarkerType.Star6, MarkerType.Star8}
            End Get
        End Property

        Public ReadOnly Property RouteOptimizations As IEnumerable(Of BingRouteOptimization)
            Get
                Return New BingRouteOptimization() {BingRouteOptimization.MinimizeTimeWithTraffic, BingRouteOptimization.MinimizeTime, BingRouteOptimization.MinimizeDistance}
            End Get
        End Property

        Public ReadOnly Property TrafficIncidentTypes As IEnumerable(Of BingTrafficIncidentType)
            Get
                Return New BingTrafficIncidentType() {BingTrafficIncidentType.Accident, BingTrafficIncidentType.Congestion, BingTrafficIncidentType.DisabledVehicle, BingTrafficIncidentType.MassTransit, BingTrafficIncidentType.Miscellaneous, BingTrafficIncidentType.OtherNews, BingTrafficIncidentType.PlannedEvent, BingTrafficIncidentType.RoadHazard, BingTrafficIncidentType.Construction, BingTrafficIncidentType.Alert, BingTrafficIncidentType.Weather}
            End Get
        End Property

        Private Shared projectionsField As List(Of Projection) = Nothing

        Public ReadOnly Property Projections As List(Of Projection)
            Get
                If projectionsField Is Nothing Then projectionsField = PopulateProjectionData()
                Return projectionsField
            End Get
        End Property

        Public ReadOnly Property DefaultProjection As Projection
            Get
                Return Projections(12)
            End Get
        End Property

        Private Shared Function PopulateProjectionData() As List(Of Projection)
            Dim LAEAParent As Projection = New Projection() With {.Name = "Lambert Azimuthal Equal Area", .PrjInstance = Nothing}
            Return New List(Of Projection)() From {New Projection() With {.Name = "Spherical Mercator", .PrjInstance = New SphericalMercatorProjection()}, New Projection() With {.Name = "Equal Area", .PrjInstance = New EqualAreaProjection()}, New Projection() With {.Name = "Equirectangular", .PrjInstance = New EquirectangularProjection()}, New Projection() With {.Name = "Elliptical Mercator", .PrjInstance = New EllipticalMercatorProjection()}, New Projection() With {.Name = "Miller", .PrjInstance = New MillerProjection()}, New Projection() With {.Name = "Equidistant", .PrjInstance = New EquidistantProjection()}, New Projection() With {.Name = "Lambert Cylindrical Equal Area", .PrjInstance = New LambertCylindricalEqualAreaProjection()}, LAEAParent, New Projection() With {.Name = "ETRS89", .PrjInstance = New Etrs89LambertAzimuthalEqualAreaProjection(), .ParentPrjName = LAEAParent.Name}, New Projection() With {.Name = "North Pole", .PrjInstance = New NorthPole(), .ParentPrjName = LAEAParent.Name}, New Projection() With {.Name = "South Pole", .PrjInstance = New SouthPole(), .ParentPrjName = LAEAParent.Name}, New Projection() With {.Name = "Braun Stereographic", .PrjInstance = New BraunStereographicProjection()}, New Projection() With {.Name = "Kavrayskiy VII", .PrjInstance = New KavrayskiyProjection()}, New Projection() With {.Name = "Sinusoidal", .PrjInstance = New SinusoidalProjection()}, New Projection() With {.Name = "EPSG:4326", .PrjInstance = New EPSG4326Projection()}}
        End Function
    End Class

    Public Module DemoUtils

        Public Iterator Function FindMap(ByVal obj As DependencyObject) As IEnumerable(Of MapControl)
            If obj IsNot Nothing Then
                For i As Integer = 0 To VisualTreeHelper.GetChildrenCount(obj) - 1
                    Dim child As DependencyObject = VisualTreeHelper.GetChild(obj, i)
                    If child IsNot Nothing AndAlso TypeOf child Is MapControl Then
                        Yield CType(child, MapControl)
                    End If

                    For Each childOfChild As MapControl In FindMap(child)
                        Yield childOfChild
                    Next
                Next
            End If
        End Function
    End Module

    Public Module DataLoader

        Private Function GetStream(ByVal fileName As String) As Stream
            fileName = "/MapDemo;component" & fileName
            Dim uri As Uri = New Uri(fileName, UriKind.RelativeOrAbsolute)
            Return Application.GetResourceStream(uri).Stream
        End Function

        Public Function LoadXmlFromResources(ByVal fileName As String) As XDocument
            Try
                Return XDocument.Load(GetStream(fileName))
            Catch
                Return Nothing
            End Try
        End Function

        Public Function LoadStreamFromResources(ByVal fileName As String) As Stream
            Try
                Return GetStream(fileName)
            Catch
                Return Nothing
            End Try
        End Function
    End Module

    Public Class DoubleToTimeSpanConvert
        Implements IValueConverter

#Region "IValueConvector implementation"
        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim doubleValue As Double = 3600 * CDbl(value)
            Return New TimeSpan(0, 0, CInt(Math.Ceiling(doubleValue)))
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
#End Region
    End Class

    Public Class SelectedLayerToVisibilityConverter
        Implements IValueConverter

#Region "IValueConverter implementation"
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return If(TypeOf value Is String, Visibility.Collapsed, Visibility.Visible)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
#End Region
    End Class

    Public Class SelectedLayerToKindConverter
        Implements IValueConverter

#Region "IValueConverter implementation"
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return If(TypeOf value Is String, OpenStreetMapKind.SeaMarks, value)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
#End Region
    End Class

    Public Class PlaneInfoToPathVisibilityConverter
        Implements IValueConverter

#Region "IValueConverter implementation"
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return value Is parameter
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
#End Region
    End Class

    Public Class RangeColor

        Private ReadOnly rangeMinField As Integer

        Private ReadOnly rangeMaxField As Integer

        Private ReadOnly fillField As Color

        Public ReadOnly Property RangeMin As Integer
            Get
                Return rangeMinField
            End Get
        End Property

        Public ReadOnly Property RangeMax As Integer
            Get
                Return rangeMaxField
            End Get
        End Property

        Public ReadOnly Property Fill As Color
            Get
                Return fillField
            End Get
        End Property

        Public Sub New(ByVal rangeMin As Integer, ByVal rangeMax As Integer, ByVal fill As Color)
            rangeMinField = rangeMin
            rangeMaxField = rangeMax
            fillField = fill
        End Sub
    End Class

    Public Class ViewTypeToBoolConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If targetType Is GetType(Boolean) AndAlso TypeOf value Is ViewType AndAlso TypeOf parameter Is ViewType Then Return CType(value, ViewType) = CType(parameter, ViewType)
            Return False
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If TypeOf value Is Boolean Then
                If targetType Is GetType(ViewType) Then
                    Return If(CBool(value), ViewType.Gallery, ViewType.Map)
                End If
            End If

            Return Nothing
        End Function
    End Class

    Public Class ViewTypeToVisibilityConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If targetType Is GetType(Visibility) AndAlso TypeOf value Is ViewType AndAlso TypeOf parameter Is ViewType Then Return If(CType(value, ViewType) = CType(parameter, ViewType), Visibility.Visible, Visibility.Hidden)
            Return Visibility.Hidden
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If TypeOf value Is Visibility Then
                If targetType Is GetType(ViewType) Then
                    Return If(CType(value, Visibility) = Visibility.Visible, ViewType.Gallery, ViewType.Map)
                End If
            End If

            Return Nothing
        End Function
    End Class

    Public Class CoordinateSystemTypeToVisibilityConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If targetType Is GetType(Visibility) AndAlso TypeOf value Is CoordinateSystemType AndAlso TypeOf parameter Is CoordinateSystemType Then Return If(CType(value, CoordinateSystemType) = CType(parameter, CoordinateSystemType), Visibility.Visible, Visibility.Hidden)
            Return Visibility.Hidden
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If TypeOf value Is Visibility Then
                If targetType Is GetType(CoordinateSystemType) Then Return If(CType(value, Visibility) = Visibility.Visible, CoordinateSystemType.Geo, CoordinateSystemType.Cartesian)
            End If

            Return Nothing
        End Function
    End Class

    Public Class DoubleToRenderTransforOffsetConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If targetType Is GetType(Double) AndAlso TypeOf value Is Double Then
                Dim doubleValue As Double = CDbl(value)
                If TypeOf parameter Is Double Then Return doubleValue / CDbl(parameter)
                Return 0
            End If

            Return Nothing
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class

    Public Class CoordinateSystemTypeToCoordinateSystemConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If targetType Is GetType(MapCoordinateSystem) AndAlso TypeOf value Is CoordinateSystemType AndAlso CType(value, CoordinateSystemType) = CoordinateSystemType.Cartesian Then Return New CartesianMapCoordinateSystem()
            Return New GeoMapCoordinateSystem()
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If TypeOf value Is MapCoordinateSystem AndAlso targetType Is GetType(CoordinateSystemType) Then Return If(TypeOf value Is GeoMapCoordinateSystem, CoordinateSystemType.Geo, CoordinateSystemType.Cartesian)
            Return Nothing
        End Function
    End Class

    Public Class ItemToTextConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is MapItem Then
                Dim path As MapPath = TryCast(value, MapPath)
                If path IsNot Nothing Then Return CalculateTitle(path)
                Dim title As ShapeTitle = TryCast(value, ShapeTitle)
                If title IsNot Nothing Then Return CalculateTitle(title.MapShape)
            End If

            Return Nothing
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class

    Public Class ItemToImageSourceConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is MapItem Then
                Dim title As ShapeTitle = TryCast(value, ShapeTitle)
                Return GetItemImageSource(If(title IsNot Nothing, title.MapShape, CType(value, MapItem)))
            End If

            Return Nothing
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class

    Public Class ItemToImageVisibilityConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is MapItem AndAlso targetType Is GetType(Visibility) Then
                Dim title As ShapeTitle = TryCast(value, ShapeTitle)
                Dim imageSource As String = GetItemImageSource(If(title IsNot Nothing, title.MapShape, CType(value, MapItem)))
                Return If(String.IsNullOrWhiteSpace(imageSource), Visibility.Collapsed, Visibility.Visible)
            End If

            Return Nothing
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class

    Public Class CountToMatrixTransformConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim itemsList As List(Of MapItem) = TryCast(value, List(Of MapItem))
            Dim count As Double = If(itemsList IsNot Nothing, itemsList.Count, 1.0)
            Dim scaleFactor As Double = Math.Log10(count / 5.0) * 0.02 + 0.05
            Dim offsetKoefX As Double = 318
            Dim offsetKoefY As Double = -455
            Return New MatrixTransform(scaleFactor, 0, 0, scaleFactor, scaleFactor * offsetKoefX, scaleFactor * offsetKoefY).Matrix
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class

    Public Class CountToTextConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim itemsList As List(Of MapItem) = TryCast(value, List(Of MapItem))
            Dim count As Integer = If(itemsList IsNot Nothing, itemsList.Count, 1)
            Return String.Format("Cluster contains {0} item(s)", count)
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class

    Public Class MapTypeToVisibilityConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is Integer AndAlso targetType Is GetType(Visibility) AndAlso TypeOf parameter Is String Then
                Dim index As Integer = CInt(value)
                Dim mapType As String = CStr(parameter)
                If index = 1 AndAlso Equals(mapType, "population") Then Return Visibility.Visible
                If index = 0 AndAlso Equals(mapType, "gdp") Then Return Visibility.Visible
                Return Visibility.Collapsed
            End If

            Return Nothing
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class

    Public Class ProviderNameToImageVisibilityConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is ProviderName AndAlso targetType Is GetType(Visibility) Then
                Return If(CType(value, ProviderName) = ProviderName.Bing, Visibility.Visible, Visibility.Collapsed)
            End If

            Return Nothing
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class

    Public Class ProviderNameToCopyrightTextConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is ProviderName Then
                Dim providerName As ProviderName = CType(value, ProviderName)
                If providerName = ProviderName.Bing Then Return "Copyright © " & Date.Now.Year & " Microsoft and its suppliers. All rights reserved."
                If providerName = ProviderName.Osm Then Return "© OpenStreetMap contributors"
                Return Nothing
            End If

            Return Nothing
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class

    Public Class BoolToCircularScrollingConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is Boolean AndAlso targetType Is GetType(CircularScrollingMode) Then
                Return If(CBool(value), CircularScrollingMode.TilesAndVectorItems, CircularScrollingMode.None)
            End If

            Return Nothing
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class

    Public Class SelectedItemToVisibilityConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If targetType Is GetType(Visibility) Then Return If(value Is Nothing, Visibility.Hidden, Visibility.Visible)
            Return Nothing
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class

    Public Class CountryToFlagConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is String AndAlso targetType Is GetType(ImageSource) Then
                Dim name As String = CStr(value)
                If String.IsNullOrEmpty(name) Then Return Nothing
                Return "..\Images\Flags\" & name & ".png"
            End If

            Return Nothing
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class ShapefileWorldResources

        Public ReadOnly Property CountriesFileUri As Uri
            Get
                Return New Uri("/MapDemo;component/Data/Shapefiles/Maps/Countries.shp", UriKind.RelativeOrAbsolute)
            End Get
        End Property

        Public ReadOnly Property AfricaFileUri As Uri
            Get
                Return New Uri("/MapDemo;component/Data/Shapefiles/Maps/Africa.shp", UriKind.RelativeOrAbsolute)
            End Get
        End Property

        Public ReadOnly Property SouthAmericaFileUri As Uri
            Get
                Return New Uri("/MapDemo;component/Data/Shapefiles/Maps/SouthAmerica.shp", UriKind.RelativeOrAbsolute)
            End Get
        End Property

        Public ReadOnly Property NorthAmericaFileUri As Uri
            Get
                Return New Uri("/MapDemo;component/Data/Shapefiles/Maps/NorthAmerica.shp", UriKind.RelativeOrAbsolute)
            End Get
        End Property

        Public ReadOnly Property AustraliaFileUri As Uri
            Get
                Return New Uri("/MapDemo;component/Data/Shapefiles/Maps/Australia.shp", UriKind.RelativeOrAbsolute)
            End Get
        End Property

        Public ReadOnly Property EurasiaFileUri As Uri
            Get
                Return New Uri("/MapDemo;component/Data/Shapefiles/Maps/Eurasia.shp", UriKind.RelativeOrAbsolute)
            End Get
        End Property

        Public ReadOnly Property IcelandFileUri As Uri
            Get
                Return New Uri("/MapDemo;component/Data/Shapefiles/Iceland.shp", UriKind.RelativeOrAbsolute)
            End Get
        End Property

        Public Sub New()
        End Sub
    End Class

    Public Class PhotoGalleryResources

        Public ReadOnly Property CityInformationControlSource As ImageSource
            Get
                Return New BitmapImage(New Uri("/MapDemo;component/Images/PhotoGallery/CityInformationControl.png", UriKind.RelativeOrAbsolute))
            End Get
        End Property

        Public ReadOnly Property LabelControlImageSource As ImageSource
            Get
                Return New BitmapImage(New Uri("/MapDemo;component/Images/PhotoGallery/Label.png", UriKind.RelativeOrAbsolute))
            End Get
        End Property

        Public ReadOnly Property PlaceInfoControlPrevImageSource As ImageSource
            Get
                Return GetSvgImage("pack://application:,,,/MapDemo;component/Images/PhotoGallery/PrevPlace.svg", New Size(16, 16))
            End Get
        End Property

        Public ReadOnly Property PlaceInfoControlNextImageSource As ImageSource
            Get
                Return GetSvgImage("pack://application:,,,/MapDemo;component/Images/PhotoGallery/NextPlace.svg", New Size(16, 16))
            End Get
        End Property

        Public Sub New()
        End Sub

        Private Shared Function GetSvgImage(ByVal imagePath As String, ByVal imageSize As Size) As ImageSource
            Dim extension = New DevExpress.Xpf.Core.SvgImageSourceExtension() With {.Uri = New Uri(imagePath), .Size = imageSize}
            Return CType(extension.ProvideValue(Nothing), ImageSource)
        End Function
    End Class

    Public Module HotelRoomTooltipHelper

#Region "inner class"
        Private Class HotelImagesGenerator

            Private Class PathsIndexPair

                Public Property Paths As String()

                Public Property Index As Integer
            End Class

            Private Shared ReadOnly Categories As String() = New String() {"Restaurant", "MeetingRoom", "Bathroom", "Bedroom", "Outofdoors", "ServiceRoom", "Pool", "Lobby"}

            Const basePath As String = "/MapDemo;component/"

            Private hotelIndexField As Integer = 0

            Private filesWithIndices As List(Of PathsIndexPair) = New List(Of PathsIndexPair)()

            Public Property HotelIndex As Integer
                Get
                    Return hotelIndexField
                End Get

                Set(ByVal value As Integer)
                    hotelIndexField = value
                    UpdateIndices()
                End Set
            End Property

            Public Sub New()
                For Each category As String In Categories
                    filesWithIndices.Add(New PathsIndexPair() With {.Index = 0, .Paths = GetAvailableFiles(category)})
                Next
            End Sub

            Private Sub UpdateIndices()
                filesWithIndices(0).Index = hotelIndexField * 2
                filesWithIndices(1).Index = 0
                filesWithIndices(2).Index = hotelIndexField * 4
                filesWithIndices(6).Index = hotelIndexField
            End Sub

            Private Function GetAvailableFiles(ByVal category As String) As String()
                Dim asm = Assembly.GetExecutingAssembly()
                Dim resName As String = asm.GetName().Name & ".g.resources"
                Using stream = asm.GetManifestResourceStream(resName)
                    Using reader = New ResourceReader(stream)
                        Return reader.Cast(Of DictionaryEntry)().[Select](Function(entry) CStr(entry.Key)).Where(Function(entry) entry.StartsWith("images/hotels/" & category.ToLowerInvariant())).ToArray()
                    End Using
                End Using
            End Function

            Private Function GetImagePath(ByVal category As Integer, ByVal name As String, ByVal roomCat As Integer) As String
                If category = 4 Then filesWithIndices(3).Index = roomCat
                Return GetCategoryImagePath(filesWithIndices(category - 1))
            End Function

            Private Function GetCategoryImagePath(ByVal pathsWithIndex As PathsIndexPair) As String
                If pathsWithIndex.Paths.Length = 0 Then Return Nothing
                Dim index As Integer = pathsWithIndex.Index Mod pathsWithIndex.Paths.Length
                pathsWithIndex.Index += 1
                Return pathsWithIndex.Paths(index)
            End Function

            Public Function GetItemImagePath(ByVal item As MapItem) As String
                Dim imagePath As String = GetImagePath(CInt(item.Attributes("CATEGORY").Value), item.Attributes("NAME").Value.ToString(), CInt(item.Attributes("ROOMCAT").Value))
                If Equals(imagePath, Nothing) Then Return Nothing
                Dim totalPath As String = basePath & imagePath
                item.Attributes.Add(New MapItemAttribute() With {.Name = "IMAGESOURCE", .Value = totalPath})
                Return totalPath
            End Function
        End Class

#End Region
        Private imagesGenerator As HotelImagesGenerator = New HotelImagesGenerator()

        Public Function CalculateTitle(ByVal item As MapItem) As String
            Dim category As Integer = CInt(item.Attributes("CATEGORY").Value)
            Dim text As String = CStr(item.Attributes("NAME").Value)
            Return If(category = 4, String.Format("Room: {0}", text), text)
        End Function

        Public Function GetItemImageSource(ByVal item As MapItem) As String
            If item Is Nothing Then Return Nothing
            Dim attr As MapItemAttribute = item.Attributes("IMAGESOURCE")
            Return If(attr IsNot Nothing, CStr(attr.Value), imagesGenerator.GetItemImagePath(item))
        End Function

        Public Sub UpdateHotelIndex(ByVal index As Integer)
            imagesGenerator.HotelIndex = index
        End Sub
    End Module

    Public Module MapArrowsDemoHelper

        Private dataItemsField As List(Of WindDataItem)

        Public ReadOnly Property DataItems As List(Of WindDataItem)
            Get
                If dataItemsField Is Nothing Then
                    dataItemsField = New List(Of WindDataItem)()
                    Try
                        Using reader As StreamReader = New StreamReader(LoadStreamFromResources("/Data/windData.csv"))
                            While Not reader.EndOfStream
                                Dim values = reader.ReadLine().Split(" "c)
                                Call dataItemsField.Add(New WindDataItem(Double.Parse(values(1), CultureInfo.InvariantCulture), Double.Parse(values(2), CultureInfo.InvariantCulture), Double.Parse(values(3), CultureInfo.InvariantCulture), Double.Parse(values(4), CultureInfo.InvariantCulture)))
                            End While
                        End Using
                    Catch
                        Throw New Exception("It's impossible to load wind data")
                    End Try
                End If

                Return dataItemsField
            End Get
        End Property
    End Module

    Public Class WindDataItem

        Private _P1 As CoordPoint, _P2 As CoordPoint, _Speed As Double

        Const ArrowLength As Double = 70000

        Public Property P1 As CoordPoint
            Get
                Return _P1
            End Get

            Private Set(ByVal value As CoordPoint)
                _P1 = value
            End Set
        End Property

        Public Property P2 As CoordPoint
            Get
                Return _P2
            End Get

            Private Set(ByVal value As CoordPoint)
                _P2 = value
            End Set
        End Property

        Public Property Speed As Double
            Get
                Return _Speed
            End Get

            Private Set(ByVal value As Double)
                _Speed = value
            End Set
        End Property

        Public Sub New(ByVal latitude As Double, ByVal longitude As Double, ByVal direction As Double, ByVal speed As Double)
            P1 = New GeoPoint(latitude, longitude)
            P2 = GeoUtils.CalculateDestinationPoint(New GeoPoint(latitude, longitude), ArrowLength, direction)
            Me.Speed = speed
        End Sub
    End Class
End Namespace
