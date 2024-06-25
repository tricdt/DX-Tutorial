Imports DevExpress.Xpf.Scheduling
Imports DevExpress.Xpf.Scheduling.Internal
Imports DevExpress.XtraScheduler.Xml
Imports System
Imports System.Collections.ObjectModel

Namespace SchedulingDemo.ViewModels

    Public Class TimeZonesDemoViewModel

        Public Overridable Property UtcAppointments As ObservableCollection(Of AppointmentEntity)

        Public Overridable Property StorageAppointments As ObservableCollection(Of AppointmentEntity)

        Public Overridable Property StorageTimeZone As TimeZoneInfo

        Public Overridable Property Appointments As ObservableCollection(Of AppointmentEntity)

        Public Sub New()
            UtcAppointments = New ObservableCollection(Of AppointmentEntity)(CreateApts(TimeZoneInfo.Utc))
            Appointments = New ObservableCollection(Of AppointmentEntity)(CreateApts(Nothing))
        End Sub

        Protected Sub OnStorageTimeZoneChanged()
            StorageAppointments = New ObservableCollection(Of AppointmentEntity)(CreateApts(StorageTimeZone))
        End Sub

        Private Function CreateApts(ByVal dbTimeZone As TimeZoneInfo) As AppointmentEntity()
            Dim tz1 = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")
            Dim tz2 = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time")
            Dim tz3 = TimeZoneInfo.FindSystemTimeZoneById("AUS Central Standard Time")
            Dim apt1 = New AppointmentEntity()
            apt1.Subject = "Regular apt"
            apt1.AppointmentType = 0
            apt1.Start = Convert(Date.Today.AddDays(1).AddHours(10), If(dbTimeZone, tz1))
            apt1.End = Convert(Date.Today.AddDays(1).AddHours(11), If(dbTimeZone, tz1))
            apt1.TimeZoneId = tz1.Id
            Dim apt2 = New AppointmentEntity()
            apt2.Subject = "All day apt"
            apt2.AppointmentType = 0
            apt2.AllDay = True
            apt2.Start = Date.Today
            apt2.End = Date.Today.AddDays(1)
            apt2.TimeZoneId = tz2.Id
            Dim recInfo = RecurrenceBuilder.Daily(Convert(Date.Today.AddHours(13), If(dbTimeZone, tz3)), 10).Build()
            Dim apt3 = New AppointmentEntity()
            apt3.Subject = "Pattern apt"
            apt3.AppointmentType = 1
            apt3.Start = Convert(Date.Today.AddHours(13), If(dbTimeZone, tz3))
            apt3.End = Convert(Date.Today.AddHours(14), If(dbTimeZone, tz3))
            apt3.RecurrenceInfo = recInfo.ToXml()
            apt3.TimeZoneId = tz3.Id
            Dim apt4 = New AppointmentEntity()
            apt4.Subject = "Changed Occurrence"
            apt4.AppointmentType = 3
            apt4.Start = Convert(Date.Today.AddDays(1).AddHours(14), If(dbTimeZone, tz3))
            apt4.End = Convert(Date.Today.AddDays(1).AddHours(15), If(dbTimeZone, tz3))
            apt4.RecurrenceInfo = New PatternReferenceXmlPersistenceHelper(New PatternReference(recInfo.Id, 1)).ToXml()
            apt4.TimeZoneId = tz3.Id
            apt4.ReminderInfo = ReminderXmlHelper.ToXml({New ReminderItem() With {.TimeBeforeStart = TimeSpan.FromMinutes(30), .AlertTime = apt4.Start - TimeSpan.FromMinutes(30)}}, Nothing, If(dbTimeZone, tz3).Id)
            Return {apt1, apt2, apt3, apt4}
        End Function

        Private Function Convert(ByVal x As Date, ByVal timeZone As TimeZoneInfo) As Date
            Return TimeZoneInfo.ConvertTime(x, timeZone)
        End Function
    End Class
End Namespace
