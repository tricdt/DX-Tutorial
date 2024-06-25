using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Map;

namespace MapDemo {
    public partial class DirectionsNavigation : MapDemoModule {
        RouteModel routeModel;
        DataTemplate driveMarkerTemplate;
        SolidColorBrush driveBrush;
        SolidColorBrush defaultBrush;

        public InformationLayer RouteLayer {
            get { return routeInformationLayer; }
        }
        public BingRouteDataProvider RouteProvider {
            get { return this.routeDataProvider; }
        }
        public DataTemplate DriveMarkerTemplate {
            get { return this.driveMarkerTemplate; }
        }
        public Brush DriveBrush {
            get { return driveBrush; }
        }
        public Brush DefaultBrush {
            get { return defaultBrush; }
        }
        public RouteModel RouteModel {
            get { return this.routeModel; }
        }

        public DirectionsNavigation() {
            InitializeComponent();
            driveBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xFE, 0x72, 0xFF));
            defaultBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0x8A, 0xFB, 0xFF));
            driveMarkerTemplate = (DataTemplate)this.Resources["DrivePushpinMarker"];
            routeModel = ViewModelSource.Create(() => new RouteModel(this));
            DataContext = routeModel;
        }

        void PushpinMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            MapPushpin pushpin = sender as MapPushpin;
            if((pushpin != null) && (pushpin.State == MapPushpinState.Normal)) {
                LocationInformation locationInformation = pushpin.Information as LocationInformation;
                routeModel.AddWaypoint(locationInformation == null ? string.Empty : locationInformation.DisplayName, (GeoPoint)pushpin.Location);
                searchInformationLayer.ClearResults();
                geocodeInformationLayer.ClearResults();
            }
            e.Handled = true;
        }
        void GeoCodeAndSearchLayerItemsGenerating(object sender, LayerItemsGeneratingEventArgs args) {
            int pushpinsCount = 0;
            foreach(MapItem item in args.Items) {
                MapPushpin pushpin = item as MapPushpin;
                if(pushpin != null) {
                    pushpin.MouseLeftButtonDown += new MouseButtonEventHandler(PushpinMouseLeftButtonDown);
                    pushpinsCount++;
                }
            }
            this.RouteModel.RaiseSearchPushpinsChanged(pushpinsCount);
        }
        void RouteItemsGenerating(object sender, LayerItemsGeneratingEventArgs args) {
            if((args.Error == null) && !args.Cancelled)
                routeModel.ProcessRouteItems(args.Items);
        }
        void RouteCalculated(object sender, BingRouteCalculatedEventArgs e) {
            if (e.CalculationResult != null && e.CalculationResult.RouteResults.Count > 0)
                routeModel.ProcessRouteResult(e.CalculationResult.RouteResults[0]);
            else
                routeModel.ProcessRouteResult(null);
        }
        void DriveClick(object sender, RoutedEventArgs e) {
            e.Handled = true;
            routeModel.State = (routeModel.State == RouteModelState.Normal) ? RouteModelState.Drive : RouteModelState.Normal;
        }
        void ClearClick(object sender, RoutedEventArgs e) {
            e.Handled = true;
            routeModel.Clear();
            routeInformationLayer.ClearResults();
            searchInformationLayer.ClearResults();
            geocodeInformationLayer.ClearResults();
        }
    }
}
