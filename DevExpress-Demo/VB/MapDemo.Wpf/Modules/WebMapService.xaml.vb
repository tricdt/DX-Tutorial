Imports DevExpress.Xpf.Map

Namespace MapDemo

    Public Partial Class WebMapService
        Inherits MapDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub OnResponseCapabilities(ByVal sender As Object, ByVal e As CapabilitiesRespondedEventArgs)
            lbWmsLayers.ItemsSource = e.Layers
        End Sub
    End Class
End Namespace
