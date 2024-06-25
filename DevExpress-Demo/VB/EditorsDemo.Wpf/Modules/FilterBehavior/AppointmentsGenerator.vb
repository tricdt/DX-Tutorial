Imports System.Collections.Generic
Imports GridDemo

Namespace EditorsDemo.FilterBehavior

    Public Class AppointmentsGenerator

        Public Shared Function CreateAppointments() As List(Of Appointment)
            Return New AppointmentsGenerator().Generate()
        End Function

        Private appointments As List(Of Appointment)

        Private generator As PersonGenerator

        Private labelCount As Integer = 11

        Private Function Generate() As List(Of Appointment)
            generator = New PersonGenerator()
            appointments = New List(Of Appointment)()
            AddAppointmentLayer(1, 0)
            AddAppointmentLayer(2, 0)
            AddAppointmentLayer(1, 1)
            AddAppointmentLayer(2, 1)
            Return appointments
        End Function

        Private Sub AddAppointmentLayer(ByVal hours As Integer, ByVal day As Integer)
            Dim i As Integer = 0
            While i < 24
                Dim person = generator.CreatePerson()
                appointments.Add(New Appointment() With {.Subject = person.FullName, .Location = person.City, .Description = person.JobTitle, .Start = Date.Today.AddDays(day).AddHours(i), .[End] = Date.Today.AddDays(day).AddHours(i + hours), .LabelId =(i + hours * (10 + 4 * day)) Mod labelCount})
                i += hours
            End While
        End Sub
    End Class

    Public Class Appointment

        Public Property Subject As String

        Public Property Location As String

        Public Property Description As String

        Public Property Start As Date

        Public Property [End] As Date

        Public Property LabelId As Object
    End Class
End Namespace
