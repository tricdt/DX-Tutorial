Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Reports.UserDesigner.Extensions

Namespace GridDemo

    Public Partial Class GridDemoRibbon
        Inherits UserControl

        Public Shared ReadOnly ExportGridProperty As DependencyProperty = DependencyProperty.Register("ExportGrid", GetType(GridControl), GetType(GridDemoRibbon), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

        Public Property ExportGrid As GridControl
            Get
                Return CType(GetValue(ExportGridProperty), GridControl)
            End Get

            Set(ByVal value As GridControl)
                SetValue(ExportGridProperty, value)
            End Set
        End Property

        Public Shared ReadOnly ReportServiceProperty As DependencyProperty = DependencyProperty.Register("ReportService", GetType(GridReportManagerService), GetType(GridDemoRibbon), New PropertyMetadata(CType(Nothing, PropertyChangedCallback)))

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
