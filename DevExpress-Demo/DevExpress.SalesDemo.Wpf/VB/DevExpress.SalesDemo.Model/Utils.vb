Imports System
Imports System.Globalization

Namespace DevExpress.SalesDemo.Model

    Public Module DateTimeUtils

        Public Function GetDayRange(ByVal [date] As Date) As DateTimeRange
            Dim startOfDate As Date = New DateTime([date].Year, [date].Month, [date].Day)
            Dim endOfToday As Date = startOfDate.AddDays(1).AddTicks(-1)
            Return New DateTimeRange(startOfDate, endOfToday)
        End Function

        Public Function GetTodayRange() As DateTimeRange
            Return GetDayRange(Date.Now)
        End Function

        Public Function GetYesterdayRange() As DateTimeRange
            Return GetDayRange(Date.Now.AddDays(-1))
        End Function

        Public Function GetLastWeekRange() As DateTimeRange
            Dim firstDay As DayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek
            Dim today As Date = Date.Today
            Dim startOfWeek As Date = today
            While startOfWeek.DayOfWeek <> firstDay
                startOfWeek = startOfWeek.AddDays(-1)
            End While

            Dim endOfWeek As Date = startOfWeek.AddDays(7).AddTicks(-1)
            Return New DateTimeRange(startOfWeek, endOfWeek)
        End Function

        Public Function GetMonthRange(ByVal [date] As Date) As DateTimeRange
            Dim startOfMonth As Date = New DateTime([date].Year, [date].Month, 1)
            Dim daysInCurrentMonth As Integer = Date.DaysInMonth([date].Year, [date].Month)
            Dim endOfMonth As Date = startOfMonth.AddDays(daysInCurrentMonth).AddTicks(-1)
            Return New DateTimeRange(startOfMonth, endOfMonth)
        End Function

        Public Function GetThidMonthRange() As DateTimeRange
            Return GetMonthRange(Date.Now)
        End Function

        Public Function GetLastMonthRange() As DateTimeRange
            Return GetMonthRange(Date.Now.AddMonths(-1))
        End Function

        Public Function GetYtdRange() As DateTimeRange
            Dim today As Date = Date.Today
            Dim startOfYear As Date = New DateTime(today.Year, 1, 1)
            Dim endOfYear As Date = today ' startOfYear.AddYears(1).AddTicks(-1);
            Return New DateTimeRange(startOfYear, endOfYear)
        End Function

        Public Function GetOneYearRange() As DateTimeRange
            Return New DateTimeRange(Date.Today.AddYears(-1), Date.Today)
        End Function

        Public Function GetYearRange(ByVal [date] As Date) As DateTimeRange
            Dim startOfYear As Date = New DateTime([date].Year, 1, 1)
            Dim endOfYear As Date = startOfYear.AddYears(1).AddTicks(-1)
            Return New DateTimeRange(startOfYear, endOfYear)
        End Function

        Public Function GetLastYearRange() As DateTimeRange
            Return GetYearRange(Date.Today.AddYears(-1))
        End Function

        Public Function GetLastYear() As Integer
            Return Date.Today.AddYears(-1).Year
        End Function

        Public Function GetCurrentYear() As Object
            Return Date.Now.Year
        End Function

        Public Function IsCurrentYear(ByVal [date] As Date) As Boolean
            Dim now As Date = Date.Now
            Return now.Year = [date].Year
        End Function

        Public Function IsCurrentMonth(ByVal [date] As Date) As Boolean
            Dim now As Date = Date.Now
            Return IsCurrentYear([date]) AndAlso now.Month = [date].Month
        End Function

        Public Function IsToday(ByVal [date] As Date) As Boolean
            Dim now As Date = Date.Now
            Return IsCurrentMonth([date]) AndAlso now.Day = [date].Day
        End Function
    End Module

    Public Structure DateTimeRange

        Private _start As Date

        Private _end As Date

        Public ReadOnly Property Start As Date
            Get
                Return _start
            End Get
        End Property

        Public ReadOnly Property [End] As Date
            Get
                Return _end
            End Get
        End Property

        Public Sub New(ByVal start As Date, ByVal [end] As Date)
            _start = start
            _end = [end]
        End Sub
    End Structure

    Public Structure DecimalRange

        Private startField As Decimal

        Private endField As Decimal

        Public ReadOnly Property Start As Decimal
            Get
                Return startField
            End Get
        End Property

        Public ReadOnly Property [End] As Decimal
            Get
                Return endField
            End Get
        End Property

        Public Sub New(ByVal start As Decimal, ByVal [end] As Decimal)
            startField = start
            endField = [end]
        End Sub
    End Structure

    Public Module SalesRangeProvider

        Public Function GetBadSalesRange() As DecimalRange
            Return New DecimalRange(0, 200000000D)
        End Function

        Public Function GetNormalSalesRange() As DecimalRange
            Return New DecimalRange(0, 400000000D)
        End Function

        Public Function GetGoodSalesRange() As DecimalRange
            Return New DecimalRange(0, 600000000D)
        End Function
    End Module
End Namespace
