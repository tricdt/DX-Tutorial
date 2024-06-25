Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.DevAV.Reports
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports System
Imports System.ComponentModel
Imports System.IO

Namespace DevExpress.DevAV.ViewModels

    Public Class OrderQuickReportsViewModel
        Implements IDocumentContent

        Private _DocumentOwnerProp As IDocumentOwner

        Public Shared Function Create(ByVal selectedOrder As Order, ByVal format As ReportFormat) As OrderQuickReportsViewModel
            Return ViewModelSource.Create(Function() New OrderQuickReportsViewModel(selectedOrder, format))
        End Function

        Protected Sub New(ByVal selectedOrder As Order, ByVal format As ReportFormat)
            Me.Format = format
            Me.SelectedOrder = selectedOrder
        End Sub

        Public Sub Close()
            If DocumentOwnerProp IsNot Nothing Then DocumentOwnerProp.Close(Me)
        End Sub

        Protected Property DocumentOwnerProp As IDocumentOwner
            Get
                Return _DocumentOwnerProp
            End Get

            Private Set(ByVal value As IDocumentOwner)
                _DocumentOwnerProp = value
            End Set
        End Property

        Public Overridable Property SelectedOrder As Order

        Public Property Format As ReportFormat

        Public Overridable Property DocumentStream As Stream

        Public Overridable Property DocumentDataSource As Tuple(Of IDevAVDbUnitOfWork, Order)

        Public Overridable Sub OnLoaded()
            Dim documentStream = New MemoryStream()
            Dim report = ReportFactory.SalesInvoice(SelectedOrder, True, False, False, False)
            Select Case Format
                Case ReportFormat.Pdf
                    report.ExportToPdf(documentStream)
                Case ReportFormat.Xls
                    report.ExportToXls(documentStream)
                Case ReportFormat.Doc
                    Dim options = New XtraPrinting.DocxExportOptions()
                    options.ExportMode = XtraPrinting.DocxExportMode.SingleFilePageByPage
                    options.TableLayout = True
                    report.ExportToDocx(documentStream, options)
            End Select

            DocumentDataSource = New Tuple(Of IDevAVDbUnitOfWork, Order)(Nothing, SelectedOrder)
            Me.DocumentStream = documentStream
        End Sub

#Region "IDocumentContent"
        Private Sub OnClose(ByVal e As CancelEventArgs) Implements IDocumentContent.OnClose
            DocumentStream.Dispose()
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
                Return String.Format("Order - {0}", SelectedOrder.InvoiceNumber)
            End Get
        End Property
#End Region
    End Class

    Public Enum ReportFormat
        Pdf
        Xls
        Doc
    End Enum
End Namespace
