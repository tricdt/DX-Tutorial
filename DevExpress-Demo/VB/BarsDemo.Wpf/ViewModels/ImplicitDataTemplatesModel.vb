Imports System.Collections.Generic

Namespace BarsDemo

    Public Class CommandModel

        Public Property Caption As String

        Public Property Glyph As String
    End Class

    Public Class GroupModel
        Inherits CommandModel

        Public Property Commands As List(Of CommandModel)
    End Class

    Public Class EditorModel
        Inherits CommandModel

        Public Property EditValue As Object
    End Class

    Public Class LabelModel
        Inherits CommandModel

        Public Property Content As Object
    End Class
End Namespace
