Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Scheduling
Imports DevExpress.Xpf.Scheduling.OutlookExchange

Namespace SchedulingDemo.ViewModels

    Public Enum OutlookExchangeType
        Import
        Export
    End Enum

    Public Class OutlookExchangeOptionsWindowViewModel

        Public Shared Function Create(ByVal scheduler As SchedulerControl, ByVal type As OutlookExchangeType, ByVal outlookCalendarPaths As String()) As OutlookExchangeOptionsWindowViewModel
            Return ViewModelSource.Create(Function() New OutlookExchangeOptionsWindowViewModel(scheduler, type, outlookCalendarPaths))
        End Function

        Private ReadOnly scheduler As SchedulerControl

        Private ReadOnly type As OutlookExchangeType

        Protected Sub New(ByVal scheduler As SchedulerControl, ByVal type As OutlookExchangeType, ByVal outlookCalendarPaths As String())
            Me.scheduler = scheduler
            Me.type = type
            Me.OutlookCalendarPaths = outlookCalendarPaths
            ProgressBarValue = 0
            Title = If(type = OutlookExchangeType.Import, "Outlook Import Options", "Outlook Export Options")
            ActionCaption = If(type = OutlookExchangeType.Import, "Cancel import for appointments:", "Cancel export for appointments:")
        End Sub

        Public Overridable Property Title As String

        Public Overridable Property ActionCaption As String

        Public Overridable Property OutlookCalendarPaths As String()

        Public Overridable Property OutlookCalendarPath As String

        Public Overridable Property UsedAppointmentType As DevExpress.XtraScheduler.UsedAppointmentType

        Public Overridable Property MaxProgressBarValue As Double

        Public Overridable Property ProgressBarValue As Double

        Private ReadOnly Property IsCancelForRecurring As Boolean
            Get
                Return Equals(UsedAppointmentType, DevExpress.XtraScheduler.UsedAppointmentType.Recurring)
            End Get
        End Property

        Private ReadOnly Property IsCancelForNonRecurring As Boolean
            Get
                Return Equals(UsedAppointmentType, DevExpress.XtraScheduler.UsedAppointmentType.NonRecurring)
            End Get
        End Property

        Public Sub Exchange()
            If type = OutlookExchangeType.Import Then
                Import()
            Else
                Export()
            End If
        End Sub

        Private Sub Import()
            Dim importer As OutlookImporter = New OutlookImporter(scheduler)
            importer.CalendarFolderName = OutlookCalendarPath
            AddHandler importer.AppointmentItemSynchronizing, AddressOf OnAppointmentItemSynchronizing
            ProgressBarValue = 0
            MaxProgressBarValue = importer.SourceObjectCount
            Try
                importer.Import()
            Finally
                ProgressBarValue = 0
            End Try
        End Sub

        Private Sub Export()
            Dim exporter As OutlookExporter = New OutlookExporter(scheduler)
            exporter.CalendarFolderName = OutlookCalendarPath
            AddHandler exporter.AppointmentItemSynchronizing, AddressOf OnAppointmentItemSynchronizing
            ProgressBarValue = 0
            MaxProgressBarValue = exporter.SourceObjectCount
            Try
                exporter.Export()
            Finally
                ProgressBarValue = 0
            End Try
        End Sub

        Private Sub OnAppointmentItemSynchronizing(ByVal sender As Object, ByVal args As AppointmentItemSynchronizingEventArgs)
            ProgressBarValue += 1
            If args.Appointment Is Nothing Then Return
            Dim cancel As Boolean = If(args.Appointment.IsRecurring, IsCancelForRecurring, IsCancelForNonRecurring)
            If cancel Then args.Cancel = True
        End Sub
    End Class
End Namespace
