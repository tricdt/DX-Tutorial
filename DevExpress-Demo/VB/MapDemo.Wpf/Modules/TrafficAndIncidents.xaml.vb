Imports System.Windows.Input
Imports DevExpress.Map
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Map

Namespace MapDemo

    Public Partial Class TrafficAndIncidents
        Inherits MapDemoModule

        Private model As TrafficAndIncidentsModel

        Public ReadOnly Property RouteDataProvider As BingRouteDataProvider
            Get
                Return routeProvider
            End Get
        End Property

        Public ReadOnly Property IncidentDataProvider As BingTrafficIncidentDataProvider
            Get
                Return trafficIncidentProvider
            End Get
        End Property

        Public ReadOnly Property IncidentLayer As InformationLayer
            Get
                Return trafficIncidentLayer
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
            model = ViewModelSource.Create(Function() New TrafficAndIncidentsModel(Me))
            DataContext = model
        End Sub

        Private Sub PushpinMouseLeftButtonDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Dim pushpin As MapPushpin = TryCast(sender, MapPushpin)
            If pushpin IsNot Nothing AndAlso pushpin.State = MapPushpinState.Normal Then
                Dim locationInformation As LocationInformation = TryCast(pushpin.Information, LocationInformation)
                model.AddWaypoint(If(locationInformation Is Nothing, String.Empty, locationInformation.DisplayName), CType(pushpin.Location, GeoPoint))
                searchInformationLayer.ClearResults()
                geocodeInformationLayer.ClearResults()
                routeInformationLayer.ClearResults()
            End If

            e.Handled = True
        End Sub

        Private Sub GeoCodeAndSearchLayerItemsGenerating(ByVal sender As Object, ByVal args As LayerItemsGeneratingEventArgs)
            For Each item As MapItem In args.Items
                Dim pushpin As MapPushpin = TryCast(item, MapPushpin)
                If pushpin IsNot Nothing Then AddHandler pushpin.MouseLeftButtonDown, New MouseButtonEventHandler(AddressOf PushpinMouseLeftButtonDown)
            Next
        End Sub

        Private Sub RouteItemsGenerating(ByVal sender As Object, ByVal args As LayerItemsGeneratingEventArgs)
            If args.Error Is Nothing AndAlso Not args.Cancelled Then model.ProcessRouteItems(args.Items)
        End Sub

        Private Sub RouteOptimizationsSelectedIndexChanged(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            model.SendRouteRequest()
        End Sub

        Private Sub MapControlViewportChanged(ByVal sender As Object, ByVal e As ViewportChangedEventArgs)
            model.UpdateViewport(New MapBounds(e.TopLeft, e.BottomRight))
        End Sub
    End Class
End Namespace
