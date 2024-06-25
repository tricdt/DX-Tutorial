Imports System.ComponentModel.DataAnnotations

Namespace GridDemo

    Public Class ProductSaleData

        Public Property Country As String

        Public Property ProductName As String

        Public Property Year As Integer

        <DisplayFormat(DataFormatString:="#,##0,,M")>
        Public Property Sales As Double

        <DisplayFormat(DataFormatString:="#,##0,,M")>
        Public Property Profit As Double

        <DisplayFormat(DataFormatString:="p", ApplyFormatInEditMode:=True), Display(Name:="Sales vs Target")>
        Public Property SalesVsTarget As Double

        <DisplayFormat(DataFormatString:="p0", ApplyFormatInEditMode:=True)>
        Public Property MarketShare As Double

        <Display(Name:="Cust Satisfaction")>
        Public Property CustomersSatisfaction As Double
    End Class
End Namespace
