Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Xml
Imports DevExpress.DemoData.Helpers
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB

Namespace PivotGridDemo.Helpers

    Public Module DatabaseHelper

#Region "Fields"
        Private ReadOnly random As System.Random = New System.Random()

        Private ReadOnly FirstNames As String() = {"Julia", "Stephanie", "Alex", "John", "Curtis", "Keith", "Timothy", "Jack", "Miranda", "Alice"}

        Private ReadOnly LastNames As String() = {"Black", "White", "Brown", "Smith", "Cooper", "Parker", "Walker", "Hunter", "Burton", "Douglas", "Fox", "Simpson"}

        Private ReadOnly Adjectives As String() = {"Ancient", "Modern", "Mysterious", "Elegant", "Red", "Green", "Blue", "Amazing", "Wonderful", "Astonishing", "Lovely", "Beautiful", "Inexpensive", "Famous", "Magnificent", "Fancy"}

        Private ReadOnly ProductNames As String() = {"Ice Cubes", "Bicycle", "Desk", "Hamburger", "Notebook", "Tea", "Cellphone", "Butter", "Frying Pan", "Napkin", "Armchair", "Chocolate", "Yoghurt", "Statuette", "Keychain"}

        Private ReadOnly CategoryNames As String() = {"Business", "Presents", "Accessories", "Home", "Hobby"}

        Private ReadOnly CustomersNames As String()

        Private ReadOnly SalesPersonsNames As String()

        Private ReadOnly Products As PivotGridDemo.ProductDataRecord()

#End Region
        Sub New()
            PivotGridDemo.Helpers.DatabaseHelper.CustomersNames = PivotGridDemo.Helpers.DatabaseHelper.GenerateUniqueValues(Of System.[String])(CInt((PivotGridDemo.Helpers.DatabaseHelper.random.[Next](CInt((40)), CInt((50))))), CType((AddressOf PivotGridDemo.Helpers.DatabaseHelper.GeneratePersonName), System.Func(Of System.[String]))).ToArray()
            PivotGridDemo.Helpers.DatabaseHelper.SalesPersonsNames = PivotGridDemo.Helpers.DatabaseHelper.GenerateUniqueValues(Of System.[String])(CInt((PivotGridDemo.Helpers.DatabaseHelper.random.[Next](CInt((40)), CInt((50))))), CType((AddressOf PivotGridDemo.Helpers.DatabaseHelper.GeneratePersonName), System.Func(Of System.[String]))).ToArray()
            PivotGridDemo.Helpers.DatabaseHelper.Products = PivotGridDemo.Helpers.DatabaseHelper.GenerateProducts()
        End Sub

