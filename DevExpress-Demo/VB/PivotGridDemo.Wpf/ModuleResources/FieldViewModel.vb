Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.PivotGrid
Imports DevExpress.Mvvm.POCO

Namespace PivotGridDemo

    <POCOViewModel>
    Public Class FieldViewModel

        Public Shared Function Create(ByVal caption As String, ByVal name As String, ByVal area As FieldArea, ByVal areaIndex As Integer) As FieldViewModel
            Return ViewModelSource.Create(Function() New FieldViewModel(caption, name, area, areaIndex))
        End Function

        Protected Sub New(ByVal caption As String, ByVal name As String, ByVal area As FieldArea, ByVal areaIndex As Integer)
            Visible = True
            Me.Caption = caption
            Me.Name = name
            Me.Area = area
            Me.AreaIndex = areaIndex
        End Sub

        Public Overridable Property Area As FieldArea

        Public Overridable Property AreaIndex As Integer

        Public Overridable Property Caption As String

        Public Overridable Property GroupInterval As FieldGroupInterval

        Public Overridable Property Name As String

        Public Overridable Property SortByFieldName As String

        Public Overridable Property SortOrder As FieldSortOrder

        Public Overridable Property TopValueCount As Decimal

        Public Overridable Property TopValueShowOthers As Boolean

        Public Overridable Property Visible As Boolean

        Public Overridable Property Width As Double
    End Class
End Namespace
