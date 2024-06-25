Imports System

Namespace GridDemo

    Public Class DataUpdateAnimationViewModel
        Inherits StockViewModelBase

        Protected Overrides Function GetDeltaPrice(ByVal currentPrice As Double, ByVal dataGeneration As Boolean) As Double
            If Not dataGeneration AndAlso Random.NextDouble() > 0.3 Then Return 0
            Dim delta As Double =(Random.NextDouble() * 0.2 - 0.1) * currentPrice
            Return If(delta > 0, Math.Max(delta, 0.01), Math.Min(delta, -0.01))
        End Function
    End Class
End Namespace
