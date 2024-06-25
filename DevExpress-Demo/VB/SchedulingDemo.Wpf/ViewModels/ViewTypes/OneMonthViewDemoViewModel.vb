Imports System.Collections.Generic

Namespace SchedulingDemo.ViewModels

    Public Class OneMonthViewDemoViewModel

        Public Overridable Property Start As Date

        Public ReadOnly Property Calendars As IEnumerable(Of EventCalendar)
            Get
                Return EventDataHelper.Calendars
            End Get
        End Property

        Public ReadOnly Property Appointments As IEnumerable(Of EventData)
            Get
                Return Events
            End Get
        End Property

        Public Sub New()
            Start = EventDataHelper.Start
        End Sub
    End Class
End Namespace
