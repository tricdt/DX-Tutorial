Imports DevExpress.Data.Filtering
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels

    Public Class FilterItem

        Public Shared Function Create(ByVal entitiesCount As Integer, ByVal name As String, ByVal filterCriteria As CriteriaOperator, ByVal imageUri As String) As FilterItem
            Return ViewModelSource.Create(Function() New FilterItem(entitiesCount, name, filterCriteria, imageUri))
        End Function

        Protected Sub New(ByVal entitiesCount As Integer, ByVal name As String, ByVal filterCriteria As CriteriaOperator, ByVal imageUri As String)
            Me.Name = name
            Me.FilterCriteria = filterCriteria
            Me.ImageUri = imageUri
            Update(entitiesCount)
        End Sub

        Public Overridable Property Name As String

        Public Overridable Property FilterCriteria As CriteriaOperator

        Public Overridable Property EntitiesCount As Integer

        Public Overridable Property DisplayText As String

        Public Overridable Property ImageUri As String

        Public Sub Update(ByVal entitiesCount As Integer)
            Me.EntitiesCount = entitiesCount
            DisplayText = String.Format("{0} ({1})", Name, entitiesCount)
        End Sub

        Public Function Clone() As FilterItem
            Return Create(EntitiesCount, Name, FilterCriteria, ImageUri)
        End Function

        Public Function Clone(ByVal name As String, ByVal imageUri As String) As FilterItem
            Return Create(EntitiesCount, name, FilterCriteria, imageUri)
        End Function

        Protected Overridable Sub OnNameChanged()
            Update(EntitiesCount)
        End Sub
    End Class
End Namespace
