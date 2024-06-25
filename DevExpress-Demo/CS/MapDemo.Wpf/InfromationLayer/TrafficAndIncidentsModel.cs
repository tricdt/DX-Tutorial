using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media.Effects;
using System.Windows.Threading;
using DevExpress.Map;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Map;

namespace MapDemo {
    [POCOViewModel]
    public class TrafficAndIncidentsModel : ViewModelBase {
        readonly TrafficAndIncidents mainModule;
        readonly ObservableCollection<MapItem> items;
        readonly DispatcherTimer trafficIncidentTimer;
        RouteWaypoint wayPointA = new RouteWaypoint("", "");
        RouteWaypoint wayPointB = new RouteWaypoint("", "");
        MapBounds viewport;
        BingTrafficIncidentType selectedTrafficIncidentType;

        public ObservableCollection<MapItem> Items { get { return items; } }
        public CoordPoint MapCenterPoint { get; set; }
        public BingTrafficIncidentType SelectedTrafficIncidentType {
            get { return selectedTrafficIncidentType; }
            set {
                if(selectedTrafficIncidentType != value) {
                    selectedTrafficIncidentType = value;
                    RequestTrafficIncidents();
                }
            } 
        }

        public TrafficAndIncidentsModel(TrafficAndIncidents module) {
            mainModule = module;
            MapCenterPoint = new GeoPoint(38.90507, -77.01909);
            items = new ObservableCollection<MapItem>();

            AddWaypoint("A", new GeoPoint(39.025538, -77.203833));
            AddWaypoint("B", new GeoPoint(38.8501856, -76.9276787));
            
            trafficIncidentTimer = new DispatcherTimer();
            trafficIncidentTimer.Interval = TimeSpan.FromSeconds(2.5);
            trafficIncidentTimer.Tick += (s, e) => RequestTrafficIncidents();
            
            selectedTrafficIncidentType = BingTrafficIncidentType.Accident | BingTrafficIncidentType.Construction;
        }
        void RequestTrafficIncidents() {
            trafficIncidentTimer.Stop();
            mainModule.IncidentLayer.ClearResults();
            if(SelectedTrafficIncidentType != 0)
                mainModule.IncidentDataProvider.RequestTrafficIncidents(
                    new SearchBoundingBox() { WestLongitude = viewport.Left, NorthLatitude = viewport.Top, EastLongitude = viewport.Right, SouthLatitude = viewport.Bottom },
                    BingTrafficIncidentSeverity.LowImpact | BingTrafficIncidentSeverity.Minor | BingTrafficIncidentSeverity.Moderate | BingTrafficIncidentSeverity.Serious,
                    SelectedTrafficIncidentType
                );
        }
        public void AddWaypoint(string description, GeoPoint location) {
            RouteWaypoint waypoint = new RouteWaypoint(description, location);
            string name;
            if(wayPointB == null) {
                wayPointB = waypoint;
                name = "B";
            }
            else {
                wayPointA = waypoint;
                wayPointB = null;
                name = "A";
                Items.Clear();
            }
            Items.Add(new MapPushpin() {
                Location = location,
                Information = description,
                TraceDepth = 0,
                Text = name
            });
            SendRouteRequest();
        }
        public void ProcessRouteItems(MapItem[] items) {
            int pushpinIndex = 0;
            foreach(MapItem item in items) {
                MapPushpin pushpin = item as MapPushpin;
                if(pushpin != null) {
                    pushpin.Text = pushpinIndex == 0 ? "A" : "B";
                    pushpinIndex++;
                }
                MapPolyline polyline = item as MapPolyline;
                if(polyline != null)
                    polyline.Effect = new DropShadowEffect() { Direction = -90, ShadowDepth = 1 };
            }
            Items.Clear();
        }
        public void UpdateViewport(MapBounds viewport) {
            this.viewport = viewport;
            this.trafficIncidentTimer.Stop();
            this.trafficIncidentTimer.Start();
        }
        public void SendRouteRequest() {
            if(wayPointA != null && wayPointB != null)
                mainModule.RouteDataProvider.CalculateRoute(new List<RouteWaypoint>() { wayPointA, wayPointB });
        }
    }

    public class BingRouteOptimizationsTypeConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            switch((BingRouteOptimization)value) {
                case BingRouteOptimization.MinimizeTime:
                    return "Time";
                case BingRouteOptimization.MinimizeTimeWithTraffic:
                    return "Time with Traffic";
                case BingRouteOptimization.MinimizeDistance:
                default:
                    return "Distance";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class BingTrafficIncidentTypeConverter : IValueConverter {
        static BingTrafficIncidentType AggregateFunc(BingTrafficIncidentType x, BingTrafficIncidentType y) {
            return x |= y;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            BingTrafficIncidentType type = (BingTrafficIncidentType)value;
            return Enum.GetValues(typeof(BingTrafficIncidentType)).Cast<BingTrafficIncidentType>().Where(c => type.HasFlag(c)).ToList();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            List<object> list = value as List<object>;
            if(list == null)
                return (BingTrafficIncidentType)0;
            return list.Cast<BingTrafficIncidentType>().Aggregate(AggregateFunc);
        }
    }
    public class BingTrafficIncidentToStringTypeConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            switch((BingTrafficIncidentType)value) {
                case BingTrafficIncidentType.DisabledVehicle:
                    return "Disabled Vehicle";
                case BingTrafficIncidentType.MassTransit:
                    return "Mass Transit";
                case BingTrafficIncidentType.OtherNews:
                    return "Other News";
                case BingTrafficIncidentType.PlannedEvent:
                    return "Planned Event";
                case BingTrafficIncidentType.RoadHazard:
                    return "Road Hazard";
            }
            return value.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
