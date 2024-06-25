Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.Map
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Threading

Namespace MapDemo

    Public Partial Class Clustering
        Inherits MapDemo.MapDemoModule

        Private viewModel As MapDemo.ClusteringViewModel

        Public Sub New()
            Me.InitializeComponent()
            Me.viewModel = DevExpress.Mvvm.POCO.ViewModelSource.Create(Function() New MapDemo.ClusteringViewModel(TryCast(Me.FindResource("clusterTemplate"), System.Windows.DataTemplate)))
            Me.DataContext = Me.viewModel
        End Sub

        Private Sub Map_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)
            Dim objects As Object() = Me.Map.CalcHitInfo(CType((e.GetPosition(CType((Me.Map), System.Windows.IInputElement))), System.Windows.Point)).HitObjects
            If objects.Length > 0 Then Me.Map.ZoomToFit(CType(objects(CInt((0))), DevExpress.Xpf.Map.MapItem).ClusteredItems)
        End Sub

        Private Sub MapDemoModule_ModuleLoaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.viewModel.IsLoaded = True
        End Sub
    End Class

    <DevExpress.Mvvm.DataAnnotations.POCOViewModelAttribute>
    Public Class ClusteringViewModel

        Private ReadOnly Shared AttributeName As String = "location"

        Private ReadOnly clusterersField As System.Collections.Generic.IList(Of MapDemo.ClustererInfo) = New System.Collections.ObjectModel.ObservableCollection(Of MapDemo.ClustererInfo) From {DevExpress.Mvvm.POCO.ViewModelSource.Create(Function() New MapDemo.ClustererInfo() With {.Name = "Marker clusterer", .Clusterer = New DevExpress.Xpf.Map.MarkerClusterer()}), DevExpress.Mvvm.POCO.ViewModelSource.Create(Function() New MapDemo.ClustererInfo() With {.Name = "Distance-based clusterer", .Clusterer = New DevExpress.Xpf.Map.DistanceBasedClusterer()})}

        Private clusterTemplate As System.Windows.DataTemplate

        Private customSettingsField As DevExpress.Xpf.Map.MapCustomElementSettings

        Private groupProviderField As DevExpress.Xpf.Map.AttributeGroupProvider

        Private ReadOnly Property Clusterer As MapClusterer
            Get
                Return If(Me.ClustererInfo IsNot Nothing, Me.ClustererInfo.Clusterer, Nothing)
            End Get
        End Property

        Private ReadOnly Property CustomSettings As MapCustomElementSettings
            Get
                If Me.customSettingsField Is Nothing Then Me.customSettingsField = New DevExpress.Xpf.Map.MapCustomElementSettings() With {.ContentTemplate = Me.clusterTemplate}
                Return Me.customSettingsField
            End Get
        End Property

        Private ReadOnly Property GroupProvider As AttributeGroupProvider
            Get
                If Me.groupProviderField Is Nothing Then Me.groupProviderField = New DevExpress.Xpf.Map.AttributeGroupProvider() With {.AttributeName = MapDemo.ClusteringViewModel.AttributeName}
                Return Me.groupProviderField
            End Get
        End Property

        Public Sub New(ByVal clusterTemplate As System.Windows.DataTemplate)
            Me.ClustererInfo = Me.clusterersField(0)
            Me.StepInPixels = 50
            Me.clusterTemplate = clusterTemplate
            Me.UsingCustomTemplate = True
        End Sub

        Public Overridable ReadOnly Property Clusterers As IList(Of MapDemo.ClustererInfo)
            Get
                Return Me.clusterersField
            End Get
        End Property

        Public Overridable Property ClustererInfo As ClustererInfo

        Public Overridable Property StepInPixels As Double

        Public Overridable Property AttributeClusteringEnabled As Boolean

        Public Overridable Property IsClustering As Boolean

        Public Overridable Property IsLoaded As Boolean

        Public Overridable Property UsingCustomTemplate As Boolean

        Public Overridable Property LegendHeaderText As String

        Public Overridable Property DataContext As Object

        Protected Sub OnStepInPixelsChanged()
            If Me.Clusterer IsNot Nothing Then Me.Clusterer.StepInPixels = CInt(Me.StepInPixels)
        End Sub

        Protected Sub OnClustererInfoChanged()
            Me.Update()
        End Sub

        Protected Sub OnClustererInfoChanging()
            If Me.Clusterer IsNot Nothing Then Me.UnsubscribeEvents()
        End Sub

        Protected Sub OnAttributeClusteringEnabledChanged()
            Me.Update()
        End Sub

        Protected Sub OnUsingCustomTemplateChanged()
            Me.Update()
        End Sub

        Private Sub SubscribeEvents()
            AddHandler Me.Clusterer.Clustered, AddressOf Me.OnClustered
            AddHandler Me.Clusterer.Clustering, AddressOf Me.OnClustering
        End Sub

        Private Sub UnsubscribeEvents()
            RemoveHandler Me.Clusterer.Clustered, AddressOf Me.OnClustered
            RemoveHandler Me.Clusterer.Clustering, AddressOf Me.OnClustering
        End Sub

        Private Sub OnClustering(ByVal sender As Object, ByVal e As System.EventArgs)
            Me.IsClustering = True
        End Sub

        Private Sub OnClustered(ByVal sender As Object, ByVal e As DevExpress.Xpf.Map.ClusteredEventArgs)
            Call System.Windows.Threading.Dispatcher.CurrentDispatcher.BeginInvoke(New System.Action(Sub() Me.IsClustering = False), System.Windows.Threading.DispatcherPriority.Render, Nothing)
        End Sub

        Private Sub Update()
            If Me.Clusterer Is Nothing Then Return
            Me.SubscribeEvents()
            Me.Clusterer.GroupProvider = If(Me.AttributeClusteringEnabled, Me.GroupProvider, Nothing)
            Me.Clusterer.ClusterSettings = If(Me.UsingCustomTemplate, Me.CustomSettings, Nothing)
            Me.LegendHeaderText = If(Me.AttributeClusteringEnabled, "Tree location", "Tree density")
        End Sub
    End Class

    <DevExpress.Mvvm.DataAnnotations.POCOViewModelAttribute>
    Public Class ClustererInfo

        Public Overridable Property Name As String

        Public Overridable Property Clusterer As MapClusterer
    End Class
End Namespace
