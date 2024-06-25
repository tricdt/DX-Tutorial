Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.IO
Imports DevExpress.DevAV.Reports
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.XtraReports.UI

Namespace DevExpress.DevAV.ViewModels

    Public Class OrderRevenueViewModel
        Implements IDocumentContent

        Private _DocumentOwnerProp As IDocumentOwner

        Public Shared Function Create(ByVal selectedItems As IEnumerable(Of OrderItem), ByVal format As RevenueReportFormat) As OrderRevenueViewModel
            Return ViewModelSource.Create(Function() New OrderRevenueViewModel(selectedItems, format))
        End Function

        Protected Sub New(ByVal selectedItems As IEnumerable(Of OrderItem), ByVal format As RevenueReportFormat)
            Me.Format = format
            Me.SelectedItems = selectedItems
        End Sub

        Public Sub Close()
            If DocumentOwnerProp IsNot Nothing Then DocumentOwnerProp.Close(Me)
        End Sub

        Protected ReadOnly Property DocumentManagerService As IDocumentManagerService
            Get
                Return GetService(Of IDocumentManagerService)()
            End Get
        End Property

        Protected Property DocumentOwnerProp As IDocumentOwner
            Get
                Return _DocumentOwnerProp
            End Get

            Private Set(ByVal value As IDocumentOwner)
                _DocumentOwnerProp = value
            End Set
        End Property

        Public Property Format As RevenueReportFormat

        Public Property SelectedItems As IEnumerable(Of OrderItem)

        Public Overridable Property Report As XtraReport

        Public Overridable Sub OnLoaded()
            Call Log(String.Format("OutlookInspiredApp: Create Report : Sales: Revenue{0}", Format.ToString()))
            Report = CreateReport()
            Report.CreateDocument()
        End Sub

        Public Sub ShowDesigner()
            Dim viewModel = ReportDesignerViewModel.Create(CloneReport(Report))
            Dim doc = DocumentManagerService.CreateDocument("ReportDesignerView", viewModel, Nothing, Me)
            doc.Title = CreateTitle()
            doc.Show()
            If viewModel.IsReportSaved Then
                Report = CloneReport(viewModel.Report)
                Report.CreateDocument()
                viewModel.Report.Dispose()
            End If
        End Sub

        Private Function CloneReport(ByVal report As XtraReport) As XtraReport
            Dim clonedReport = CloneReportLayout(report)
            InitReport(clonedReport)
            Return clonedReport
        End Function

        Private Sub InitReport(ByVal report As XtraReport)
            report.DataSource = SelectedItems
            report.Parameters("paramOrderDate").Value = True
        End Sub

        Private Function CreateReport() As XtraReport
            Return If(Format = RevenueReportFormat.Summary, ReportFactory.SalesRevenueReport(SelectedItems, True), ReportFactory.SalesRevenueAnalysisReport(SelectedItems, True))
        End Function

        Private Function CreateTitle() As String
            Return String.Format("DevAV - {0}", If(Format = RevenueReportFormat.Analysis, "Revenue Analysis", "Revenue"))
        End Function

        Private Shared Function CloneReportLayout(ByVal report As XtraReport) As XtraReport
            Using stream = New MemoryStream()
                report.SaveLayoutToXml(stream)
                stream.Position = 0
                Return XtraReport.FromStream(stream, True)
            End Using
        End Function

#Region "IDocumentContent"
        Private Sub OnClose(ByVal e As CancelEventArgs) Implements IDocumentContent.OnClose
            Report.Dispose()
        End Sub

        Private Sub OnDestroy() Implements IDocumentContent.OnDestroy
        End Sub

        Private Property DocumentOwner As IDocumentOwner Implements IDocumentContent.DocumentOwner
            Get
                Return DocumentOwnerProp
            End Get

            Set(ByVal value As IDocumentOwner)
                DocumentOwnerProp = value
            End Set
        End Property

        Private ReadOnly Property Title As Object Implements IDocumentContent.Title
            Get
                Return CreateTitle()
            End Get
        End Property
#End Region
    End Class

    Public Enum RevenueReportFormat
        Summary
        Analysis
    End Enum
End Namespace
