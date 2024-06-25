Imports System.Collections.Generic
Imports System.Globalization
Imports System.Threading
Imports DevExpress.Xpf.PivotGrid

Namespace PivotGridDemo

    Public Partial Class IntervalGrouping
        Inherits PivotGridDemoModule

        Public Class GroupIntervalItem

            Public Property GroupInterval As FieldGroupInterval

            Public Property Caption As String
        End Class

        Public Sub New()
            InitializeComponent()
        End Sub

        Public Shared ReadOnly Property FieldGroupIntervals As IEnumerable(Of Object)
            Get
                Return {New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.Default, .Caption = "Exact Date"}, New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateYear, .Caption = "Year"}, New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateQuarter, .Caption = "Quarter"}, New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateMonth, .Caption = "Month"}, New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateDay, .Caption = "Day"}, New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateMonthYear, .Caption = "Month-Year"}, New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateQuarterYear, .Caption = "Quarter-Year"}, New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateWeekYear, .Caption = "Week-Year"}, New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.Date, .Caption = "Date"}}
            End Get
        End Property

        Public Shared ReadOnly Property FieldExtendedGroupIntervals As IEnumerable(Of Object)
            Get
                Return {New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.Default, .Caption = "Exact Date"}, New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateYear, .Caption = "Year"}, New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateQuarter, .Caption = "Quarter"}, New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateMonth, .Caption = "Month"}, New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateDay, .Caption = "Day"}, New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.Hour, .Caption = "Hour"}, New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.Minute, .Caption = "Minute"}, New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.Second, .Caption = "Second"}, New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateDayOfYear, .Caption = "Day of Year"}, New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateDayOfWeek, .Caption = "Day of Week"}, New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateWeekOfYear, .Caption = "Week of Year"}, New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateWeekOfMonth, .Caption = "Week of Month"}, New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateMonthYear, .Caption = "Month-Year"}, New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateQuarterYear, .Caption = "Quarter-Year"}, New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateWeekYear, .Caption = "Week-Year"}, New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.Date, .Caption = "Date"}, New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateHour, .Caption = "Date-Hour"}, New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateHourMinute, .Caption = "Date-Minute"}, New GroupIntervalItem With {.GroupInterval = FieldGroupInterval.DateHourMinuteSecond, .Caption = "Date-Hour-Minute-Second"}}
            End Get
        End Property

        Private Sub pivotGrid_FieldValueDisplayText(ByVal sender As Object, ByVal e As PivotFieldDisplayTextEventArgs)
            If ReferenceEquals(e.Field, fieldOrderDate) Then
                If CType(e.Field.DataBinding, DataSourceColumnBinding).GroupInterval = FieldGroupInterval.DateWeekYear Then
                    Dim [date] As Date = CDate(e.Value)
                    Dim culture As CultureInfo = Thread.CurrentThread.CurrentUICulture
                    Dim weekNumber As Integer = culture.Calendar.GetWeekOfYear([date], culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek)
                    e.DisplayText = String.Format("Week {0} of {1}", weekNumber, [date].Year)
                    If e.ValueType = FieldValueType.Total Then e.DisplayText += " Total"
                End If
            End If
        End Sub
    End Class
End Namespace
