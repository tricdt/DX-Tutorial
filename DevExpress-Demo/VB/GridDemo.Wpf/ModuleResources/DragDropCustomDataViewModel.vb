Imports System.Collections.ObjectModel
Imports DevExpress.DemoData.Models

Namespace GridDemo

    Public Class DragDropCustomDataViewModel

        Private _Inbox As ObservableCollection(Of GridDemo.SchedulerTask), _Appointments As ObservableCollection(Of GridDemo.Appointment), _Employees As ObservableCollection(Of DevExpress.DemoData.Models.Employee), _AppointmentTypes As ObservableCollection(Of GridDemo.AppointmentType)

        Public Sub New()
            Dim provider = New SchedulerTaskProvider()
            Inbox = New ObservableCollection(Of SchedulerTask)(provider.Inbox)
            Appointments = New ObservableCollection(Of Appointment)(provider.Appointments)
            Employees = New ObservableCollection(Of Employee)(provider.Employees)
            AppointmentTypes = New ObservableCollection(Of AppointmentType)(provider.AppointmentTypes)
        End Sub

        Public Property Inbox As ObservableCollection(Of SchedulerTask)
            Get
                Return _Inbox
            End Get

            Private Set(ByVal value As ObservableCollection(Of SchedulerTask))
                _Inbox = value
            End Set
        End Property

        Public Property Appointments As ObservableCollection(Of Appointment)
            Get
                Return _Appointments
            End Get

            Private Set(ByVal value As ObservableCollection(Of Appointment))
                _Appointments = value
            End Set
        End Property

        Public Property Employees As ObservableCollection(Of Employee)
            Get
                Return _Employees
            End Get

            Private Set(ByVal value As ObservableCollection(Of Employee))
                _Employees = value
            End Set
        End Property

        Public Property AppointmentTypes As ObservableCollection(Of AppointmentType)
            Get
                Return _AppointmentTypes
            End Get

            Private Set(ByVal value As ObservableCollection(Of AppointmentType))
                _AppointmentTypes = value
            End Set
        End Property
    End Class
End Namespace
