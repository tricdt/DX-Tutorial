Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.DemoData.Models

Namespace DevExpress.Diagram.Demos

    Public Module OrderDataGenerator

        Private Function ExtractCustomers(ByVal count As Integer) As List(Of DevExpress.Diagram.Demos.CustomerData)
            Dim customers = DevExpress.DemoData.Models.NWindContext.Create().Customers.Take(count).ToList()
            Dim customerData = New System.Collections.Generic.List(Of DevExpress.Diagram.Demos.CustomerData)(count)
            For Each row In customers
                customerData.Add(New DevExpress.Diagram.Demos.CustomerData(row.ContactName, row.CompanyName, row.Phone, row.City))
            Next

            Return customerData
        End Function

        Private Function ExtractCategoryDataList(ByVal count As Integer) As List(Of DevExpress.Diagram.Demos.CategoryData)
            Dim categoryData As System.Collections.Generic.List(Of DevExpress.Diagram.Demos.CategoryData) = New System.Collections.Generic.List(Of DevExpress.Diagram.Demos.CategoryData)(count)
            Dim categories = DevExpress.DemoData.Models.NWindContext.Create().Categories.Take(count).ToList()
            For Each row In categories
                categoryData.Add(New DevExpress.Diagram.Demos.CategoryData(row.CategoryName, row.Description, row.Icon17))
            Next

            Return categoryData
        End Function

        Private Function ExtractProductDataList(ByVal categoriesList As System.Collections.Generic.List(Of DevExpress.Diagram.Demos.CategoryData)) As List(Of DevExpress.Diagram.Demos.ProductData)
            Dim productData As System.Collections.Generic.List(Of DevExpress.Diagram.Demos.ProductData) = New System.Collections.Generic.List(Of DevExpress.Diagram.Demos.ProductData)()
            Dim categoryProducts = DevExpress.DemoData.Models.NWindContext.Create().CategoryProducts.ToList()
            productData.Capacity = categoryProducts.Count
            Dim rand As System.Random = New System.Random()
            For Each row In categoryProducts
                Dim category = DevExpress.Diagram.Demos.OrderDataGenerator.FindCategory(categoriesList, row.CategoryName)
                If category IsNot Nothing Then productData.Add(New DevExpress.Diagram.Demos.ProductData(row.ProductName, category, CDec((rand.[Next](20) + rand.[Next](99))) / 100D))
            Next

            Return productData
        End Function

        Private Function FindCategory(ByVal categoriesList As System.Collections.Generic.List(Of DevExpress.Diagram.Demos.CategoryData), ByVal name As String) As CategoryData
            For Each category As DevExpress.Diagram.Demos.CategoryData In categoriesList
                If Equals(category.Name, name) Then Return category
            Next

            Return Nothing
        End Function

        Private Function GenerateOrders(ByVal orderCount As Integer, ByVal customerCount As Integer, ByVal categoryCount As Integer) As List(Of DevExpress.Diagram.Demos.OrderData)
            Dim result As System.Collections.Generic.List(Of DevExpress.Diagram.Demos.OrderData) = New System.Collections.Generic.List(Of DevExpress.Diagram.Demos.OrderData)(orderCount)
            Dim customers As System.Collections.Generic.List(Of DevExpress.Diagram.Demos.CustomerData) = DevExpress.Diagram.Demos.OrderDataGenerator.ExtractCustomers(customerCount)
            Dim categoriesList As System.Collections.Generic.List(Of DevExpress.Diagram.Demos.CategoryData) = DevExpress.Diagram.Demos.OrderDataGenerator.ExtractCategoryDataList(categoryCount)
            Dim productsList As System.Collections.Generic.List(Of DevExpress.Diagram.Demos.ProductData) = DevExpress.Diagram.Demos.OrderDataGenerator.ExtractProductDataList(categoriesList)
            Dim rand As System.Random = New System.Random()
            Dim generateCountPerCent As Integer = orderCount \ 100
            For i As Integer = 0 To orderCount - 1
                Dim randomProduct As DevExpress.Diagram.Demos.ProductData = productsList(rand.[Next](productsList.Count))
                Dim data As DevExpress.Diagram.Demos.OrderData = New DevExpress.Diagram.Demos.OrderData(orderId:=i, orderDate:=System.DateTime.Today.Subtract(System.TimeSpan.FromDays(rand.[Next](180))), customer:=customers(rand.[Next](customers.Count)), quantity:=rand.[Next](200) + 1, productCategory:=randomProduct.Category, productName:=randomProduct.Name, price:=randomProduct.Price)
                result.Add(data)
            Next

            Return result
        End Function

        Public Function GenerateProductFlowInfo() As ProductFlowInfo
            Dim orders = DevExpress.Diagram.Demos.OrderDataGenerator.GenerateOrders(CInt((50)), CInt((4)), CInt((5))).ToArray()
            Dim customers = orders.[Select](Function(x) x.Customer).Distinct().ToArray()
            Dim categories = orders.[Select](Function(x) x.Category).Distinct().ToArray()
            Dim groupedOrders = orders.GroupBy(Function(x) New With {x.Customer, x.Category}).ToArray()
            Dim minCount = groupedOrders.Min(Function(x) x.Sum(Function(y) y.Quantity))
            Dim maxCount = groupedOrders.Max(Function(x) x.Sum(Function(y) y.Quantity))
            Const minWeight As Single = 1, maxWeight As Single = 8
            Dim productFlows = orders.GroupBy(Function(x) New With {x.Customer, x.Category}).[Select](Function(x) New DevExpress.Diagram.Demos.ProductFlowData(x.Key.Customer, x.Key.Category, minWeight + (maxWeight - minWeight) * (x.Sum(Function(d) d.Quantity) - minCount) / (maxCount - minCount))).ToArray()
            Return New DevExpress.Diagram.Demos.ProductFlowInfo(productFlows, orders, customers, categories)
        End Function
    End Module

    Public Class ProductFlowInfo

        Private _Items As Object(), _ProductFlows As DevExpress.Diagram.Demos.ProductFlowData(), _Orders As DevExpress.Diagram.Demos.OrderData(), _Customers As DevExpress.Diagram.Demos.CustomerData(), _Categories As DevExpress.Diagram.Demos.CategoryData()

        Public Sub New(ByVal productFlows As DevExpress.Diagram.Demos.ProductFlowData(), ByVal orders As DevExpress.Diagram.Demos.OrderData(), ByVal customers As DevExpress.Diagram.Demos.CustomerData(), ByVal categories As DevExpress.Diagram.Demos.CategoryData())
            Me.ProductFlows = productFlows
            Me.Orders = orders
            Me.Customers = customers
            Me.Categories = categories
            Me.Items = Me.Customers.Cast(Of Object)().Concat(Me.Categories).ToArray()
        End Sub

        Public Property Items As Object()
            Get
                Return _Items
            End Get

            Private Set(ByVal value As Object())
                _Items = value
            End Set
        End Property

        Public Property ProductFlows As DevExpress.Diagram.Demos.ProductFlowData()
            Get
                Return _ProductFlows
            End Get

            Private Set(ByVal value As DevExpress.Diagram.Demos.ProductFlowData())
                _ProductFlows = value
            End Set
        End Property

        Public Property Orders As DevExpress.Diagram.Demos.OrderData()
            Get
                Return _Orders
            End Get

            Private Set(ByVal value As DevExpress.Diagram.Demos.OrderData())
                _Orders = value
            End Set
        End Property

        Public Property Customers As DevExpress.Diagram.Demos.CustomerData()
            Get
                Return _Customers
            End Get

            Private Set(ByVal value As DevExpress.Diagram.Demos.CustomerData())
                _Customers = value
            End Set
        End Property

        Public Property Categories As DevExpress.Diagram.Demos.CategoryData()
            Get
                Return _Categories
            End Get

            Private Set(ByVal value As DevExpress.Diagram.Demos.CategoryData())
                _Categories = value
            End Set
        End Property
    End Class

    Public Class CategoryData

        Private _Name As String, _Description As String, _Picture As Byte()

        Public Property Name As String
            Get
                Return _Name
            End Get

            Private Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Property Description As String
            Get
                Return _Description
            End Get

            Private Set(ByVal value As String)
                _Description = value
            End Set
        End Property

        Public Property Picture As Byte()
            Get
                Return _Picture
            End Get

            Private Set(ByVal value As Byte())
                _Picture = value
            End Set
        End Property

        Public Sub New(ByVal name As String, ByVal description As String, ByVal picture As Byte())
            Me.Name = name
            Me.Description = description
            Me.Picture = picture
        End Sub

        Public Overrides Function ToString() As String
            Return Me.Name
        End Function
    End Class

    Public Class ProductData

        Private _Name As String, _Category As CategoryData, _Price As Decimal

        Public Sub New(ByVal name As String, ByVal category As DevExpress.Diagram.Demos.CategoryData, ByVal price As Decimal)
            Me.Name = name
            Me.Category = category
            Me.Price = price
        End Sub

        Public Property Name As String
            Get
                Return _Name
            End Get

            Private Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Property Category As CategoryData
            Get
                Return _Category
            End Get

            Private Set(ByVal value As CategoryData)
                _Category = value
            End Set
        End Property

        Public Property Price As Decimal
            Get
                Return _Price
            End Get

            Private Set(ByVal value As Decimal)
                _Price = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return Me.Name
        End Function
    End Class

    Public Class CustomerData

        Private _Name As String, _CompanyName As String, _PhoneNumber As String, _City As String

        Public Property Name As String
            Get
                Return _Name
            End Get

            Private Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Property CompanyName As String
            Get
                Return _CompanyName
            End Get

            Private Set(ByVal value As String)
                _CompanyName = value
            End Set
        End Property

        Public Property PhoneNumber As String
            Get
                Return _PhoneNumber
            End Get

            Private Set(ByVal value As String)
                _PhoneNumber = value
            End Set
        End Property

        Public Property City As String
            Get
                Return _City
            End Get

            Private Set(ByVal value As String)
                _City = value
            End Set
        End Property

        Public Sub New(ByVal name As String, ByVal companyName As String, ByVal phoneNumber As String, ByVal city As String)
            Me.Name = name
            Me.CompanyName = companyName
            Me.PhoneNumber = phoneNumber
            Me.City = city
        End Sub

        Public Overrides Function ToString() As String
            Return Me.CompanyName & Global.Microsoft.VisualBasic.Constants.vbCrLf & Global.Microsoft.VisualBasic.Constants.vbCrLf & Me.City & Global.Microsoft.VisualBasic.Constants.vbCrLf & Me.PhoneNumber
        End Function
    End Class

    Public Class OrderData

        Private _OrderId As Integer, _Customer As CustomerData, _Category As CategoryData, _ProductName As String, _OrderDate As DateTime, _Quantity As Integer

        Public Sub New(ByVal orderId As Integer, ByVal customer As DevExpress.Diagram.Demos.CustomerData, ByVal productCategory As DevExpress.Diagram.Demos.CategoryData, ByVal productName As String, ByVal orderDate As System.DateTime, ByVal quantity As Integer, ByVal price As Decimal)
            Me.OrderId = orderId
            Me.Customer = customer
            Me.Category = productCategory
            Me.ProductName = productName
            Me.OrderDate = orderDate
            Me.Quantity = quantity
            Me.Price = price
        End Sub

        Public Property OrderId As Integer
            Get
                Return _OrderId
            End Get

            Private Set(ByVal value As Integer)
                _OrderId = value
            End Set
        End Property

        Public Property Customer As CustomerData
            Get
                Return _Customer
            End Get

            Private Set(ByVal value As CustomerData)
                _Customer = value
            End Set
        End Property

        Public Property Category As CategoryData
            Get
                Return _Category
            End Get

            Private Set(ByVal value As CategoryData)
                _Category = value
            End Set
        End Property

        Public Property ProductName As String
            Get
                Return _ProductName
            End Get

            Private Set(ByVal value As String)
                _ProductName = value
            End Set
        End Property

        Public Property OrderDate As DateTime
            Get
                Return _OrderDate
            End Get

            Private Set(ByVal value As DateTime)
                _OrderDate = value
            End Set
        End Property

        Public Property Quantity As Integer
            Get
                Return _Quantity
            End Get

            Private Set(ByVal value As Integer)
                _Quantity = value
            End Set
        End Property

        Public Property Price As Decimal
    End Class

    Public Class ProductFlowData

        Private _Customer As CustomerData, _Category As CategoryData, _Weight As Single

        Public Sub New(ByVal customer As DevExpress.Diagram.Demos.CustomerData, ByVal category As DevExpress.Diagram.Demos.CategoryData, ByVal weight As Single)
            Me.Customer = customer
            Me.Category = category
            Me.Weight = weight
        End Sub

        Public Property Customer As CustomerData
            Get
                Return _Customer
            End Get

            Private Set(ByVal value As CustomerData)
                _Customer = value
            End Set
        End Property

        Public Property Category As CategoryData
            Get
                Return _Category
            End Get

            Private Set(ByVal value As CategoryData)
                _Category = value
            End Set
        End Property

        Public Property Weight As Single
            Get
                Return _Weight
            End Get

            Private Set(ByVal value As Single)
                _Weight = value
            End Set
        End Property
    End Class
End Namespace
