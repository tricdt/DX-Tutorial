Imports DevExpress.Mvvm.POCO
Imports DevExpress.SalesDemo.Model
Imports System

Namespace DevExpress.SalesDemo.Wpf.ViewModel

    Public Class PerformanceBarChartViewModel
        Inherits PerformanceAreaChartViewModel

        Public Shared Overloads Function Create(ByVal moduleType As String, ByVal mode As PerformanceViewMode) As PerformanceBarChartViewModel
            Return ViewModelSource.Create(Function() New PerformanceBarChartViewModel(moduleType, mode))
        End Function

        Protected Sub New()
            Me.New(Products, PerformanceViewMode.Daily)
        End Sub

        Protected Sub New(ByVal moduleType As String, ByVal mode As PerformanceViewMode)
            MyBase.New(moduleType, mode)
        End Sub

        Protected Overrides Sub ValidateMode(ByVal moduleType As String, ByVal mode As PerformanceViewMode)
            If Equals(moduleType, Products) OrElse Equals(moduleType, Sectors) OrElse Equals(moduleType, Modules.Regions) Then Return
            Throw New NotImplementedException()
        End Sub

        Protected Overrides Sub InitializeInDailyMode()
            ArgumentDataMember = "GroupName"
            ValueDataMember = "TotalCost"
        End Sub

        Protected Overrides Sub InitializeInMonthlyMode()
            ArgumentDataMember = "GroupName"
            ValueDataMember = "Units"
        End Sub

        Protected Overrides Sub RequestData()
            Dim range As DateTimeRange = If(Mode = PerformanceViewMode.Daily, DateTimeUtils.GetDayRange(SelectedDate), DateTimeUtils.GetMonthRange(SelectedDate))
            If Equals(ModuleType, Products) Then
                RequestData("ChartDataSource", Function(x) x.GetSalesByProduct(range, GroupingPeriod.All), Sub(x) ChartDataSource = x)
                Return
            End If

            If Equals(ModuleType, Sectors) Then
                RequestData("ChartDataSource", Function(x) x.GetSalesBySector(range, GroupingPeriod.All), Sub(x) ChartDataSource = x)
                Return
            End If

            If Equals(ModuleType, Modules.Regions) Then
                RequestData("ChartDataSource", Function(x) x.GetSalesByRegion(range, GroupingPeriod.All), Sub(x) ChartDataSource = x)
                Return
            End If

            Throw New NotImplementedException()
        End Sub

        Protected Overrides Sub UpdateVolumeLables()
            Dim requestDataCore As Action(Of String, DateTimeRange, Action(Of SalesGroup)) = Sub(id, range, callback)
                RequestData(id, Function(x) x.GetTotalSalesByRange(range), callback)
            End Sub
            If Mode = PerformanceViewMode.Daily Then
                requestDataCore("FirstSalesVolume", DateTimeUtils.GetTodayRange(), Sub(x) FirstSalesVolume = x.TotalCost.ToString("$0,,.00M"))
                requestDataCore("SecondSalesVolume", DateTimeUtils.GetYesterdayRange(), Sub(x) SecondSalesVolume = x.TotalCost.ToString("$0,,.00M"))
                requestDataCore("ThirdSalesVolume", DateTimeUtils.GetLastWeekRange(), Sub(x) ThirdSalesVolume = x.TotalCost.ToString("$0,,.0M"))
            Else
                requestDataCore("FirstSalesVolume", DateTimeUtils.GetThidMonthRange(), Sub(x) FirstSalesVolume = x.Units.ToString("0,K Units"))
                requestDataCore("SecondSalesVolume", DateTimeUtils.GetLastMonthRange(), Sub(x) SecondSalesVolume = x.Units.ToString("0,K Units"))
                requestDataCore("ThirdSalesVolume", DateTimeUtils.GetYtdRange(), Sub(x) ThirdSalesVolume = x.Units.ToString("0,K Units"))
            End If
        End Sub
    End Class
End Namespace
