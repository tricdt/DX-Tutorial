Imports DevExpress.DevAV.ViewModels
Imports DevExpress.Xpf.Map
Imports System.Windows.Controls

Namespace DevExpress.DevAV.Views

    Public Partial Class OrderMapView
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub BingGeocodeDataProvider_LayerItemsGenerating(ByVal sender As Object, ByVal args As LayerItemsGeneratingEventArgs)
            For Each pushpinItem In args.Items
                Dim pushpin As MapPushpin = TryCast(pushpinItem, MapPushpin)
                If pushpin IsNot Nothing Then CType(DataContext, INavigatorMapViewModel).NewPushpinCreated(pushpin)
            Next
        End Sub

        Private Sub BingRouteDataProvider_LayerItemsGenerating(ByVal sender As Object, ByVal args As LayerItemsGeneratingEventArgs)
            If args.Items.Length < 3 Then Return
            CType(args.Items(1), MapPushpin).Text = "A"
            CType(args.Items(2), MapPushpin).Text = "B"
            Me.mapControl.ZoomToRegion(CType(args.Items(1), MapPushpin).Location, CType(args.Items(2), MapPushpin).Location, 0.4)
        End Sub
    End Class
End Namespace
