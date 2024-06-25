using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using DevExpress.Mvvm;
using DevExpress.Xpf.Map;
using DevExpress.Xpf.Map.Native;

namespace MapDemo {
    public class DriveModel : BindableBase {
        const double driveSpeed = 100.0;
        const double driveTicksPerSecond = 100;
        const double driveTimeQuant = 1 / driveTicksPerSecond;

        MapPushpin drivePushpin;
        MapPolyline drivePath;
        RouteModel parentModel;
        List<GeoPoint> routePath;
        int routeNodeIndex;
        GeoPoint baseLocation;
        GeoPoint targetLocation;
        MapUnit basePoint;
        MapUnit targetPoint;
        double distance;
        double currentDistance;
        double currentDirection;
        MapUnit currentPoint;
        List<BingItineraryItem> itineraryItems;
        List<MapPushpin> routePushpins;
        DispatcherTimer animationTimer;
        string actionText;

        public event PropertyChangedEventHandler CurrentLocationChanged;
        public void RaiseCurrentLocationChanged() {
            if(CurrentLocationChanged != null)
                CurrentLocationChanged(this, new PropertyChangedEventArgs("CurrentLocation"));
        }
        public ObservableCollection<MapItem> DriveItems { get; private set; }
        public string ActionText {
            get { return this.actionText; }
            set { if (SetProperty(ref actionText, value, "ActionText")) parentModel.RaiseDriveModelChanged(); }
        }
        public GeoPoint CurrentLocation {
            get { return parentModel.Navigator.RouteLayer.MapUnitToGeoPoint(currentPoint); }
        }
        public double CurrentDirection { get { return currentDirection; } }

        static public double DistanceBetweenPoints(MapUnit a, MapUnit b) {
            MapUnit vector = new MapUnit(b.X - a.X, b.Y - a.Y);
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }
        static public double KilometerPerHourToMapUnitsPerSecond(double kmh) {
            return kmh / 40000.0 / 360.0;
        }

        public DriveModel(RouteModel parentModel) {
            this.parentModel = parentModel;
            routePath = parentModel.RoutePath;
            itineraryItems = new List<BingItineraryItem>();
            itineraryItems.AddRange(parentModel.ItineraryItems);
            routePushpins = new List<MapPushpin>();
            routePushpins.AddRange(parentModel.RoutePushpins);

            drivePushpin = new MapPushpin();
            drivePushpin.Location = routePath[0];
            drivePushpin.MarkerTemplate = parentModel.Navigator.DriveMarkerTemplate;
            drivePath = new MapPolyline();
            drivePath.Stroke = parentModel.Navigator.DriveBrush;
            drivePath.StrokeStyle = new StrokeStyle() { Thickness = 4, StartLineCap = PenLineCap.Round, EndLineCap = PenLineCap.Round };
            drivePath.Points.Add(targetLocation);
            targetLocation = routePath[0];

            DriveItems = new ObservableCollection<MapItem>();
            DriveItems.Add(drivePath);
            DriveItems.Add(drivePushpin);

            this.animationTimer = new DispatcherTimer();
            this.animationTimer.Interval = TimeSpan.FromSeconds(driveTimeQuant);
            this.animationTimer.Tick += new EventHandler(AnimationTimer);

            Advance();
        }

