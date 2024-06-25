Imports System.Collections.Generic
Imports DevExpress.Mvvm

Namespace ControlsDemo.BreadcrumbSamples

    Public Class SelfReferentialDataViewModel
        Inherits ViewModelBase

        Public Property Items As List(Of SelfRefDataItem)

        Public Sub New()
            Items = New List(Of SelfRefDataItem) From {New SelfRefDataItem() With {.Name = "Root item 1", .Id = -1}, New SelfRefDataItem() With {.Name = "Item 1.1", .Id = 1, .ParentId = -1}, New SelfRefDataItem() With {.Name = "Item 1.1.1", .Id = 3, .ParentId = 1}, New SelfRefDataItem() With {.Name = "Item 1.1.1.1", .Id = 5, .ParentId = 3}, New SelfRefDataItem() With {.Name = "Item 1.1.2", .Id = 4, .ParentId = 1}, New SelfRefDataItem() With {.Name = "Item 1.2", .Id = 2, .ParentId = -1}, New SelfRefDataItem() With {.Name = "Root item 2", .Id = -2}, New SelfRefDataItem() With {.Name = "Item 2.1", .Id = 6, .ParentId = -2}}
        End Sub
    End Class

    Public Class SelfRefDataItem

        Public Property Id As Integer

        Public Property Name As String

        Public Property ParentId As Integer?

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class
End Namespace
