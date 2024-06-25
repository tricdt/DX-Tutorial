Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Globalization

Namespace ChartsDemo

    Public Class DevAVSaleItem

        Private ReadOnly saleItemsField As System.Collections.Generic.List(Of ChartsDemo.DevAVSaleItem) = New System.Collections.Generic.List(Of ChartsDemo.DevAVSaleItem)()

        Public Property OrderDate As DateTime

        Public Property Product As String

        Public Property Company As String

        Public Property Month As String

        Public Property Income As Double

        Public Property Revenue As Double

        Public Property Category As String

        Public ReadOnly Property SaleItems As List(Of ChartsDemo.DevAVSaleItem)
            Get
                Return Me.saleItemsField
            End Get
        End Property

        Public ReadOnly Property TotalIncome As Double
            Get
                Dim total As Double = 0
                For Each saleItem As ChartsDemo.DevAVSaleItem In Me.SaleItems
                    total += saleItem.Income
                Next

                Return total
            End Get
        End Property
    End Class

    Public Class DevAVSales

        Private ReadOnly companies As String() = New String() {"DevAV North", "DevAV South", "DevAV West", "DevAV East", "DevAV Central"}

        Private categorizedProductsField As System.Collections.Generic.Dictionary(Of String, System.Collections.Generic.List(Of String))

        Private ReadOnly rnd As System.Random = New System.Random(2019)

        Private endDate As System.DateTime

        Private ReadOnly Property CategorizedProducts As Dictionary(Of String, System.Collections.Generic.List(Of String))
            Get
                If Me.categorizedProductsField Is Nothing Then
                    Me.categorizedProductsField = New System.Collections.Generic.Dictionary(Of String, System.Collections.Generic.List(Of String))()
                    Me.categorizedProductsField("Cell Phones") = New System.Collections.Generic.List(Of String)() From {"Smartphones", "Mobile Phones", "Smart Watches", "Sim Cards"}
                    Me.categorizedProductsField("Computers") = New System.Collections.Generic.List(Of String)() From {"PCs", "Laptops", "Tablets", "Printers"}
                    Me.categorizedProductsField("TV, Audio") = New System.Collections.Generic.List(Of String)() From {"TVs", "Home Audio", "Headphones", "DVD Players"}
                    Me.categorizedProductsField("Car Electronics") = New System.Collections.Generic.List(Of String)() From {"GPS Units", "Radars", "Car Alarms", "Car Accessories"}
                    Me.categorizedProductsField("Power Devices") = New System.Collections.Generic.List(Of String)() From {"Batteries", "Chargers", "Converters", "Testers", "AC/DC Adapters"}
                    Me.categorizedProductsField("Photo") = New System.Collections.Generic.List(Of String)() From {"Cameras", "Camcorders", "Binoculars", "Flashes", "Tripodes"}
                End If

                Return Me.categorizedProductsField
            End Get
        End Property

        Public Sub New()
            Dim now As System.DateTime = System.DateTime.Now
            Me.endDate = New System.DateTime(now.Year, now.Month, 1)
        End Sub

        Public Function GetProductsByMonths() As List(Of ChartsDemo.DevAVSaleItem)
            Dim rnd As System.Random = New System.Random(1)
            Dim items As System.Collections.Generic.List(Of ChartsDemo.DevAVSaleItem) = New System.Collections.Generic.List(Of ChartsDemo.DevAVSaleItem)()
            For Each company As String In Me.companies
                For Each product As String In Me.CategorizedProducts("Photo")
                    Dim dateTime As System.DateTime = New System.DateTime(System.DateTime.Now.Year, 12, 01)
                    For i As Integer = 0 To 12 - 1
                        Dim income As Integer = rnd.[Next](20, 100)
                        Dim revenue As Integer = income + rnd.[Next](20, 50)
                        items.Add(New ChartsDemo.DevAVSaleItem() With {.Company = company, .Product = product, .Month = dateTime.AddMonths(CInt((1))).ToString("MMMM", System.Globalization.CultureInfo.InvariantCulture), .Income = income, .Revenue = revenue})
                        dateTime = dateTime.AddMonths(1)
                    Next
                Next
            Next

            Return items
        End Function

        Public Function GetProductsByCompany(ByVal companyIndex As Integer) As List(Of ChartsDemo.DevAVSaleItem)
            Dim rnd As System.Random = New System.Random(companyIndex)
            Dim items As System.Collections.Generic.List(Of ChartsDemo.DevAVSaleItem) = New System.Collections.Generic.List(Of ChartsDemo.DevAVSaleItem)()
            For Each category As String In Me.CategorizedProducts.Keys
                For Each product As String In Me.CategorizedProducts(category)
                    Dim income As Integer = rnd.[Next](20, 100)
                    Dim revenue As Integer = income + rnd.[Next](20, 50)
                    items.Add(New ChartsDemo.DevAVSaleItem() With {.Company = Me.companies(companyIndex), .Product = product, .Income = income, .Revenue = revenue, .Category = category})
                Next
            Next

            Return items
        End Function

        Public Function GetSalesMixByRegion() As DataTable
            Dim table As System.Data.DataTable = New System.Data.DataTable()
            table.Columns.AddRange(New System.Data.DataColumn() {New System.Data.DataColumn("ProductCategory", GetType(String)), New System.Data.DataColumn("Region", GetType(String)), New System.Data.DataColumn("Sales", GetType(Decimal))})
            table.Rows.Add("Video players", "Asia", 853R)
            table.Rows.Add("Video players", "Australia", 321R)
            table.Rows.Add("Video players", "Europe", 655R)
            table.Rows.Add("Video players", "North America", 1325R)
            table.Rows.Add("Video players", "South America", 653R)
            table.Rows.Add("Automation", "Asia", 172R)
            table.Rows.Add("Automation", "Australia", 255R)
            table.Rows.Add("Automation", "Europe", 981R)
            table.Rows.Add("Automation", "North America", 963R)
            table.Rows.Add("Automation", "South America", 123R)
            table.Rows.Add("Monitors", "Asia", 1011R)
            table.Rows.Add("Monitors", "Australia", 359R)
            table.Rows.Add("Monitors", "Europe", 721R)
            table.Rows.Add("Monitors", "North America", 565R)
            table.Rows.Add("Monitors", "South America", 532R)
            table.Rows.Add("Projectors", "Asia", 998R)
            table.Rows.Add("Projectors", "Australia", 222R)
            table.Rows.Add("Projectors", "Europe", 865R)
            table.Rows.Add("Projectors", "North America", 787R)
            table.Rows.Add("Projectors", "South America", 332R)
            table.Rows.Add("Televisions", "Asia", 1356R)
            table.Rows.Add("Televisions", "Australia", 232R)
            table.Rows.Add("Televisions", "Europe", 1323R)
            table.Rows.Add("Televisions", "North America", 1125R)
            table.Rows.Add("Televisions", "South America", 865R)
            Return table
        End Function

        Public Function GetProductsIncome() As List(Of ChartsDemo.DevAVSaleItem)
            Dim rnd As System.Random = New System.Random(1)
            Dim items As System.Collections.Generic.List(Of ChartsDemo.DevAVSaleItem) = New System.Collections.Generic.List(Of ChartsDemo.DevAVSaleItem)()
            For i As Integer = 0 To 50 - 1
                For Each product As String In Me.CategorizedProducts("Photo")
                    items.Add(New ChartsDemo.DevAVSaleItem() With {.Product = product, .Income = rnd.[Next](20, 100)})
                Next
            Next

            Return items
        End Function

        Public Function GetCategoriesIncome() As List(Of ChartsDemo.DevAVSaleItem)
            Dim rnd As System.Random = New System.Random(4)
            Dim categoryItems As System.Collections.Generic.List(Of ChartsDemo.DevAVSaleItem) = New System.Collections.Generic.List(Of ChartsDemo.DevAVSaleItem)()
            For Each category As String In Me.CategorizedProducts.Keys
                Dim categoryItem As ChartsDemo.DevAVSaleItem = New ChartsDemo.DevAVSaleItem() With {.Category = category}
                For Each product As String In Me.CategorizedProducts(category)
                    Dim year As Integer = System.DateTime.Now.Year - 1
                    Dim dateTime As System.DateTime = New System.DateTime(year, 1, 1)
                    For i As Integer = 0 To 12 - 1
                        categoryItem.SaleItems.Add(New ChartsDemo.DevAVSaleItem() With {.Category = category, .Product = product, .OrderDate = dateTime.AddMonths(i), .Income = System.Math.Round(rnd.NextDouble() * 50 + 20, 2)})
                    Next
                Next

                categoryItems.Add(categoryItem)
            Next

            Return categoryItems
        End Function

        Private Function GenerateSalesForProduct(ByVal companyFactor As Double, ByVal categoryFactor As Double) As List(Of ChartsDemo.DevAVMonthlyIncome)
            Dim data As System.Collections.Generic.List(Of ChartsDemo.DevAVMonthlyIncome) = New System.Collections.Generic.List(Of ChartsDemo.DevAVMonthlyIncome)()
            Dim year As Integer = System.DateTime.Now.Year - 1
            Dim baseDate As System.DateTime = New System.DateTime(year, 1, 1)
            Dim maxIncome As Integer = Me.rnd.[Next](60, 140)
            For i As Integer = 0 To 1000 - 1
                If i Mod 100 = 0 Then maxIncome = System.Math.Max(40, Me.rnd.[Next](maxIncome - 20, maxIncome + 20))
                Dim month As System.DateTime = Me.endDate.AddDays(-i - 1)
                Dim income As Double = Me.rnd.[Next](20, maxIncome) * companyFactor * categoryFactor
                Dim monthlyIncome As ChartsDemo.DevAVMonthlyIncome = New ChartsDemo.DevAVMonthlyIncome(month, income)
                data.Add(monthlyIncome)
            Next

            Return data
        End Function

        Private Function GenerateCategoryProducts(ByVal categoryProductsPair As System.Collections.Generic.KeyValuePair(Of String, System.Collections.Generic.List(Of String)), ByVal companyFactor As Double, ByVal categoryFactor As Double) As List(Of ChartsDemo.DevAVProduct)
            Dim products As System.Collections.Generic.List(Of ChartsDemo.DevAVProduct) = New System.Collections.Generic.List(Of ChartsDemo.DevAVProduct)()
            For Each productName As String In categoryProductsPair.Value
                Dim sales As System.Collections.Generic.List(Of ChartsDemo.DevAVMonthlyIncome) = Me.GenerateSalesForProduct(companyFactor, categoryFactor)
                Dim product As ChartsDemo.DevAVProduct = New ChartsDemo.DevAVProduct(productName, sales)
                products.Add(product)
            Next

            Return products
        End Function

        Private Function GenerateBranchSales(ByVal companyFactor As Double) As List(Of ChartsDemo.DevAVProductCategory)
            Dim categories As System.Collections.Generic.List(Of ChartsDemo.DevAVProductCategory) = New System.Collections.Generic.List(Of ChartsDemo.DevAVProductCategory)()
            For Each categoryProductsPair In Me.CategorizedProducts
                Dim categoryFactor As Double = Me.rnd.NextDouble() * 0.6 + 1
                Dim products As System.Collections.Generic.List(Of ChartsDemo.DevAVProduct) = Me.GenerateCategoryProducts(categoryProductsPair, companyFactor, categoryFactor)
                Dim category As ChartsDemo.DevAVProductCategory = New ChartsDemo.DevAVProductCategory(categoryProductsPair.Key, products)
                categories.Add(category)
            Next

            Return categories
        End Function

        Friend Function GetHierarchicalSalesData() As List(Of ChartsDemo.DevAVBranch)
            Dim data As System.Collections.Generic.List(Of ChartsDemo.DevAVBranch) = New System.Collections.Generic.List(Of ChartsDemo.DevAVBranch)()
            For Each branchName As String In Me.companies
                Dim companyFactor As Double = Me.rnd.NextDouble() * 0.6 + 1
                Dim categories As System.Collections.Generic.List(Of ChartsDemo.DevAVProductCategory) = Me.GenerateBranchSales(companyFactor)
                Dim branch As ChartsDemo.DevAVBranch = New ChartsDemo.DevAVBranch(branchName, categories)
                data.Add(branch)
            Next

            Return data
        End Function
    End Class
End Namespace
