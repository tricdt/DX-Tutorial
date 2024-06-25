Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Data
Imports System.Linq
Imports System.Windows
Imports DevExpress.Utils

Namespace CommonChartsDemo

    Public Class BikeReportItem
        Inherits CommonChartsDemo.SalesProductData.SaleItemBase

    End Class

    Public Class BikeReportRangeItem

        Public Property Revenue As Decimal

        Public Property ReportDate As DateTime
    End Class

    Public Class BikeReport

        Public Property BicyclesData As List(Of CommonChartsDemo.BikeReportItem)

        Public Property BicycleRangesData As List(Of CommonChartsDemo.BikeReportRangeItem)
    End Class

    Public Class SalesProductData

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
            Inherits CommonChartsDemo.SalesProductData.SaleItemBase

            Public Property State As String

            Public Property Product As String

            <System.ComponentModel.DataAnnotations.DataTypeAttribute(System.ComponentModel.DataAnnotations.DataType.Currency)>
            Public Property Price As Decimal
        End Class

        Private Shared rnd As System.Random = New System.Random()

        Private Shared dataField As System.Collections.Generic.List(Of CommonChartsDemo.SalesProductData.SaleItem)

        Private Shared bicyclesReportField As CommonChartsDemo.BikeReport

        Public Shared ReadOnly Property Data As List(Of CommonChartsDemo.SalesProductData.SaleItem)
            Get
                Return If(CommonChartsDemo.SalesProductData.dataField, Function()
                    CommonChartsDemo.SalesProductData.dataField = CommonChartsDemo.SalesProductData.GetSalesData()
                    Return CommonChartsDemo.SalesProductData.dataField
                End Function())
            End Get
        End Property

        Public Shared ReadOnly Property BicyclesReport As BikeReport
            Get
                Return If(CommonChartsDemo.SalesProductData.bicyclesReportField, Function()
                    CommonChartsDemo.SalesProductData.bicyclesReportField = CommonChartsDemo.SalesProductData.GenerateBicyclesReport()
                    Return CommonChartsDemo.SalesProductData.bicyclesReportField
                End Function())
            End Get
        End Property

        Public Shared BikeCategories As System.Collections.Generic.List(Of String) = New System.Collections.Generic.List(Of String)() From {"Mountain", "Hybrid/Cross", "Road", "Comfort", "Youth", "Cruiser", "Electric"}

        Private Shared Function LoadData() As DataSet
            Dim ds As System.Data.DataSet = New System.Data.DataSet()
            Dim uri As System.Uri = New System.Uri(String.Format("/{0};component/Data/DashboardSales.xml", DevExpress.Utils.AssemblyHelper.GetPartialName(GetType(CommonChartsDemo.SalesProductData).Assembly)), System.UriKind.RelativeOrAbsolute)
            ds.ReadXml(System.Windows.Application.GetResourceStream(CType((uri), System.Uri)).Stream)
            Return ds
        End Function

        Private Shared Function CreateProductBase(ByVal dataRow As System.Data.DataRow, ByVal categoryName As String) As Object
            Return New CommonChartsDemo.SalesProductData.ProductItemBase() With {.Price = dataRow.Field(Of Decimal)("ListPrice"), .Product = dataRow.Field(Of String)("Name"), .Category = categoryName}
        End Function

        Private Shared Function GenerateData(ByVal regions As System.Data.DataRowCollection, ByVal products As System.Collections.Generic.IEnumerable(Of CommonChartsDemo.SalesProductData.ProductItemBase)) As List(Of CommonChartsDemo.SalesProductData.SaleItem)
            Dim totalSales As System.Collections.Generic.List(Of CommonChartsDemo.SalesProductData.SaleItem) = New System.Collections.Generic.List(Of CommonChartsDemo.SalesProductData.SaleItem)()
            For Each region As System.Data.DataRow In regions
                Dim state As String = CStr(region("Region"))
                Dim year As Integer = System.DateTime.Today.Year - 1
                For month As Integer = 1 To 12
                    For Each product As CommonChartsDemo.SalesProductData.ProductItemBase In products
                        Dim tsItem As CommonChartsDemo.SalesProductData.SaleItem = New CommonChartsDemo.SalesProductData.SaleItem With {.State = state, .Category = product.Category, .Product = product.Product, .Price = product.Price}
                        Dim dt As System.DateTime = New System.DateTime(year, month, 1)
                        Dim uSold As Integer = CommonChartsDemo.SalesProductData.GetUnitsSold(product.Category)
                        Dim uSoldTarget As Integer = uSold + CommonChartsDemo.SalesProductData.rnd.[Next](-CInt((uSold * 0.2)), CInt((uSold * 0.2)))
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

        Private Shared Function GetUnitsSold(ByVal category As String) As Integer
            Dim max As Integer = If(category.Equals("Bikes"), 50, 250)
            Return CommonChartsDemo.SalesProductData.rnd.[Next](1, max)
        End Function

        Private Shared Function GetState(ByVal region As System.Data.DataRow) As String
            Return CStr(region("Region"))
        End Function

        Private Shared Function GetSalesData() As List(Of CommonChartsDemo.SalesProductData.SaleItem)
            Dim dataSet As System.Data.DataSet = CommonChartsDemo.SalesProductData.LoadData()
            Dim products As System.Data.DataTable = dataSet.Tables("Products")
            Dim categories As System.Data.DataTable = dataSet.Tables("Categories")
            Dim regions As System.Data.DataTable = dataSet.Tables("Regions")
            Dim items = From t1 In products.AsEnumerable() Join t2 In categories.AsEnumerable() On t1("CategoryID") Equals t2("CategoryID") Select CommonChartsDemo.SalesProductData.CreateProductBase(t1, CStr(t2("CategoryName")))
            Return CommonChartsDemo.SalesProductData.GenerateData(regions.Rows, items.Cast(Of CommonChartsDemo.SalesProductData.ProductItemBase)())
        End Function

        Private Shared Function GenerateBicyclesReport() As BikeReport
            Dim result As System.Collections.Generic.List(Of CommonChartsDemo.BikeReportItem) = New System.Collections.Generic.List(Of CommonChartsDemo.BikeReportItem)()
            Dim rangeData As System.Collections.Generic.List(Of CommonChartsDemo.BikeReportRangeItem) = New System.Collections.Generic.List(Of CommonChartsDemo.BikeReportRangeItem)()
            Dim year As Integer = System.DateTime.Today.Year - 1
            Dim startDate As System.DateTime = New System.DateTime(year, 1, 1)
            Dim averageMonthSold As Integer = 1700
            Dim averagePrice As Decimal = 900
            Dim [date] As System.DateTime = startDate
            For day As Integer = 1 To 365 Step 7
                Dim revenue As Decimal = 0
                Dim minDay As Integer = CommonChartsDemo.SalesProductData.rnd.[Next](100, 200)
                Dim maxDay As Integer = CommonChartsDemo.SalesProductData.rnd.[Next](250, 300)
                [date] = startDate.AddDays(day)
                For i As Integer = 0 To CommonChartsDemo.SalesProductData.BikeCategories.Count - 1
                    Dim category As String = CommonChartsDemo.SalesProductData.BikeCategories(i)
                    Dim deltaCorrection As Double = 2 * CommonChartsDemo.SalesProductData.rnd.NextDouble() + 0.2
                    Dim tsItem As CommonChartsDemo.BikeReportItem = New CommonChartsDemo.BikeReportItem With {.Category = category}
                    Dim correction As Double = 22 - i * 3 - CommonChartsDemo.SalesProductData.rnd.NextDouble()
                    If day > minDay AndAlso day < maxDay Then correction += deltaCorrection
                    If day > maxDay Then correction -= deltaCorrection
                    Dim uSold As Integer = CInt((averageMonthSold * correction / 100.0))
                    Dim uSoldTarget As Integer = uSold + CommonChartsDemo.SalesProductData.rnd.[Next](-CInt((uSold * 0.2)), CInt((uSold * 0.2)))
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

                rangeData.Add(New CommonChartsDemo.BikeReportRangeItem() With {.ReportDate = [date], .Revenue = revenue})
            Next

            Return New CommonChartsDemo.BikeReport() With {.BicyclesData = result, .BicycleRangesData = rangeData}
        End Function
    End Class
End Namespace
