Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase
Imports DevExpress.Xpf.Scheduling
Imports DevExpress.XtraScheduler.Native
Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace SchedulingDemo

    <NoAutogeneratedCodeFiles, CodeFiles("Modules/ViewTypes/TimelineViewDemoModule.xaml", "ViewModels/ViewTypes/TimelineViewDemoViewModel.(cs)", "Themes/Ribbon.xaml")>
    Public Partial Class TimelineViewDemoModule
        Inherits SchedulingDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Function CoerceDateNavigatorSelectedDates(ByVal dates As IList(Of Date)) As Boolean
            If dates.Count = 0 Then Return False
            Dim start = dates.Min()
            Dim [end] = dates.Max()
            If timelineViewDay.IsActive Then
                dates.Clear()
                dates.Add(start)
                Return True
            End If

            If timelineViewWeek.IsActive Then
                Dim actualStart = DateTimeHelper.GetStartOfWeekUI(start, scheduler.FirstDayOfWeek)
                Dim actualEnd = actualStart.AddDays(7)
                dates.Clear()
                While actualStart < actualEnd
                    dates.Add(actualStart)
                    actualStart = actualStart.AddDays(1)
                End While

                Return True
            End If

            If timelineViewMonth.IsActive Then
                Dim actualStart = New DateTime(start.Year, start.Month, 1)
                Dim actualEnd = actualStart.AddMonths(1)
                dates.Clear()
                While actualStart < actualEnd
                    dates.Add(actualStart)
                    actualStart = actualStart.AddDays(1)
                End While

                Return True
            End If

            Return False
        End Function

        Private Sub dateNavigatorSettings_CustomizeSelectedDates(ByVal sender As Object, ByVal e As CustomizeSelectedDatesEventArgs)
            e.Handled = CoerceDateNavigatorSelectedDates(e.Dates)
        End Sub

        Private Sub scheduler_ActiveViewChanged(ByVal sender As Object, ByVal e As ValueChangedEventArgs(Of ViewBase))
            If e.NewValue Is Nothing Then Return
            Dim selectionStart = scheduler.SelectedInterval.Start
            Dim dates = New List(Of Date)() From {selectionStart.Date}
            If Not CoerceDateNavigatorSelectedDates(dates) Then Return
            scheduler.Start = dates.Min()
            Dim intervalDuration = Enumerable.Max(dates).AddDays(1) - dates.Min()
            Dim isUnlimitedView As Boolean = e.NewValue Is timelineView
            Dim isDayView As Boolean = e.NewValue Is timelineViewDay
            If(isUnlimitedView OrElse isDayView) AndAlso selectionStart.TimeOfDay = TimeSpan.Zero Then selectionStart = selectionStart.AddHours(9)
            CType(e.NewValue, TimelineView).IntervalDuration = intervalDuration
            CType(e.NewValue, TimelineView).ViewportStart = selectionStart
            scheduler.SelectedInterval = New DateTimeRange(selectionStart, TimeSpan.Zero)
        End Sub
    End Class
End Namespace
