Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.DevAV
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels

    Public Class CustomerStatisticsViewModel
        Inherits StatisticsViewModelBase

        Public Shared Function Create() As CustomerStatisticsViewModel
            Return ViewModelSource.Create(Function() New CustomerStatisticsViewModel())
        End Function

        Protected Sub New()
            MyBase.New()
        End Sub

        Protected Overrides Function GetActualStatistics(ByVal unitOfWork As IDevAVDbUnitOfWork) As IEnumerable(Of SalesViewModel)
            Return QueriesHelper.GetSaleMapItemsByCustomerAndCity(unitOfWork.OrderItems, EntityId, SelectedAddress.Address.City, FilterType).GroupBy(Function(mi) mi.Product).[Select](Function(gr) New SalesViewModel(gr.Key.Name, gr.CustomSum(Function(mi) mi.Total)))
        End Function
    End Class
End Namespace
