Imports DevExpress.Mvvm.Native
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Scheduling
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq

Namespace SchedulingDemo.ViewModels

    Public Class TimeRegionsDemoViewModel

        Public Const AnyResource As String = "(Any)"

        Private ReadOnly regionDuration As TimeSpan = TimeSpan.FromHours(3)

        Private regionStart As Date

        Private timeRegionNumber As Integer = 0

        Public Overridable Property Start As Date

        Public ReadOnly Property Calendars As IEnumerable(Of WorkCalendar)
            Get
                Return WorkData.Calendars
            End Get
        End Property

        Public ReadOnly Property Appointments As IEnumerable(Of WorkAppointment)
            Get
                Return WorkData.Appointments
            End Get
        End Property

        Public Overridable Property TimeRegions As ObservableCollection(Of TimeRegionViewModel)

        Public Overridable Property SelectedTimeRegion As TimeRegionViewModel

        Public Overridable Property Resources As IList(Of String)

        Public Sub New()
            Start = TodayStart
            TimeRegions = New ObservableCollection(Of TimeRegionViewModel)()
            regionStart = Start.AddHours(9)
            Init()
        End Sub

        Private Sub Init()
            Enumerable.ToList(WorkData.Calendars).ForEach(Sub(x) x.IsVisible = False)
            CalendarConferenceRoom.IsVisible = True
            CalendarTrainingRoom.IsVisible = True
            Resources = CalendarsRooms.[Select](Function(x) x.Name).ToList()
            Resources.Insert(0, AnyResource)
            Dim customRegionStart As Date = Start.AddDays(1).AddHours(9)
            AddCustomTimeRegion("Predefined TimeRegion1", customRegionStart, customRegionStart.AddMinutes(45), DefaultBrushNames.TimeRegion1Hatch)
            customRegionStart = Start.AddDays(1).AddHours(13)
            AddCustomTimeRegion("Predefined TimeRegion2", customRegionStart, customRegionStart.AddMinutes(15), DefaultBrushNames.TimeRegion8Dotted)
            customRegionStart = Start.AddDays(2).AddHours(11)
            AddCustomTimeRegion("Predefined TimeRegion3", customRegionStart, customRegionStart.AddMinutes(75), DefaultBrushNames.TimeRegion2Hatch, New Long() {1})
            customRegionStart = Start.AddDays(3).AddHours(10)
            AddCustomTimeRegion("Predefined TimeRegion4", customRegionStart, customRegionStart.AddHours(5), DefaultBrushNames.TimeRegion3Hatch)
            SelectedTimeRegion = TimeRegions(0)
        End Sub

        Private Sub AddCustomTimeRegion(ByVal caption As String, ByVal start As Date, ByVal [end] As Date, ByVal brushName As String, ByVal Optional resourceIds As IEnumerable(Of Long) = Nothing)
            Dim res = TimeRegionViewModel.Create()
            res.Caption = caption
            res.Start = start
            res.End = [end]
            res.BrushName = brushName
            res.ResourceIds = resourceIds
            TimeRegions.Add(res)
        End Sub

        Private Sub AddNewTimeRegionInternal(ByVal caption As String)
            Dim res = TimeRegionViewModel.Create()
            res.Caption = caption
            res.Start = regionStart
            res.End = regionStart + regionDuration
            res.BrushName = DefaultBrushNames.TimeRegions(timeRegionNumber Mod DefaultBrushNames.TimeRegions.Length)
            TimeRegions.Add(res)
            timeRegionNumber += 1
            regionStart += regionDuration
            SelectedTimeRegion = res
        End Sub

        Public Sub AddNewTimeRegion(ByVal Optional caption As String = Nothing)
            If String.IsNullOrEmpty(caption) Then caption = "TimeRegion"
            AddNewTimeRegionInternal(caption & timeRegionNumber)
        End Sub

        Public Sub RemoveSelectedTimeRegion()
            TimeRegions.Remove(SelectedTimeRegion)
            SelectedTimeRegion = TimeRegions.FirstOrDefault()
        End Sub
    End Class

    Public Class TimeRegionViewModel

        Public Shared Function Create() As TimeRegionViewModel
            Return ViewModelSource.Create(Function() New TimeRegionViewModel())
        End Function

        Protected Sub New()
        End Sub

        Public Overridable Property Caption As String

        Public Overridable Property Start As Date

        Public Overridable Property [End] As Date

        Public Overridable Property BrushName As String

        Public Overridable ReadOnly Property BorderBrushName As String
            Get
                Return DefaultBrushNames.Resources(DefaultBrushNames.TimeRegions.IndexOf(Function(x) Equals(x, BrushName)) Mod DefaultBrushNames.Resources.Length)
            End Get
        End Property

        Public Overridable Property ResourceIds As IEnumerable(Of Long)
    End Class
End Namespace
