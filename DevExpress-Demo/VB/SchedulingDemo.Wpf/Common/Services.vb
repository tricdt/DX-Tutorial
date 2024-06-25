Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Scheduling
Imports DevExpress.XtraScheduler.Reporting
Imports System.Windows

Namespace SchedulingDemo

    Public Interface ISchedulerReportService

        Function CreateReport(ByVal template As ReportTemplate) As ISchedulerReport

        Sub ShowPrintPreview(ByVal template As ReportTemplate)

    End Interface

    Public Class SchedulerReportService
        Inherits ServiceBase
        Implements ISchedulerReportService

        Public Shared ReadOnly SchedulerProperty As DependencyProperty = DependencyProperty.Register("Scheduler", GetType(SchedulerControl), GetType(SchedulerReportService), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property Scheduler As SchedulerControl
            Get
                Return CType(GetValue(SchedulerProperty), SchedulerControl)
            End Get

            Set(ByVal value As SchedulerControl)
                SetValue(SchedulerProperty, value)
            End Set
        End Property

        Private Function CreateReport(ByVal template As ReportTemplate) As ISchedulerReport Implements ISchedulerReportService.CreateReport
            Dim res = Scheduler.CreateReport(template)
            res.CreateDocument()
            Return res
        End Function

        Private Sub ShowPrintPreview(ByVal template As ReportTemplate) Implements ISchedulerReportService.ShowPrintPreview
            Scheduler.ShowPrintPreview(template)
        End Sub
    End Class
End Namespace
