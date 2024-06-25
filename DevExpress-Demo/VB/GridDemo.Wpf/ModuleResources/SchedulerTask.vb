Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.DemoData
Imports DevExpress.DemoData.Models

Namespace GridDemo

    Public Class SchedulerTask

        Public Property Subject As String

        Public Property Priority As IssuePriority

        Public Property Duration As TimeSpan

        Public ReadOnly Property TotalHours As Integer
            Get
                Return CInt(System.Math.Ceiling(Me.Duration.TotalHours))
            End Get
        End Property

        Public Property Description As String

        Public ReadOnly Property Name As String
            Get
                Return If(Not String.IsNullOrEmpty(Me.Description), Me.Description, Me.Subject)
            End Get
        End Property

        Public Property AllDay As Boolean

        Public Overrides Function ToString() As String
            Return Me.Subject
        End Function
    End Class

    Public Class Appointment

        Public Property Id As Integer

        Public Property LabelId As IssuePriority

        Public Property Start As DateTime

        Public Property [End] As DateTime

        Public Property OwnerId As Long

        Public Property Subject As String

        Public Property Description As String

        Public Property AllDay As Boolean
    End Class

    Public Class AppointmentType

        Public Property Priority As IssuePriority

        Public ReadOnly Property Caption As String
            Get
                Return Me.Priority.ToString()
            End Get
        End Property
    End Class

    Public Class SchedulerTaskProvider

        Private _Inbox As IList(Of GridDemo.SchedulerTask), _Employees As IList(Of DevExpress.DemoData.Models.Employee), _AppointmentTypes As IList(Of GridDemo.AppointmentType), _Appointments As IList(Of GridDemo.Appointment)

        Public Property Inbox As IList(Of GridDemo.SchedulerTask)
            Get
                Return _Inbox
            End Get

            Protected Set(ByVal value As IList(Of GridDemo.SchedulerTask))
                _Inbox = value
            End Set
        End Property

        Public Property Employees As IList(Of DevExpress.DemoData.Models.Employee)
            Get
                Return _Employees
            End Get

            Protected Set(ByVal value As IList(Of DevExpress.DemoData.Models.Employee))
                _Employees = value
            End Set
        End Property

        Public Property AppointmentTypes As IList(Of GridDemo.AppointmentType)
            Get
                Return _AppointmentTypes
            End Get

            Protected Set(ByVal value As IList(Of GridDemo.AppointmentType))
                _AppointmentTypes = value
            End Set
        End Property

        Public Property Appointments As IList(Of GridDemo.Appointment)
            Get
                Return _Appointments
            End Get

            Protected Set(ByVal value As IList(Of GridDemo.Appointment))
                _Appointments = value
            End Set
        End Property

        Public Sub New()
            Me.GenerateData()
        End Sub

        Private Sub GenerateData()
            Const appointmentsCount As Integer = 4
            Dim inboxTaskDescriptions As String() = GridDemo.SchedulerTaskProvider.GetAllSubjects().Skip(appointmentsCount).ToArray()
            Dim appointmentsDescriptions As String() = GridDemo.SchedulerTaskProvider.GetAllSubjects().Take(appointmentsCount).ToArray()
            Me.Inbox = GridDemo.SchedulerTaskProvider.GenerateSchedulerTasks(inboxTaskDescriptions)
            Me.Employees = GridDemo.SchedulerTaskProvider.GenerateEmployees()
            Me.Appointments = GridDemo.SchedulerTaskProvider.GenerateAppointments(appointmentsDescriptions, GridDemo.SchedulerTaskProvider.GetOwnerIds(Me.Employees))
            Me.AppointmentTypes = GridDemo.SchedulerTaskProvider.GenerateAppointmentTypes()
        End Sub

        Private Shared Function GetAllSubjects() As IEnumerable(Of String)
            Return GridDemo.OutlookDataGenerator.Subjects
        End Function

        Private Shared Function GenerateSchedulerTasks(ByVal descriptions As String()) As IList(Of GridDemo.SchedulerTask)
            Dim severityRandom = New System.Random()
            Dim priorityRandom = New System.Random()
            Dim durationRandom = New System.Random()
            Return descriptions.[Select](Function(d) GridDemo.SchedulerTaskProvider.CreateSchedulerTask(d, severityRandom, priorityRandom, durationRandom)).ToList()
        End Function

        Private Shared Function CreateSchedulerTask(ByVal description As String, ByVal severityRandom As System.Random, ByVal priorityRandom As System.Random, ByVal durationRandom As System.Random) As SchedulerTask
            Return New GridDemo.SchedulerTask With {.Subject = GridDemo.SchedulerTaskProvider.GetSubject(description), .Priority = GetRandomEnumValue(Of DevExpress.DemoData.IssuePriority)(priorityRandom), .Duration = System.TimeSpan.FromMinutes(durationRandom.[Next](1, 5) * 60), .Description = description}
        End Function

        Private Shared Function GetSubject(ByVal description As String) As String
            If Not System.[String].IsNullOrWhiteSpace(description) Then
                Dim charLocation As Integer = description.IndexOf("."c)
                If charLocation > 0 Then Return description.Substring(0, charLocation)
            End If

            Return System.[String].Empty
        End Function

        Private Shared Function GetRandomEnumValue(Of T)(ByVal random As System.Random) As T
            Dim values As System.Array = System.[Enum].GetValues(GetType(T))
            Return CType(values.GetValue(random.[Next](values.Length)), T)
        End Function

        Private Shared Function GenerateEmployees() As IList(Of DevExpress.DemoData.Models.Employee)
            Const resourceCount As Integer = 3
            Return DevExpress.DemoData.NWindDataProvider.Employees.Take(resourceCount).ToList()
        End Function

        Private Shared Function GetOwnerIds(ByVal employees As System.Collections.Generic.IEnumerable(Of DevExpress.DemoData.Models.Employee)) As Long()
            Return employees.[Select](Function(x) x.EmployeeID).ToArray()
        End Function

        Private Shared Function GenerateAppointmentTypes() As IList(Of GridDemo.AppointmentType)
            Dim appointmentType = New System.Collections.Generic.List(Of GridDemo.AppointmentType)()
            appointmentType.Add(New GridDemo.AppointmentType With {.Priority = DevExpress.DemoData.IssuePriority.Low})
            appointmentType.Add(New GridDemo.AppointmentType With {.Priority = DevExpress.DemoData.IssuePriority.High})
            appointmentType.Add(New GridDemo.AppointmentType With {.Priority = DevExpress.DemoData.IssuePriority.Medium})
            Return appointmentType
        End Function

        Private Shared Function GenerateAppointments(ByVal descriptions As String(), ByVal ownerIds As Long()) As IList(Of GridDemo.Appointment)
            Dim appointments = New System.Collections.Generic.List(Of GridDemo.Appointment)()
            appointments.Add(New GridDemo.Appointment With {.Id = 0, .Subject = GridDemo.SchedulerTaskProvider.GetSubject(descriptions(0)), .Description = descriptions(0), .OwnerId = ownerIds(0), .LabelId = DevExpress.DemoData.IssuePriority.Low, .Start = GridDemo.SchedulerTaskProvider.GetTime(9, 30), .[End] = GridDemo.SchedulerTaskProvider.GetTime(10, 30)})
            appointments.Add(New GridDemo.Appointment With {.Id = 1, .Subject = GridDemo.SchedulerTaskProvider.GetSubject(descriptions(1)), .Description = descriptions(1), .OwnerId = ownerIds(1), .LabelId = DevExpress.DemoData.IssuePriority.Medium, .Start = GridDemo.SchedulerTaskProvider.GetTime(9, 30), .[End] = GridDemo.SchedulerTaskProvider.GetTime(10, 30)})
            appointments.Add(New GridDemo.Appointment With {.Id = 2, .Subject = GridDemo.SchedulerTaskProvider.GetSubject(descriptions(2)), .Description = descriptions(2), .OwnerId = ownerIds(1), .LabelId = DevExpress.DemoData.IssuePriority.Low, .Start = GridDemo.SchedulerTaskProvider.GetTime(11, 0), .[End] = GridDemo.SchedulerTaskProvider.GetTime(12, 0)})
            appointments.Add(New GridDemo.Appointment With {.Id = 3, .Subject = GridDemo.SchedulerTaskProvider.GetSubject(descriptions(3)), .Description = descriptions(3), .OwnerId = ownerIds(2), .LabelId = DevExpress.DemoData.IssuePriority.High, .Start = GridDemo.SchedulerTaskProvider.GetTime(9, 30), .[End] = GridDemo.SchedulerTaskProvider.GetTime(12, 0)})
            Return appointments
        End Function

        Private Shared Function GetTime(ByVal hours As Integer, ByVal minutes As Integer) As DateTime
            Return System.DateTime.Today.AddHours(CDbl((hours))).AddMinutes(minutes)
        End Function
    End Class
End Namespace
