Namespace PivotGridDemo

    Public Enum FormatConditionType
        DataBar
    End Enum

    Public Class FormatConditionItem

        Public Property Type As FormatConditionType

        Public Property MeasureName As String

        Public Property RowName As String

        Public Property ColumnName As String

        Public Property PredefinedFormatName As String
    End Class
End Namespace
