Imports DevExpress.DemoData.Models
Imports DevExpress.Mvvm
Imports System.Collections.Generic
Imports System.Linq
#If DXCORE3
using Microsoft.EntityFrameworkCore;
#Else
Imports System.Data.Entity

#End If
Namespace EditorsDemo.FilterBehavior

    Public Class FilterBehaviorListBoxEditViewModel
        Inherits ViewModelBase

        Private _Products As IList(Of DevExpress.DemoData.Models.Product), _Categories As List(Of String)

        Public Property Products As IList(Of Product)
            Get
                Return _Products
            End Get

            Private Set(ByVal value As IList(Of Product))
                _Products = value
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
            Products = context.Products.Include(Function(x) x.Category).ToList()
            Categories = context.Categories.[Select](Function(category) category.CategoryName).ToList()
        End Sub
    End Class
End Namespace
