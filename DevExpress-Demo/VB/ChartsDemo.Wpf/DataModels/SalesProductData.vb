Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Data
Imports System.Linq
Imports System.Windows
Imports DevExpress.Utils

Namespace ChartsDemo

    Public Class BikeReportItem
        Inherits ChartsDemo.SalesProductData.SaleItemBase

    End Class

    Public Class BikeReportRangeItem

        Public Property Revenue As Decimal

        Public Property ReportDate As DateTime
    End Class

    Public Class BikeReport

        Public Property BicyclesData As List(Of ChartsDemo.BikeReportItem)

        Public Property BicycleRangesData As List(Of ChartsDemo.BikeReportRangeItem)
    End Class

    Public Class SalesProductData

#Region "Nested classes: ProductItemBase, SaleItemBase, SaleItem"
        Private Class ProductItemBase

            Public Property Product As String

            Public Property Category As String

            Public Property Price As Decimal
        End Class

        Public Class SaleItemBase

            Public Property Category As String

            Public Property UnitsSold As Integer

            Public Property Revenue As Decimal

            Public Property UnitsSoldTarget As Integer

            <System.ComponentModel.DataAnnotations.DataTypeAttribute(System.ComponentModel.DataAnnotations.DataType.Currency)>
            Public Property RevenueTarget As Decimal

            <System.ComponentModel.DataAnnotations.DisplayFormatAttribute(DataFormatString:="p")>
            Public ReadOnly Property SalesDynamic As Single
                Get
                    Return CSng(((Me.Revenue - Me.RevenueTarget) / Me.Revenue))
                End Get
            End Property

            Public Property ReportDate As DateTime
        End Class

        Public Class SaleItem
            Inherits ChartsDemo.SalesProductData.SaleItemBase

            Public Property State As String

            Public Property Product As String

            <System.ComponentModel.DataAnnotations.DataTypeAttribute(System.ComponentModel.DataAnnotations.DataType.Currency)>
            Public Property Price As Decimal
        End Class

