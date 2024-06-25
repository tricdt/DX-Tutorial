Imports System
Imports System.Collections.ObjectModel

Namespace SchedulingDemo.ViewModels

    Public Class DayViewPerformanceDemoViewModel

        Private Shared ReadOnly data As SampleDataGenerator

        Shared Sub New()
            data = New SampleDataGenerator()
            data.SetUp(10, 15, 500)
        End Sub

        Protected Sub New()
            AppointmentsPerDays = New ObservableCollection(Of Integer)() From {50, 100, 200, 500, 1000}
            AppointmentsPerDay = data.AppointmentsPerDay
            DayCount = 2
            ResourceCount = data.ResourceCount
            Resources = data.Resources
        End Sub

        Public Overridable Property Appointments As ObservableCollection(Of AppointmentData)

        Public Overridable Property Resources As ObservableCollection(Of ResourceData)

        Public Overridable Property AppointmentsPerDays As ObservableCollection(Of Integer)

        Public Overridable Property AppointmentsPerDay As Integer

        Public Overridable Property DayCount As Integer

        Public Overridable Property ResourceCount As Integer

        Protected Sub OnAppointmentsPerDayChanged()
            SetUp()
        End Sub

        Protected Sub OnDayCountChanged()
            SetUp()
        End Sub

        Protected Sub OnResourceCountChanged()
            SetUp()
        End Sub

        Private Sub SetUp()
            Resources = Nothing
            Appointments = Nothing
            data.SetUp(Math.Max(data.DayCount, DayCount), ResourceCount, AppointmentsPerDay)
            Resources = data.Resources
            Appointments = data.Appointments
        End Sub
    End Class
End Namespace
