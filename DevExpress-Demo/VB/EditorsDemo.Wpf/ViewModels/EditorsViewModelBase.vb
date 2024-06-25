Imports System.Collections.Generic
Imports DevExpress.DemoData
Imports DevExpress.DemoData.Models
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.DemoBase.DataClasses
Imports Employee = DevExpress.Xpf.DemoBase.DataClasses.Employee

Namespace EditorsDemo

    Public MustInherit Class EditorsViewModelBase
        Inherits DevExpress.Mvvm.ViewModelBase

        Private _Employees As IList(Of DevExpress.Xpf.DemoBase.DataClasses.Employee), _Products As IList(Of DevExpress.DemoData.Models.Product)

        Protected Shared DataLoader As DevExpress.DemoData.NWindDataLoader

        Shared Sub New()
            EditorsDemo.EditorsViewModelBase.DataLoader = New DevExpress.DemoData.NWindDataLoader()
        End Sub

        Public Sub New()
            Me.Employees = New System.Collections.Generic.List(Of DevExpress.Xpf.DemoBase.DataClasses.Employee)(DevExpress.Xpf.DemoBase.DataClasses.EmployeesWithPhotoData.DataSource)
            Me.Products = New System.Collections.Generic.List(Of DevExpress.DemoData.Models.Product)(CType(EditorsDemo.EditorsViewModelBase.DataLoader.Products, System.Collections.Generic.IList(Of DevExpress.DemoData.Models.Product)))
        End Sub

        Public Property Employees As IList(Of DevExpress.Xpf.DemoBase.DataClasses.Employee)
            Get
                Return _Employees
            End Get

            Private Set(ByVal value As IList(Of DevExpress.Xpf.DemoBase.DataClasses.Employee))
                _Employees = value
            End Set
        End Property

        Public Property Products As IList(Of DevExpress.DemoData.Models.Product)
            Get
                Return _Products
            End Get

            Private Set(ByVal value As IList(Of DevExpress.DemoData.Models.Product))
                _Products = value
            End Set
        End Property
    End Class
End Namespace
