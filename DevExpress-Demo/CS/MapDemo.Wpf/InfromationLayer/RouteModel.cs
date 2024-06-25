using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media.Effects;
using DevExpress.Map;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Map;

namespace MapDemo {
    public enum RouteModelState {
        Normal,
        Drive
    }

    [POCOViewModel]
    public class RouteModel : ViewModelBase {
        ObservableCollection<MapItem> helpers;
        List<RouteWaypoint> waypoints;
        List<GeoPoint> routePath;
        int waypointIndex;
        int searchPushpinsCount;

        public ObservableCollection<MapItem> Helpers {
            get { return this.helpers; }
        }
        public virtual bool IsCalculating { get; set; }
        public virtual RouteModelState State { get; set; }
        public virtual DriveModel DriveModel { get; set; }
        public virtual DirectionsNavigation Navigator { get; set; }
        public List<GeoPoint> RoutePath {
            get { return routePath; }
        }
        public virtual double DrivePathDistance { get; set; }
        public virtual List<BingItineraryItem> ItineraryItems { get; set; }
        public virtual List<MapPushpin> RoutePushpins { get; set; }
        public string ActionText {
            get {
                if(State == RouteModelState.Drive && DriveModel != null) {
                    return DriveModel.ActionText;
                } else {
                    if(waypoints.Count == 0) {
                        if(searchPushpinsCount > 0)
                            return "Click the  pushpin to set a start point.";
                        else
                            return "Click the map or use Search to find a location.";
                    } else if(waypoints.Count == 1) {
                        return "Set a finish point to calculate a route.";
                    } else {
                        return "Set another finish point or click Drive.";
                    }
                }
            }
        }
        public virtual CoordPoint MapCenter { get; set; }
        public virtual double MapAngle { get; set; }
        public virtual bool InMotion { get; set; }
        public virtual bool EnableRotation { get; set; }
        public List<RouteWaypoint> Waypoints {
            get { return this.waypoints; }
        }

        public RouteModel(DirectionsNavigation navigator) {
            MapCenter = new GeoPoint(34.158506, -118.255629);
            Navigator = navigator;
            this.helpers = new ObservableCollection<MapItem>();
            waypoints = new List<RouteWaypoint>();
            RoutePushpins = new List<MapPushpin>();
            ItineraryItems = new List<BingItineraryItem>();
            EnableRotation = true;
        }

        void SendRouteRequest() {
            IsCalculating = true;
            if(Waypoints.Count > 1)
                this.Navigator.RouteProvider.CalculateRoute(Waypoints);
        }
        string NextWaypointLetter() {
            string letter = "" + (char)((byte)'A' + waypointIndex % 26);
            waypointIndex++;
            return letter;
        }
        void ExtractItineraryItems(BingRouteResult result) {
            ItineraryItems.Clear();
            foreach(BingRouteLeg leg in result.Legs)
                foreach(BingItineraryItem item in leg.Itinerary)
                    ItineraryItems.Add(item);
        }
        void BeginDrive() {
            if((RoutePath != null) && (RoutePath.Count > 1)) {
                StopDrive();
                this.DriveModel = new DriveModel(this);
                this.DriveModel.CurrentLocationChanged += OnCurrentLocationChanged;
                InMotion = true;
            }
        }
        void OnCurrentLocationChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            MapCenter = this.DriveModel.CurrentLocation;
            MapAngle = EnableRotation ? this.DriveModel.CurrentDirection + 90.0 : 0;
        }

        void StopDrive() {
            if(DriveModel != null) {
                this.DriveModel.Cleanup();
                this.DriveModel = null;
                MapCenter = (GeoPoint)MapCenter;
                MapAngle = 0;
            }
            InMotion = false;
        }
        protected void OnStateChanged() {
            switch(State) {
                case RouteModelState.Drive:
                    BeginDrive();
                    break;
                case RouteModelState.Normal:
                    StopDrive();
                    break;
            }
            RaisePropertyChanged("ActionText");
        }
        void CalculatePathDistance() {
            DrivePathDistance = 0;
            if(routePath != null) {
                MapUnit? lastPoint = null;
                foreach(GeoPoint node in routePath) {
                    if(lastPoint != null) {
                        MapUnit currentPoint = this.Navigator.RouteLayer.GeoPointToMapUnit(node);
                        DrivePathDistance += DriveModel.DistanceBetweenPoints(currentPoint, lastPoint.Value);
                        lastPoint = currentPoint;
                    } else
                        lastPoint = this.Navigator.RouteLayer.GeoPointToMapUnit(node);
                }
            }
        }

        internal void RaiseDriveModelChanged() {
            RaisePropertyChanged("ActionText");
        }

        internal void RaiseSearchPushpinsChanged(int count) {
            searchPushpinsCount = count;
            RaisePropertyChanged("ActionText");
        }

        public void AddWaypoint(string description, GeoPoint location) {
            RouteWaypoint waypoint = new RouteWaypoint(description, location);
            if(!waypoints.Contains(waypoint)) {
                MapPushpin pushpin = new MapPushpin();
                pushpin.Location = location;
                pushpin.Information = description;
                pushpin.Text = NextWaypointLetter();
                pushpin.TraceDepth = 0;
                pushpin.State = MapPushpinState.Busy;
                Helpers.Add(pushpin);
                waypoints.Add(waypoint);
                SendRouteRequest();
            }
            RaisePropertyChanged("ActionText");
            RaisePropertyChanged("Waypoints");
        }
        public void ProcessRouteItems(MapItem[] items) {
            waypointIndex = 0;
            RoutePushpins.Clear();
            foreach(MapItem item in items) {
                MapPushpin pushpin = item as MapPushpin;
                if(pushpin != null) {
                    pushpin.Text = NextWaypointLetter();
                    RoutePushpins.Add(pushpin);
                }
                MapPolyline polyline = item as MapPolyline;
                if(polyline != null)
                    polyline.Effect = new DropShadowEffect() { Direction = -90, ShadowDepth = 1 };
            }
            Helpers.Clear();
        }
        public void ProcessRouteResult(BingRouteResult result) {
            if(result != null) {
                ExtractItineraryItems(result);
                routePath = result.RoutePath;
            } else
                routePath = null;
            CalculatePathDistance();
        }
        public void Clear() {
            if(RoutePath != null)
                RoutePath.Clear();
            Helpers.Clear();
            RoutePushpins.Clear();
            ItineraryItems.Clear();
            DrivePathDistance = 0.0;
            waypoints.Clear();
            waypointIndex = 0;
            RaiseSearchPushpinsChanged(0);
            RaisePropertyChanged("Waypoints");
        }
        public void RotationStateChanged(){
            EnableRotation = !EnableRotation;
        }
    }

    public class RouteModelStateToButtonTextConverter : IValueConverter {
        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if((value != null) && (value.GetType() == typeof(RouteModelState))) {
                RouteModelState state = (RouteModelState)value;
                switch(state) {
                    case RouteModelState.Drive:
                        return "Stop";
                    case RouteModelState.Normal:
                        return "Drive";
                }
            }
            return string.Empty;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class RouteModelNormalStateToBoolConverter : IValueConverter {
        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if((value != null) && (value.GetType() == typeof(RouteModelState)))
                return ((RouteModelState)value) == RouteModelState.Normal;
            return false;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class WaypointsCountToDriveAbilityConverter : IValueConverter {
        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if((value != null) && (value.GetType() == typeof(int)))
                return ((int)value) > 1;
            return false;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        #endregion

    }      
    public class InvertDoubleConverter : MarkupExtension, IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value is double ? (double)value * -1d : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
