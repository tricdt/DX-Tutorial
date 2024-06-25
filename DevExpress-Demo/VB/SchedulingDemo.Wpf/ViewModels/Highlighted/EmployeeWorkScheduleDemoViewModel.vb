Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Windows
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Scheduling
Imports DevExpress.Xpf.Scheduling.iCalendar
Imports Microsoft.Win32

Namespace SchedulingDemo.ViewModels

    Public Class EmployeeWorkScheduleDemoViewModel

        Public Overridable Property Start As DateTime

        Public ReadOnly Property Calendars As IEnumerable(Of SchedulingDemo.WorkCalendar)
            Get
                Return SchedulingDemo.WorkData.Calendars
            End Get
        End Property

        Public ReadOnly Property Appointments As IEnumerable(Of SchedulingDemo.WorkAppointment)
            Get
                Return SchedulingDemo.WorkData.Appointments
            End Get
        End Property

        Public Sub New()
            Me.Start = SchedulingDemo.WorkData.TodayStart
            Me.Init()
        End Sub

        Private Sub Init()
            System.Linq.Enumerable.ToList(Of SchedulingDemo.WorkCalendar)(SchedulingDemo.WorkData.Calendars).ForEach(Sub(x) x.IsVisible = False)
            System.Linq.Enumerable.ToList(Of SchedulingDemo.WorkCalendar)(SchedulingDemo.WorkData.CalendarsSupport).ForEach(Sub(x) x.IsVisible = True)
        End Sub

        Public Sub OutlookImport(ByVal scheduler As DevExpress.Xpf.Scheduling.SchedulerControl)
            Me.OutlookExchange(scheduler, SchedulingDemo.ViewModels.OutlookExchangeType.Import)
        End Sub

        Public Sub OutlookExport(ByVal scheduler As DevExpress.Xpf.Scheduling.SchedulerControl)
            Me.OutlookExchange(scheduler, SchedulingDemo.ViewModels.OutlookExchangeType.Export)
        End Sub

        Public Sub iCalendarImport(ByVal scheduler As DevExpress.Xpf.Scheduling.SchedulerControl)
            Dim importer As DevExpress.Xpf.Scheduling.iCalendar.ICalendarImporter = New DevExpress.Xpf.Scheduling.iCalendar.ICalendarImporter(scheduler)
            Using stream As System.IO.Stream = Me.OpenRead("Calendar", "iCalendar files (*.ics)|*.ics")
                If stream IsNot Nothing Then importer.Import(stream)
            End Using
        End Sub

        Public Sub iCalendarExport(ByVal scheduler As DevExpress.Xpf.Scheduling.SchedulerControl)
            Dim exporter As DevExpress.Xpf.Scheduling.iCalendar.ICalendarExporter = New DevExpress.Xpf.Scheduling.iCalendar.ICalendarExporter(scheduler)
            Using stream As System.IO.Stream = Me.OpenWrite("Calendar", "iCalendar files (*.ics)|*.ics")
                If stream IsNot Nothing Then
                    exporter.Export(stream)
                    stream.Flush()
                End If
            End Using
        End Sub

        Private Sub OutlookExchange(ByVal scheduler As DevExpress.Xpf.Scheduling.SchedulerControl, ByVal exchangeType As SchedulingDemo.ViewModels.OutlookExchangeType)
            Try
                Dim outlookCalendarPaths As String() = DevExpress.XtraScheduler.Outlook.OutlookExchangeHelper.GetOutlookCalendarPaths()
                If outlookCalendarPaths Is Nothing OrElse outlookCalendarPaths.Length = 0 Then Return
                Dim optionsWindow As SchedulingDemo.OutlookExchangeOptionsWindow = New SchedulingDemo.OutlookExchangeOptionsWindow()
                optionsWindow.DataContext = SchedulingDemo.ViewModels.OutlookExchangeOptionsWindowViewModel.Create(scheduler, exchangeType, outlookCalendarPaths)
                optionsWindow.Owner = System.Windows.Window.GetWindow(scheduler)
                optionsWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner
                optionsWindow.ShowDialog()
            Catch
                Call DevExpress.Xpf.Core.ThemedMessageBox.Show("Import from MS Outlook", System.[String].Format("Unable to {0}." & Global.Microsoft.VisualBasic.Constants.vbLf & "Check whether MS Outlook is installed.", "get the list of available calendars from Microsoft Outlook"), System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning)
            End Try
        End Sub

        Private Function OpenRead(ByVal fileName As String, ByVal filter As String) As Stream
            Dim dialog As Microsoft.Win32.OpenFileDialog = New Microsoft.Win32.OpenFileDialog() With {.FileName = fileName, .Filter = filter, .FilterIndex = 1}
            If dialog.ShowDialog() <> True Then Return Nothing
            Return dialog.OpenFile()
        End Function

        Private Function OpenWrite(ByVal fileName As String, ByVal filter As String) As Stream
            Dim dialog As Microsoft.Win32.SaveFileDialog = New Microsoft.Win32.SaveFileDialog() With {.FileName = fileName, .Filter = filter, .FilterIndex = 1}
            If dialog.ShowDialog() <> True Then Return Nothing
            Return dialog.OpenFile()
        End Function
    End Class
End Namespace
