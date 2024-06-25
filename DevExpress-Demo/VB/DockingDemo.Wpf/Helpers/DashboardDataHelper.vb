Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports System.IO
Imports System.Reflection
Imports DevExpress.DemoData.Helpers
Imports DevExpress.Xpf.DemoBase.Helpers

Namespace DockingDemo.Helpers

    Public Module DataLoader

        Private Function LoadData(ByVal fileName As String) As DataSet
            Dim path As String = DevExpress.DemoData.Helpers.DataFilesHelper.FindFile(String.Format("{0}.xml", fileName), DevExpress.DemoData.Helpers.DataFilesHelper.DataPath)
            Dim ds As System.Data.DataSet = New System.Data.DataSet()
            ds.ReadXml(path, System.Data.XmlReadMode.ReadSchema)
            Return ds
        End Function

        Public Function LoadSales() As DataSet
            Return DockingDemo.Helpers.DataLoader.LoadData("DashboardSales")
        End Function
    End Module

    Public MustInherit Class SalesDataGenerator

        Public Class Context

            Private ReadOnly st As String

            Private ReadOnly prodName As String

            Private ReadOnly catName As String

            Private ReadOnly lPrice As Decimal

            Private ReadOnly uSoldGenerator As DockingDemo.Helpers.UnitsSoldRandomGenerator

            Public ReadOnly Property State As String
                Get
                    Return Me.st
                End Get
            End Property

            Public ReadOnly Property ProductName As String
                Get
                    Return Me.prodName
                End Get
            End Property

            Public ReadOnly Property CategoryName As String
                Get
                    Return Me.catName
                End Get
            End Property

            Public ReadOnly Property ListPrice As Decimal
                Get
                    Return Me.lPrice
                End Get
            End Property

            Public ReadOnly Property UnitsSoldGenerator As UnitsSoldRandomGenerator
                Get
                    Return Me.uSoldGenerator
                End Get
            End Property

            Public Sub New(ByVal st As String, ByVal prodName As String, ByVal catName As String, ByVal lPrice As Decimal, ByVal uSoldGenerator As DockingDemo.Helpers.UnitsSoldRandomGenerator)
                Me.st = st
                Me.prodName = prodName
                Me.catName = catName
                Me.lPrice = lPrice
                Me.uSoldGenerator = uSoldGenerator
            End Sub
        End Class

        Protected Shared Function GetState(ByVal region As System.Data.DataRow) As String
            Return CStr(region("Region"))
        End Function

        Protected Shared Function GetProductName(ByVal product As System.Data.DataRow) As String
            Return CStr(product("Name"))
        End Function

        Protected Shared Function GetListPrice(ByVal product As System.Data.DataRow) As Decimal
            Return CDec(product("ListPrice"))
        End Function

        Private ReadOnly ds As System.Data.DataSet

        Private ReadOnly categoriesTable As System.Data.DataTable

        Private ReadOnly productsTable As System.Data.DataTable

        Private ReadOnly regionsTable As System.Data.DataTable

        Private ReadOnly rand As System.Random = New System.Random(1)

        Private ReadOnly prodClassesField As DockingDemo.Helpers.ProductClasses

        Private ReadOnly regClassesField As DockingDemo.Helpers.RegionClasses

        Protected ReadOnly Property Regions As DataRowCollection
            Get
                Return Me.regionsTable.Rows
            End Get
        End Property

        Protected ReadOnly Property Products As DataRowCollection
            Get
                Return Me.productsTable.Rows
            End Get
        End Property

        Protected ReadOnly Property ProdClasses As ProductClasses
            Get
                Return Me.prodClassesField
            End Get
        End Property

        Protected ReadOnly Property RegClasses As RegionClasses
            Get
                Return Me.regClassesField
            End Get
        End Property

        Protected ReadOnly Property Random As Random
            Get
                Return Me.rand
            End Get
        End Property

        Protected Sub New(ByVal ds As System.Data.DataSet)
            Me.ds = ds
            Me.categoriesTable = ds.Tables("Categories")
            Me.productsTable = ds.Tables("Products")
            Me.regionsTable = ds.Tables("Regions")
            Me.prodClassesField = New DockingDemo.Helpers.ProductClasses(Me.productsTable.Rows)
            Me.regClassesField = New DockingDemo.Helpers.RegionClasses(Me.regionsTable.Rows)
        End Sub

        Protected Function GetRegionWeigtht(ByVal region As System.Data.DataRow) As Double
            Return Me.regClassesField(CInt(region("RegionID")))
        End Function

        Protected Function GetProductClass(ByVal product As System.Data.DataRow) As ProductClass
            Return Me.prodClassesField(CInt(product("ProductID")))
        End Function

        Protected Function GetCategoryName(ByVal product As System.Data.DataRow) As String
            Return CStr(Me.categoriesTable.[Select](String.Format("CategoryID = {0}", product("CategoryID")))(0)("CategoryName"))
        End Function

        Protected Function CreateUnitsSoldGenerator(ByVal regionWeight As Double, ByVal productClass As DockingDemo.Helpers.ProductClass) As UnitsSoldRandomGenerator
            Return New DockingDemo.Helpers.UnitsSoldRandomGenerator(Me.rand, CInt(System.Math.Ceiling(productClass.SaleProbability * regionWeight)))
        End Function

        Protected MustOverride Sub Generate(ByVal context As DockingDemo.Helpers.SalesDataGenerator.Context)

        Protected Overridable Sub EndGenerate()
        End Sub

        Public Sub Generate()
            For Each region As System.Data.DataRow In Me.Regions
                Dim state As String = DockingDemo.Helpers.SalesDataGenerator.GetState(region)
                Dim regionWeight As Double = Me.GetRegionWeigtht(region)
                For Each product As System.Data.DataRow In Me.Products
                    Dim unitsSoldgenerator As DockingDemo.Helpers.UnitsSoldRandomGenerator = Me.CreateUnitsSoldGenerator(regionWeight, Me.GetProductClass(product))
                    Me.Generate(New DockingDemo.Helpers.SalesDataGenerator.Context(state, DockingDemo.Helpers.SalesDataGenerator.GetProductName(product), Me.GetCategoryName(product), DockingDemo.Helpers.SalesDataGenerator.GetListPrice(product), unitsSoldgenerator))
                Next
            Next

            Me.EndGenerate()
        End Sub
    End Class

    Public Class SalesPerformanceDataGenerator
        Inherits DockingDemo.Helpers.SalesDataGenerator

        Private ReadOnly monthlySalesField As System.Collections.Generic.List(Of DockingDemo.Helpers.MonthlySalesItem) = New System.Collections.Generic.List(Of DockingDemo.Helpers.MonthlySalesItem)()

        Private ReadOnly totalSalesField As System.Collections.Generic.List(Of DockingDemo.Helpers.TotalSalesItem) = New System.Collections.Generic.List(Of DockingDemo.Helpers.TotalSalesItem)()

        Private ReadOnly item As DockingDemo.Helpers.KeyMetricsItem = New DockingDemo.Helpers.KeyMetricsItem()

        Public ReadOnly Property MonthlySales As IEnumerable(Of DockingDemo.Helpers.MonthlySalesItem)
            Get
                Return Me.monthlySalesField
            End Get
        End Property

        Public ReadOnly Property TotalSales As IEnumerable(Of DockingDemo.Helpers.TotalSalesItem)
            Get
                Return Me.totalSalesField
            End Get
        End Property

        Public ReadOnly Property KeyMetrics As IEnumerable(Of DockingDemo.Helpers.KeyMetricsItem)
            Get
                Return New DockingDemo.Helpers.KeyMetricsItem() {Me.item}
            End Get
        End Property

        Public Sub New(ByVal dataSet As System.Data.DataSet)
            MyBase.New(dataSet)
        End Sub

        Protected Overrides Sub Generate(ByVal context As DockingDemo.Helpers.SalesDataGenerator.Context)
            Dim tsItem As DockingDemo.Helpers.TotalSalesItem = New DockingDemo.Helpers.TotalSalesItem With {.State = context.State, .Category = context.CategoryName, .Product = context.ProductName}
            Dim y As Integer = System.DateTime.Today.Year - 1
            For currentMonth As Integer = 1 To 12
                Dim dt As System.DateTime = New System.DateTime(y, currentMonth, 1)
                context.UnitsSoldGenerator.[Next]()
                Dim uSold As Integer = context.UnitsSoldGenerator.UnitsSold
                Dim uSoldTarget As Integer = context.UnitsSoldGenerator.UnitsSoldTarget
                Dim rev As Decimal = uSold * context.ListPrice
                Dim revTarget As Decimal = uSoldTarget * context.ListPrice
                Me.monthlySalesField.Add(New DockingDemo.Helpers.MonthlySalesItem With {.State = context.State, .Product = context.ProductName, .Category = context.CategoryName, .CurrentDate = dt, .UnitsSold = uSold, .UnitsSoldTarget = uSoldTarget, .Revenue = rev, .RevenueTarget = revTarget})
                tsItem.RevenueYTD += rev
                tsItem.RevenueYTDTarget += revTarget
                tsItem.UnitsSoldYTD += uSold
                tsItem.UnitsSoldYTDTarget += uSoldTarget
                If currentMonth >= 10 AndAlso currentMonth <= 12 Then
                    tsItem.RevenueQTD += rev
                    tsItem.RevenueQTDTarget += revTarget
                End If

                Me.item.RevenueYTD += rev
                Me.item.RevenueYTDTarget += revTarget
            Next

            Me.totalSalesField.Add(tsItem)
        End Sub

        Protected Overrides Sub EndGenerate()
            MyBase.EndGenerate()
            Me.item.ExpencesYTD = Me.item.RevenueYTDTarget * 0.2D
            Me.item.ExpencesYTDTarget = Me.item.RevenueYTDTarget * 0.1999D
            Me.item.ProfitYTD = Me.item.RevenueYTD - Me.item.ExpencesYTD
            Me.item.ProfitYTDTarget = Me.item.RevenueYTDTarget - Me.item.ExpencesYTDTarget
            Me.item.AvgOrderSizeYTD = Me.item.RevenueYTD * 0.006D
            Me.item.AvgOrderSizeYTDTarget = Me.item.RevenueYTDTarget * 0.0055D
            Me.item.NewCustomersYTD = CInt(System.Math.Round(Me.item.RevenueYTD * 0.0013D))
            Me.item.NewCustomersYTDTarget = CInt(System.Math.Round(Me.item.RevenueYTDTarget * 0.00125D))
            Me.item.MarketShare = 0.23F
        End Sub
    End Class

    Public Class UnitsSoldRandomGenerator

        Const MinUnitsSold As Integer = 5

        Private ReadOnly rand As System.Random

        Private ReadOnly startUnitsSold As Integer

        Private prevUnitsSold As Integer?

        Private prevPrevUnitsSold As Integer?

        Private unitsSoldField As Integer

        Private unitsSoldTargetField As Integer

        Private isFirst As Boolean = True

        Public ReadOnly Property UnitsSold As Integer
            Get
                Return Me.unitsSoldField
            End Get
        End Property

        Public ReadOnly Property UnitsSoldTarget As Integer
            Get
                Return Me.unitsSoldTargetField
            End Get
        End Property

        Public Sub New(ByVal rand As System.Random, ByVal startUnitsSold As Integer)
            Me.rand = rand
            Me.startUnitsSold = System.Math.Max(startUnitsSold, DockingDemo.Helpers.UnitsSoldRandomGenerator.MinUnitsSold)
        End Sub

        Public Sub [Next]()
            If Me.isFirst Then
                Me.unitsSoldField = Me.startUnitsSold
                Me.isFirst = False
            Else
                Me.unitsSoldField = Me.unitsSoldField + CInt(System.Math.Round(DockingDemo.Helpers.DataHelper.Random(Me.rand, Me.unitsSoldField * 0.5)))
                Me.unitsSoldField = System.Math.Max(Me.unitsSoldField, DockingDemo.Helpers.UnitsSoldRandomGenerator.MinUnitsSold)
            End If

            Dim unitsSoldSum As Integer = Me.unitsSoldField
            Dim count As Integer = 1
            If Me.prevUnitsSold.HasValue Then
                unitsSoldSum += Me.prevUnitsSold.Value
                count += 1
            End If

            If Me.prevPrevUnitsSold.HasValue Then
                unitsSoldSum += Me.prevPrevUnitsSold.Value
                count += 1
            End If

            Me.unitsSoldTargetField = CInt(System.Math.Round(CDbl(unitsSoldSum) / count))
            Me.unitsSoldTargetField = Me.unitsSoldTargetField + CInt(System.Math.Round(DockingDemo.Helpers.DataHelper.Random(Me.rand, Me.unitsSoldTargetField)))
            Me.prevPrevUnitsSold = Me.prevUnitsSold
            Me.prevUnitsSold = Me.unitsSoldField
        End Sub
    End Class

    Public Class ProductClasses
        Inherits System.Collections.Generic.List(Of DockingDemo.Helpers.ProductClass)

        Default Public Overloads ReadOnly Property Item(ByVal productID As Integer) As ProductClass
            Get
                For Each productClass As DockingDemo.Helpers.ProductClass In Me
                    If productClass.ContainsProduct(productID) Then Return productClass
                Next

                Throw New System.ArgumentException("procutID")
            End Get
        End Property

        Public Sub New(ByVal products As System.Collections.ICollection)
            Me.Add(New DockingDemo.Helpers.ProductClass(Nothing, 100D, 0.5))
            Me.Add(New DockingDemo.Helpers.ProductClass(100D, 500D, 0.4))
            Me.Add(New DockingDemo.Helpers.ProductClass(500D, 1500D, 0.3))
            Me.Add(New DockingDemo.Helpers.ProductClass(1500D, Nothing, 0.2))
            For Each product As System.Data.DataRow In products
                Dim productID As Integer = CInt(product("ProductID"))
                Dim listPrice As Decimal = CDec(product("ListPrice"))
                For Each productClass As DockingDemo.Helpers.ProductClass In Me
                    If productClass.AddProduct(productID, listPrice) Then Exit For
                Next
            Next
        End Sub
    End Class

    Public Class RegionClasses
        Inherits System.Collections.Generic.Dictionary(Of Integer, Double)

        Public Sub New(ByVal regions As System.Collections.ICollection)
            Dim numberEmployeesMin As Integer? = Nothing
            For Each region As System.Data.DataRow In regions
                Dim numberEmployees As Short = CShort(region("NumberEmployees"))
                numberEmployeesMin = If(numberEmployeesMin.HasValue, System.Math.Min(numberEmployeesMin.Value, numberEmployees), numberEmployees)
            Next

            For Each region As System.Data.DataRow In regions
                Me.Add(CInt(region("RegionID")), CShort(region("NumberEmployees")) / CDbl(numberEmployeesMin.Value))
            Next
        End Sub
    End Class

    Public Class ProductClass

        Private ReadOnly productIDs As System.Collections.Generic.List(Of Integer) = New System.Collections.Generic.List(Of Integer)()

        Private ReadOnly minPrice As Decimal?

        Private ReadOnly maxPrice As Decimal?

        Private ReadOnly saleProbabilityField As Double

        Public ReadOnly Property SaleProbability As Double
            Get
                Return Me.saleProbabilityField
            End Get
        End Property

        Public Sub New(ByVal minPrice As Decimal?, ByVal maxPrice As Decimal?, ByVal saleProbability As Double)
            Me.minPrice = minPrice
            Me.maxPrice = maxPrice
            Me.saleProbabilityField = saleProbability
        End Sub

        Public Function AddProduct(ByVal productID As Integer, ByVal price As Decimal) As Boolean
            Dim satisfyMinPrice As Boolean = Not Me.minPrice.HasValue OrElse price >= Me.minPrice.Value
            Dim satisfyMaxPrice As Boolean = Not Me.maxPrice.HasValue OrElse price < Me.maxPrice.Value
            If satisfyMinPrice AndAlso satisfyMaxPrice Then
                Me.productIDs.Add(productID)
                Return True
            End If

            Return False
        End Function

        Public Function ContainsProduct(ByVal productID As Integer) As Boolean
            Return Me.productIDs.Contains(productID)
        End Function
    End Class

    Public Module DataHelper

        Public Function Random(ByVal pRandom As System.Random, ByVal deviation As Double, ByVal positive As Boolean) As Double
            Dim rand As Integer = pRandom.[Next](If(positive, 0, -1000000), 1000000)
            Return CDbl(rand) / 1000000 * deviation
        End Function

        Public Function Random(ByVal pRandom As System.Random, ByVal deviation As Double) As Double
            Return DockingDemo.Helpers.DataHelper.Random(pRandom, deviation, False)
        End Function
    End Module

    Public Class TotalSalesItem

        Public Property State As String

        Public Property Category As String

        Public Property Product As String

        Public Property RevenueYTD As Decimal

        Public Property RevenueYTDTarget As Decimal

        Public Property RevenueQTD As Decimal

        Public Property RevenueQTDTarget As Decimal

        Public Property UnitsSoldYTD As Integer

        Public Property UnitsSoldYTDTarget As Integer
    End Class

    Public Class MonthlySalesItem

        Public Property State As String

        Public Property Product As String

        Public Property Category As String

        Public Property CurrentDate As DateTime

        Public Property Revenue As Decimal

        Public Property RevenueTarget As Decimal

        Public Property UnitsSold As Integer

        Public Property UnitsSoldTarget As Integer
    End Class

    Public Class KeyMetricsItem

        Public Property RevenueYTD As Decimal

        Public Property RevenueYTDTarget As Decimal

        Public Property ExpencesYTD As Decimal

        Public Property ExpencesYTDTarget As Decimal

        Public Property ProfitYTD As Decimal

        Public Property ProfitYTDTarget As Decimal

        Public Property AvgOrderSizeYTD As Decimal

        Public Property AvgOrderSizeYTDTarget As Decimal

        Public Property NewCustomersYTD As Integer

        Public Property NewCustomersYTDTarget As Integer

        Public Property MarketShare As Single
    End Class
End Namespace
