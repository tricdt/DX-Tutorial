Imports System.ComponentModel.DataAnnotations

Namespace GridDemo

    Public Class SaleOverviewData

        Public Sub New(ByVal state As String, ByVal sales As Double, ByVal salesVsTarget As Double, ByVal profit As Double, ByVal customersSatisfaction As Double, ByVal markerShare As Double)
            Me.State = state
            Me.Sales = sales
            Me.Profit = profit
            Me.SalesVsTarget = salesVsTarget
            Me.CustomersSatisfaction = customersSatisfaction
            MarketShare = markerShare
        End Sub

        Public Property State As String

        <DisplayFormat(DataFormatString:="#,##0,,M")>
        Public Property Sales As Double

        <DisplayFormat(DataFormatString:="#,##0,,M")>
        Public Property Profit As Double

        <DisplayFormat(DataFormatString:="p", ApplyFormatInEditMode:=True), Display(Name:="Sales vs Target")>
        Public Property SalesVsTarget As Double

        <DisplayFormat(DataFormatString:="p0", ApplyFormatInEditMode:=True)>
        Public Property MarketShare As Double

        Public Property CustomersSatisfaction As Double
    End Class
End Namespace
