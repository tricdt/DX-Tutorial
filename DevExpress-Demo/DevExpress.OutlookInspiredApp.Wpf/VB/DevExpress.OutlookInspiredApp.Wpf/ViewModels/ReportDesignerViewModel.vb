Imports DevExpress.Mvvm.POCO
Imports DevExpress.XtraReports.UI

Namespace DevExpress.DevAV.ViewModels

    Public Class ReportDesignerViewModel

        Private _Report As XtraReport, _IsReportSaved As Boolean

        Public Shared Function Create(ByVal report As XtraReport) As ReportDesignerViewModel
            Return ViewModelSource.Create(Function() New ReportDesignerViewModel(report))
        End Function

        Protected Sub New(ByVal report As XtraReport)
            Me.Report = report
        End Sub

        Public Property Report As XtraReport
            Get
                Return _Report
            End Get

            Private Set(ByVal value As XtraReport)
                _Report = value
            End Set
        End Property

        Public Property IsReportSaved As Boolean
            Get
                Return _IsReportSaved
            End Get

            Private Set(ByVal value As Boolean)
                _IsReportSaved = value
            End Set
        End Property

        Public Overridable Sub Save()
            IsReportSaved = True
        End Sub
    End Class
End Namespace
