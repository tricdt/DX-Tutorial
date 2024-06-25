Imports DevExpress.Mvvm.POCO
Imports DevExpress.SalesDemo.Model
Imports System
Imports System.Collections.Generic

Namespace DevExpress.SalesDemo.Wpf.ViewModel

    Public Class PerformanceLineChartViewModel
        Inherits DataViewModel

        Private _ModuleType As String

        Public Shared Function Create(ByVal moduleType As String) As PerformanceLineChartViewModel
            Return ViewModelSource.Create(Function() New PerformanceLineChartViewModel(moduleType))
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
            Me.New(Channels)
        End Sub

        Protected Sub New(ByVal moduleType As String)
            Me.ModuleType = moduleType
            SelectedDate = Date.Now
        End Sub

        Protected Overridable Overloads Sub RequestData()
            If Equals(ModuleType, Channels) Then
                Dim todayRange As DateTimeRange = DateTimeUtils.GetDayRange(SelectedDate)
                RequestData("ChartDataSource", Function(x) x.GetSalesByChannel(todayRange, GroupingPeriod.Hour), Sub(x) ChartDataSource = x)
                RequestData("TotalSalesBySector", Function(x) x.GetSalesByChannel(todayRange, GroupingPeriod.All), Sub(x)
                    TotalSalesBySector = x
                    Dim total As Decimal = 0D
                    For Each grp As SalesGroup In TotalSalesBySector
                        total += grp.TotalCost
                    Next

                    TotalSalesVolumeForDay = total
                End Sub)
                Return
            End If

            Throw New NotImplementedException()
        End Sub

        Public ReadOnly Property Mode As PerformanceViewMode
            Get
                Return PerformanceViewMode.Daily
            End Get
        End Property

        Public Overridable Property ChartDataSource As Object

        Public Overridable Property SelectedDate As Date

        Public Overridable Property SelectedDateString As String

        Public Overridable Property TotalSalesBySector As IEnumerable(Of SalesGroup)

        Public Overridable Property TotalSalesVolumeForDay As Decimal

        Public Sub TimeForward()
            SelectedDate = SelectedDate.AddDays(1)
        End Sub

        Public Sub TimeBackward()
            SelectedDate = SelectedDate.AddDays(-1)
        End Sub

        Public Function CanTimeForward() As Boolean
            Return Not DateTimeUtils.IsToday(SelectedDate)
        End Function

        Public Sub SetCurrentTimePeriod()
            Throw New NotImplementedException()
        End Sub

        Public Sub SetLastTimePeriod()
            Throw New NotImplementedException()
        End Sub

        Protected Sub OnSelectedDateChanged()
            SelectedDateString = SelectedDate.ToString("D")
            RequestData()
        End Sub
    End Class
End Namespace
