Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Xml.Linq
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Map
Imports DevExpress.Xpf.TreeMap
Imports System.Windows
Imports DevExpress.Xpf.DemoBase

Namespace TreeMapDemo

    <CodeFile("Modules/TreeMap/Selection.xaml")>
    <CodeFile("Modules/TreeMap/Selection.xaml.(cs)")>
    <NoAutogeneratedCodeFiles>
    Public Partial Class Selection
        Inherits TreeMapDemoModule

        Private viewModel As DashboardViewModel = New DashboardViewModel()

        Public Sub New()
            InitializeComponent()
            DataContext = ViewModelSource.Create(Function() New DashboardViewModel())
        End Sub

        Private Sub ShapefileLoader_ShapesLoaded(ByVal sender As Object, ByVal e As ShapesLoadedEventArgs)
            CType(DataContext, DashboardViewModel).SetMapItems(e.Shapes)
        End Sub

        Private Sub OnMapSizeChanged(ByVal sender As Object, ByVal e As SizeChangedEventArgs)
            map.EnableZooming = True
            map.ZoomToFitLayerItems()
            map.EnableZooming = False
        End Sub
    End Class

    Friend Class CountriesInfoDataReader

        Private Shared Function LoadStatistic(ByVal exportOfGoodsDynamic As XElement) As List(Of GDPStatisticByYear)
            Dim statistic As List(Of GDPStatisticByYear) = New List(Of GDPStatisticByYear)()
            For Each exportOfGoodsDynamicItem As XElement In exportOfGoodsDynamic.Elements("GDPByYear")
                Dim year As Integer = Integer.Parse(exportOfGoodsDynamicItem.Element("Year").Value)
                Dim exportOfGoodsPercent As Double = Double.Parse(exportOfGoodsDynamicItem.Element("GDP").Value, CultureInfo.InvariantCulture)
                Dim popDynamicItem As GDPStatisticByYear = New GDPStatisticByYear(year, exportOfGoodsPercent)
                statistic.Add(popDynamicItem)
            Next

            Return statistic
        End Function

        Public Shared Function Load() As List(Of CountryStatisticInfo)
            Dim data As List(Of CountryStatisticInfo) = New List(Of CountryStatisticInfo)()
            Try
                Dim Top10LargestCountries_xml As XDocument = LoadXDocumentFromResources("/Data/CountriesGDPByYears.xml")
                For Each countryInfo As XElement In Top10LargestCountries_xml.Root.Elements("CountryInfo")
                    Dim name As String = countryInfo.Element("Name").Value
                    Dim gdp As String = countryInfo.Element("GDP").Value
                    Dim continent As String = countryInfo.Element("Continent").Value
                    Dim statistic As List(Of GDPStatisticByYear) = LoadStatistic(countryInfo.Element("Statistic"))
                    Dim countryInfoInstance As CountryStatisticInfo = New CountryStatisticInfo(name, continent, Convert.ToDouble(gdp), statistic)
                    data.Add(countryInfoInstance)
                Next
            Catch
            End Try

            Return data
        End Function
    End Class

    <POCOViewModel>
    Public Class DashboardViewModel
        Inherits ViewModelBase

        Public Overridable Property CountriesData As List(Of CountryStatisticInfo)

        Public Overridable Property SelectedCountry As CountryStatisticInfo

        Public Overridable Property ChartTitle As String

        Public Overridable Property MapFileUri As Uri

        Protected Sub OnSelectedCountryChnged()
        End Sub

        Public Sub New()
            MapFileUri = GetResourceUri("/Data/CountriesGDP.shp")
            CountriesData = CountriesInfoDataReader.Load()
        End Sub

        Public Sub SetMapItems(ByVal layerMapItemCollection As IList(Of MapItem))
            For Each item As MapItem In layerMapItemCollection
                Dim shapeName As String = CStr(item.Attributes("NAME").Value)
                If Equals(shapeName, "Others") Then item.Visible = False
                Dim countryInfo As CountryStatisticInfo = CountriesData.Find(Function(info) Equals(info.Name, shapeName))
                If countryInfo IsNot Nothing Then
                    countryInfo.Shape = item
                    item.Attributes.Add(New MapItemAttribute() With {.Name = "CountryInfo", .Type = GetType(CountryStatisticInfo), .Value = countryInfo})
                End If
            Next

            SelectedCountry = CountriesData(25)
        End Sub
    End Class

    Public Class CountryStatisticInfo

        Private ReadOnly nameField As String

        Private ReadOnly continentField As String

        Private ReadOnly statistic As List(Of GDPStatisticByYear)

        Private ReadOnly gdpField As Double

        Private shapeField As WeakReference

        Public ReadOnly Property Name As String
            Get
                Return nameField
            End Get
        End Property

        Public ReadOnly Property Continent As String
            Get
                Return continentField
            End Get
        End Property

        Public ReadOnly Property GDPDynamic As List(Of GDPStatisticByYear)
            Get
                Return statistic
            End Get
        End Property

        Public ReadOnly Property GDP As Double
            Get
                Return gdpField
            End Get
        End Property

        Public Property Shape As MapItem
            Get
                Return If(shapeField IsNot Nothing, CType(shapeField.Target, MapItem), Nothing)
            End Get

            Set(ByVal value As MapItem)
                If value Is Nothing Then
                    shapeField = Nothing
                Else
                    If shapeField Is Nothing OrElse shapeField.Target IsNot value Then
                        shapeField = New WeakReference(value)
                    End If
                End If
            End Set
        End Property

        Public Sub New(ByVal name As String, ByVal continent As String, ByVal gdp As Double, ByVal statistic As List(Of GDPStatisticByYear))
            nameField = name
            continentField = continent
            Me.statistic = statistic
            gdpField = gdp
        End Sub
    End Class

    Public Class GDPStatisticByYear

        Private yearField As Integer

        Private gdpField As Double

        Public ReadOnly Property Year As Integer
            Get
                Return yearField
            End Get
        End Property

        Public ReadOnly Property GDP As Double
            Get
                Return gdpField
            End Get
        End Property

        Public Sub New(ByVal year As Integer, ByVal gdp As Double)
            yearField = year
            gdpField = gdp
        End Sub
    End Class

    Public Class SelectedMapItemConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then Return Nothing
            Dim country As CountryStatisticInfo = CType(value, CountryStatisticInfo)
            Return country.Shape
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Dim selectedShape As MapItem = CType(value, MapItem)
            Return selectedShape.Attributes("CountryInfo").Value
        End Function
    End Class

    Public Class ChartPaletteToMapColorsConverter
        Implements IValueConverter

        Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim chartColors As PaletteBase = CType(value, PaletteBase)
            Dim mapColors As DevExpress.Xpf.Map.ColorCollection = New DevExpress.Xpf.Map.ColorCollection()
            Dim rangeStops As DoubleCollection = CType(parameter, DoubleCollection)
            Dim colorsCount As Integer = CInt(rangeStops(rangeStops.Count - 1) - rangeStops(0)) + 1
            For i As Integer = 0 To colorsCount - 1
                mapColors.Add(chartColors(i))
            Next

            Return mapColors
        End Function

        Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class

    Friend Class ColorsMultiConverter
        Implements IMultiValueConverter

        Public Function Convert(ByVal values As Object(), ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IMultiValueConverter.Convert
            Dim chartPalette As DevExpress.Xpf.Charts.CustomPalette = New DevExpress.Xpf.Charts.CustomPalette()
            Dim palette As PaletteBase = If(values(0) Is Nothing, Nothing, CType(values(0), PaletteBase))
            Dim countryInfo As CountryStatisticInfo = If(values(0) Is Nothing, Nothing, CType(values(1), CountryStatisticInfo))
            If countryInfo IsNot Nothing AndAlso countryInfo.Shape IsNot Nothing Then
                Dim index As Integer = Short.Parse(countryInfo.Shape.Attributes("MAP_COLOR").Value.ToString())
                chartPalette.Colors.Add(palette(index))
            End If

            Return chartPalette
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetTypes As Type(), ByVal parameter As Object, ByVal culture As CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
            Return Nothing
        End Function
    End Class
End Namespace
