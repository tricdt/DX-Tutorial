Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Linq.Expressions
Imports DevExpress.DevAV
Imports DevExpress.Mvvm.DataModel

Namespace DevExpress.DevAV.ViewModels

    Partial Class QuoteCollectionViewModel

        Const NumberOfAverageQuotes As Integer = 300

        Protected Overrides Sub OnInitializeInRuntime()
            MyBase.OnInitializeInRuntime()
            UpdateAverageQuotes()
        End Sub

        Public Overridable Property AverageQuotes As List(Of Quote)

        Public Overridable Property SelectedMapItem As Object

        Public Function GetOpportunitiesInfo(ByVal start As Date, ByVal [end] As Date) As IList(Of QuoteSummaryItem)
            Return QueriesHelper.GetSummaryOpportunities(CreateReadOnlyRepository().GetFilteredEntities(Nothing).Where(Function(x) x.Date >= start AndAlso x.Date <= [end])).ToList()
        End Function

        Protected Overrides Sub OnIsLoadingChanged()
            MyBase.OnIsLoadingChanged()
            If Not IsLoading Then
                UpdateAverageQuotes()
            End If
        End Sub

        Private Sub UpdateAverageQuotes()
            AverageQuotes = QueriesHelper.GetAverageQuotes(CreateReadOnlyRepository().GetFilteredEntities(Nothing), NumberOfAverageQuotes)
        End Sub

        Public Function UpdateMapItems(ByVal start As Date, ByVal [end] As Date) As IEnumerable(Of Object)
            Dim mapItems As IEnumerable(Of Object)
            Dim items = GetOpportunities(Stage.Summary, Function(x) x.Date > start AndAlso x.Date < [end]).GroupBy(Function(item) item.Address, New AddressComparer())
            If items.Count() > 0 Then
                Dim minValue As Decimal = items.Min(Function(group) group.CustomSum(Function(quote) quote.Value))
                Dim maxValue As Decimal = items.Max(Function(group) group.CustomSum(Function(quote) quote.Value))
                Dim dif As Decimal = If((maxValue - minValue > CDec(0.01)), maxValue - minValue, 1)
                mapItems = items.[Select](Function(group) New With {.Address = group.Key, .Total = group.CustomSum(Function(item) item.Value), .AbsSize = CDbl((group.CustomSum(Function(item) item.Value) - minValue) / dif)})
            Else
                mapItems = Enumerable.Empty(Of Object)()
            End If

            SelectedMapItem = mapItems.LastOrDefault()
            Return mapItems
        End Function

        Private Function GetOpportunities(ByVal stage As Stage, ByVal Optional filterExpression As Expression(Of Func(Of Quote, Boolean)) = Nothing) As IList(Of QuoteMapItem)
            Dim unitOfWork = UnitOfWorkFactory.CreateUnitOfWork()
            Dim quotes = ReadOnlyRepositoryExtensions.GetFilteredEntities(unitOfWork.Quotes, filterExpression).ActualQuotes()
            Return QueriesHelper.GetOpportunities(quotes, unitOfWork.Customers, stage).ToList()
        End Function
    End Class

    Friend Class AddressComparer
        Implements IEqualityComparer(Of Address)

        Public Overloads Function Equals(ByVal x As Address, ByVal y As Address) As Boolean Implements IEqualityComparer(Of Address).Equals
            Return x.State.Equals(y.State) AndAlso x.City.Equals(y.City)
        End Function

        Public Overloads Function GetHashCode(ByVal obj As Address) As Integer Implements IEqualityComparer(Of Address).GetHashCode
            Return obj.State.GetHashCode() Xor obj.City.GetHashCode()
        End Function
    End Class
End Namespace
