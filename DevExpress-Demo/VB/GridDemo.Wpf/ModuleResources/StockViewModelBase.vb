Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase.Helpers
Imports System
Imports System.Collections.Generic
Imports System.Reflection
Imports System.Windows.Threading
Imports System.Xml.Serialization

Namespace GridDemo

    <POCOViewModel>
    Public MustInherit Class StockViewModelBase

        Const HistoryLength As Integer = 20

        Const UpdateInterval As Integer = 1000

        Protected ReadOnly Random As Random = New Random()

        Private timer As DispatcherTimer

        Public Sub New()
            Dim data As ObservableCollectionCore(Of StockItem) = StockItems.GetData()
            For Each stockItem As StockItem In data
                For i As Integer = 0 To HistoryLength - 1
                    UpdatePrice(stockItem, True)
                Next
            Next

            Me.Data = data
        End Sub

        Public Property Data As ObservableCollectionCore(Of StockItem)

        Public Sub StartUpdate()
            timer = New DispatcherTimer() With {.Interval = TimeSpan.FromMilliseconds(UpdateInterval)}
            AddHandler timer.Tick, AddressOf timer_Tick
            timer.Start()
        End Sub

        Public Sub StopUpdate()
            timer.Stop()
            RemoveHandler timer.Tick, AddressOf timer_Tick
            timer = Nothing
        End Sub

        Private Sub timer_Tick(ByVal sender As Object, ByVal e As EventArgs)
            Data.BeginUpdate()
            For Each stockItem As StockItem In Data
                UpdatePrice(stockItem, False)
            Next

            Data.EndUpdate()
        End Sub

        Private Sub UpdatePrice(ByVal stockItem As StockItem, ByVal dataGeneration As Boolean)
            Dim newDelta As Double = GetDeltaPrice(stockItem.Price, dataGeneration)
            If stockItem.Price + newDelta <= 0 Then newDelta = 0
            If Math.Sign(stockItem.DeltaPrice) = Math.Sign(newDelta) Then
                stockItem.DeltaChange = 0
            Else
                stockItem.DeltaChange = Math.Sign(newDelta)
            End If

            stockItem.DeltaPrice = newDelta
            stockItem.Price += stockItem.DeltaPrice
            If stockItem.Price < stockItem.LowPrice Then stockItem.LowPrice = stockItem.Price
            If stockItem.Price > stockItem.HighPrice Then stockItem.HighPrice = stockItem.Price
            stockItem.DeltaPricePercent = stockItem.DeltaPrice / stockItem.Price
            stockItem.Volume += Convert.ToInt32(Random.NextDouble() * stockItem.Volume * 0.005 - 0.0025)
            UpdatePriceHistory(stockItem)
        End Sub

        Private Sub UpdatePriceHistory(ByVal stockItem As StockItem)
            Dim newPriceHistory As List(Of Double) = New List(Of Double)(New Double(19) {})
            For i As Integer = 1 To HistoryLength - 1
                newPriceHistory(i - 1) = If(i < stockItem.PriceHistory.Count, stockItem.PriceHistory(i), 0)
            Next

            newPriceHistory(HistoryLength - 1) = stockItem.Price
            stockItem.PriceHistory = newPriceHistory
        End Sub

        Protected MustOverride Function GetDeltaPrice(ByVal currentPrice As Double, ByVal dataGeneration As Boolean) As Double
    End Class

    <XmlRoot("StockItems")>
    Public Class StockItems
        Inherits ObservableCollectionCore(Of StockItem)

        Public Shared Function GetData() As ObservableCollectionCore(Of StockItem)
            Dim s As XmlSerializer = New XmlSerializer(GetType(StockItems))
            Dim assembly As Assembly = GetType(StockItems).Assembly
            Return CType(s.Deserialize(assembly.GetManifestResourceStream(DemoHelper.GetPath("GridDemo.Data.", assembly) & "StockSource.xml")), ObservableCollectionCore(Of StockItem))
        End Function
    End Class

    Public Class StockItem

        Public Property CompanyName As String

        Public Property Price As Double

        Public Property Volume As Integer

        Public Property LowPrice As Double

        Public Property HighPrice As Double

        Public Property DeltaPrice As Double

        Public Property DeltaPricePercent As Double

        Public Property DeltaChange As Integer

        Public Sub New()
            PriceHistory = New List(Of Double)()
        End Sub

        Public Property PriceHistory As List(Of Double)
    End Class
End Namespace
