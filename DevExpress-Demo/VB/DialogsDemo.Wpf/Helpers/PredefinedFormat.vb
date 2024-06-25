Namespace DialogsDemo.Helpers

    Public Class PredefinedFormat

        Public Sub New(ByVal example As String, ByVal format As String)
            Me.Example = example
            Me.Format = format
        End Sub

        Public ReadOnly Property Example As String

        Public ReadOnly Property Format As String

        Overrides Public Function ToString() As String
            Return $"{Example} [{Format}]"
        End Function
    End Class
End Namespace
