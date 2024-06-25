Imports System.Collections.Generic
Imports System.ComponentModel
Imports DevExpress.DevAV.ViewModels
Imports DevExpress.Mvvm
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels

    Public Class CustomerAnalysisViewModel
        Implements IDocumentContent

        Private _DocumentOwnerProp As IDocumentOwner

        Private unitOfWork As IDevAVDbUnitOfWork

        Public Shared Function Create() As CustomerAnalysisViewModel
            Return ViewModelSource.Create(Function() New CustomerAnalysisViewModel())
        End Function

        Protected Sub New()
            unitOfWork = GetUnitOfWorkFactory().CreateUnitOfWork()
        End Sub

        Public Function GetSalesReport() As IEnumerable(Of CustomersAnalysis.Item)
            Return unitOfWork.GetSalesReport
        End Function

        Public Function GetSalesData() As IEnumerable(Of CustomersAnalysis.Item)
            Return unitOfWork.GetSalesData
        End Function

        Public Function GetStates(ByVal states As IEnumerable(Of StateEnum)) As IEnumerable(Of String)
            Return QueriesHelper.GetStateNames(unitOfWork.States, states)
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
