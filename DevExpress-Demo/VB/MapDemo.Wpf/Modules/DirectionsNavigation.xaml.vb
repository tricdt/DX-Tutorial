Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Media
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Map

Namespace MapDemo

    Public Partial Class DirectionsNavigation
        Inherits MapDemoModule

        Private routeModelField As RouteModel

        Private driveMarkerTemplateField As DataTemplate

        Private driveBrushField As SolidColorBrush

        Private defaultBrushField As SolidColorBrush

        Public ReadOnly Property RouteLayer As InformationLayer
            Get
                Return routeInformationLayer
            End Get
        End Property

        Public ReadOnly Property RouteProvider As BingRouteDataProvider
            Get
                Return routeDataProvider
            End Get
        End Property

        Public ReadOnly Property DriveMarkerTemplate As DataTemplate
            Get
                Return driveMarkerTemplateField
            End Get
        End Property

        Public ReadOnly Property DriveBrush As Brush
            Get
                Return driveBrushField
            End Get
        End Property

        Public ReadOnly Property DefaultBrush As Brush
            Get
                Return defaultBrushField
            End Get
        End Property

        Public ReadOnly Property RouteModel As RouteModel
            Get
                Return routeModelField
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
            driveBrushField = New SolidColorBrush(Color.FromArgb(&HFF, &HFE, &H72, &HFF))
            defaultBrushField = New SolidColorBrush(Color.FromArgb(&HFF, &H8A, &HFB, &HFF))
            driveMarkerTemplateField = CType(Resources("DrivePushpinMarker"), DataTemplate)
            routeModelField = ViewModelSource.Create(Function() New RouteModel(Me))
            DataContext = routeModelField
        End Sub

        Private Sub PushpinMouseLeftButtonDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Dim pushpin As MapPushpin = TryCast(sender, MapPushpin)
            If pushpin IsNot Nothing AndAlso pushpin.State = MapPushpinState.Normal Then
                Dim locationInformation As LocationInformation = TryCast(pushpin.Information, LocationInformation)
                routeModelField.AddWaypoint(If(locationInformation Is Nothing, String.Empty, locationInformation.DisplayName), CType(pushpin.Location, GeoPoint))
                searchInformationLayer.ClearResults()
                geocodeInformationLayer.ClearResults()
            End If

            e.Handled = True
        End Sub

        Private Sub GeoCodeAndSearchLayerItemsGenerating(ByVal sender As Object, ByVal args As LayerItemsGeneratingEventArgs)
            Dim pushpinsCount As Integer = 0
            For Each item As MapItem In args.Items
                Dim pushpin As MapPushpin = TryCast(item, MapPushpin)
                If pushpin IsNot Nothing Then
                    AddHandler pushpin.MouseLeftButtonDown, New MouseButtonEventHandler(AddressOf PushpinMouseLeftButtonDown)
                    pushpinsCount += 1
                End If
            Next

            RouteModel.RaiseSearchPushpinsChanged(pushpinsCount)
        End Sub

        Private Sub RouteItemsGenerating(ByVal sender As Object, ByVal args As LayerItemsGeneratingEventArgs)
            If args.Error Is Nothing AndAlso Not args.Cancelled Then routeModelField.ProcessRouteItems(args.Items)
        End Sub

        Private Sub RouteCalculated(ByVal sender As Object, ByVal e As BingRouteCalculatedEventArgs)
            If e.CalculationResult IsNot Nothing AndAlso e.CalculationResult.RouteResults.Count > 0 Then
                routeModelField.ProcessRouteResult(e.CalculationResult.RouteResults(0))
            Else
                routeModelField.ProcessRouteResult(Nothing)
            End If
        End Sub

        Private Sub DriveClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            e.Handled = True
            routeModelField.State = If(routeModelField.State = RouteModelState.Normal, RouteModelState.Drive, RouteModelState.Normal)
        End Sub

        Private Sub ClearClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            e.Handled = True
            routeModelField.Clear()
            routeInformationLayer.ClearResults()
            searchInformationLayer.ClearResults()
            geocodeInformationLayer.ClearResults()
        End Sub
    End Class
End Namespace
