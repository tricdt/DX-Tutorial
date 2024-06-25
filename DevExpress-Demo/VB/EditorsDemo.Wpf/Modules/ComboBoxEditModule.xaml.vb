Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Controls
Imports DevExpress.DemoData.Models

Namespace EditorsDemo

    Public Partial Class ComboBoxEditModule
        Inherits EditorsDemoModule

        Public Sub New()
            DataContext = New ComboBoxEditViewModel()
            InitializeComponent()
        End Sub
    End Class

    Public Class ComboBoxEditViewModel
        Inherits EditorsViewModelBase

        Private _Countries As IEnumerable(Of String), _Categories As IList(Of DevExpress.DemoData.Models.Category)

        Public Sub New()
            MyBase.New()
            Dim context = NWindContext.Create()
            Countries = context.CountriesArray
            Categories = context.Categories.ToList()
            SelectedCategories = New Category() {Categories(2), Categories(5), Categories(6)}
            SelectedProduct = Products(5).ProductName
        End Sub

        Public Property Countries As IEnumerable(Of String)
            Get
                Return _Countries
            End Get

            Private Set(ByVal value As IEnumerable(Of String))
                _Countries = value
            End Set
        End Property

        Public Property Categories As IList(Of Category)
            Get
                Return _Categories
            End Get

            Private Set(ByVal value As IList(Of Category))
                _Categories = value
            End Set
        End Property

        Public Property SelectedCategories As Object

        Public Property SelectedProduct As String
    End Class
End Namespace
