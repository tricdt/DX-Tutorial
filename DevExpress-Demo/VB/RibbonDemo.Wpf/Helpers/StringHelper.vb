Namespace RibbonDemo

    Public Module StringHelper

        Public Function Split(ByVal str As String, ByVal separators As String) As String()
            Return str.Split(separators.ToCharArray())
        End Function
    End Module
End Namespace
