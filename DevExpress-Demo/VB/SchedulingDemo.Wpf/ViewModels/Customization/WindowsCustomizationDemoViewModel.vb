Imports DevExpress.Xpf.Scheduling
Imports System.Collections.ObjectModel

Namespace SchedulingDemo.ViewModels

    Public Class WindowsCustomizationDemoViewModel

        Private ReadOnly data As GymData = New GymData(14)

        Protected Sub New()
        End Sub

        Public Overridable ReadOnly Property GymEvents As ObservableCollection(Of GymEvent)
            Get
                Return data.GymEvents
            End Get
        End Property

        Public Overridable ReadOnly Property Trainers As ObservableCollection(Of Trainer)
            Get
                Return data.Trainers
            End Get
        End Property

        Public Overridable ReadOnly Property Trainings As ObservableCollection(Of Training)
            Get
                Return data.Trainings
            End Get
        End Property

        Public Sub InitNewAppointment(ByVal apt As AppointmentItem)
            apt.ResourceId = apt.LabelId
        End Sub
    End Class
End Namespace
