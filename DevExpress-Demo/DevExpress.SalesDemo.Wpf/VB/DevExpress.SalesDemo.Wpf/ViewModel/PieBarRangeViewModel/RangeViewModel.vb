Imports DevExpress.Mvvm.POCO
Imports DevExpress.SalesDemo.Model
Imports System

Namespace DevExpress.SalesDemo.Wpf.ViewModel

    Public Class RangeViewModel
        Inherits DataViewModel

        Public Shared Function Create() As RangeViewModel
            Return ViewModelSource.Create(Function() New RangeViewModel())
        End Function

        Protected Sub New()
            AreaSeriesArgumentDataMember = "StartOfPeriod"
            AreaSeriesValueDataMember = "TotalCost"
            Dim today As Date = Date.Today
            RangeEnd = today
            RangeStart = today.AddDays(-DaysInMonth)
            SelectedRangeStart = RangeStart.Value.AddDays(OneThirdOfMonth)
            SelectedRangeEnd = RangeEnd.Value.AddDays(-OneThirdOfMonth)
            If IsInDesignMode() Then OnInitializeInDesignMode()
        End Sub

        Const OneThirdOfMonth As Integer = 10

        Const DaysInMonth As Integer = 30

        Public Overridable Property AreaSeriesDataSource As Object

        Public Overridable Property RangeStart As Date?

        Public Overridable Property RangeEnd As Date?

        Public Overridable Property VisibleRangeStart As Date?

        Public Overridable Property VisibleRangeEnd As Date?

        Public Overridable Property SelectedRangeStart As Date?

        Public Overridable Property SelectedRangeEnd As Date?

        Public Overridable Property AreaSeriesArgumentDataMember As String

        Public Overridable Property AreaSeriesValueDataMember As String

        Public Event RangeChanged As EventHandler

        Public Event SelectedRangeChanged As EventHandler

        Public Function GetSelectedRange() As DateTimeRange?
            If SelectedRangeStart Is Nothing OrElse SelectedRangeEnd Is Nothing OrElse SelectedRangeStart >= SelectedRangeEnd Then Return Nothing
            Return New DateTimeRange(SelectedRangeStart.Value, SelectedRangeEnd.Value)
        End Function

        Public Sub LoadData(ByVal data As Object)
            AreaSeriesDataSource = data
        End Sub

        Public Sub SetPrePeriod()
            If RangeStart Is Nothing OrElse RangeEnd Is Nothing Then Return
            Dim oldRangeEnd As Date = RangeEnd.Value
            Dim dateInPreMotnth As Date = oldRangeEnd.AddMonths(-1)
            Dim start As Date = New DateTime(dateInPreMotnth.Year, dateInPreMotnth.Month, 1)
            Dim daysInPreMonts As Integer = Date.DaysInMonth(dateInPreMotnth.Year, dateInPreMotnth.Month)
            Dim [end] As Date = New DateTime(dateInPreMotnth.Year, dateInPreMotnth.Month, daysInPreMonts)
            RangeStart = start
            RangeEnd = [end]
        End Sub

        Public Sub SetNextPeriod()
            If RangeStart Is Nothing OrElse RangeEnd Is Nothing Then Return
            RangeEnd = RangeEnd.Value.AddMonths(1)
            RangeStart = RangeStart.Value.AddMonths(1)
            SetSelectedRangeInMiddle()
        End Sub

        Public Function CanSetNextPeriod() As Boolean
            Return Date.Today > RangeEnd
        End Function

        Protected Sub OnRangeStartChanged()
            SetSelectedRangeInMiddle()
            VisibleRangeStart = RangeStart
            RaiseRangeChanged()
        End Sub

        Protected Sub OnRangeEndChanged()
            SetSelectedRangeInMiddle()
            VisibleRangeEnd = RangeEnd
            RaiseRangeChanged()
        End Sub

        Protected Sub OnSelectedRangeStartChanged()
            RaiseSelectedRangeChanged()
        End Sub

        Protected Sub OnSelectedRangeEndChanged()
            RaiseSelectedRangeChanged()
        End Sub

        Private Sub OnInitializeInDesignMode()
            RequestData("Data", Function(x) x.GetSales(RangeStart.Value, RangeEnd.Value.AddDays(1), GroupingPeriod.Day), Sub(x) LoadData(x))
        End Sub

        Private Sub SetSelectedRangeInMiddle()
            If RangeStart Is Nothing OrElse RangeEnd Is Nothing OrElse RangeStart > RangeEnd Then Return
            SelectedRangeEnd = RangeEnd.Value.AddDays(-OneThirdOfMonth)
            SelectedRangeStart = RangeStart.Value.AddDays(OneThirdOfMonth)
        End Sub

        Private Sub RaiseRangeChanged()
            RaiseEvent RangeChanged(Me, EventArgs.Empty)
        End Sub

        Private Sub RaiseSelectedRangeChanged()
            RaiseEvent SelectedRangeChanged(Me, EventArgs.Empty)
        End Sub
    End Class
End Namespace
