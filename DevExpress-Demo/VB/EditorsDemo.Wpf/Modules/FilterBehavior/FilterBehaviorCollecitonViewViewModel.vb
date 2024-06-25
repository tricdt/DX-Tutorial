Imports DevExpress.DemoData.Models
Imports DevExpress.Mvvm
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Windows.Data

Namespace EditorsDemo.FilterBehavior

    Public Class FilterBehaviorCollecitonViewViewModel
        Inherits ViewModelBase

        Private _ProductsView As ICollectionView, _Categories As List(Of String)

        Public Property ProductsView As ICollectionView
            Get
                Return _ProductsView
            End Get

            Private Set(ByVal value As ICollectionView)
                _ProductsView = value
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
            Dim context = New NWindContext()
            Dim products = context.Products.[Select](Function(product) New With {product.ProductName, product.Category.CategoryName, product.Discontinued, .CategoryPicture = product.Category.Icon25}).ToList()
            ProductsView = New ListCollectionView(products)
            Categories = context.Categories.[Select](Function(category) category.CategoryName).ToList()
        End Sub
    End Class
End Namespace
