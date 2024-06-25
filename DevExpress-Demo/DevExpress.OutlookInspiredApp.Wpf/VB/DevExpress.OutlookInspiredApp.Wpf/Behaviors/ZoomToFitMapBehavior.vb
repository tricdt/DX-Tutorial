Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Map

Namespace DevExpress.DevAV.Common.View

    Public Class ZoomToFitMapBehavior
        Inherits Behavior(Of MapControl)

        Public Sub New()
            mapVectorLayers = New List(Of VectorLayer)()
        End Sub

        Private mapVectorLayers As List(Of VectorLayer)

        Private zoomValueUpdated As Boolean = False

        Public Property ZoomLayerName As String

        Public Property PaddingFactor As Double

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.Loaded, AddressOf MapControlLoaded
        End Sub

        Protected Overrides Sub OnDetaching()
            RemoveHandler AssociatedObject.Loaded, AddressOf MapControlLoaded
            MyBase.OnDetaching()
        End Sub

        Private Sub MapControlLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            mapVectorLayers.Clear()
            For Each layer In AssociatedObject.Layers
                Dim vectorLayer As VectorLayer = TryCast(layer, VectorLayer)
                If vectorLayer IsNot Nothing AndAlso vectorLayer.Data IsNot Nothing AndAlso If(String.IsNullOrEmpty(ZoomLayerName), True, Equals(vectorLayer.Name, ZoomLayerName)) Then
                    mapVectorLayers.Add(vectorLayer)
                    RemoveHandler vectorLayer.DataLoaded, AddressOf OnDataLoaded
                    AddHandler vectorLayer.DataLoaded, AddressOf OnDataLoaded
                End If
            Next
        End Sub

        Private Sub OnDataLoaded(ByVal sender As Object, ByVal e As DataLoadedEventArgs)
            Dim displayItems = CType(sender, VectorLayer).Data.DisplayItems
            If displayItems Is Nothing Then Return
            If displayItems.Count() > 3 AndAlso Not zoomValueUpdated Then
                zoomValueUpdated = True
                ZoomToFit()
            End If
        End Sub

        Private Sub ZoomToFit()
            AssociatedObject.ZoomToFitLayerItems(mapVectorLayers, PaddingFactor)
        End Sub
    End Class
End Namespace
