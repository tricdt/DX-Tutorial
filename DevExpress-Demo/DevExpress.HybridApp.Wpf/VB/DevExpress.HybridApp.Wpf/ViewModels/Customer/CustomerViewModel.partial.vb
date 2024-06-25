Imports System.Collections.Generic
Imports System.Linq

Namespace DevExpress.DevAV.ViewModels

    Partial Class CustomerViewModel

        Public Overridable Property SelectedItem As Object

        Protected Overrides Sub OnEntityChanged()
            MyBase.OnEntityChanged()
            Log("HybridApp: View Customer")
        End Sub

        Public Function UpdateMapItems(ByVal start As Date, ByVal [end] As Date) As IEnumerable(Of Object)
            Dim mapItems As IEnumerable(Of Object)
            Dim items = Entity.Orders.Where(Function(x) x.OrderDate >= start AndAlso x.OrderDate <= [end]).GroupBy(Function(order) order.Store)
            If items.Count() > 0 Then
                Dim minimumSalesValue As Decimal = items.Min(Function(orders) orders.CustomSum(Function(order) order.TotalAmount))
                Dim maximumSalesValue As Decimal = items.Max(Function(orders) orders.CustomSum(Function(order) order.TotalAmount))
                Dim dif As Decimal = maximumSalesValue - minimumSalesValue
                mapItems = items.[Select](Function(group) New With {group.Key.Address, .TotalSales = group.CustomSum(Function(order) order.TotalAmount), .TotalOpportunities = QueriesHelper.GetQuotesTotal(UnitOfWork.Quotes, group.Key, start, [end]), .AbsSize = If(dif > 0, CDbl((group.CustomSum(Function(order) order.TotalAmount) - minimumSalesValue) / dif), 1.0)})
            Else
                mapItems = Enumerable.Empty(Of Object)()
            End If

            SelectedItem = mapItems.LastOrDefault()
            Return mapItems
        End Function

        Protected Overrides Function GetTitle() As String
            Return String.Format("Customer {0}", Entity.Name)
        End Function
    End Class
End Namespace
