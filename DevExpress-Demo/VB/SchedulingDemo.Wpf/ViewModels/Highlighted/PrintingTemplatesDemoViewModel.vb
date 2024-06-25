Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Scheduling

Namespace SchedulingDemo.ViewModels

    <POCOViewModel(ImplementIDataErrorInfo:=True), MetadataType(GetType(PrintingTemplatesDemoViewModel.PrintingDemoViewModelMetadata))>
    Public Class PrintingTemplatesDemoViewModel

        Public Class PrintingDemoViewModelMetadata
            Implements IMetadataProvider(Of PrintingTemplatesDemoViewModel)

            Private Sub BuildMetadata(ByVal builder As MetadataBuilder(Of PrintingTemplatesDemoViewModel)) Implements IMetadataProvider(Of PrintingTemplatesDemoViewModel).BuildMetadata
                builder.Property(Function(x) x.PrintStart).MatchesInstanceRule(Function(v, x) v < x.PrintEnd)
                builder.Property(Function(x) x.PrintEnd).MatchesInstanceRule(Function(v, x) v > x.PrintStart)
            End Sub
        End Class

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

        Public Overridable ReadOnly Property SchedulerReportService As ISchedulerReportService
            Get
                Return Nothing
            End Get
        End Property

        Public Overridable ReadOnly Property DispatcherService As IDispatcherService
            Get
                Return Nothing
            End Get
        End Property

        Public Overridable Property IsPreviewPageVisible As Boolean

        Public Overridable Property ReportTemplate As ReportTemplate

        Public Overridable Property PrintVisibleInterval As Boolean

        Public Overridable Property PrintVisibleResources As Boolean

        Public Overridable Property PrintStart As Date

        Public Overridable Property PrintEnd As Date

        Public ReadOnly Property PrintInterval As DateTimeRange
            Get
                Return If(PrintVisibleInterval, DateTimeRange.Empty, New DateTimeRange(PrintStart, PrintEnd))
            End Get
        End Property

        Public ReadOnly Property PrintResources As IEnumerable(Of Object)
            Get
                Return If(PrintVisibleResources, Nothing, WorkData.Calendars.Where(Function(x) CBool(x.Tag)).Cast(Of Object)())
            End Get
        End Property

        Public Overridable Property Report As Object

        Public Overridable Property DailyReportShowCalendar As Boolean

        Public Overridable Property DailyReportShowInterval As Boolean

        Public Overridable Property DailyReportDaysPerPage As Integer

        Public Overridable Property DailyReportResourcesPerPage As Integer

        Public Overridable Property WeeklyReportShowCalendar As Boolean

        Public Overridable Property WeeklyReportShowInterval As Boolean

        Public Overridable Property WeeklyReportResourcesPerPage As Integer

        Public Overridable Property MonthlyReportShowCalendar As Boolean

        Public Overridable Property MonthlyReportShowInterval As Boolean

        Public Overridable Property MonthlyReportResourcesPerPage As Integer

        Public Overridable Property TimelineReportShowCalendar As Boolean

        Public Overridable Property TimelineReportShowInterval As Boolean

        Public Overridable Property TimelineReportResourcesPerPage As Integer

        Public Overridable Property TimelineReportIntervalsPerPage As Integer

        Public Overridable Property TrifoldReportShowCalendar As Boolean

        Public Sub New()
            Start = TodayStart
            PrintVisibleInterval = True
            PrintVisibleResources = True
            PrintStart = Start
            PrintEnd = Start.AddDays(1)
            ReportTemplate = ReportTemplate.DailyStyle
            Enumerable.ToList(WorkData.Calendars).ForEach(Sub(x)
                x.IsVisible = False
                x.Tag = False
            End Sub)
            CalendarMark.IsVisible = True
            CalendarMark.Tag = True
            DailyReportShowCalendar = True
            DailyReportShowInterval = True
            DailyReportDaysPerPage = 0
            DailyReportResourcesPerPage = 0
            WeeklyReportShowCalendar = True
            WeeklyReportShowInterval = True
            WeeklyReportResourcesPerPage = 0
            MonthlyReportShowCalendar = True
            MonthlyReportShowInterval = True
            MonthlyReportResourcesPerPage = 0
            TimelineReportShowCalendar = True
            TimelineReportShowInterval = True
            TimelineReportResourcesPerPage = 0
            TimelineReportIntervalsPerPage = 0
            TrifoldReportShowCalendar = False
            AddHandler CType(Me, INotifyPropertyChanged).PropertyChanged, AddressOf OnPropertyChanged
        End Sub

        Public Sub OnLoaded()
            IsPreviewPageVisible = True
        End Sub

        Public Sub PreviewInNewWindow()
            SchedulerReportService.ShowPrintPreview(ReportTemplate)
        End Sub

        Public Sub UpdateReport()
            If SchedulerReportService Is Nothing Then Return
            RaisePropertyChanged(Function(x) x.PrintInterval)
            RaisePropertyChanged(Function(x) x.PrintResources)
            DispatcherService.BeginInvoke(New Action(Sub() Report = SchedulerReportService.CreateReport(ReportTemplate)))
        End Sub

        Protected Sub OnIsPreviewPageVisibleChanged()
            If IsPreviewPageVisible Then UpdateReport()
        End Sub

        Private Sub OnPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
            If Equals(e.PropertyName, BindableBase.GetPropertyName(Function() DailyReportShowCalendar)) OrElse Equals(e.PropertyName, BindableBase.GetPropertyName(Function() DailyReportShowInterval)) OrElse Equals(e.PropertyName, BindableBase.GetPropertyName(Function() DailyReportDaysPerPage)) OrElse Equals(e.PropertyName, BindableBase.GetPropertyName(Function() DailyReportResourcesPerPage)) OrElse Equals(e.PropertyName, BindableBase.GetPropertyName(Function() WeeklyReportShowCalendar)) OrElse Equals(e.PropertyName, BindableBase.GetPropertyName(Function() WeeklyReportShowInterval)) OrElse Equals(e.PropertyName, BindableBase.GetPropertyName(Function() WeeklyReportResourcesPerPage)) OrElse Equals(e.PropertyName, BindableBase.GetPropertyName(Function() MonthlyReportShowCalendar)) OrElse Equals(e.PropertyName, BindableBase.GetPropertyName(Function() MonthlyReportShowInterval)) OrElse Equals(e.PropertyName, BindableBase.GetPropertyName(Function() MonthlyReportResourcesPerPage)) OrElse Equals(e.PropertyName, BindableBase.GetPropertyName(Function() TimelineReportShowCalendar)) OrElse Equals(e.PropertyName, BindableBase.GetPropertyName(Function() TimelineReportShowInterval)) OrElse Equals(e.PropertyName, BindableBase.GetPropertyName(Function() TimelineReportResourcesPerPage)) OrElse Equals(e.PropertyName, BindableBase.GetPropertyName(Function() TimelineReportIntervalsPerPage)) OrElse Equals(e.PropertyName, BindableBase.GetPropertyName(Function() TrifoldReportShowCalendar)) OrElse Equals(e.PropertyName, BindableBase.GetPropertyName(Function() PrintStart)) OrElse Equals(e.PropertyName, BindableBase.GetPropertyName(Function() PrintEnd)) OrElse Equals(e.PropertyName, BindableBase.GetPropertyName(Function() PrintVisibleInterval)) OrElse Equals(e.PropertyName, BindableBase.GetPropertyName(Function() PrintVisibleResources)) OrElse Equals(e.PropertyName, BindableBase.GetPropertyName(Function() ReportTemplate)) Then UpdateReport()
        End Sub
    End Class
End Namespace
