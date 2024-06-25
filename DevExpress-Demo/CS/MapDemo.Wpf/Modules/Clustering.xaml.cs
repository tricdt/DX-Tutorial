using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Map;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace MapDemo {

    public partial class Clustering : MapDemoModule {
        ClusteringViewModel viewModel;

        public Clustering() {
            InitializeComponent();
            this.viewModel = ViewModelSource.Create(() => new ClusteringViewModel(FindResource("clusterTemplate") as DataTemplate));
            DataContext = this.viewModel;
        }

        void Map_MouseUp(object sender, MouseButtonEventArgs e) {
            object[] objects = Map.CalcHitInfo(e.GetPosition(Map)).HitObjects;
            if(objects.Length > 0)
               Map.ZoomToFit(((MapItem)objects[0]).ClusteredItems);
        }
        void MapDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            this.viewModel.IsLoaded = true;
        }
    }

    [POCOViewModel]
    public class ClusteringViewModel {
        readonly static string AttributeName = "location";
        readonly IList<ClustererInfo> clusterers = new ObservableCollection<ClustererInfo> { 
            ViewModelSource.Create(() => new ClustererInfo() { Name = "Marker clusterer", Clusterer = new MarkerClusterer() }), 
            ViewModelSource.Create(() => new ClustererInfo() { Name = "Distance-based clusterer", Clusterer = new DistanceBasedClusterer() }) };

        DataTemplate clusterTemplate;
        MapCustomElementSettings customSettings;
        AttributeGroupProvider groupProvider;

        MapClusterer Clusterer { get { return ClustererInfo != null ? ClustererInfo.Clusterer : null; } }
        MapCustomElementSettings CustomSettings {
            get {
                if(customSettings == null)
                    customSettings = new MapCustomElementSettings() { ContentTemplate = this.clusterTemplate };
                return customSettings;
            }
        }
        AttributeGroupProvider GroupProvider {
            get {
                if(groupProvider == null)
                    groupProvider = new AttributeGroupProvider() { AttributeName = AttributeName };
                return groupProvider;
            }
        }

        public ClusteringViewModel(DataTemplate clusterTemplate) {
            this.ClustererInfo = clusterers[0];
            this.StepInPixels = 50;
            this.clusterTemplate = clusterTemplate;
            this.UsingCustomTemplate = true;
        }

        public virtual IList<ClustererInfo> Clusterers { get { return clusterers; } }
        public virtual ClustererInfo ClustererInfo { get; set; }
        public virtual double StepInPixels { get; set; }
        public virtual bool AttributeClusteringEnabled { get; set; }
        public virtual bool IsClustering { get; set; }
        public virtual bool IsLoaded { get; set; }
        public virtual bool UsingCustomTemplate { get; set; }
        public virtual string LegendHeaderText { get; set; }
        public virtual object DataContext { get; set; }

        protected void OnStepInPixelsChanged() {
            if(Clusterer != null)
                Clusterer.StepInPixels = (int)StepInPixels;
        }
        protected void OnClustererInfoChanged() {
            Update();
        }
        protected void OnClustererInfoChanging() {
            if(Clusterer != null)
                UnsubscribeEvents();
        }
        protected void OnAttributeClusteringEnabledChanged() {
            Update();
        }
        protected void OnUsingCustomTemplateChanged() {
            Update();                
        }

        void SubscribeEvents() {
            Clusterer.Clustered += OnClustered;
            Clusterer.Clustering += OnClustering;
        }
        void UnsubscribeEvents() {
            Clusterer.Clustered -= OnClustered;
            Clusterer.Clustering -= OnClustering;
        }
        void OnClustering(object sender, System.EventArgs e) {
            IsClustering = true;
        }
        void OnClustered(object sender, ClusteredEventArgs e) {
            Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => { IsClustering = false; }), DispatcherPriority.Render, null);
        }
        void Update() {
            if(Clusterer == null) return;
            SubscribeEvents();
            Clusterer.GroupProvider = AttributeClusteringEnabled ? GroupProvider : null;
            Clusterer.ClusterSettings = UsingCustomTemplate ? CustomSettings : null; 
            LegendHeaderText = AttributeClusteringEnabled ? "Tree location" : "Tree density";
        }
    }

    [POCOViewModel]
    public class ClustererInfo {
        public virtual string Name { get; set; }
        public virtual MapClusterer Clusterer { get; set; }
    }
}
