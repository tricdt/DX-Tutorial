Imports DevExpress.DemoData.Models
Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace GridDemo

    Public Module GroupIntervalData

        Public rnd As System.Random = New System.Random()

        Public ReadOnly Property Invoices As List(Of DevExpress.DemoData.Models.Invoice)
            Get
                Dim lInvoices = DevExpress.DemoData.Models.NWindContext.Create().Invoices.ToList()
                For Each invoice In lInvoices
                    invoice.OrderDate = GridDemo.GroupIntervalData.GetDate(True)
                Next

                Return lInvoices
            End Get
        End Property

        Public ReadOnly Property Products As List(Of GridDemo.ProductInfo)
            Get
                Const rowCount As Integer = 1000
                Dim lProducts = DevExpress.DemoData.Models.NWindContext.Create().Products.ToList()
                Dim shuffledProducts = New System.Collections.Generic.List(Of GridDemo.ProductInfo)()
                For i As Integer = 0 To rowCount - 1
                    Dim product As DevExpress.DemoData.Models.Product = lProducts(GridDemo.GroupIntervalData.rnd.[Next](lProducts.Count - 1))
                    shuffledProducts.Add(New GridDemo.ProductInfo() With {.ProductName = product.ProductName, .UnitPrice = product.UnitPrice, .Count = GridDemo.GroupIntervalData.GetCount(), .OrderDate = GridDemo.GroupIntervalData.GetDate(False)})
                Next

                Return shuffledProducts
            End Get
        End Property

        Private Function GetDate(ByVal range As Boolean) As DateTime
            Dim result As System.DateTime = System.DateTime.Now
            Dim r As Integer = GridDemo.GroupIntervalData.rnd.[Next](20)
            If range Then
                If r > 1 Then result = result.AddDays(GridDemo.GroupIntervalData.rnd.[Next](80) - 40)
                If r > 18 Then result = result.AddMonths(GridDemo.GroupIntervalData.rnd.[Next](18))
            Else
                result = result.AddDays(GridDemo.GroupIntervalData.rnd.[Next](r * 30) - r * 15)
            End If

            Return result
        End Function

        Private Function GetCount() As Decimal
            Return GridDemo.GroupIntervalData.rnd.[Next](50) + 10
        End Function
    End Module

    Public Class ProductInfo

        Public Property ProductName As String

        Public Property UnitPrice As Decimal?

        Public Property Count As Decimal

        Public Property OrderDate As DateTime
    End Class
End Namespace
