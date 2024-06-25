using System.Windows.Input;
using DevExpress.Map;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Map;

namespace MapDemo {
    public partial class TrafficAndIncidents : MapDemoModule {
        TrafficAndIncidentsModel model;

        public BingRouteDataProvider RouteDataProvider { get { return routeProvider; } }
        public BingTrafficIncidentDataProvider IncidentDataProvider { get { return trafficIncidentProvider; } }
        public InformationLayer IncidentLayer { get { return trafficIncidentLayer; } }

        public TrafficAndIncidents() {
            InitializeComponent();
            model = ViewModelSource.Create(() => new TrafficAndIncidentsModel(this));
            DataContext = model;
        }
        void PushpinMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            MapPushpin pushpin = sender as MapPushpin;
            if((pushpin != null) && (pushpin.State == MapPushpinState.Normal)) {
                LocationInformation locationInformation = pushpin.Information as LocationInformation;
                model.AddWaypoint(locationInformation == null ? string.Empty : locationInformation.DisplayName, (GeoPoint)pushpin.Location);
                searchInformationLayer.ClearResults();
                geocodeInformationLayer.ClearResults();
                routeInformationLayer.ClearResults();
            }
            e.Handled = true;
        }
        void GeoCodeAndSearchLayerItemsGenerating(object sender, LayerItemsGeneratingEventArgs args) {
            foreach(MapItem item in args.Items) {
                MapPushpin pushpin = item as MapPushpin;
                if(pushpin != null)
                    pushpin.MouseLeftButtonDown += new MouseButtonEventHandler(PushpinMouseLeftButtonDown);
            }
        }
        void RouteItemsGenerating(object sender, LayerItemsGeneratingEventArgs args) {
            if(args.Error == null && !args.Cancelled)
                model.ProcessRouteItems(args.Items);
        }
        void RouteOptimizationsSelectedIndexChanged(object sender, System.Windows.RoutedEventArgs e) {
            model.SendRouteRequest();
        }
        void MapControlViewportChanged(object sender, ViewportChangedEventArgs e) {
            model.UpdateViewport(new MapBounds(e.TopLeft, e.BottomRight));
        }
    }
}
