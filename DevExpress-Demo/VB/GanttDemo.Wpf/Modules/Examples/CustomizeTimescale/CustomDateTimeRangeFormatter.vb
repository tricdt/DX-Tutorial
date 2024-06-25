Imports DevExpress.Mvvm
Imports System
Imports System.Globalization

Namespace GanttDemo.Examples

#Region "!"
    Public Class CustomDateTimeRangeFormatter
        Implements IFormatProvider, ICustomFormatter

        Public Function Format(ByVal pFormat As String, ByVal arg As Object, ByVal formatProvider As IFormatProvider) As String Implements ICustomFormatter.Format
            If TypeOf arg Is DateTimeRange Then
                Dim range = CType(arg, DateTimeRange)
                Dim culture = CultureInfo.CurrentCulture
                Dim weekNum As Integer = culture.Calendar.GetWeekOfYear(range.Start, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek)
                Return String.Format("Week {0}", weekNum)
            End If

            Return Nothing
        End Function

        Public Function GetFormat(ByVal formatType As Type) As Object Implements IFormatProvider.GetFormat
            If formatType Is GetType(ICustomFormatter) Then Return Me
            Return CultureInfo.CurrentCulture.GetFormat(formatType)
        End Function
    End Class
#End Region
End Namespace