#Region "Public"
        Public Function CreateValues() As Dictionary(Of String, Object)
            Dim dict = New System.Collections.Generic.Dictionary(Of String, Object)()
            Dim product = PivotGridDemo.Helpers.DatabaseHelper.GetProduct()
            dict.Add("OrderDate", PivotGridDemo.Helpers.DatabaseHelper.GetOrderDate())
            dict.Add("Quantity", PivotGridDemo.Helpers.DatabaseHelper.GetQuantity())
            dict.Add("UnitPrice", PivotGridDemo.Helpers.DatabaseHelper.GetProductPrice(product))
            dict.Add("CustomerName", PivotGridDemo.Helpers.DatabaseHelper.GetCustomerName())
            dict.Add("ProductName", product.ProductName)
            dict.Add("CategoryName", product.CategoryName)
            dict.Add("SalesPersonName", PivotGridDemo.Helpers.DatabaseHelper.GetSalesPersonName())
            Return dict
        End Function

        Public Function GetOrderDate() As DateTime
            Return New System.DateTime(PivotGridDemo.Helpers.DatabaseHelper.random.[Next](2007, 2015), PivotGridDemo.Helpers.DatabaseHelper.random.[Next](1, 13), PivotGridDemo.Helpers.DatabaseHelper.random.[Next](1, 28))
        End Function

        Public Function GetQuantity() As Integer
            Return PivotGridDemo.Helpers.DatabaseHelper.random.[Next](1, 100)
        End Function

        Public Function GetProductPrice(ByVal product As PivotGridDemo.ProductDataRecord) As Decimal
            Dim price = product.UnitPrice * CDec((0.5 + PivotGridDemo.Helpers.DatabaseHelper.random.NextDouble()))
            Return System.Math.Round(price, 2)
        End Function

        Public Function GetProduct() As ProductDataRecord
            Return PivotGridDemo.Helpers.DatabaseHelper.Products(PivotGridDemo.Helpers.DatabaseHelper.random.[Next](PivotGridDemo.Helpers.DatabaseHelper.Products.Length))
        End Function

        Public Function GetCustomerName() As String
            Return PivotGridDemo.Helpers.DatabaseHelper.CustomersNames(PivotGridDemo.Helpers.DatabaseHelper.random.[Next](PivotGridDemo.Helpers.DatabaseHelper.CustomersNames.Length))
        End Function

        Public Function GetSalesPersonName() As String
            Return PivotGridDemo.Helpers.DatabaseHelper.SalesPersonsNames(PivotGridDemo.Helpers.DatabaseHelper.random.[Next](PivotGridDemo.Helpers.DatabaseHelper.SalesPersonsNames.Length))
        End Function

#End Region
#Region "Private"
        Private Function GenerateUniqueValues(Of T)(ByVal count As Integer, ByVal generateValue As System.Func(Of T)) As List(Of T)
            Dim values = New System.Collections.Generic.HashSet(Of T)()
            While values.Count < count
                Dim value = generateValue()
                If Not values.Contains(value) Then values.Add(value)
            End While

            Return values.ToList()
        End Function

        Private Function GenerateProducts() As PivotGridDemo.ProductDataRecord()
            Return GenerateUniqueValues(PivotGridDemo.Helpers.DatabaseHelper.random.[Next](80, 100), New Global.System.Func(Of System.String)(AddressOf PivotGridDemo.Helpers.DatabaseHelper.GenerateProductName)).[Select](Function(productName) New PivotGridDemo.ProductDataRecord With {.ProductName = productName, .UnitPrice = PivotGridDemo.Helpers.DatabaseHelper.random.[Next](10, 500), .CategoryName = PivotGridDemo.Helpers.DatabaseHelper.GenerateCategoryName()}).ToArray()
        End Function

        Private Function GenerateCategoryName() As String
            Return PivotGridDemo.Helpers.DatabaseHelper.CategoryNames(PivotGridDemo.Helpers.DatabaseHelper.random.[Next](PivotGridDemo.Helpers.DatabaseHelper.CategoryNames.Length))
        End Function

        Private Function GeneratePersonName() As String
            Return PivotGridDemo.Helpers.DatabaseHelper.FirstNames(PivotGridDemo.Helpers.DatabaseHelper.random.[Next](PivotGridDemo.Helpers.DatabaseHelper.FirstNames.Length)) & " " & PivotGridDemo.Helpers.DatabaseHelper.LastNames(PivotGridDemo.Helpers.DatabaseHelper.random.[Next](PivotGridDemo.Helpers.DatabaseHelper.LastNames.Length))
        End Function

        Private Function GenerateProductName() As String
            Return PivotGridDemo.Helpers.DatabaseHelper.Adjectives(PivotGridDemo.Helpers.DatabaseHelper.random.[Next](PivotGridDemo.Helpers.DatabaseHelper.Adjectives.Length)) & " " & PivotGridDemo.Helpers.DatabaseHelper.ProductNames(PivotGridDemo.Helpers.DatabaseHelper.random.[Next](PivotGridDemo.Helpers.DatabaseHelper.ProductNames.Length))
        End Function
#End Region
    End Module
End Namespace
