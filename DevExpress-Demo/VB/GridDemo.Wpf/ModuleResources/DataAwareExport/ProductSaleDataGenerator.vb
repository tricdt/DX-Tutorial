Imports DevExpress.DemoData.Models
Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace GridDemo

    Public Module ProductSaleDataGenerator

        Public Function GenerateSales(ByVal count As Integer) As List(Of ProductSaleData)
            Dim random = New Random()
            Dim products = NWindContext.Create().Invoices.[Select](Function(invoice) invoice.ProductName).Distinct().ToList()
            Dim sales As List(Of ProductSaleData) = New List(Of ProductSaleData)()
            Dim countries = NWindContext.Create().CountriesArray
            Return Enumerable.Range(0, count).[Select](Function(index) CreateSale(random, countries(index Mod countries.Length), products(index Mod products.Count), Date.Today.Year - index Mod 9)).ToList()
        End Function

        Private Function CreateSale(ByVal random As Random, ByVal country As String, ByVal product As String, ByVal year As Integer) As ProductSaleData
            Dim sale = New ProductSaleData()
            sale.Country = country
            sale.ProductName = product
            sale.Year = year
            sale.Sales = random.Next(50000000, 500000000)
            sale.SalesVsTarget =(random.NextDouble() - 0.5) / 5
            sale.Profit = random.Next(-30000000, 50000000)
            sale.CustomersSatisfaction = Math.Round((random.NextDouble() + 1) * 2.5, 1)
            sale.MarketShare = random.NextDouble() / 3 + 0.15
            Return sale
        End Function
    End Module
End Namespace
