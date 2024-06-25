Imports System
Imports System.Collections.Generic

Namespace GanttDemo

    Public Module DateTimeHelper

        Public Iterator Function GetHolidays(ByVal startDate As Date, ByVal endDate As Date) As IEnumerable(Of Date)
            If startDate > endDate Then Return
            Dim [date] = startDate.Date
            While [date] <= endDate
                If IsUSHoliday([date]) Then Yield [date]
                [date] = [date].AddDays(1)
            End While
        End Function

        Private Function IsUSHoliday(ByVal [date] As Date) As Boolean
            Dim nthWeekDay As Integer = CInt(Math.Ceiling([date].Day / 7.0R))
            Dim dayName As DayOfWeek = [date].DayOfWeek
            Dim isThursday As Boolean = dayName = DayOfWeek.Thursday
            Dim isFriday As Boolean = dayName = DayOfWeek.Friday
            Dim isMonday As Boolean = dayName = DayOfWeek.Monday
            Dim isWeekend As Boolean = dayName = DayOfWeek.Saturday OrElse dayName = DayOfWeek.Sunday
            If [date].Month = 12 AndAlso [date].Day = 31 AndAlso isFriday OrElse [date].Month = 1 AndAlso [date].Day = 1 AndAlso Not isWeekend OrElse [date].Month = 1 AndAlso [date].Day = 2 AndAlso isMonday Then Return True
            If [date].Month = 1 AndAlso isMonday AndAlso nthWeekDay = 3 Then Return True
            If [date].Month = 2 AndAlso isMonday AndAlso nthWeekDay = 3 Then Return True
            If [date].Month = 5 AndAlso isMonday AndAlso [date].AddDays(7).Month = 6 Then Return True
            If [date].Month = 7 AndAlso [date].Day = 3 AndAlso isFriday OrElse [date].Month = 7 AndAlso [date].Day = 4 AndAlso Not isWeekend OrElse [date].Month = 7 AndAlso [date].Day = 5 AndAlso isMonday Then Return True
            If [date].Month = 9 AndAlso isMonday AndAlso nthWeekDay = 1 Then Return True
            If [date].Month = 10 AndAlso isMonday AndAlso nthWeekDay = 2 Then Return True
            If [date].Month = 11 AndAlso [date].Day = 10 AndAlso isFriday OrElse [date].Month = 11 AndAlso [date].Day = 11 AndAlso Not isWeekend OrElse [date].Month = 11 AndAlso [date].Day = 12 AndAlso isMonday Then Return True
            If [date].Month = 11 AndAlso isThursday AndAlso nthWeekDay = 4 Then Return True
            If [date].Month = 12 AndAlso [date].Day = 24 AndAlso isFriday OrElse [date].Month = 12 AndAlso [date].Day = 25 AndAlso Not isWeekend OrElse [date].Month = 12 AndAlso [date].Day = 26 AndAlso isMonday Then Return True
            Return False
        End Function
    End Module
End Namespace
