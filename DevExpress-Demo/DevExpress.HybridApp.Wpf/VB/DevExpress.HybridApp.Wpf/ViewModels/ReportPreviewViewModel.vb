Imports System.ComponentModel
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels

    Public Class ReportPreviewViewModel
        Implements IDocumentContent

        Private _DocumentOwnerProp As IDocumentOwner

        Public Shared Function Create(ByVal reportInfo As IReportInfo) As ReportPreviewViewModel
            Return ViewModelSource.Create(Function() New ReportPreviewViewModel(reportInfo))
        End Function

        Private reportInfo As IReportInfo

        Protected Sub New(ByVal reportInfo As IReportInfo)
            Me.reportInfo = reportInfo
        End Sub

        Public Sub OnLoaded()
            GetRequiredService(Of IReportService).ShowReport(reportInfo)
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

#Region "IDocumentContent"
        Private Sub OnClose(ByVal e As CancelEventArgs) Implements IDocumentContent.OnClose
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
                Return Nothing
            End Get
        End Property
#End Region
    End Class
End Namespace
