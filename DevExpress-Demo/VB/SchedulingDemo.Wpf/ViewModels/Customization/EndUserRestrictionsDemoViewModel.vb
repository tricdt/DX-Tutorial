Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Diagnostics
Imports DevExpress.Xpf.Scheduling

Namespace SchedulingDemo.ViewModels

    Public Class EndUserRestrictionsDemoViewModel

        Protected Sub New()
            Start = TeamData.Start
            Calendars = New ObservableCollection(Of TeamCalendar)(TeamData.Calendars)
            Appointments = New ObservableCollection(Of TeamAppointment)(AllAppointments)
        End Sub

        Public Overridable Property Start As Date

        Public Overridable Property Calendars As IEnumerable(Of TeamCalendar)

        Public Overridable Property Appointments As IEnumerable(Of TeamAppointment)

        Public Overridable Property IsTeamCalendarReadonly As Boolean

        Public Overridable Property SelectedResource As ResourceItem

        Public Sub CustomAllowAppointmentItemOperation(ByVal e As AppointmentItemOperationEventArgs)
            If e.Appointment Is Nothing AndAlso SelectedResource Is Nothing Then Return
            Dim resource = If(e.Appointment Is Nothing, SelectedResource.Id, e.Appointment.ResourceId)
            e.Allow = Not IsReadOnly(resource)
        End Sub

        Public Sub StartAppointmentDrag(ByVal e As StartAppointmentDragEventArgs)
            e.Cancel = IsCancelDragDrop(e.SourceAppointments)
        End Sub

        Public Sub DropAppointment(ByVal e As DropAppointmentEventArgs)
            e.Cancel = IsCancelDragDrop(e.DragAppointments)
        End Sub

        Public Sub DragAppointmentOver(ByVal e As DragAppointmentOverEventArgs)
            If IsCancelDragDrop(e.DragAppointments) Then
                e.Effects = Windows.DragDropEffects.None
                For i As Integer = 0 To e.DragAppointments.Count - 1
                    e.ConflictedAppointments(i).Add(e.DragAppointments(i))
                Next
            End If
        End Sub

        Public Sub StartAppointmentResize(ByVal e As StartAppointmentResizeEventArgs)
            e.Cancel = IsTeamCalendarReadonly AndAlso e.SourceAppointment.ResourceId.Equals(1)
        End Sub

        Private Function IsCancelDragDrop(ByVal appointments As IEnumerable(Of AppointmentItem)) As Boolean
            Dim cancel As Boolean = False
            For Each appointment As AppointmentItem In appointments
                cancel = IsReadOnly(appointment.ResourceId)
                If cancel Then Exit For
            Next

            Return cancel
        End Function

        Private Function IsReadOnly(ByVal resourceId As Object) As Boolean
            Return IsTeamCalendarReadonly AndAlso resourceId.Equals(1)
        End Function
    End Class
End Namespace
