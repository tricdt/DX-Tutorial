Imports System.Collections.Generic
Imports System.Linq

Namespace SchedulingDemo.ViewModels

    Public Class TimelineViewDemoViewModel

        Public Overridable Property Start As Date

        Public ReadOnly Property Calendars As IEnumerable(Of WorkCalendar)
            Get
                Return WorkData.Calendars
            End Get
        End Property

        Public ReadOnly Property Appointments As IEnumerable(Of WorkAppointment)
            Get
                Return WorkData.Appointments
            End Get
        End Property

        Public Sub New()
            Start = TodayStart
            Init()
        End Sub

        Private Sub Init()
            Enumerable.ToList(WorkData.Calendars).ForEach(Sub(x) x.IsVisible = True)
            Enumerable.ToList(CalendarsEmployees).ForEach(Sub(x) x.IsVisible = True)
        End Sub
    End Class
End Namespace