        void CheckItinerary() {
            InformationLayer routeLayer = parentModel.Navigator.RouteLayer;
            GeoPoint location = routeLayer.MapUnitToGeoPoint(currentPoint);
            BingItineraryItem currentItem = itineraryItems[0];
            Size geoSize = new Size(Math.Abs(location.Latitude - currentItem.Location.Latitude), Math.Abs(location.Longitude - currentItem.Location.Longitude));
            Size metricSize = routeLayer.GeoToKilometersSize(location, geoSize);
            double distance = Math.Sqrt(metricSize.Width * metricSize.Width + metricSize.Height * metricSize.Height);
            if(distance < 0.005)
                if(itineraryItems.Count > 1)
                    itineraryItems.Remove(currentItem);
                else
                    distance = 0.0;
            if(distance > 0.0)
                ActionText = CommonUtils.GetUserFriendlyEnumString(itineraryItems[0].Maneuver) + " after " + ((distance > 0.9) ? String.Format("{0:0} km", Math.Ceiling(distance)) : String.Format("{0:0} m", Math.Ceiling(distance * 10) * 100));
            else
                ActionText = "Finish! Click Stop and Clear to set a new route.";
        }
        void CheckRoutePushpins(GeoPoint location) {
            foreach(MapPushpin pushpin in routePushpins) {
                GeoPoint pushpinLocation = (GeoPoint)pushpin.Location;
                if ((Math.Abs(pushpinLocation.Latitude - location.Latitude) < 0.00001) && (Math.Abs(pushpinLocation.Longitude - location.Longitude) < 0.00001))
                    pushpin.Brush = parentModel.Navigator.DriveBrush;
            }
        }
        bool Advance() {
            if(routeNodeIndex < (routePath.Count - 1)) {
                routeNodeIndex++;
                InformationLayer routeLayer = parentModel.Navigator.RouteLayer;
                baseLocation = targetLocation;
                currentPoint = routeLayer.GeoPointToMapUnit(baseLocation);
                targetLocation = routePath[routeNodeIndex];
                basePoint = routeLayer.GeoPointToMapUnit(baseLocation);
                targetPoint = routeLayer.GeoPointToMapUnit(targetLocation);
                distance = DriveModel.DistanceBetweenPoints(targetPoint, basePoint);
                currentDistance = 0;

                drivePath.Points[drivePath.Points.Count - 1] = baseLocation;
                drivePath.Points.Add(baseLocation);

                CheckItinerary();
                CheckRoutePushpins(baseLocation);

                if(!animationTimer.IsEnabled)
                    animationTimer.Start();
                return true;
            } else {
                if(routePushpins.Count > 0)
                    routePushpins[routePushpins.Count - 1].Brush = parentModel.Navigator.DriveBrush;
                if(animationTimer.IsEnabled)
                    animationTimer.Stop();
                drivePushpin.Visible = false;
                return false;
            }
        }
        void AnimationTimer(object sender, EventArgs e) {
            double scaledTime = driveTimeQuant;
            while(scaledTime > 0.0) {
                double quant = Math.Min(scaledTime, driveTimeQuant);
                double excess = Update(quant * KilometerPerHourToMapUnitsPerSecond(driveSpeed));
                if(excess > 0.0) {
                    if(!Advance()) {
                        CheckItinerary();
                        PlaceItems();
                        return;
                    }
                    excess = Update(excess);
                }
                PlaceItems();
                CheckItinerary();
                scaledTime -= quant;
            }
        }
        void PlaceItems() {
            PlaceItems(parentModel.Navigator.RouteLayer.MapUnitToGeoPoint(currentPoint));
            RaisePropertyChanged("CurrentLocation");
            RaiseCurrentLocationChanged();
        }
        void PlaceItems(GeoPoint location) {
            drivePath.Points[drivePath.Points.Count - 1] = location;
            drivePushpin.Location = location;
        }
        MapUnit GetDirection(out double angle) {
            MapUnit direction = new MapUnit(targetPoint.X - basePoint.X, targetPoint.Y - basePoint.Y);
            double length = Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);
            if(length > 0.0) {
                double oneByLength = 1 / length;
                direction.X *= oneByLength;
                direction.Y *= oneByLength;
            }
            angle = Math.Atan2(direction.Y, direction.X) * 180.0 / Math.PI;
            if(angle < 0.0)
                angle += 360.0;
            return direction;
        }
        double CalculateNavAngle(double direction) {
            double angle = direction;
            const double delta = 2.0;
            if(Math.Abs(direction - currentDirection) > delta) {
                double a = (360.0 + direction - currentDirection) % 360;
                angle = currentDirection + (a > 180 ? -delta : delta);
            }
            angle = angle % 360;
            if(angle < 0)
                angle += 360;
            return angle;
        }

        double Update(double distanceToGo) {
            currentDistance += distanceToGo;
            if(currentDistance > distance) {
                currentPoint = targetPoint;
                return currentDistance - distance;
            }
            double direction = 0;
            MapUnit offset = GetDirection(out direction);
            offset.X *= currentDistance;
            offset.Y *= currentDistance;
            currentPoint = new MapUnit(basePoint.X + offset.X, basePoint.Y + offset.Y);
            currentDirection = CalculateNavAngle(direction);
            return 0.0;
        }

        public void Cleanup() {
            foreach(MapPushpin pushpin in routePushpins)
                pushpin.Brush = parentModel.Navigator.DefaultBrush;
            if(animationTimer.IsEnabled)
                animationTimer.Stop();
        }
    }
}
