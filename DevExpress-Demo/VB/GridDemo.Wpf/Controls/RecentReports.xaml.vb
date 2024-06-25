Imports DevExpress.Xpf.DemoBase.Helpers
Imports DevExpress.Xpf.Reports.UserDesigner.Extensions
Imports DevExpress.XtraExport.Helpers
Imports System.Windows
Imports System.Windows.Controls

Namespace GridDemo

    Public Partial Class RecentReports
        Inherits UserControl

        Public Shared ReadOnly ReportServiceProperty As DependencyProperty = DependencyProperty.Register("ReportService", GetType(GridReportManagerService), GetType(RecentReports), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property ReportService As GridReportManagerService
            Get
                Return CType(GetValue(ReportServiceProperty), GridReportManagerService)
            End Get

            Set(ByVal value As GridReportManagerService)
                SetValue(ReportServiceProperty, value)
            End Set
        End Property

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class
End Namespace
