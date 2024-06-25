Imports System.Net
Imports DevExpress.Xpf.Map

Namespace MapDemo

    Public Partial Class OpenStreetMapProvider
        Inherits MapDemoModule

        Public Sub New()
            ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol Or SecurityProtocolType.Tls12
            InitializeComponent()
        End Sub

        Private Sub overlayLayerKind_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            MoveMap(overlayLayerKind.SelectedIndex)
        End Sub

        Private Sub MoveMap(ByVal index As Integer)
            Select Case index
                Case 0
                    map.CenterPoint = New GeoPoint(50.067, 14.417)
                    map.ZoomLevel = 15
                    Exit Select
                Case 1
                    map.CenterPoint = New GeoPoint(54.15, 11.75)
                    map.ZoomLevel = 14
                    Exit Select
                Case 2
                    map.CenterPoint = New GeoPoint(41.5, 2.0)
                    map.ZoomLevel = 11
                    Exit Select
                Case 3
                    map.CenterPoint = New GeoPoint(51.5, -3.2)
                    map.ZoomLevel = 13
                    Exit Select
                Case 4
                    map.CenterPoint = New GeoPoint(48.85, 2.3)
                    map.ZoomLevel = 11
                    Exit Select
            End Select
        End Sub

        Private Sub OnWebRequest(ByVal sender As Object, ByVal e As MapWebRequestEventArgs)
            e.UserAgent = "DevExpress Map Demo"
        End Sub
    End Class
End Namespace
