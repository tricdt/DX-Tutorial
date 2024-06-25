Imports DevExpress.Xpf.DemoBase.DataClasses
Imports System.Collections
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Windows.Data

Namespace EditorsDemo

    Public Class CollectionViewViewModel

        Private _CollectionView As ICollectionView

        Private employeesField As IList = New ObservableCollection(Of Object)(EmployeesWithPhotoData.DataSource)

        Public ReadOnly Property Employees As IList
            Get
                Return employeesField
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
        End Sub
    End Class
End Namespace
