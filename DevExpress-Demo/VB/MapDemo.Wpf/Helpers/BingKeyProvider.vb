Namespace MapDemo

    Public Module BingKeyProvider

        Const key As String = DevExpress.Map.Native.DXBingKeyVerifier.BingKeyWpfMapDemo

        Public ReadOnly Property BingKey As String
            Get
                Return key
            End Get
        End Property
    End Module
End Namespace
