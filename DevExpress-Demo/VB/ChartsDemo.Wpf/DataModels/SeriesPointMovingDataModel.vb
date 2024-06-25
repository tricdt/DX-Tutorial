Imports System
Imports System.ComponentModel
Imports System.Globalization
Imports System.Linq

Namespace ChartsDemo

    Public Class DraggableItem
        Implements INotifyPropertyChanged

        Private incomeField As Double

        Private costField As Double

        Private supply As Double

        Private demandField As Double

        Private stockField As Double

        Private isHighlightedField As Boolean

        Public Property Income As Double
            Get
                Return incomeField
            End Get

            Set(ByVal value As Double)
                incomeField = value
                NotifyPropertyChanged("Income")
            End Set
        End Property

        Public Property Cost As Double
            Get
                Return costField
            End Get

            Set(ByVal value As Double)
                costField = value
                NotifyPropertyChanged("Cost")
            End Set
        End Property

        Public Property Production As Double
            Get
                Return supply
            End Get

            Set(ByVal value As Double)
                supply = value
                NotifyPropertyChanged("Production")
            End Set
        End Property

        Public Property Demand As Double
            Get
                Return demandField
            End Get

            Set(ByVal value As Double)
                demandField = value
                NotifyPropertyChanged("Demand")
            End Set
        End Property

        Public Property Stock As Double
            Get
                Return stockField
            End Get

            Set(ByVal value As Double)
                stockField = value
                NotifyPropertyChanged("Stock")
            End Set
        End Property

        Public Property Month As String

        Public Property IsHighlighted As Boolean
            Get
                Return isHighlightedField
            End Get

            Set(ByVal value As Boolean)
                isHighlightedField = value
                NotifyPropertyChanged("IsHighlighted")
            End Set
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Public Sub New(ByVal income As Double, ByVal cost As Double, ByVal production As Double, ByVal demand As Double, ByVal stock As Double, ByVal month As String)
            Me.Income = income
            Me.Cost = cost
            Me.Production = production
            Me.Demand = demand
            Me.Stock = stock
            Me.Month = month
            isHighlightedField = False
        End Sub

        Private Sub NotifyPropertyChanged(ByVal propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub

        Public Sub UpdateCost(ByVal newValue As Double)
            Cost = newValue
        End Sub

        Public Function UpdateProduction(ByVal newValue As Double) As Boolean
            If newValue < 0 Then Return False
            Production = newValue
            Return True
        End Function

        Public Sub UpdateDemand(ByVal newValue As Double)
            Demand = newValue
        End Sub
    End Class

    Public Class DraggableDataModel
        Inherits BindingList(Of DraggableItem)

        Public Shared Function CreateModel(ByVal Optional itemProductionCost As Integer = 50) As DraggableDataModel
            Dim rnd As Random = New Random()
            Dim now As Date = Date.Now
            Dim startDate As Date = New DateTime(now.Year, now.Month, 1)
            Dim data As DraggableDataModel = New DraggableDataModel(itemProductionCost)
            data.Add(New DraggableItem(0, 120, 2000, 1000, 0, startDate.ToString("MMMM", CultureInfo.InvariantCulture)))
            startDate = startDate.AddMonths(1)
            For i As Integer = 1 To 12 - 1
                Dim month As String = startDate.ToString("MMMM", CultureInfo.InvariantCulture)
                Dim cost As Double
                Dim demand As Double
                If i < 4 OrElse i > 7 Then
                    cost = data(i - 1).Cost + rnd.Next(3, 6)
                    If i < 4 Then
                        demand = data(i - 1).Demand + (rnd.NextDouble() * (5 - 3) + 3) * 1000
                    Else
                        demand = data(i - 1).Demand + (rnd.NextDouble() * (8 - 6) + 6) * 1000
                    End If
                Else
                    cost = data(i - 1).Cost + rnd.Next(10, 20)
                    demand = data(i - 1).Demand + (rnd.NextDouble() * (13 - 8) + 8) * 1000
                End If

                Dim production As Double = data(i - 1).Production + (rnd.NextDouble() * (8 - 5) + 5) * 1000
                data.Add(New DraggableItem(0, cost, production, demand, 0, month))
                startDate = startDate.AddMonths(1)
            Next

            data.InitStock()
            data.CalcIncome(0)
            Return data
        End Function

        Public ReadOnly ItemProductionCost As Double

        Public ReadOnly Property TotalIncome As Double
            Get
                Return Enumerable.Select(Of DraggableItem, Global.System.[Double])(Me, CType(Function(x) CDbl(x.Income), Func(Of DraggableItem, Double))).Sum()
            End Get
        End Property

        Public Sub New(ByVal unitProductionCost As Double)
            ItemProductionCost = unitProductionCost
        End Sub

        Private Sub CalcFutureStock(ByVal startingIndex As Integer, ByVal reminderDiff As Double)
            For i As Integer = startingIndex To Count - 1
                Dim oldStock As Double = Me(i).Stock
                Me(i).Stock = Math.Max(Me(i).Production, Me(i).Stock + reminderDiff)
                If oldStock < Me(i).Demand AndAlso Me(i).Stock < Me(i).Demand Then Exit For
                If oldStock < Me(i).Demand AndAlso Me(i).Stock > Me(i).Demand Then
                    reminderDiff = Me(i).Stock - Me(i).Demand
                ElseIf oldStock > Me(i).Demand AndAlso Me(i).Stock < Me(i).Demand Then
                    reminderDiff = Me(i).Demand - oldStock
                End If
            Next
        End Sub

        Private Function CalcStockDiff(ByVal oldDemand As Double, ByVal newDemand As Double, ByVal stock As Double) As Double
            If oldDemand <= stock AndAlso newDemand <= stock Then
                Return -(newDemand - oldDemand)
            ElseIf oldDemand <= stock AndAlso newDemand > stock Then
                Return oldDemand - stock
            ElseIf oldDemand > stock AndAlso newDemand < stock Then
                Return stock - newDemand
            Else
                Return 0
            End If
        End Function

        Public Sub InitStock()
            Dim rnd As Random = New Random()
            Dim initialStock As Double = rnd.Next(20, 40) * 1000
            For i As Integer = 0 To Count - 1
                Me(i).Stock = initialStock + Me(i).Production
                initialStock = Math.Max(0, Me(i).Stock - Me(i).Demand)
            Next
        End Sub

        Public Sub UpdateCost(ByVal item As DraggableItem, ByVal newValue As Double)
            item.UpdateCost(newValue)
            Dim index As Integer = IndexOf(item)
            CalcIncome(index)
        End Sub

        Public Sub UpdateProduction(ByVal item As DraggableItem, ByVal newValue As Double)
            Dim stockDiff As Double = newValue - item.Production
            If stockDiff < 0 AndAlso item.Stock <= 0 Then Return
            If Not item.UpdateProduction(newValue) Then Return
            Dim index As Integer = IndexOf(item)
            CalcFutureStock(index, stockDiff)
            CalcIncome(index)
        End Sub

        Public Sub UpdateDemand(ByVal item As DraggableItem, ByVal newValue As Double)
            Dim oldDemand As Double = item.Demand
            item.UpdateDemand(newValue)
            Dim index As Integer = IndexOf(item)
            Dim stockDiff As Double = CalcStockDiff(oldDemand, newValue, item.Stock)
            If stockDiff <> 0 Then CalcFutureStock(index + 1, stockDiff)
            CalcIncome(index)
        End Sub

        Public Sub CalcIncome(ByVal startingIndex As Integer)
            For i As Integer = startingIndex To Count - 1
                Dim unitsSold As Double = Math.Min(Me(i).Demand, Me(i).Stock)
                Me(i).Income =(unitsSold * Me(i).Cost - Me(i).Production * ItemProductionCost) / 1000
            Next
        End Sub
    End Class
End Namespace
