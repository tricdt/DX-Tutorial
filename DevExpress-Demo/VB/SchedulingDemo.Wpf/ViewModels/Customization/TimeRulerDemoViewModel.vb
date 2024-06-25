Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports DevExpress.Mvvm.POCO
Imports DevExpress.XtraScheduler

Namespace SchedulingDemo.ViewModels

    Public Class TimeRulerDemoViewModel

        Private timeRulerNumber As Integer = 0

        Protected Sub New()
            Start = TeamData.Start
            Calendars = New ObservableCollection(Of TeamCalendar)(TeamData.Calendars)
            Appointments = New ObservableCollection(Of TeamAppointment)(AllAppointments)
            TimeRulers = New ObservableCollection(Of TimeRulerViewModel)()
            AddNewTimeRuler("GMT", TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"))
            AddNewTimeRuler("Local", TimeZoneInfo.Local)
        End Sub

        Public Overridable Property Calendars As IEnumerable(Of TeamCalendar)

        Public Overridable Property Appointments As IEnumerable(Of TeamAppointment)

        Public Overridable Property Start As Date

        Public Overridable Property TimeRulers As ObservableCollection(Of TimeRulerViewModel)

        Public Overridable Property SelectedTimeRuler As TimeRulerViewModel

        Public Sub AddNewTimeRuler(ByVal Optional caption As String = Nothing)
            If String.IsNullOrEmpty(caption) Then caption = "Ruler"
            AddNewTimeRuler(caption & timeRulerNumber, TimeZoneInfo.Local)
        End Sub

        Public Sub RemoveSelectedTimeRuler()
            TimeRulers.Remove(SelectedTimeRuler)
            SelectedTimeRuler = TimeRulers.FirstOrDefault()
        End Sub

        Private Sub AddNewTimeRuler(ByVal caption As String, ByVal timeZoneInfo As TimeZoneInfo)
            Dim res = TimeRulerViewModel.Create()
            res.Caption = caption
            res.AlwaysShowTimeDesignator = False
            res.ShowMinutes = False
            res.TimeZone = timeZoneInfo
            TimeRulers.Add(res)
            timeRulerNumber += 1
            SelectedTimeRuler = res
        End Sub
    End Class

    Public Class TimeRulerViewModel

        Public Shared Function Create() As TimeRulerViewModel
            Return ViewModelSource.Create(Function() New TimeRulerViewModel())
        End Function

        Protected Sub New()
        End Sub

        Public Overridable Property Caption As String

        Public Overridable Property TimeZone As TimeZoneInfo

        Public Overridable Property TimeMarkerVisibility As TimeMarkerVisibility

        Public Overridable Property AlwaysShowTimeDesignator As Boolean

        Public Overridable Property ShowMinutes As Boolean
    End Class
End Namespace
