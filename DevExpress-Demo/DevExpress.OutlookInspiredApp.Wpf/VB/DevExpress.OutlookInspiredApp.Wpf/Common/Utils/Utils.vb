Namespace DevExpress.DevAV

    Public Module Logger

        Public Sub Log(ByVal message As String)
#If Not DXCORE3
            Xpf.DemoBase.Helpers.Logger.Log(message)
#End If
        End Sub
    End Module
End Namespace
