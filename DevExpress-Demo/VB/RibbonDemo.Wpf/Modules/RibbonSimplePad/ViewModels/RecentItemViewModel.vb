Namespace RibbonDemo

    Public Class RecentItemViewModel

        Private Shared count As Integer = 0

        Public Property Caption As String

        Public Property Description As String

        Public Property DateModified As Date

        Public Sub New()
        End Sub

        Public Sub New(ByVal caption As String, ByVal description As String)
            Me.Caption = caption
            Me.Description = description
            DateModified = Date.Now.AddDays(count)
            count += 1
        End Sub
    End Class
End Namespace
