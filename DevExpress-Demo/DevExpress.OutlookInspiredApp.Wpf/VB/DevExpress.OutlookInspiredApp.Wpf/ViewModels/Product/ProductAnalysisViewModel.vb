Imports System.Collections.Generic
Imports System.ComponentModel
Imports DevExpress.Mvvm
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels

    Public Class ProductAnalysisViewModel
        Implements IDocumentContent

        Private _DocumentOwnerProp As IDocumentOwner

        Private unitOfWork As IDevAVDbUnitOfWork

        Public Shared Function Create() As ProductAnalysisViewModel
            Return ViewModelSource.Create(Function() New ProductAnalysisViewModel())
        End Function

        Protected Sub New()
            unitOfWork = GetUnitOfWorkFactory().CreateUnitOfWork()
        End Sub

        Public Function GetFinancialReport() As IEnumerable(Of ProductsAnalysis.Item)
            Return unitOfWork.GetFinancialReport
        End Function

        Public Function GetFinancialData() As IEnumerable(Of ProductsAnalysis.Item)
            Return unitOfWork.GetFinancialData
        End Function

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
                Return "DevAV - Sales Analysis"
            End Get
        End Property
#End Region
    End Class
End Namespace
