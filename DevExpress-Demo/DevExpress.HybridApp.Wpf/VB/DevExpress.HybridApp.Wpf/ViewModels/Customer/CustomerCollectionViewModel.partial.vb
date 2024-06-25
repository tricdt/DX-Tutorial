Imports System
Imports System.Linq
Imports System.Collections.Generic
Imports System.Linq.Expressions
Imports DevExpress.Mvvm
Imports DevExpress.DevAV.DevAVDbDataModel

Namespace DevExpress.DevAV.ViewModels

    Partial Class CustomerCollectionViewModel
        Implements ISupportFiltering(Of Customer)

        Protected Overrides Sub OnEntitiesLoaded(ByVal unitOfWork As IDevAVDbUnitOfWork, ByVal entities As IEnumerable(Of CustomerInfoWithSales))
            MyBase.OnEntitiesLoaded(unitOfWork, entities)
            Call QueriesHelper.UpdateCustomerInfoWithSales(entities, unitOfWork.CustomerStores, unitOfWork.CustomerEmployees, unitOfWork.Orders.ActualOrders())
        End Sub

        Public Overridable Property FilterTreeViewModel As FilterTreeViewModel(Of Customer, Long)

        Public Sub CreateCustomFilter()
            Call Messenger.Default.Send(New CreateCustomFilterMessage(Of Customer)())
        End Sub

        Protected Overrides Sub OnEntitiesAssigned(ByVal getSelectedEntityCallback As Func(Of CustomerInfoWithSales))
            MyBase.OnEntitiesAssigned(getSelectedEntityCallback)
            If SelectedEntity Is Nothing Then SelectedEntity = Entities.FirstOrDefault()
        End Sub

#Region "ISupportFiltering"
        Private Property ISupportFiltering_FilterExpression As Expression(Of Func(Of Customer, Boolean)) Implements ISupportFiltering(Of Customer).FilterExpression
            Get
                Return FilterExpression
            End Get

            Set(ByVal value As Expression(Of Func(Of Customer, Boolean)))
                FilterExpression = value
            End Set
        End Property
#End Region
    End Class
End Namespace
