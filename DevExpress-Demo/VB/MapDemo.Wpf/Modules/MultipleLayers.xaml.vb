Namespace MapDemo

    Public Partial Class MultipleLayers
        Inherits MapDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub OnWebRequest(ByVal sender As Object, ByVal e As DevExpress.Xpf.Map.MapWebRequestEventArgs)
            e.UserAgent = "DevExpress WPF Map Control Main Demo"
        End Sub
    End Class
End Namespace