#End Region
        Private ReadOnly rnd As System.Random = New System.Random(1)

        Private ReadOnly bikeCategoriesField As System.Collections.Generic.List(Of String) = New System.Collections.Generic.List(Of String)() From {"Mountain", "Hybrid/Cross", "Road", "Comfort", "Youth", "Cruiser", "Electric"}

        Public ReadOnly Property BikeCategories As List(Of String)
            Get
                Return Me.bikeCategoriesField
            End Get
        End Property

        Private Function LoadData() As DataSet
            Dim ds As System.Data.DataSet = New System.Data.DataSet()
            Dim uri As System.Uri = New System.Uri(String.Format("/{0};component/Data/DashboardSales.xml", DevExpress.Utils.AssemblyHelper.GetPartialName(GetType(ChartsDemo.SalesProductData).Assembly)), System.UriKind.RelativeOrAbsolute)
            ds.ReadXml(System.Windows.Application.GetResourceStream(CType((uri), System.Uri)).Stream)
            Return ds
        End Function

        Private Function CreateProductBase(ByVal dataRow As System.Data.DataRow, ByVal categoryName As String) As Object
            Return New ChartsDemo.SalesProductData.ProductItemBase() With {.Price = dataRow.Field(Of Decimal)("ListPrice"), .Product = dataRow.Field(Of String)("Name"), .Category = categoryName}
        End Function

        Private Function GenerateData(ByVal regions As System.Data.DataRowCollection, ByVal products As System.Collections.Generic.IEnumerable(Of ChartsDemo.SalesProductData.ProductItemBase)) As List(Of ChartsDemo.SalesProductData.SaleItem)
            Dim totalSales As System.Collections.Generic.List(Of ChartsDemo.SalesProductData.SaleItem) = New System.Collections.Generic.List(Of ChartsDemo.SalesProductData.SaleItem)()
            For Each region As System.Data.DataRow In regions
                Dim state As String = CStr(region("Region"))
                Dim year As Integer = System.DateTime.Today.Year - 1
                For month As Integer = 1 To 12
                    For Each product As ChartsDemo.SalesProductData.ProductItemBase In products
                        Dim tsItem As ChartsDemo.SalesProductData.SaleItem = New ChartsDemo.SalesProductData.SaleItem With {.State = state, .Category = product.Category, .Product = product.Product, .Price = product.Price}
                        Dim dt As System.DateTime = New System.DateTime(year, month, 1)
                        Dim uSold As Integer = Me.GetUnitsSold(product.Category)
                        Dim uSoldTarget As Integer = uSold + Me.rnd.[Next](-CInt((uSold * 0.2)), CInt((uSold * 0.2)))
                        Dim rev As Decimal = uSold * product.Price
                        Dim revTarget As Decimal = uSoldTarget * product.Price
                        tsItem.Revenue = rev
                        tsItem.RevenueTarget = revTarget
                        tsItem.UnitsSold = uSold
                        tsItem.UnitsSoldTarget = uSoldTarget
                        tsItem.ReportDate = dt
                        totalSales.Add(tsItem)
                    Next
                Next
            Next

            Return totalSales
        End Function

        Private Function GetUnitsSold(ByVal category As String) As Integer
            Dim max As Integer = If(category.Equals("Bikes"), 50, 250)
            Return Me.rnd.[Next](1, max)
        End Function

        Public Function GetSalesData() As List(Of ChartsDemo.SalesProductData.SaleItem)
            Dim dataSet As System.Data.DataSet = Me.LoadData()
            Dim products As System.Data.DataTable = dataSet.Tables("Products")
            Dim categories As System.Data.DataTable = dataSet.Tables("Categories")
            Dim regions As System.Data.DataTable = dataSet.Tables("Regions")
            Dim items = From t1 In products.AsEnumerable() Join t2 In categories.AsEnumerable() On t1("CategoryID") Equals t2("CategoryID") Select Me.CreateProductBase(t1, CStr(t2("CategoryName")))
            Return Me.GenerateData(regions.Rows, items.Cast(Of ChartsDemo.SalesProductData.ProductItemBase)())
        End Function

        Public Function GenerateBicyclesReport() As BikeReport
            Dim result As System.Collections.Generic.List(Of ChartsDemo.BikeReportItem) = New System.Collections.Generic.List(Of ChartsDemo.BikeReportItem)()
            Dim rangeData As System.Collections.Generic.List(Of ChartsDemo.BikeReportRangeItem) = New System.Collections.Generic.List(Of ChartsDemo.BikeReportRangeItem)()
            Dim year As Integer = System.DateTime.Today.Year - 1
            Dim startDate As System.DateTime = New System.DateTime(year, 1, 1)
            Dim averageMonthSold As Integer = 1700
            Dim averagePrice As Decimal = 900
            Dim [date] As System.DateTime = startDate
            For day As Integer = 1 To 365 Step 7
                Dim revenue As Decimal = 0
                Dim minDay As Integer = Me.rnd.[Next](100, 200)
                Dim maxDay As Integer = Me.rnd.[Next](250, 300)
                [date] = startDate.AddDays(day)
                For i As Integer = 0 To Me.BikeCategories.Count - 1
                    Dim category As String = Me.BikeCategories(i)
                    Dim deltaCorrection As Double = 2 * Me.rnd.NextDouble() + 0.2
                    Dim tsItem As ChartsDemo.BikeReportItem = New ChartsDemo.BikeReportItem With {.Category = category}
                    Dim correction As Double = 22 - i * 3 - Me.rnd.NextDouble()
                    If day > minDay AndAlso day < maxDay Then correction += deltaCorrection
                    If day > maxDay Then correction -= deltaCorrection
                    Dim uSold As Integer = CInt((averageMonthSold * correction / 100.0))
                    Dim uSoldTarget As Integer = uSold + Me.rnd.[Next](-CInt((uSold * 0.2)), CInt((uSold * 0.2)))
                    Dim rev As Decimal = uSold * averagePrice
                    Dim revTarget As Decimal = uSoldTarget * averagePrice
                    tsItem.Revenue = rev
                    tsItem.RevenueTarget = revTarget
                    tsItem.UnitsSold = uSold
                    tsItem.UnitsSoldTarget = uSoldTarget
                    tsItem.ReportDate = [date]
                    revenue += rev
                    result.Add(tsItem)
                Next

                rangeData.Add(New ChartsDemo.BikeReportRangeItem() With {.ReportDate = [date], .Revenue = revenue})
            Next

            Return New ChartsDemo.BikeReport() With {.BicyclesData = result, .BicycleRangesData = rangeData}
        End Function
    End Class
End Namespace
