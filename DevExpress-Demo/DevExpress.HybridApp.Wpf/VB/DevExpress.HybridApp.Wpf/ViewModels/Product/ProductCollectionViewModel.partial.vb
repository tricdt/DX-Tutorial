Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Linq.Expressions
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.Mvvm

Namespace DevExpress.DevAV.ViewModels

    Partial Class ProductCollectionViewModel
        Implements ISupportFiltering(Of Product), IFilterTreeViewModelContainer(Of Product, Long)

        Public Overridable Property FilterTreeViewModel As FilterTreeViewModel(Of Product, Long) Implements IFilterTreeViewModelContainer(Of Product, Long).FilterTreeViewModel

        Public Sub CreateCustomFilter()
            Call Messenger.Default.Send(New CreateCustomFilterMessage(Of Product)())
        End Sub

        Protected Overrides Sub OnEntitiesLoaded(ByVal unitOfWork As IDevAVDbUnitOfWork, ByVal entities As IEnumerable(Of ProductInfoWithSales))
            MyBase.OnEntitiesLoaded(unitOfWork, entities)
            QueriesHelper.UpdateMonthlySales(unitOfWork.OrderItems, entities)
        End Sub

        Protected Overrides Sub OnEntitiesAssigned(ByVal getSelectedEntityCallback As Func(Of ProductInfoWithSales))
            MyBase.OnEntitiesAssigned(getSelectedEntityCallback)
            If SelectedEntity Is Nothing Then SelectedEntity = Entities.FirstOrDefault()
        End Sub

#Region "ISupportFiltering"
        Private Property ISupportFiltering_FilterExpression As Expression(Of Func(Of Product, Boolean)) Implements ISupportFiltering(Of Product).FilterExpression
            Get
                Return FilterExpression
            End Get

            Set(ByVal value As Expression(Of Func(Of Product, Boolean)))
                FilterExpression = value
            End Set
        End Property
#End Region
    End Class
End Namespace
