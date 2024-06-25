Imports DevExpress.Xpf.Charts

Namespace ChartsDemo

    Friend Class CustomBar3DModelNamed
        Inherits CustomBar3DModel

        Public Overrides ReadOnly Property ModelName As String
            Get
                Return If(CustomName, "Custom")
            End Get
        End Property

        Public Property CustomName As String
    End Class
End Namespace
