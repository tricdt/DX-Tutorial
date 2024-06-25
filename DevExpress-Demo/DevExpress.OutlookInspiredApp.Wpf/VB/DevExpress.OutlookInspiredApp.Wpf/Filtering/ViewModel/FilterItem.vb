Imports DevExpress.Data.Filtering
Imports DevExpress.Mvvm.POCO
Imports System.Linq

Namespace DevExpress.DevAV.ViewModels

    Public Class FilterItem

        Private _FilterTreeViewModel As IFilterTreeViewModel

        Public Shared Function Create(ByVal entitiesCount As Integer, ByVal name As String, ByVal filterCriteria As CriteriaOperator, ByVal imageUri As String, ByVal filterTreeViewModel As IFilterTreeViewModel) As FilterItem
            Return ViewModelSource.Create(Function() New FilterItem(entitiesCount, name, filterCriteria, imageUri, filterTreeViewModel))
        End Function

        Protected Sub New(ByVal entitiesCount As Integer, ByVal name As String, ByVal filterCriteria As CriteriaOperator, ByVal imageUri As String, ByVal filterTreeViewModel As IFilterTreeViewModel)
            Me.Name = name
            Me.FilterCriteria = filterCriteria
            Me.ImageUri = imageUri
            Me.FilterTreeViewModel = filterTreeViewModel
            Update(entitiesCount)
        End Sub

        Public Overridable Property Name As String

        Public Overridable Property FilterCriteria As CriteriaOperator

        Public Overridable Property EntitiesCount As Integer

        Public Overridable Property DisplayText As String

        Public Overridable Property ImageUri As String

        Public Property FilterTreeViewModel As IFilterTreeViewModel
            Get
                Return _FilterTreeViewModel
            End Get

            Protected Set(ByVal value As IFilterTreeViewModel)
                _FilterTreeViewModel = value
            End Set
        End Property

        Public ReadOnly Property IsCustomFilter As Boolean
            Get
                If IsInDesignMode() Then Return False
                Dim customCategory = FilterTreeViewModel.Categories.FirstOrDefault(Function(x) x.IsCustom)
                Return If(customCategory Is Nothing, False, customCategory.FilterItems.Contains(Me))
            End Get
        End Property

        Public Sub Update(ByVal entitiesCount As Integer)
            Me.EntitiesCount = entitiesCount
            DisplayText = String.Format("{0} ({1})", Name, entitiesCount)
        End Sub

        Public Function Clone() As FilterItem
            Return Create(EntitiesCount, Name, FilterCriteria, ImageUri, FilterTreeViewModel)
        End Function

        Public Function Clone(ByVal name As String, ByVal imageUri As String) As FilterItem
            Return Create(EntitiesCount, name, FilterCriteria, imageUri, FilterTreeViewModel)
        End Function

        Protected Overridable Sub OnNameChanged()
            Update(EntitiesCount)
        End Sub
    End Class
End Namespace
