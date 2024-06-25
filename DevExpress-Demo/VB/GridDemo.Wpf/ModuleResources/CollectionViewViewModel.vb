Imports DevExpress.Mvvm
Imports DevExpress.Xpf.DemoBase.DataClasses
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Data

Namespace GridDemo

    Public Class CollectionViewViewModel
        Inherits BindableBase

        Private _CollectionView As ICollectionView

        Private employeesCore As IList = EmployeesWithPhotoData.DataSource

        Public ReadOnly Property Employees As IList
            Get
                Return employeesCore
            End Get
        End Property

        Public Property CollectionView As ICollectionView
            Get
                Return _CollectionView
            End Get

            Private Set(ByVal value As ICollectionView)
                _CollectionView = value
            End Set
        End Property

        Public Sub New()
            CollectionView = New CollectionViewSource() With {.Source = Employees}.View
            CollectionView.GroupDescriptions.Add(New PropertyGroupDescription("JobTitle"))
            CollectionView.SortDescriptions.Add(New SortDescription("JobTitle", ListSortDirection.Ascending))
            CollectionView.MoveCurrentToFirst()
        End Sub
    End Class
End Namespace
