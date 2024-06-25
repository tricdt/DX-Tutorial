Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.Linq
Imports DevExpress.DevAV.DevAVDbDataModel

Namespace DevExpress.DevAV.ViewModels

    Public Partial Class DashboardViewModel

        Private unitOfWork As IDevAVDbUnitOfWork = GetUnitOfWorkFactory().CreateUnitOfWork()

        Public Sub New()
            DashboardInitialization()
        End Sub

        Public Overridable Property DashboardOrders As IList(Of OrderInfo)

        Public Overridable Property SummaryOpportunities As IList(Of QuoteSummaryItem)

        Public Overridable Property SalesSummarySelectedItem As IList(Of SalesSummaryItem)

        Public Overridable Property CostSelectedItem As IList(Of CostAverageItem)

        Public Overridable Property GoodSoldPeriodSelector As ObservableCollection(Of Boolean)

        Public Overridable Property RevenuePeriodSelector As ObservableCollection(Of Boolean)

        Private salesSummaryItems As List(Of IEnumerable(Of SalesSummaryItem))

        Private costAverageItems As List(Of IEnumerable(Of CostAverageItem))

        Private Sub GoodSoldSelectorChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
            If CBool(e.NewItems(0)) Then CostSelectedItem = costAverageItems(e.NewStartingIndex).ToList()
            SelectorReset(GoodSoldPeriodSelector, e)
        End Sub

        Private Sub RevenuesSelectorChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
            If CBool(e.NewItems(0)) Then SalesSummarySelectedItem = salesSummaryItems(e.NewStartingIndex).ToList()
            SelectorReset(RevenuePeriodSelector, e)
        End Sub

        Private Function GetSalesSummaryItems() As List(Of IEnumerable(Of SalesSummaryItem))
            Return New List(Of IEnumerable(Of SalesSummaryItem))() From {QueriesHelper.GetSalesSummaryItems(unitOfWork.OrderItems, Period.ThisYear), QueriesHelper.GetSalesSummaryItems(unitOfWork.OrderItems, Period.ThisMonth), QueriesHelper.GetSalesSummaryItems(unitOfWork.OrderItems, Period.FixedDate, LastOrderDate(unitOfWork))}
        End Function

        Private Function GetCostAverageItems() As List(Of IEnumerable(Of CostAverageItem))
            Return New List(Of IEnumerable(Of CostAverageItem))() From {QueriesHelper.GetCostAverageItems(unitOfWork.OrderItems, Period.ThisYear), QueriesHelper.GetCostAverageItems(unitOfWork.OrderItems, Period.ThisMonth), QueriesHelper.GetCostAverageItems(unitOfWork.OrderItems, Period.FixedDate, LastOrderDate(unitOfWork))}
        End Function

        Private Shared Function LastOrderDate(ByVal unitOfWork As IDevAVDbUnitOfWork) As Date
            Return unitOfWork.OrderItems.Where(Function(x) x.Order.OrderDate <= Date.Today).Max(Function(x) x.Order.OrderDate)
        End Function

        Private Sub DashboardInitialization()
            SummaryOpportunities = QueriesHelper.GetSummaryOpportunities(unitOfWork.Quotes).ToList()
            GoodSoldPeriodSelector = New ObservableCollection(Of Boolean)() From {True, False, False}
            RevenuePeriodSelector = New ObservableCollection(Of Boolean)() From {True, False, False}
            AddHandler GoodSoldPeriodSelector.CollectionChanged, AddressOf GoodSoldSelectorChanged
            AddHandler RevenuePeriodSelector.CollectionChanged, AddressOf RevenuesSelectorChanged
            salesSummaryItems = GetSalesSummaryItems()
            costAverageItems = GetCostAverageItems()
            DashboardOrders = QueriesHelper.GetOrderInfo(unitOfWork.Orders)
            SalesSummarySelectedItem = salesSummaryItems(0).ToList()
            CostSelectedItem = costAverageItems(0).ToList()
        End Sub

        Private Sub SelectorReset(ByVal collection As ObservableCollection(Of Boolean), ByVal e As NotifyCollectionChangedEventArgs)
            If CBool(e.NewItems(0)) Then
                For i As Integer = 0 To collection.Count - 1
                    If i <> e.NewStartingIndex AndAlso collection(i) Then collection(i) = False
                Next
            ElseIf Not collection.Contains(True) Then
                collection(e.NewStartingIndex) = True
            End If
        End Sub
    End Class
End Namespace
