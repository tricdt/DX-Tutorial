Imports System.Collections.ObjectModel

Namespace SchedulingDemo.ViewModels

    Public Class MonthViewPerformanceDemoViewModel

        Private Shared ReadOnly data As SampleDataGenerator

        Shared Sub New()
            data = New SampleDataGenerator(Date.Today.AddDays(-200), True)
            data.SetUp(500, 10, 30)
        End Sub

        Protected Sub New()
            AppointmentsPerDays = New ObservableCollection(Of Integer)() From {10, 30, 50, 100}
            AppointmentsPerDay = data.AppointmentsPerDay
            DayCount = data.DayCount
            ResourceCount = data.ResourceCount
        End Sub

        Public Overridable ReadOnly Property Appointments As ObservableCollection(Of AppointmentData)
            Get
                Return data.Appointments
            End Get
        End Property

        Public Overridable ReadOnly Property Resources As ObservableCollection(Of ResourceData)
            Get
                Return data.Resources
            End Get
        End Property

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
            data.SetUp(DayCount, ResourceCount, AppointmentsPerDay)
        End Sub
    End Class
End Namespace
