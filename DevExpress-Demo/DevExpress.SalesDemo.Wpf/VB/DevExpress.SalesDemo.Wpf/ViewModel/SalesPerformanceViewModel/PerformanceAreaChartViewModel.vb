Imports DevExpress.Mvvm.POCO
Imports DevExpress.SalesDemo.Model
Imports System

Namespace DevExpress.SalesDemo.Wpf.ViewModel

    Public Class PerformanceAreaChartViewModel
        Inherits DataViewModel

        Private _ModuleType As String, _Mode As PerformanceViewMode

        Public Shared Function Create(ByVal moduleType As String, ByVal mode As PerformanceViewMode) As PerformanceAreaChartViewModel
            Return ViewModelSource.Create(Function() New PerformanceAreaChartViewModel(moduleType, mode))
        End Function

        Protected Property ModuleType As String
            Get
                Return _ModuleType
            End Get

            Private Set(ByVal value As String)
                _ModuleType = value
            End Set
        End Property

        Protected Sub New()
            Me.New(Dashboard, PerformanceViewMode.Daily)
        End Sub

        Protected Sub New(ByVal moduleType As String, ByVal mode As PerformanceViewMode)
            ValidateMode(moduleType, mode)
            Me.ModuleType = moduleType
            Me.Mode = mode
            If Me.Mode = PerformanceViewMode.Daily Then
                InitializeInDailyMode()
            Else
                InitializeInMonthlyMode()
            End If

            SelectedDate = Date.Now
            UpdateVolumeLables()
        End Sub

        Protected Overridable Sub ValidateMode(ByVal moduleType As String, ByVal mode As PerformanceViewMode)
            If Not Equals(moduleType, Dashboard) Then Throw New NotImplementedException()
        End Sub

        Protected Overridable Sub InitializeInDailyMode()
            ArgumentDataMember = "StartOfPeriod"
            ValueDataMember = "TotalCost"
        End Sub

        Protected Overridable Sub InitializeInMonthlyMode()
            InitializeInDailyMode()
        End Sub

        Protected Overridable Overloads Sub RequestData()
            Dim range As DateTimeRange = If(Mode = PerformanceViewMode.Daily, DateTimeUtils.GetDayRange(SelectedDate), DateTimeUtils.GetMonthRange(SelectedDate))
            Dim groupingPeriod As GroupingPeriod = If(Mode = PerformanceViewMode.Daily, GroupingPeriod.Hour, GroupingPeriod.Day)
            RequestData("ChartDataSource", Function(x) x.GetSales(range, groupingPeriod), Sub(x) ChartDataSource = x)
        End Sub

        Protected Overridable Sub UpdateVolumeLables()
            Dim requestDataCore As Action(Of String, DateTimeRange, Action(Of SalesGroup)) = Sub(id, range, callback)
                RequestData(id, Function(x) x.GetTotalSalesByRange(range), callback)
            End Sub
            If Mode = PerformanceViewMode.Daily Then
                requestDataCore("FirstSalesVolume", DateTimeUtils.GetTodayRange(), Sub(x) FirstSalesVolume = GetTotalCost(x))
                requestDataCore("SecondSalesVolume", DateTimeUtils.GetYesterdayRange(), Sub(x) SecondSalesVolume = GetTotalCost(x))
                requestDataCore("ThirdSalesVolume", DateTimeUtils.GetLastWeekRange(), Sub(x) ThirdSalesVolume = GetTotalCost(x))
            Else
                requestDataCore("FirstSalesVolume", DateTimeUtils.GetThidMonthRange(), Sub(x) FirstSalesVolume = GetTotalCost(x))
                requestDataCore("SecondSalesVolume", DateTimeUtils.GetLastMonthRange(), Sub(x) SecondSalesVolume = GetTotalCost(x))
                requestDataCore("ThirdSalesVolume", DateTimeUtils.GetYtdRange(), Sub(x) ThirdSalesVolume = GetTotalCost(x))
            End If
        End Sub

        Private Function GetTotalCost(ByVal group As SalesGroup) As String
            Return group.TotalCost.ToString("$#,#,0")
        End Function

        Public Property Mode As PerformanceViewMode
            Get
                Return _Mode
            End Get

            Private Set(ByVal value As PerformanceViewMode)
                _Mode = value
            End Set
        End Property

        Public Overridable Property ChartDataSource As Object

        Public Overridable Property ArgumentDataMember As String

        Public Overridable Property ValueDataMember As String

        Public Overridable Property FirstSalesVolume As String

        Public Overridable Property SecondSalesVolume As String

        Public Overridable Property ThirdSalesVolume As String

        Public Overridable Property SelectedDate As Date

        Public Overridable Property SelectedDateString As String

        Public Sub TimeForward()
            SelectedDate = If(Mode = PerformanceViewMode.Daily, SelectedDate.AddDays(1), SelectedDate.AddMonths(1))
        End Sub

        Public Sub TimeBackward()
            SelectedDate = If(Mode = PerformanceViewMode.Daily, SelectedDate.AddDays(-1), SelectedDate.AddMonths(-1))
        End Sub

        Public Function CanTimeForward() As Boolean
            Return If(Mode = PerformanceViewMode.Daily, Not DateTimeUtils.IsToday(SelectedDate), Not DateTimeUtils.IsCurrentMonth(SelectedDate))
        End Function

        Public Sub SetCurrentTimePeriod()
            SelectedDate = Date.Now
        End Sub

        Public Sub SetLastTimePeriod()
            SelectedDate = If(Mode = PerformanceViewMode.Daily, Date.Now.AddDays(-1), Date.Now.AddMonths(-1))
        End Sub

        Protected Sub OnSelectedDateChanged()
            Dim fomatString As String = If(Mode = PerformanceViewMode.Daily, "d", "MMMM, yyyy")
            SelectedDateString = SelectedDate.ToString(fomatString)
            RequestData()
        End Sub
    End Class
End Namespace
