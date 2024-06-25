Imports DevExpress.DemoData.Models
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Core.FilteringUI
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Linq.Expressions

Namespace EditorsDemo.FilterBehavior

    Public Class FilterBehaviorViewModelDataViewModel
        Inherits DevExpress.Mvvm.ViewModelBase

        Private _Categories As List(Of String)

        Public Property Products As List(Of EditorsDemo.FilterBehavior.ProductInfo)
            Get
                Return GetValue(Of System.Collections.Generic.List(Of EditorsDemo.FilterBehavior.ProductInfo))()
            End Get

            Private Set(ByVal value As List(Of EditorsDemo.FilterBehavior.ProductInfo))
                SetValue(value)
            End Set
        End Property

        Public Property ProductsCount As Integer
            Get
                Return GetValue(Of Integer)()
            End Get

            Private Set(ByVal value As Integer)
                SetValue(value)
            End Set
        End Property

        Public Property Categories As List(Of String)
            Get
                Return _Categories
            End Get

            Private Set(ByVal value As List(Of String))
                _Categories = value
            End Set
        End Property

        Public Sub New()
            Dim context = New DevExpress.DemoData.Models.NWindContext()
            Me.Categories = context.Categories.[Select](Function(category) category.CategoryName).ToList()
        End Sub

        <DevExpress.Mvvm.DataAnnotations.CommandAttribute>
        Public Sub ShowProducts()
            Me.Products = Me.GetProductsQuery().ToList()
        End Sub

        Friend Function GetCategoryUniqueValues() As IList(Of DevExpress.Xpf.Core.FilteringUI.CountedValue)
            Return EditorsDemo.FilterBehavior.FilterBehaviorViewModelDataViewModel.GetProductsQueryCore().GroupBy(Function(x) x.CategoryName).[Select](Function(x) New With {x.Key, .Count = x.Count()}).ToList().[Select](Function(x) New DevExpress.Xpf.Core.FilteringUI.CountedValue(x.Key, x.Count)).ToList()
        End Function

        Private filterExpression As System.Linq.Expressions.Expression(Of System.Func(Of EditorsDemo.FilterBehavior.ProductInfo, Boolean))

        Friend Sub SetFilter(ByVal filterExpression As System.Linq.Expressions.Expression(Of System.Func(Of EditorsDemo.FilterBehavior.ProductInfo, Boolean)))
            Me.filterExpression = filterExpression
            Me.ProductsCount = Me.GetProductsQuery().Count()
        End Sub

        Private Function GetProductsQuery() As IQueryable(Of EditorsDemo.FilterBehavior.ProductInfo)
            If Me.filterExpression Is Nothing Then Return System.Linq.Enumerable.Empty(Of EditorsDemo.FilterBehavior.ProductInfo)().AsQueryable()
            Return EditorsDemo.FilterBehavior.FilterBehaviorViewModelDataViewModel.GetProductsQueryCore().Where(Me.filterExpression)
        End Function

        Private Shared Function GetProductsQueryCore() As IQueryable(Of EditorsDemo.FilterBehavior.ProductInfo)
            Dim context = New DevExpress.DemoData.Models.NWindContext()
            Return context.Products.[Select](Function(product) New EditorsDemo.FilterBehavior.ProductInfo With {.ProductName = product.ProductName, .CategoryName = product.Category.CategoryName, .Discontinued = product.Discontinued, .CategoryPicture = product.Category.Icon25})
        End Function
    End Class
End Namespace
