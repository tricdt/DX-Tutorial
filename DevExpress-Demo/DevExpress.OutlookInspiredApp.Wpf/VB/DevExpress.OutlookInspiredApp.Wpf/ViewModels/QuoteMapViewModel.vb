Imports System
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Collections.Generic
Imports DevExpress.DevAV
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.DevAV.Common
Imports DevExpress.Mvvm.DataModel

Namespace DevExpress.DevAV.ViewModels

    Public Class QuoteMapViewModel
        Inherits CollectionViewModel(Of Quote, Long, IDevAVDbUnitOfWork)

        Private _Quotes As QuoteCollectionViewModel

        Public Property Quotes As QuoteCollectionViewModel
            Get
                Return _Quotes
            End Get

            Private Set(ByVal value As QuoteCollectionViewModel)
                _Quotes = value
            End Set
        End Property

        Public Overridable Property GrouppedMapItems As IEnumerable(Of Object)

        Public Overridable Property Stage As Stage

        Public Overridable Property SelectedItem As Object

        Public Overridable Property IsHighStage As Boolean

        Public Overridable Property IsMediumStage As Boolean

        Public Overridable Property IsLowStage As Boolean

        Public Overridable Property IsUnlikelyStage As Boolean

        Public ReadOnly Property LinksViewModel As LinksViewModel
            Get
                If linksViewModelField Is Nothing Then linksViewModelField = LinksViewModel.Create()
                Return linksViewModelField
            End Get
        End Property

        Public Sub New()
            MyBase.New(GetUnitOfWorkFactory(), Function(x) x.Quotes)
            IsHighStage = True
            UpdateMapItems()
        End Sub

        Protected Overrides Sub OnFilterExpressionChanged()
            MyBase.OnFilterExpressionChanged()
            UpdateMapItems()
        End Sub

        Protected Overridable Sub OnStageChanged()
            IsHighStage = Equals(Stage, Stage.High)
            IsMediumStage = Equals(Stage, Stage.Medium)
            IsLowStage = Equals(Stage, Stage.Low)
            IsUnlikelyStage = Equals(Stage, Stage.Unlikely)
            UpdateMapItems()
        End Sub

        Protected Overridable Sub OnIsHighStageChanged()
            If IsHighStage Then Stage = Stage.High
        End Sub

        Protected Overridable Sub OnIsMediumStageChanged()
            If IsMediumStage Then Stage = Stage.Medium
        End Sub

        Protected Overridable Sub OnIsLowStageChanged()
            If IsLowStage Then Stage = Stage.Low
        End Sub

        Protected Overridable Sub OnIsUnlikelyStageChanged()
            If IsUnlikelyStage Then Stage = Stage.Unlikely
        End Sub

        Private Sub UpdateMapItems()
            Dim items = GetOpportunities(Stage, FilterExpression).GroupBy(Function(item) item.Address, New AddressComparer())
            If items.Count() > 0 Then
                Dim minValue As Decimal = items.Min(Function(group) group.CustomSum(Function(quote) quote.Value))
                Dim maxValue As Decimal = items.Max(Function(group) group.CustomSum(Function(quote) quote.Value))
                Dim dif As Decimal = maxValue - minValue
                If dif < CDec(0.01) Then dif = 1
                GrouppedMapItems = items.[Select](Function(group) New With {.Address = group.Key, .Total = group.CustomSum(Function(item) item.Value), .AbsSize = CDbl((group.CustomSum(Function(item) item.Value) - minValue) / dif)})
            Else
                GrouppedMapItems = Enumerable.Empty(Of Object)()
            End If

            SelectedItem = GrouppedMapItems.LastOrDefault()
        End Sub

        Public Function GetOpportunities(ByVal stage As Stage, ByVal Optional filterExpression As Expression(Of Func(Of Quote, Boolean)) = Nothing) As IList(Of QuoteMapItem)
            Dim unitOfWork = CreateUnitOfWork()
            Dim quotes = ReadOnlyRepositoryExtensions.GetFilteredEntities(unitOfWork.Quotes, filterExpression).ActualQuotes()
            Dim customers = unitOfWork.Customers
            Return QueriesHelper.GetOpportunities(quotes, customers, stage).ToList()
        End Function

        Private linksViewModelField As LinksViewModel
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
