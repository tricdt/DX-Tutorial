Imports DevExpress.Mvvm
Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports DevExpress.SalesDemo.Model

Namespace ProductsDemo.Modules

    Public Class PivotGridViewModel
        Inherits ViewModelBase

        Private rangeStartField, rangeEndField, selectedRangeStartField, selectedRangeEndField As Date

        Private filteredSourceField, dataSourceField, filteredSourceByDateField As IEnumerable(Of SalesGroup)

        Private tilesDataField As IEnumerable(Of TileData)

        Private dataProvider As IDataProvider = GetDataProvider()

        Public Sub New()
            Dim range As DateTimeRange = GetOneYearRange()
            RangeStart = range.Start
            SelectedRangeStart = RangeStart
            RangeEnd = range.End
            SelectedRangeEnd = RangeEnd
            DataSource = dataProvider.GetSales(RangeStart, RangeEnd, GroupingPeriod.Day)
            UpdateTiles()
            OnSelectedRangeChanged()
        End Sub

        Private Sub UpdateTiles()
            Dim tiles As List(Of TileData) = New List(Of TileData)()
            Dim ytd As DateTimeRange = GetYtdRange()
            Dim ytdPrev As DateTimeRange = New DateTimeRange(ytd.Start.AddYears(-1), ytd.End.AddYears(-1))
            Dim ytdSales As SalesGroup = dataProvider.GetTotalSalesByRange(ytd.Start, ytd.End)
            Dim ytdSalesPrev As SalesGroup = dataProvider.GetTotalSalesByRange(ytdPrev.Start, ytdPrev.End)
            Dim percentsString = "N/A"
            If ytdSalesPrev.TotalCost <> 0 Then
                Dim percents As Decimal =(ytdSales.TotalCost - ytdSalesPrev.TotalCost) / ytdSalesPrev.TotalCost
                percentsString = String.Format("{1}{0:P1}", Math.Abs(percents), If(percents < 0, "-", "+"))
            End If

            tiles.Add(New TileData("Revenues", "YTD GROWTH", percentsString))
            tiles.Add(New TileData("Unit Sales", "YTD", ytdSales.Units.ToString("n0")))
            tiles.Add(New TileData("Direct Sales", "YTD", ytdSales.TotalCost.ToString("$#,##0,,M", CultureInfo.InvariantCulture)))
            Dim sector As SalesGroup = dataProvider.GetSalesBySector(ytd.Start, ytd.End, GroupingPeriod.All).OrderByDescending(Function(q) q.TotalCost).FirstOrDefault()
            If sector IsNot Nothing Then tiles.Add(New TileData("Best Sector YTD", sector.GroupName.ToUpper(), sector.TotalCost.ToString("$#,##0,,M", CultureInfo.InvariantCulture)))
            TilesData = tiles
        End Sub

        Public Property RangeStart As Date
            Get
                Return rangeStartField
            End Get

            Private Set(ByVal value As Date)
                SetProperty(rangeStartField, value, Function() RangeStart)
            End Set
        End Property

        Public Property RangeEnd As Date
            Get
                Return rangeEndField
            End Get

            Private Set(ByVal value As Date)
                SetProperty(rangeEndField, value, Function() RangeEnd)
            End Set
        End Property

        Public Property SelectedRangeStart As Date
            Get
                Return selectedRangeStartField
            End Get

            Set(ByVal value As Date)
                SetProperty(selectedRangeStartField, value, Function() SelectedRangeStart, New Action(AddressOf OnSelectedRangeChanged))
            End Set
        End Property

        Public Property SelectedRangeEnd As Date
            Get
                Return selectedRangeEndField
            End Get

            Set(ByVal value As Date)
                SetProperty(selectedRangeEndField, value, Function() SelectedRangeEnd, New Action(AddressOf OnSelectedRangeChanged))
            End Set
        End Property

        Public Property DataSource As IEnumerable(Of SalesGroup)
            Get
                Return dataSourceField
            End Get

            Private Set(ByVal value As IEnumerable(Of SalesGroup))
                SetProperty(dataSourceField, value, "DataSource")
            End Set
        End Property

        Public Property FilteredSource As IEnumerable(Of SalesGroup)
            Get
                Return filteredSourceField
            End Get

            Private Set(ByVal value As IEnumerable(Of SalesGroup))
                SetProperty(filteredSourceField, value, "FilteredSource")
            End Set
        End Property

        Public Property FilteredSourceByDate As IEnumerable(Of SalesGroup)
            Get
                Return filteredSourceByDateField
            End Get

            Private Set(ByVal value As IEnumerable(Of SalesGroup))
                SetProperty(filteredSourceByDateField, value, "FilteredSourceByDate")
            End Set
        End Property

        Public Property TilesData As IEnumerable(Of TileData)
            Get
                Return tilesDataField
            End Get

            Private Set(ByVal value As IEnumerable(Of TileData))
                SetProperty(tilesDataField, value, "TilesData")
            End Set
        End Property

        Private Sub OnSelectedRangeChanged()
            If SelectedRangeEnd < SelectedRangeStart Then Return
            FilteredSourceByDate = dataProvider.GetSalesByProduct(SelectedRangeStart, SelectedRangeEnd, GroupingPeriod.Day)
            FilteredSource = dataProvider.GetSalesByProduct(SelectedRangeStart, SelectedRangeEnd, GroupingPeriod.All)
        End Sub
    End Class

    Public Class TileData
        Inherits ViewModelBase

        Private svalue, subTextField, mainTextField As String

        Public Sub New(ByVal mainText As String, ByVal subText As String, ByVal value As String)
            mainTextField = mainText
            subTextField = subText
            svalue = value
        End Sub

        Public Property MainText As String
            Get
                Return mainTextField
            End Get

            Private Set(ByVal value As String)
                SetProperty(mainTextField, value, "MainText")
            End Set
        End Property

        Public Property SubText As String
            Get
                Return subTextField
            End Get

            Private Set(ByVal value As String)
                SetProperty(subTextField, value, "SubText")
            End Set
        End Property

        Public Property Value As String
            Get
                Return svalue
            End Get

            Private Set(ByVal value As String)
                SetProperty(svalue, value, "Value")
            End Set
        End Property
    End Class
End Namespace
