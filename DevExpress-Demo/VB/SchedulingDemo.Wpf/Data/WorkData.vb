Imports DevExpress.Mvvm.Native
Imports DevExpress.Xpf.Scheduling
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Native
Imports DevExpress.XtraScheduler.Xml
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq

Namespace SchedulingDemo

    Public Class WorkCalendar

        Public Property Id As Long

        Public Property Name As String

        Public Property IsVisible As Boolean

        Public Property Group As String

        Public Property Tag As Object
    End Class

    Public Class WorkAppointment

        Public Property Id As Long

        Public Property AppointmentType As Integer

        Public Property AllDay As Boolean

        Public Property Start As DateTime

        Public Property [End] As DateTime

        Public Property Subject As String

        Public Property Description As String

        Public Property Status As Integer

        Public Property Label As Integer

        Public Property Location As String

        Public Property CalendarIds As IEnumerable(Of Long)

        Public WriteOnly Property CalendarId As Long
            Set(ByVal value As Long)
                Me.CalendarIds = New Long() {value}
            End Set
        End Property

        Public Property RecurrenceInfo As String

        Public Property ReminderInfo As String

        Public Property TimeZoneId As String

        Public Property IsPrivate As Boolean
    End Class

    Public Class WorkLabel

        Public Property Id As Integer

        Public Property Caption As String
    End Class

    Public Module WorkData

        Private _Start As DateTime, _TodayStart As DateTime, _Calendars As ObservableCollection(Of SchedulingDemo.WorkCalendar), _CalendarsEmployees As ObservableCollection(Of SchedulingDemo.WorkCalendar), _CalendarsSupport As ObservableCollection(Of SchedulingDemo.WorkCalendar), _CalendarsRD As ObservableCollection(Of SchedulingDemo.WorkCalendar), _CalendarsRooms As ObservableCollection(Of SchedulingDemo.WorkCalendar), _CalendarMark As WorkCalendar, _CalendarNancy As WorkCalendar, _CalendarAndrew As WorkCalendar, _CalendarJanet As WorkCalendar, _CalendarMichael As WorkCalendar, _CalendarCarolyn As WorkCalendar, _CalendarAnthony As WorkCalendar, _CalendarFrancine As WorkCalendar, _CalendarAaron As WorkCalendar, _CalendarDailiah As WorkCalendar, _CalendarAnita As WorkCalendar, _CalendarConferenceRoom As WorkCalendar, _CalendarTrainingRoom As WorkCalendar, _Appointments As ObservableCollection(Of SchedulingDemo.WorkAppointment)

        Public ReadOnly Property CalendarConferenceRoomId As Long
            Get
                Return SchedulingDemo.WorkData.CalendarConferenceRoom.Id
            End Get
        End Property

        Public ReadOnly Property CalendarTrainingRoomId As Long
            Get
                Return SchedulingDemo.WorkData.CalendarTrainingRoom.Id
            End Get
        End Property

        Public Property Start As DateTime
            Get
                Return _Start
            End Get

            Private Set(ByVal value As DateTime)
                _Start = value
            End Set
        End Property

        Public Property TodayStart As DateTime
            Get
                Return _TodayStart
            End Get

            Private Set(ByVal value As DateTime)
                _TodayStart = value
            End Set
        End Property

        Public Property Calendars As ObservableCollection(Of SchedulingDemo.WorkCalendar)
            Get
                Return _Calendars
            End Get

            Private Set(ByVal value As ObservableCollection(Of SchedulingDemo.WorkCalendar))
                _Calendars = value
            End Set
        End Property

        Public Property CalendarsEmployees As ObservableCollection(Of SchedulingDemo.WorkCalendar)
            Get
                Return _CalendarsEmployees
            End Get

            Private Set(ByVal value As ObservableCollection(Of SchedulingDemo.WorkCalendar))
                _CalendarsEmployees = value
            End Set
        End Property

        Public Property CalendarsSupport As ObservableCollection(Of SchedulingDemo.WorkCalendar)
            Get
                Return _CalendarsSupport
            End Get

            Private Set(ByVal value As ObservableCollection(Of SchedulingDemo.WorkCalendar))
                _CalendarsSupport = value
            End Set
        End Property

        Public Property CalendarsRD As ObservableCollection(Of SchedulingDemo.WorkCalendar)
            Get
                Return _CalendarsRD
            End Get

            Private Set(ByVal value As ObservableCollection(Of SchedulingDemo.WorkCalendar))
                _CalendarsRD = value
            End Set
        End Property

        Public Property CalendarsRooms As ObservableCollection(Of SchedulingDemo.WorkCalendar)
            Get
                Return _CalendarsRooms
            End Get

            Private Set(ByVal value As ObservableCollection(Of SchedulingDemo.WorkCalendar))
                _CalendarsRooms = value
            End Set
        End Property

        Public Property CalendarMark As WorkCalendar
            Get
                Return _CalendarMark
            End Get

            Private Set(ByVal value As WorkCalendar)
                _CalendarMark = value
            End Set
        End Property

        Public Property CalendarNancy As WorkCalendar
            Get
                Return _CalendarNancy
            End Get

            Private Set(ByVal value As WorkCalendar)
                _CalendarNancy = value
            End Set
        End Property

        Public Property CalendarAndrew As WorkCalendar
            Get
                Return _CalendarAndrew
            End Get

            Private Set(ByVal value As WorkCalendar)
                _CalendarAndrew = value
            End Set
        End Property

        Public Property CalendarJanet As WorkCalendar
            Get
                Return _CalendarJanet
            End Get

            Private Set(ByVal value As WorkCalendar)
                _CalendarJanet = value
            End Set
        End Property

        Public Property CalendarMichael As WorkCalendar
            Get
                Return _CalendarMichael
            End Get

            Private Set(ByVal value As WorkCalendar)
                _CalendarMichael = value
            End Set
        End Property

        Public Property CalendarCarolyn As WorkCalendar
            Get
                Return _CalendarCarolyn
            End Get

            Private Set(ByVal value As WorkCalendar)
                _CalendarCarolyn = value
            End Set
        End Property

        Public Property CalendarAnthony As WorkCalendar
            Get
                Return _CalendarAnthony
            End Get

            Private Set(ByVal value As WorkCalendar)
                _CalendarAnthony = value
            End Set
        End Property

        Public Property CalendarFrancine As WorkCalendar
            Get
                Return _CalendarFrancine
            End Get

            Private Set(ByVal value As WorkCalendar)
                _CalendarFrancine = value
            End Set
        End Property

        Public Property CalendarAaron As WorkCalendar
            Get
                Return _CalendarAaron
            End Get

            Private Set(ByVal value As WorkCalendar)
                _CalendarAaron = value
            End Set
        End Property

        Public Property CalendarDailiah As WorkCalendar
            Get
                Return _CalendarDailiah
            End Get

            Private Set(ByVal value As WorkCalendar)
                _CalendarDailiah = value
            End Set
        End Property

        Public Property CalendarAnita As WorkCalendar
            Get
                Return _CalendarAnita
            End Get

            Private Set(ByVal value As WorkCalendar)
                _CalendarAnita = value
            End Set
        End Property

        Public Property CalendarConferenceRoom As WorkCalendar
            Get
                Return _CalendarConferenceRoom
            End Get

            Private Set(ByVal value As WorkCalendar)
                _CalendarConferenceRoom = value
            End Set
        End Property

        Public Property CalendarTrainingRoom As WorkCalendar
            Get
                Return _CalendarTrainingRoom
            End Get

            Private Set(ByVal value As WorkCalendar)
                _CalendarTrainingRoom = value
            End Set
        End Property

        Public Property Appointments As ObservableCollection(Of SchedulingDemo.WorkAppointment)
            Get
                Return _Appointments
            End Get

            Private Set(ByVal value As ObservableCollection(Of SchedulingDemo.WorkAppointment))
                _Appointments = value
            End Set
        End Property

        Private random As System.Random

        Sub New()
            SchedulingDemo.WorkData.random = New System.Random()
            SchedulingDemo.WorkData.Start = SchedulingDemo.WorkData.GetMonday(System.DateTime.Today)
            SchedulingDemo.WorkData.TodayStart = System.DateTime.Today.[Date]
            SchedulingDemo.WorkData.Appointments = New System.Collections.ObjectModel.ObservableCollection(Of SchedulingDemo.WorkAppointment)()
            SchedulingDemo.WorkData.Calendars = New System.Collections.ObjectModel.ObservableCollection(Of SchedulingDemo.WorkCalendar)()
            SchedulingDemo.WorkData.CalendarsEmployees = New System.Collections.ObjectModel.ObservableCollection(Of SchedulingDemo.WorkCalendar)()
            SchedulingDemo.WorkData.CalendarsSupport = New System.Collections.ObjectModel.ObservableCollection(Of SchedulingDemo.WorkCalendar)()
            SchedulingDemo.WorkData.CalendarsRD = New System.Collections.ObjectModel.ObservableCollection(Of SchedulingDemo.WorkCalendar)()
            SchedulingDemo.WorkData.CalendarsRooms = New System.Collections.ObjectModel.ObservableCollection(Of SchedulingDemo.WorkCalendar)()
            Call SchedulingDemo.WorkData.InitCalendars()
            Call SchedulingDemo.WorkData.InitApts()
        End Sub

        Private Sub InitCalendars()
            SchedulingDemo.WorkData.CalendarMark = New SchedulingDemo.WorkCalendar() With {.Id = 0, .Name = "Mark Oliver", .Group = "Support Team"}
            SchedulingDemo.WorkData.CalendarNancy = New SchedulingDemo.WorkCalendar() With {.Id = 1, .Name = "Nancy Davolio", .Group = "Support Team"}
            SchedulingDemo.WorkData.CalendarAndrew = New SchedulingDemo.WorkCalendar() With {.Id = 2, .Name = "Andrew Fuller", .Group = "Support Team"}
            SchedulingDemo.WorkData.CalendarJanet = New SchedulingDemo.WorkCalendar() With {.Id = 3, .Name = "Janet Leverling", .Group = "Support Team"}
            SchedulingDemo.WorkData.CalendarMichael = New SchedulingDemo.WorkCalendar() With {.Id = 4, .Name = "Michael Suyama", .Group = "Support Team"}
            SchedulingDemo.WorkData.CalendarCarolyn = New SchedulingDemo.WorkCalendar() With {.Id = 5, .Name = "Carolyn Baker", .Group = "R&D Team"}
            SchedulingDemo.WorkData.CalendarAnthony = New SchedulingDemo.WorkCalendar() With {.Id = 6, .Name = "Anthony Rounds", .Group = "R&D Team"}
            SchedulingDemo.WorkData.CalendarFrancine = New SchedulingDemo.WorkCalendar() With {.Id = 7, .Name = "Francine Bing", .Group = "R&D Team"}
            SchedulingDemo.WorkData.CalendarAaron = New SchedulingDemo.WorkCalendar() With {.Id = 8, .Name = "Aaron Borrmann", .Group = "R&D Team"}
            SchedulingDemo.WorkData.CalendarDailiah = New SchedulingDemo.WorkCalendar() With {.Id = 9, .Name = "Dailiah Campbell", .Group = "R&D Team"}
            SchedulingDemo.WorkData.CalendarAnita = New SchedulingDemo.WorkCalendar() With {.Id = 10, .Name = "Anita Andrews", .Group = "R&D Team"}
            SchedulingDemo.WorkData.CalendarConferenceRoom = New SchedulingDemo.WorkCalendar() With {.Id = 11, .Name = "Conference Room", .Group = "Rooms"}
            SchedulingDemo.WorkData.CalendarTrainingRoom = New SchedulingDemo.WorkCalendar() With {.Id = 12, .Name = "Training Room", .Group = "Rooms"}
            Call SchedulingDemo.WorkData.CalendarsSupport.Add(SchedulingDemo.WorkData.CalendarMark)
            Call SchedulingDemo.WorkData.CalendarsSupport.Add(SchedulingDemo.WorkData.CalendarNancy)
            Call SchedulingDemo.WorkData.CalendarsSupport.Add(SchedulingDemo.WorkData.CalendarAndrew)
            Call SchedulingDemo.WorkData.CalendarsSupport.Add(SchedulingDemo.WorkData.CalendarJanet)
            Call SchedulingDemo.WorkData.CalendarsSupport.Add(SchedulingDemo.WorkData.CalendarMichael)
            Call SchedulingDemo.WorkData.CalendarsRD.Add(SchedulingDemo.WorkData.CalendarCarolyn)
            Call SchedulingDemo.WorkData.CalendarsRD.Add(SchedulingDemo.WorkData.CalendarAnthony)
            Call SchedulingDemo.WorkData.CalendarsRD.Add(SchedulingDemo.WorkData.CalendarFrancine)
            Call SchedulingDemo.WorkData.CalendarsRD.Add(SchedulingDemo.WorkData.CalendarAaron)
            Call SchedulingDemo.WorkData.CalendarsRD.Add(SchedulingDemo.WorkData.CalendarDailiah)
            Call SchedulingDemo.WorkData.CalendarsRD.Add(SchedulingDemo.WorkData.CalendarAnita)
            Call SchedulingDemo.WorkData.CalendarsRooms.Add(SchedulingDemo.WorkData.CalendarConferenceRoom)
            Call SchedulingDemo.WorkData.CalendarsRooms.Add(SchedulingDemo.WorkData.CalendarTrainingRoom)
            Call SchedulingDemo.WorkData.CalendarsSupport.ForEach(Sub(x) SchedulingDemo.WorkData.CalendarsEmployees.Add(x))
            Call SchedulingDemo.WorkData.CalendarsRD.ForEach(Sub(x) SchedulingDemo.WorkData.CalendarsEmployees.Add(x))
            Call SchedulingDemo.WorkData.CalendarsEmployees.ForEach(Sub(x) SchedulingDemo.WorkData.Calendars.Add(x))
            Call SchedulingDemo.WorkData.CalendarsRooms.ForEach(Sub(x) SchedulingDemo.WorkData.Calendars.Add(x))
        End Sub

        Private Sub InitApts()
            Call SchedulingDemo.WorkData.AddSupportTeamApts()
            Call SchedulingDemo.WorkData.AddRDTeamApts()
            Call SchedulingDemo.WorkData.AddConferences()
            Call SchedulingDemo.WorkData.AddTrainings()
        End Sub

        Private Sub AddSupportTeamApts()
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.SecondShift(SchedulingDemo.WorkData.Start, SchedulingDemo.WorkData.CalendarAndrew.Id))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.SecondShift(SchedulingDemo.WorkData.Start.AddDays(7), SchedulingDemo.WorkData.CalendarJanet.Id))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.SecondShift(SchedulingDemo.WorkData.Start.AddDays(14), SchedulingDemo.WorkData.CalendarMichael.Id))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.SecondShift(SchedulingDemo.WorkData.Start.AddDays(21), SchedulingDemo.WorkData.CalendarNancy.Id))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.SecondShift(SchedulingDemo.WorkData.Start.AddDays(28), SchedulingDemo.WorkData.CalendarMark.Id))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.DayOff(SchedulingDemo.WorkData.Start, SchedulingDemo.WorkData.CalendarNancy.Id))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.DayOff(SchedulingDemo.WorkData.Start.AddDays(CDbl((7))).AddDays(3), SchedulingDemo.WorkData.CalendarAndrew.Id))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.DayOff(SchedulingDemo.WorkData.Start.AddDays(CDbl((14))).AddDays(4), SchedulingDemo.WorkData.CalendarJanet.Id))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.DayOff(SchedulingDemo.WorkData.Start.AddDays(CDbl((14))).AddDays(2), SchedulingDemo.WorkData.CalendarMark.Id))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.Vacation(SchedulingDemo.WorkData.Start.AddDays(21), SchedulingDemo.WorkData.CalendarMichael.Id))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.Vacation(SchedulingDemo.WorkData.Start.AddDays(42), SchedulingDemo.WorkData.CalendarJanet.Id))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.Vacation(SchedulingDemo.WorkData.Start.AddDays(49), SchedulingDemo.WorkData.CalendarMark.Id))
        End Sub

        Private Sub AddRDTeamApts()
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.DayOff(SchedulingDemo.WorkData.Start, SchedulingDemo.WorkData.CalendarCarolyn.Id))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.DayOff(SchedulingDemo.WorkData.Start.AddDays(CDbl((7))).AddDays(2), SchedulingDemo.WorkData.CalendarAnthony.Id))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.DayOff(SchedulingDemo.WorkData.Start.AddDays(CDbl((14))).AddDays(2), SchedulingDemo.WorkData.CalendarFrancine.Id))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.DayOff(SchedulingDemo.WorkData.Start.AddDays(CDbl((14))).AddDays(3), SchedulingDemo.WorkData.CalendarAaron.Id))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.DayOff(SchedulingDemo.WorkData.Start.AddDays(CDbl((21))).AddDays(3), SchedulingDemo.WorkData.CalendarDailiah.Id))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.DayOff(SchedulingDemo.WorkData.Start.AddDays(CDbl((28))).AddDays(4), SchedulingDemo.WorkData.CalendarAnita.Id))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.Vacation(SchedulingDemo.WorkData.Start, SchedulingDemo.WorkData.CalendarDailiah.Id))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.Vacation(SchedulingDemo.WorkData.Start.AddDays(28), SchedulingDemo.WorkData.CalendarCarolyn.Id))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.Vacation(SchedulingDemo.WorkData.Start.AddDays(63), SchedulingDemo.WorkData.CalendarAnthony.Id))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.Vacation(SchedulingDemo.WorkData.Start.AddDays(126), SchedulingDemo.WorkData.CalendarFrancine.Id))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.Vacation(SchedulingDemo.WorkData.Start.AddDays(105), SchedulingDemo.WorkData.CalendarAaron.Id))
        End Sub

        Private Sub AddConferences()
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.Conference1(SchedulingDemo.WorkData.Start.AddDays(CDbl((1))).AddHours(10), SchedulingDemo.WorkData.CalendarsEmployees.[Select](Function(x) x.Id).Concat({SchedulingDemo.WorkData.CalendarConferenceRoom.Id})))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.Conference2(SchedulingDemo.WorkData.Start.AddDays(CDbl((2))).AddHours(9), {SchedulingDemo.WorkData.CalendarMark.Id, SchedulingDemo.WorkData.CalendarNancy.Id, SchedulingDemo.WorkData.CalendarJanet.Id, SchedulingDemo.WorkData.CalendarMichael.Id, SchedulingDemo.WorkData.CalendarConferenceRoom.Id}))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.Conference3(SchedulingDemo.WorkData.Start.AddDays(CDbl((3))).AddHours(9), SchedulingDemo.WorkData.CalendarsEmployees.[Select](Function(x) x.Id).Concat({SchedulingDemo.WorkData.CalendarConferenceRoom.Id})))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.Conference4(SchedulingDemo.WorkData.Start.AddDays(CDbl((-14))).AddDays(CDbl((4))).AddHours(17), SchedulingDemo.WorkData.CalendarsSupport.[Select](Function(x) x.Id).Concat({SchedulingDemo.WorkData.CalendarConferenceRoom.Id})))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.Conference5(SchedulingDemo.WorkData.Start.AddHours(9), SchedulingDemo.WorkData.CalendarsRD.[Select](Function(x) x.Id).Concat({SchedulingDemo.WorkData.CalendarConferenceRoom.Id})))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.Conference6(SchedulingDemo.WorkData.Start.AddDays(CDbl((11))).AddHours(15.5), SchedulingDemo.WorkData.CalendarsRD.[Select](Function(x) x.Id).Concat({SchedulingDemo.WorkData.CalendarConferenceRoom.Id})))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.Conference7(SchedulingDemo.WorkData.Start.AddDays(CDbl((4))).AddHours(CDbl((12))).AddMinutes(15), SchedulingDemo.WorkData.CalendarsRD.[Select](Function(x) x.Id).Concat({SchedulingDemo.WorkData.CalendarConferenceRoom.Id})))
        End Sub

        Private Sub AddTrainings()
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.TrainingFrenchLesson(SchedulingDemo.WorkData.Start.AddDays(CDbl((-7))).AddHours(CDbl((11))).AddMinutes(10), {SchedulingDemo.WorkData.CalendarJanet.Id, SchedulingDemo.WorkData.CalendarMichael.Id, SchedulingDemo.WorkData.CalendarTrainingRoom.Id}))
            Call SchedulingDemo.WorkData.Appointments.Add(SchedulingDemo.WorkData.TrainingTrainStaffOnNewRemoteControls(SchedulingDemo.WorkData.Start.AddDays(CDbl((3))).AddHours(CDbl((14))).AddMinutes(10), {SchedulingDemo.WorkData.CalendarMark.Id, SchedulingDemo.WorkData.CalendarNancy.Id, SchedulingDemo.WorkData.CalendarJanet.Id, SchedulingDemo.WorkData.CalendarTrainingRoom.Id}))
            Dim germanLessons = SchedulingDemo.WorkData.TrainingGermanLesson(SchedulingDemo.WorkData.Start.AddHours(CDbl((15))).AddMinutes(40), {SchedulingDemo.WorkData.CalendarMark.Id, SchedulingDemo.WorkData.CalendarNancy.Id, SchedulingDemo.WorkData.CalendarAndrew.Id, SchedulingDemo.WorkData.CalendarTrainingRoom.Id})
            germanLessons.ForEach(Sub(x) SchedulingDemo.WorkData.Appointments.Add(x))
        End Sub

        Private Function DayOff(ByVal start As System.DateTime, ByVal calendarId As Long) As WorkAppointment
            Dim newStart As System.DateTime = start
            Dim newEnd As System.DateTime = newStart.AddDays(1)
            Dim appt = New SchedulingDemo.WorkAppointment() With {.AppointmentType = CInt(DevExpress.XtraScheduler.AppointmentType.Normal), .AllDay = True, .Start = newStart, .[End] = newEnd, .Subject = "Day off", .CalendarId = calendarId}
            Return appt
        End Function

        Private Function SecondShift(ByVal start As System.DateTime, ByVal calendarId As Long) As WorkAppointment
            Dim newStart As System.DateTime = start
            Dim newEnd As System.DateTime = newStart.AddDays(5)
            Dim appt = New SchedulingDemo.WorkAppointment() With {.AppointmentType = CInt(DevExpress.XtraScheduler.AppointmentType.Normal), .AllDay = True, .Start = newStart, .[End] = newEnd, .Subject = "Second shift", .CalendarId = calendarId}
            Return appt
        End Function

        Private Function Vacation(ByVal start As System.DateTime, ByVal calendarId As Long) As WorkAppointment
            Dim newStart As System.DateTime = start
            Dim newEnd As System.DateTime = newStart.AddDays(12)
            Dim appt = New SchedulingDemo.WorkAppointment() With {.AppointmentType = CInt(DevExpress.XtraScheduler.AppointmentType.Normal), .AllDay = True, .Start = newStart, .[End] = newEnd, .Subject = "Vacation", .CalendarId = calendarId}
            Return appt
        End Function

        Private Function Conference1(ByVal start As System.DateTime, ByVal calendarIds As System.Collections.Generic.IEnumerable(Of Long)) As WorkAppointment
            Dim newStart As System.DateTime = start
            Dim newEnd As System.DateTime = newStart.AddHours(1)
            Dim appt = New SchedulingDemo.WorkAppointment() With {.AppointmentType = CInt(DevExpress.XtraScheduler.AppointmentType.Normal), .Start = newStart, .[End] = newEnd, .Subject = "Company-wide meeting", .Location = "Conference Room", .Description = "Everyone must be ready to ask questions and inform leadership team why they are not performing as expected and what we need to do as a team to improve morale.", .CalendarIds = calendarIds, .Label = 1}
            Return appt
        End Function

        Private Function Conference2(ByVal start As System.DateTime, ByVal calendarIds As System.Collections.Generic.IEnumerable(Of Long)) As WorkAppointment
            Dim newStart As System.DateTime = start
            Dim newEnd As System.DateTime = newStart.AddHours(1)
            Dim appt = New SchedulingDemo.WorkAppointment() With {.AppointmentType = CInt(DevExpress.XtraScheduler.AppointmentType.Normal), .Start = newStart, .[End] = newEnd, .Subject = "Customer retention review", .Location = "Conference Room", .Description = "Discuss ways in which we can improve our relationship with customers and prove to them that we are the long term source for all their A/V needs.", .CalendarIds = calendarIds}
            Return appt
        End Function

        Private Function Conference3(ByVal start As System.DateTime, ByVal calendarIds As System.Collections.Generic.IEnumerable(Of Long)) As WorkAppointment
            Dim newStart As System.DateTime = start
            Dim newEnd As System.DateTime = newStart.AddHours(2)
            Dim appt = New SchedulingDemo.WorkAppointment() With {.AppointmentType = CInt(DevExpress.XtraScheduler.AppointmentType.Normal), .Start = newStart, .[End] = newEnd, .Subject = "Final website design review", .Location = "Conference Room", .Description = "It's time to launch our new website. Management can no longer tolerate delays nor accept excuses. We've been waiting long enough for the overhaul.", .CalendarIds = calendarIds, .Label = 1}
            Return appt
        End Function

        Private Function Conference4(ByVal start As System.DateTime, ByVal calendarIds As System.Collections.Generic.IEnumerable(Of Long)) As WorkAppointment
            Dim newStart As System.DateTime = start.AddMinutes(15)
            Dim newEnd As System.DateTime = newStart.AddMinutes(45)
            Dim appt = New SchedulingDemo.WorkAppointment() With {.AppointmentType = CInt(DevExpress.XtraScheduler.AppointmentType.Pattern), .Start = newStart, .[End] = newEnd, .Subject = "Support Team - Weekly meeting", .CalendarIds = calendarIds}
            appt.RecurrenceInfo = DevExpress.Xpf.Scheduling.RecurrenceBuilder.Weekly(CDate((newStart))).ByDay(CType((DevExpress.XtraScheduler.WeekDays.Friday), DevExpress.XtraScheduler.WeekDays)).Build().ToXml()
            Return appt
        End Function

        Private Function Conference5(ByVal start As System.DateTime, ByVal calendarIds As System.Collections.Generic.IEnumerable(Of Long)) As WorkAppointment
            Dim newStart As System.DateTime = start
            Dim newEnd As System.DateTime = newStart.AddHours(1)
            Dim appt = New SchedulingDemo.WorkAppointment() With {.AppointmentType = CInt(DevExpress.XtraScheduler.AppointmentType.Normal), .Start = newStart, .[End] = newEnd, .Subject = "UL testing and certification", .Description = "Discuss Underwriters Laboratories requirements. Review testing status, expected costs, and determine likelihood of certification.", .Location = "Conference Room", .CalendarIds = calendarIds}
            Return appt
        End Function

        Private Function Conference6(ByVal start As System.DateTime, ByVal calendarIds As System.Collections.Generic.IEnumerable(Of Long)) As WorkAppointment
            Dim newStart As System.DateTime = start
            Dim newEnd As System.DateTime = newStart.AddHours(1.5)
            Dim appt = New SchedulingDemo.WorkAppointment() With {.AppointmentType = CInt(DevExpress.XtraScheduler.AppointmentType.Normal), .Start = newStart, .[End] = newEnd, .Subject = "OLED and 8K transition plans", .Description = "Review final OLED schematics. Verify supply-chain requirements and finalize 8K specs for marketing.", .Location = "Conference Room", .CalendarIds = calendarIds}
            Return appt
        End Function

        Private Function Conference7(ByVal start As System.DateTime, ByVal calendarIds As System.Collections.Generic.IEnumerable(Of Long)) As WorkAppointment
            Dim newStart As System.DateTime = start
            Dim newEnd As System.DateTime = newStart.AddHours(CDbl((1))).AddMinutes(45)
            Dim appt = New SchedulingDemo.WorkAppointment() With {.AppointmentType = CInt(DevExpress.XtraScheduler.AppointmentType.Normal), .Start = newStart, .[End] = newEnd, .Subject = "Home automation protocol", .Description = "Discuss HDMI-CEC protocols and decide whether to introduce control protocol via IP interface.", .Location = "Conference Room", .CalendarIds = calendarIds}
            Return appt
        End Function

        Private Function TrainingFrenchLesson(ByVal start As System.DateTime, ByVal calendarIds As System.Collections.Generic.IEnumerable(Of Long)) As WorkAppointment
            Dim newStart As System.DateTime = start
            Dim newEnd As System.DateTime = newStart.AddHours(1.5)
            Dim appt = New SchedulingDemo.WorkAppointment() With {.AppointmentType = CInt(DevExpress.XtraScheduler.AppointmentType.Pattern), .Start = newStart, .[End] = newEnd, .Subject = "French lesson", .Location = "Training Room", .Description = "Another french lesson.Once again, without mastering French, our success on the Continent will be less likely.Everyone needs to learn French.", .CalendarIds = calendarIds}
            appt.RecurrenceInfo = DevExpress.Xpf.Scheduling.RecurrenceBuilder.Weekly(CDate((newStart)), CInt((10))).ByDay(CType((DevExpress.XtraScheduler.WeekDays.Monday Or DevExpress.XtraScheduler.WeekDays.Wednesday), DevExpress.XtraScheduler.WeekDays)).Build().ToXml()
            Return appt
        End Function

        Private Function TrainingGermanLesson(ByVal start As System.DateTime, ByVal calendarIds As System.Collections.Generic.IEnumerable(Of Long)) As List(Of SchedulingDemo.WorkAppointment)
            Dim newStart As System.DateTime = start.AddDays(1)
            Dim newEnd As System.DateTime = newStart.AddHours(1.5)
            Dim appt = New SchedulingDemo.WorkAppointment() With {.AppointmentType = CInt(DevExpress.XtraScheduler.AppointmentType.Pattern), .Start = newStart, .[End] = newEnd, .Subject = "German lesson", .Location = "Training Room", .Description = "We're learning French but we also need to become fluent in German. The German market for A/V products is huge. We need to be able to communicate in German if we are to have any luck in Germany.", .CalendarIds = calendarIds}
            Dim recInfo = DevExpress.Xpf.Scheduling.RecurrenceBuilder.Weekly(CDate((newStart)), CInt((10))).ByDay(CType((DevExpress.XtraScheduler.WeekDays.Tuesday Or DevExpress.XtraScheduler.WeekDays.Friday), DevExpress.XtraScheduler.WeekDays)).Build()
            appt.RecurrenceInfo = recInfo.ToXml()
            Dim changed = SchedulingDemo.WorkData.ChangedOccurrence(appt, newStart.AddDays(CDbl((7))).AddHours(-1), recInfo.Id, 2, calendarIds)
            Dim deleted = SchedulingDemo.WorkData.DeletedOccurrence(appt, newStart.AddDays(14), recInfo.Id, 4, calendarIds)
            Return {appt, changed, deleted}.ToList()
        End Function

        Private Function TrainingTrainStaffOnNewRemoteControls(ByVal start As System.DateTime, ByVal calendarIds As System.Collections.Generic.IEnumerable(Of Long)) As WorkAppointment
            Dim newStart As System.DateTime = start
            Dim newEnd As System.DateTime = newStart.AddHours(CDbl((1))).AddMinutes(50)
            Dim appt = New SchedulingDemo.WorkAppointment() With {.AppointmentType = CInt(DevExpress.XtraScheduler.AppointmentType.Normal), .Start = newStart, .[End] = newEnd, .Subject = "Train staff on new remote controls", .Location = "Training Room", .Description = "Our newest remote controls are ready for production. Everyone needs to understand how our new universal remote works and our long term plans for better automation.", .CalendarIds = calendarIds}
            Return appt
        End Function

        Private Function ChangedOccurrence(ByVal pattern As SchedulingDemo.WorkAppointment, ByVal start As System.DateTime, ByVal recId As Object, ByVal index As Integer, ByVal calendarIds As System.Collections.Generic.IEnumerable(Of Long)) As WorkAppointment
            Dim duration = pattern.[End] - pattern.Start
            Dim appt = New SchedulingDemo.WorkAppointment() With {.AppointmentType = CInt(DevExpress.XtraScheduler.AppointmentType.ChangedOccurrence), .AllDay = pattern.AllDay, .Start = start, .[End] = start + duration, .Subject = pattern.Subject, .Location = pattern.Location, .Description = pattern.Description, .CalendarIds = calendarIds, .IsPrivate = pattern.IsPrivate, .Label = pattern.Label, .Status = pattern.Status}
            appt.RecurrenceInfo = New DevExpress.XtraScheduler.Xml.PatternReferenceXmlPersistenceHelper(CType((New DevExpress.XtraScheduler.Xml.PatternReference(CObj((recId)), CInt((index)))), DevExpress.XtraScheduler.Xml.PatternReference)).ToXml()
            Return appt
        End Function

        Private Function DeletedOccurrence(ByVal pattern As SchedulingDemo.WorkAppointment, ByVal start As System.DateTime, ByVal recId As Object, ByVal index As Integer, ByVal calendarIds As System.Collections.Generic.IEnumerable(Of Long)) As WorkAppointment
            Dim duration = pattern.[End] - pattern.Start
            Dim appt = New SchedulingDemo.WorkAppointment() With {.AppointmentType = CInt(DevExpress.XtraScheduler.AppointmentType.DeletedOccurrence), .AllDay = pattern.AllDay, .Start = start, .[End] = start + duration, .Subject = pattern.Subject, .Location = pattern.Location, .Description = pattern.Description, .CalendarIds = calendarIds, .IsPrivate = pattern.IsPrivate, .Label = pattern.Label, .Status = pattern.Status}
            appt.RecurrenceInfo = New DevExpress.XtraScheduler.Xml.PatternReferenceXmlPersistenceHelper(CType((New DevExpress.XtraScheduler.Xml.PatternReference(CObj((recId)), CInt((index)))), DevExpress.XtraScheduler.Xml.PatternReference)).ToXml()
            Return appt
        End Function

        Private Function GetMonday(ByVal [date] As System.DateTime) As DateTime
            Dim dayOfWeek As System.DayOfWeek = [date].DayOfWeek
            If dayOfWeek = System.DayOfWeek.Monday Then Return [date].[Date]
            If dayOfWeek = System.DayOfWeek.Saturday Then Return [date].[Date].AddDays(2)
            If dayOfWeek = System.DayOfWeek.Sunday Then Return [date].[Date].AddDays(1)
            Return [date].[Date].AddDays(-(CInt(dayOfWeek) - 1))
        End Function
    End Module
End Namespace
