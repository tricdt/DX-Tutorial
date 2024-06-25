Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports DevExpress.Mvvm.POCO

Namespace ChartsDemo

    Public Class BindingIndividualSeriesViewModel

        Public Shared Function Create() As BindingIndividualSeriesViewModel
            Return ViewModelSource.Create(Function() New BindingIndividualSeriesViewModel())
        End Function

        Private ReadOnly devAVSales As DevAVSales = New DevAVSales()

        Public Overridable Property ArgumentDataMembers As ObservableCollection(Of String)

        Public Overridable Property ValueDataMembers As ObservableCollection(Of String)

        Public Overridable Property Categories As ObservableCollection(Of String)

        Public Overridable Property SelectedArgumentDataMember As String

        Public Overridable Property SelectedValueDataMember As String

        Public Overridable Property SelectedCategory As String

        Public Overridable Property FilterString As String

        Public ReadOnly Property DevAVNorthData As List(Of DevAVSaleItem)
            Get
                Return devAVSales.GetProductsByCompany(0)
            End Get
        End Property

        Public ReadOnly Property DevAVSouthData As List(Of DevAVSaleItem)
            Get
                Return devAVSales.GetProductsByCompany(1)
            End Get
        End Property

        Protected Sub New()
            ArgumentDataMembers = New ObservableCollection(Of String) From {"Product", "Category"}
            ValueDataMembers = New ObservableCollection(Of String) From {"Income", "Revenue"}
            Categories = New ObservableCollection(Of String) From {"All Categories", "Photo", "Cell Phones", "Computers", "TV, Audio"}
            SelectedArgumentDataMember = ArgumentDataMembers(0)
            SelectedValueDataMember = ValueDataMembers(0)
            SelectedCategory = Categories(0)
        End Sub

        Protected Sub OnSelectedCategoryChanged()
            If Equals(SelectedCategory, "All Categories") Then
                FilterString = String.Empty
            Else
                FilterString = String.Format("[Category] = '{0}'", SelectedCategory)
            End If
        End Sub
    End Class
End Namespace
