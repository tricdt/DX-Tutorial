Imports System
Imports System.Linq
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels

    Public Partial Class OrderViewModel

        Public Overridable Property SpreadsheetDataSource As Tuple(Of IDevAVDbUnitOfWork, Order)

        Private linksViewModelField As LinksViewModel

        Protected Overrides Sub OnEntityChanged()
            MyBase.OnEntityChanged()
            If Entity IsNot Nothing Then
                SpreadsheetDataSource = New Tuple(Of IDevAVDbUnitOfWork, Order)(UnitOfWork, Entity)
                Log(String.Format("OutlookInspiredApp: Edit Order: {0}", If(String.IsNullOrEmpty(Entity.InvoiceNumber), "<New>", Entity.InvoiceNumber)))
            End If
        End Sub

        Public Overridable Sub ResetAll()
            Reset()
            CType(ParentViewModel, OrderCollectionViewModel).Refresh()
        End Sub

        Public Function CanResetAll() As Boolean
            Return CanReset()
        End Function

        Public Overrides Sub Save()
            UnitOfWork.SaveChanges()
        End Sub

        Protected Overrides Function HasValidationErrors() As Boolean
            Return MyBase.HasValidationErrors() OrElse Entity Is Nothing OrElse Entity.Customer Is Nothing
        End Function

        Public Overrides Function CanSave() As Boolean
            Return MyBase.CanSave() AndAlso UnitOfWork.HasChanges() AndAlso Entity.OrderItems.Any()
        End Function

        Public ReadOnly Property OrderCollectionViewModel As OrderCollectionViewModel
            Get
                Return GetParentViewModel(Of OrderCollectionViewModel)()
            End Get
        End Property

        Public ReadOnly Property MasterEntity As Order
            Get
                Return If(OrderCollectionViewModel IsNot Nothing, OrderCollectionViewModel.SelectedEntity, Nothing)
            End Get
        End Property

        Public Overrides Function CanDelete() As Boolean
            Return MasterEntity IsNot Nothing
        End Function

        Public Overrides Sub Delete()
            OrderCollectionViewModel.Delete(MasterEntity)
        End Sub

        Public ReadOnly Property LinksViewModel As LinksViewModel
            Get
                If linksViewModelField Is Nothing Then linksViewModelField = LinksViewModel.Create()
                Return linksViewModelField
            End Get
        End Property
    End Class
End Namespace
