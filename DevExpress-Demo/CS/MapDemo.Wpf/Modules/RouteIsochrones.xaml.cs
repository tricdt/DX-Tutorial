using System.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Map;

namespace MapDemo {
    public partial class RouteIsochrones : MapDemoModule {
        RouteIsochronesModel model;

        public RouteIsochrones() {
            InitializeComponent();
            GeoPoint center = (GeoPoint)mapControl.CenterPoint;
            model = ViewModelSource.Create(() => new RouteIsochronesModel(routeIsochroneDataProvider, center));
            DataContext = model;
            geocodeDataProvider.RequestLocationInformation(center);
        }
        void GeoCodeItemsGenerating(object sender, LayerItemsGeneratingEventArgs args) {
            model.OnGeoCodeItemsGenerating(args.Items);
        }
        void LayerItemsGenerating(object sender, LayerItemsGeneratingEventArgs args) {
            geocodeInformationLayer.ClearResults();
            model.OnIsochroneGenerating(args.Items);
        }
    }

    [POCOViewModel]
    public class RouteIsochronesModel : ViewModelBase {
        readonly BingRouteIsochroneDataProvider provider;
        int parameterIndex = 0;
        int parameterValue = 15;
        MapPushpin currentPushpin;
        GeoPoint origin = new GeoPoint(42.3589935302734, -71.0586318969727);

        public int ParameterIndex {
            get { return parameterIndex; }
            set {
                if(parameterIndex != value) {
                    parameterIndex = value;
                    if(currentPushpin != null)
                        currentPushpin.State = MapPushpinState.Busy;
                    UpdateAndCalculateIsochrone();
                }
            }
        }
        public double ParameterValue {
            get { return parameterValue; }
            set {
                if(parameterValue != (int)value) {
                    parameterValue = (int)value;
                    if(currentPushpin != null)
                        currentPushpin.State = MapPushpinState.Busy;
                    UpdateAndCalculateIsochrone();
                }
            }
        }

        public RouteIsochronesModel(BingRouteIsochroneDataProvider provider, GeoPoint origin) {
            this.provider = provider;
            this.origin = origin;
        }
        void UpdateAndCalculateIsochrone() {
            if(parameterIndex == 0)
                provider.CalculateIsochroneByTime(new RouteWaypoint("", origin), parameterValue);
            else {
                provider.IsochroneOptions.DistanceUnit = parameterIndex == 1 ? DistanceMeasureUnit.Kilometer : DistanceMeasureUnit.Mile;
                provider.CalculateIsochroneByDistance(new RouteWaypoint("", origin), parameterValue);
            }
        }
        public void OnGeoCodeItemsGenerating(MapItem[] items) {
            MapPushpin pushpin = items.FirstOrDefault(x => x is MapPushpin) as MapPushpin;
            if(pushpin != null) {
                origin = (GeoPoint)pushpin.Location;
                pushpin.State = MapPushpinState.Busy;
                UpdateAndCalculateIsochrone();
            }
        }
        public void OnIsochroneGenerating(MapItem[] items) {
            currentPushpin = items.FirstOrDefault(x => x is MapPushpin) as MapPushpin;
        }
    }
}
