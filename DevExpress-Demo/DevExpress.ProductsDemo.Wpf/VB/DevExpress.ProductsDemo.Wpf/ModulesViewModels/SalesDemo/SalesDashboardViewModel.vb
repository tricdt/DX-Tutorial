Imports DevExpress.Xpf.Charts
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Windows.Media.Imaging
Imports DevExpress.SalesDemo.Model

Namespace ProductsDemo

    Public Class SalesDashboardViewModel
        Inherits NavigationModule

        Const SalesPerformanceArgumentDataMember As String = "StartOfPeriod"

        Const SalesPerformanceValueDataMember As String = "TotalCost"

        Const FirstPartOfFiscalYearHeder As String = "YEAR "

        Const SalesVolumeForDayFormatString As String = "$#,#,0"

        Const SalesVolumeForWeekFormatString As String = "$#,#,0"

        Const SalesVolumeForMonthFormatString As String = "$#,#,0"

        Const SalesVolumeForYearFormatString As String = "$#,#,0"

        Const FirstPartOfSalesForecastDashboardGroupHeader As String = "SALES FORECAST "

        Private selectedDayField As Date = Date.Now

        Private selectedMonthField As Date = Date.Now

        Private salesBySectorField As IEnumerable(Of SalesGroup)

        Private ytdSalesVolumeField As Decimal

        Private ytdSalesForecastField As Decimal

        Private lastYearSalesVolumeField As Decimal

        Private annualSalesFirstRangeEndField As Decimal

        Private annualSalesSecondRangeEndField As Decimal

        Private annualSalesThirdRangeEndField As Decimal

        Private lastYearHeder As String

        Private salesForecastDashboardGroupHeaderField As String

        Private dailySalesPerformanceViewModelField As SalesPerformanceViewModel

        Private monthlySalesPerformanceViewModelField As SalesPerformanceViewModel

        Public Property SelectedDay As Date
            Get
                Return selectedDayField
            End Get

            Private Set(ByVal value As Date)
                SetProperty(selectedDayField, value, "SelectedDay", New Action(AddressOf OnSelectedDayChanged))
            End Set
        End Property

        Public Property SelectedMonth As Date
            Get
                Return selectedMonthField
            End Get

            Private Set(ByVal value As Date)
                SetProperty(selectedMonthField, value, "SelectedMonth", New Action(AddressOf OnSelectedMonthChanged))
            End Set
        End Property

        Public Property YTDSalesVolume As Decimal
            Get
                Return ytdSalesVolumeField
            End Get

            Set(ByVal value As Decimal)
                SetProperty(ytdSalesVolumeField, value, "YTDSalesVolume")
            End Set
        End Property

        Public Property YTDSalesForecast As Decimal
            Get
                Return ytdSalesForecastField
            End Get

            Private Set(ByVal value As Decimal)
                SetProperty(ytdSalesForecastField, value, "YTDSalesForecast")
            End Set
        End Property

        Public Property LastYearSalesVolume As Decimal
            Get
                Return lastYearSalesVolumeField
            End Get

            Private Set(ByVal value As Decimal)
                SetProperty(lastYearSalesVolumeField, value, "LastYearSalesVolume")
            End Set
        End Property

        Public Property AnnualSalesFirstRangeEnd As Decimal
            Get
                Return annualSalesFirstRangeEndField
            End Get

            Private Set(ByVal value As Decimal)
                SetProperty(annualSalesFirstRangeEndField, value, "AnnualSalesFirstRangeEnd")
            End Set
        End Property

        Public Property AnnualSalesSecondRangeEnd As Decimal
            Get
                Return annualSalesSecondRangeEndField
            End Get

            Private Set(ByVal value As Decimal)
                SetProperty(annualSalesSecondRangeEndField, value, "AnnualSalesSecondRangeEnd")
            End Set
        End Property

        Public Property AnnualSalesThirdRangeEnd As Decimal
            Get
                Return annualSalesThirdRangeEndField
            End Get

            Private Set(ByVal value As Decimal)
                SetProperty(annualSalesThirdRangeEndField, value, "AnnualSalesThirdRangeEnd")
            End Set
        End Property

        Public Property LastYearHeader As String
            Get
                Return lastYearHeder
            End Get

            Private Set(ByVal value As String)
                SetProperty(lastYearHeder, value, "LastYearHeader")
            End Set
        End Property

        Public Property SalesForecastDashboardGroupHeader As String
            Get
                Return salesForecastDashboardGroupHeaderField
            End Get

            Private Set(ByVal value As String)
                SetProperty(salesForecastDashboardGroupHeaderField, value, "SalesForecastDashboardGroupHeader")
            End Set
        End Property

        Public Property SalesBySector As IEnumerable(Of SalesGroup)
            Get
                Return salesBySectorField
            End Get

            Private Set(ByVal value As IEnumerable(Of SalesGroup))
                SetProperty(salesBySectorField, value, "SalesBySector")
            End Set
        End Property

        Public Property DailySalesPerformanceViewModel As SalesPerformanceViewModel
            Get
                Return dailySalesPerformanceViewModelField
            End Get

            Private Set(ByVal value As SalesPerformanceViewModel)
                SetProperty(dailySalesPerformanceViewModelField, value, "DailySalesPerformanceViewModel")
            End Set
        End Property

        Public Property MonthlySalesPerformanceViewModel As SalesPerformanceViewModel
            Get
                Return monthlySalesPerformanceViewModelField
            End Get

            Private Set(ByVal value As SalesPerformanceViewModel)
                SetProperty(monthlySalesPerformanceViewModelField, value, "MonthlySalesPerformanceViewModel")
            End Set
        End Property

        Public Overrides ReadOnly Property Caption As String
            Get
                Return "Sales"
            End Get
        End Property

        Public Overrides ReadOnly Property Description As String
            Get
                Return "Revenue" & Environment.NewLine & "Snapshots"
            End Get
        End Property

        Public Overrides ReadOnly Property Glyph As BitmapImage
            Get
                Return GetResourceImage("Sales.png")
            End Get
        End Property

        Protected Overrides Sub SaveAndClearData()
            SaveAndClearPropertyValue(lastYearSalesVolumeField, "LastYearSalesVolume", 0.0D)
            SaveAndClearPropertyValue(ytdSalesVolumeField, "YTDSalesVolume", 0.0D)
            SaveAndClearPropertyValue(ytdSalesForecastField, "YTDSalesForecast", 0.0D)
            SaveAndClearPropertyValue(salesBySectorField, "SalesBySector")
            SavePropertyValue(DailySalesPerformanceViewModel.ChartDataSource, "DailySalesPerformanceViewModel.ChartDataSource")
            DailySalesPerformanceViewModel.ChartDataSource = Nothing
            SavePropertyValue(MonthlySalesPerformanceViewModel.ChartDataSource, "MonthlySalesPerformanceViewModel.ChartDataSource")
            MonthlySalesPerformanceViewModel.ChartDataSource = Nothing
        End Sub

        Protected Overrides Sub RestoreData()
            RestorePropertyValue(lastYearSalesVolumeField, "LastYearSalesVolume", True)
            RestorePropertyValue(ytdSalesVolumeField, "YTDSalesVolume", True)
            RestorePropertyValue(ytdSalesForecastField, "YTDSalesForecast", True)
            RestorePropertyValue(salesBySectorField, "SalesBySector", True)
            Dim dailySalesPerformanceViewModelChartDataSource As Object = Nothing
            RestorePropertyValue(dailySalesPerformanceViewModelChartDataSource, "DailySalesPerformanceViewModel.ChartDataSource", False)
            DailySalesPerformanceViewModel.ChartDataSource = dailySalesPerformanceViewModelChartDataSource
            Dim monthlySalesPerformanceViewModelChartDataSource As Object = Nothing
            RestorePropertyValue(monthlySalesPerformanceViewModelChartDataSource, "MonthlySalesPerformanceViewModel.ChartDataSource", False)
            MonthlySalesPerformanceViewModel.ChartDataSource = monthlySalesPerformanceViewModelChartDataSource
        End Sub

        Protected Overrides Sub InitializeData()
            Dim currentYearRange As DateTimeRange = GetYtdRange()
            YTDSalesVolume = DataProvider.GetTotalSalesByRange(currentYearRange.Start, currentYearRange.End).TotalCost
            YTDSalesForecast = SalesForecastMaker.GetYtdForecast(YTDSalesVolume)
            Dim lastYearRange As DateTimeRange = GetLastYearRange()
            LastYearSalesVolume = DataProvider.GetTotalSalesByRange(lastYearRange.Start, lastYearRange.End).TotalCost
            LastYearHeader = FirstPartOfFiscalYearHeder & GetLastYear()
            SalesForecastDashboardGroupHeader = FirstPartOfSalesForecastDashboardGroupHeader & "(" & GetCurrentYear().ToString() & ")"
            SalesBySector = DataProvider.GetSalesBySector(New DateTime(), Date.Now, GroupingPeriod.All)
            Dim badSalesRange As DecimalRange = GetBadSalesRange()
            Dim normalSalesRange As DecimalRange = GetNormalSalesRange()
            Dim goodSalesRange As DecimalRange = GetGoodSalesRange()
            AnnualSalesFirstRangeEnd = badSalesRange.End
            AnnualSalesSecondRangeEnd = normalSalesRange.End
            AnnualSalesThirdRangeEnd = goodSalesRange.End
            InitializeDailySalesPerformanceViewModel()
            InitializeMonthlySalesPerformanceViewModel()
        End Sub

        Private Sub InitializeDailySalesPerformanceViewModel()
            DailySalesPerformanceViewModel = New SalesPerformanceViewModel() With {.Mode = SalesPerformanceViewMode.Daily, .AreaSeriesVisible = True, .ArgumentDataMember = SalesPerformanceArgumentDataMember, .ValueDataMember = SalesPerformanceValueDataMember, .DateTimeMeasureUnit = DateTimeMeasureUnit.Hour, .DateTimeGridAlignment = DateTimeGridAlignment.Hour, .AxisXGridSpacing = 2, .AxisXMinorCount = 1, .AxisXLabelFormatString = "t", .AreaSeriesCrosshairLabelPattern = "{A:t}:  ${V:###,###,###}"}
            Dim todayRange As DateTimeRange = GetDayRange(Date.Now)
            Dim todaySalesGroup As SalesGroup = DataProvider.GetTotalSalesByRange(todayRange.Start, todayRange.End)
            DailySalesPerformanceViewModel.FirstSalesVolume = todaySalesGroup.TotalCost.ToString(SalesVolumeForDayFormatString)
            Dim yesterdayRange As DateTimeRange = GetDayRange(Date.Now.AddDays(-1))
            Dim yesterdaySalesGroup As SalesGroup = DataProvider.GetTotalSalesByRange(yesterdayRange.Start, yesterdayRange.End)
            DailySalesPerformanceViewModel.SecondSalesVolume = yesterdaySalesGroup.TotalCost.ToString(SalesVolumeForDayFormatString)
            Dim lastWeekRange As DateTimeRange = GetLastWeekRange()
            Dim lastWeekSalesGroup As SalesGroup = DataProvider.GetTotalSalesByRange(lastWeekRange.Start, lastWeekRange.End)
            DailySalesPerformanceViewModel.ThirdSalesVolume = lastWeekSalesGroup.TotalCost.ToString(SalesVolumeForWeekFormatString)
            AddHandler DailySalesPerformanceViewModel.PropertyChanged, AddressOf OnDailySalesPerformanceViewModelPropertyChanged
            RequestDataForDaylySalesPerformanceChart()
        End Sub

        Private Sub InitializeMonthlySalesPerformanceViewModel()
            MonthlySalesPerformanceViewModel = New SalesPerformanceViewModel() With {.Mode = SalesPerformanceViewMode.Monthly, .AreaSeriesVisible = True, .ArgumentDataMember = SalesPerformanceArgumentDataMember, .ValueDataMember = SalesPerformanceValueDataMember, .DateTimeMeasureUnit = DateTimeMeasureUnit.Day, .DateTimeGridAlignment = DateTimeGridAlignment.Day, .AxisXGridSpacing = 3, .AxisXMinorCount = 2, .AxisXLabelFormatString = " d", .AreaSeriesCrosshairLabelPattern = "{A:d}:  ${V:###,###,###}"}
            Dim thisMonthRange As DateTimeRange = GetMonthRange(Date.Now)
            Dim thisMonthSalesGroup As SalesGroup = DataProvider.GetTotalSalesByRange(thisMonthRange.Start, thisMonthRange.End)
            MonthlySalesPerformanceViewModel.FirstSalesVolume = thisMonthSalesGroup.TotalCost.ToString(SalesVolumeForMonthFormatString)
            Dim lastMonthRange As DateTimeRange = GetMonthRange(Date.Now.AddMonths(-1))
            Dim lastMonthSalesGroup As SalesGroup = DataProvider.GetTotalSalesByRange(lastMonthRange.Start, lastMonthRange.End)
            MonthlySalesPerformanceViewModel.SecondSalesVolume = lastMonthSalesGroup.TotalCost.ToString(SalesVolumeForMonthFormatString)
            MonthlySalesPerformanceViewModel.ThirdSalesVolume = YTDSalesVolume.ToString(SalesVolumeForYearFormatString)
            AddHandler MonthlySalesPerformanceViewModel.PropertyChanged, AddressOf OnMonthlySalesPerformanceViewModelPropertyChanged
            RequestDataForMonthlySalesPerformanceChart()
        End Sub

        Private Sub RequestDataForDaylySalesPerformanceChart()
            Dim start As Date = New DateTime(SelectedDay.Year, SelectedDay.Month, SelectedDay.Day)
            Dim [end] As Date = start.AddDays(1)
            DailySalesPerformanceViewModel.ChartDataSource = DataProvider.GetSales(start, [end], GroupingPeriod.Hour)
        End Sub

        Private Sub RequestDataForMonthlySalesPerformanceChart()
            Dim start As Date = New DateTime(SelectedMonth.Year, SelectedMonth.Month, 1)
            Dim daysInMonth As Integer = Date.DaysInMonth(SelectedMonth.Year, SelectedMonth.Month)
            Dim [end] As Date = New DateTime(SelectedMonth.Year, SelectedMonth.Month, daysInMonth)
            MonthlySalesPerformanceViewModel.ChartDataSource = DataProvider.GetSales(start, [end], GroupingPeriod.Day)
        End Sub

        Private Sub OnDailySalesPerformanceViewModelPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
            If Equals(e.PropertyName, "SelectedDate") Then SelectedDay = DailySalesPerformanceViewModel.SelectedDate
        End Sub

        Private Sub OnMonthlySalesPerformanceViewModelPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
            If Equals(e.PropertyName, "SelectedDate") Then SelectedMonth = MonthlySalesPerformanceViewModel.SelectedDate
        End Sub

        Private Sub OnSelectedDayChanged()
            Dim day As DateTimeRange = GetDayRange(SelectedDay)
            DailySalesPerformanceViewModel.ChartDataSource = DataProvider.GetSales(day.Start, day.End, GroupingPeriod.Hour)
        End Sub

        Private Sub OnSelectedMonthChanged()
            Dim month As DateTimeRange = GetMonthRange(SelectedMonth)
            MonthlySalesPerformanceViewModel.ChartDataSource = DataProvider.GetSales(month.Start, month.End, GroupingPeriod.Day)
        End Sub
    End Class
End Namespace
