Imports DevExpress.Xpf.PivotGrid

Namespace PivotGridDemo

    Public Class FieldItem

        Public Sub New()
            ExpandedInFieldsGroup = True
            SummaryType = FieldSummaryType.Sum
            ValueFormat = Nothing
            CellFormat = Nothing
            GroupName = Nothing
            Width = Double.NaN
        End Sub

        Public Property Name As String

        Public Property DataBinding As DataBinding

        Public Property Area As FieldArea

        Public Property Width As Double

        Public Property Caption As String

        Public Property GroupName As String

        Public Property ExpandedInFieldsGroup As Boolean

        Public Property ValueFormat As String

        Public Property SummaryType As FieldSummaryType

        Public Property CellFormat As String
    End Class
End Namespace
