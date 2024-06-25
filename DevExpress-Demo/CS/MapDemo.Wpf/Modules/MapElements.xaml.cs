using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml.Linq;
using DevExpress.Map;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Map;

namespace MapDemo {
    public partial class MapElements : MapDemoModule {
        FlightMapDataGenerator dataGenerator;

        public MapElements() {
            InitializeComponent();
            dataGenerator = ViewModelSource.Create(() => new FlightMapDataGenerator());
            DataContext = dataGenerator;
            dataGenerator.SpeedScale = Convert.ToDouble(tbSpeedScale.Value);
            ModuleUnloaded += MapElements_Unloaded;
        }

        void MapElements_Unloaded(object sender, RoutedEventArgs e) {
            dataGenerator.Dispose();
        }
        void tbSpeedScale_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            if(dataGenerator != null)
                dataGenerator.SpeedScale = Convert.ToDouble(e.NewValue);
        }
    }

    public class PlaneTrajectory {
        class TrajectoryPart {
            readonly GeoPoint startPointField;
            readonly GeoPoint endPointField;
            readonly double flightTimeField;
            readonly double courseField;

            public GeoPoint StartPoint { get { return startPointField; } }
            public GeoPoint EndPoint { get { return endPointField; } }
            public double FlightTime { get { return flightTimeField; } }
            public double Course { get { return courseField; } }

            public TrajectoryPart(ProjectionBase projection, GeoPoint startPoint, GeoPoint endPoint, double speedInKmH) {
                this.startPointField = startPoint;
                this.endPointField = endPoint;
                Size sizeInKm = projection.GeoToKilometersSize(startPoint, new Size(Math.Abs(startPoint.Longitude - endPoint.Longitude), Math.Abs(startPoint.Latitude - endPoint.Latitude)));
                double partlength = Math.Sqrt(sizeInKm.Width * sizeInKm.Width + sizeInKm.Height * sizeInKm.Height);
                flightTimeField = partlength / speedInKmH;
                courseField = Math.Atan2(endPoint.Longitude - startPoint.Longitude, endPoint.Latitude - startPoint.Latitude) * 180 / Math.PI;
            }
            public GeoPoint GetPointByCurrentFlightTime(double currentFlightTime) {
                if(currentFlightTime > FlightTime)
                    return endPointField;
                double ratio = currentFlightTime / FlightTime;
                return new GeoPoint(startPointField.Latitude + ratio * (endPointField.Latitude - startPointField.Latitude), startPointField.Longitude + ratio * (endPointField.Longitude - startPointField.Longitude));
            }
        }

        readonly List<TrajectoryPart> trajectory = new List<TrajectoryPart>();
        readonly SphericalMercatorProjection projection = new SphericalMercatorProjection();

        public GeoPoint StartPoint {
            get { return (trajectory.Count > 0) ? trajectory[0].StartPoint : new GeoPoint(0, 0); }
        }
        public GeoPoint EndPoint {
            get { return (trajectory.Count > 0) ? trajectory[trajectory.Count - 1].EndPoint : new GeoPoint(0, 0); }
        }
        public double FlightTime {
            get {
                double result = 0.0;
                foreach(TrajectoryPart part in trajectory)
                    result += part.FlightTime;
                return result;
            }
        }
        public MapPolyline TrajectoryPresentation { get; private set; }

        public PlaneTrajectory(CoordPointCollection points, double speedInKmH) {
            TrajectoryPresentation = new MapPolyline() { Points = points, IsGeodesic = true };
            List<GeoPoint> geoPoints = TrajectoryPresentation.ActualPoints.Cast<GeoPoint>().ToList();
            for(int i = 0; i < geoPoints.Count - 1; i++) 
                trajectory.Add(new TrajectoryPart(projection, geoPoints[i], geoPoints[i + 1], speedInKmH));
        }
        public GeoPoint GetPointByCurrentFlightTime(double currentFlightTime) {
            double time = 0.0;
            for(int i = 0; i < trajectory.Count - 1; i++) {
                if(trajectory[i].FlightTime > currentFlightTime - time)
                    return trajectory[i].GetPointByCurrentFlightTime(currentFlightTime - time);
                time += trajectory[i].FlightTime;
            }
            return trajectory[trajectory.Count - 1].GetPointByCurrentFlightTime(currentFlightTime - time);
        }
        public double GetCourseByCurrentFlightTime(double currentFlightTime) {
            double time = 0.0;
            for(int i = 0; i < trajectory.Count - 1; i++) {
                if(trajectory[i].FlightTime > currentFlightTime - time)
                    return trajectory[i].Course;
                time += trajectory[i].FlightTime;
            }
            return trajectory[trajectory.Count - 1].Course;
        }
    }
    [POCOViewModel]
    public class PlaneInfo : BindableBase {
        public PlaneInfo() {
            CurrentFlightTime = 0;
            Course = 0;
        }

        public virtual double CurrentFlightTime { get; set; }
        public virtual GeoPoint Position { get; set; }
        public virtual double Course { get; set; }

        protected void OnCurrentFlightTimeChanged() {
            this.UpdatePosition(CurrentFlightTime);
        }
        static string ConvertPlaneNameToFilePath(string PlaneName) {
            string result = PlaneName.Replace(" ", "");
            result = "../Images/Planes/" + result.Replace("-", "") + ".png";
            return result;
        }

        bool isLandedField = false;
        readonly string planeIDField;
        readonly string nameField;
        readonly string endPointNameField;
        readonly string startPointNameField;
        readonly double speedInKmHField;
        readonly double flightAltitudeField;
        readonly string imagePathField;
        readonly PlaneTrajectory trajectoryField;

        public string PlaneID { get { return planeIDField; } }
        public string Name { get { return nameField; } }
        public string EndPointName { get { return endPointNameField; } }
        public string StartPointName { get { return startPointNameField; } }
        public GeoPoint StartPoint { get { return trajectoryField.StartPoint; } }
        public GeoPoint EndPoint { get { return trajectoryField.EndPoint; } }
        public double SpeedKmH { get { return isLandedField ? 0.0 : speedInKmHField; } }
        public double FlightAltitude { get { return isLandedField ? 0.0 : flightAltitudeField; } }
        public string ImagePath { get { return imagePathField; } }
        public bool IsLanded { get { return isLandedField; } }
        public double TotalFlightTime { get { return trajectoryField.FlightTime; } }
        public MapPolyline TrajectoryPresentation { get { return trajectoryField.TrajectoryPresentation; } }

        public PlaneInfo(string name, string id, string endPointName, string startPointName, double speedInKmH, double flightAltitude, CoordPointCollection points) {
            this.nameField = name;
            this.planeIDField = id;
            this.endPointNameField = endPointName;
            this.startPointNameField = startPointName;
            this.speedInKmHField = speedInKmH;
            this.flightAltitudeField = flightAltitude;
            this.trajectoryField = new PlaneTrajectory(points, speedInKmH);
            imagePathField = ConvertPlaneNameToFilePath(name);
            UpdatePosition(CurrentFlightTime);
        }
        void UpdatePosition(double flightTime) {
            isLandedField = flightTime >= trajectoryField.FlightTime;
            Position = trajectoryField.GetPointByCurrentFlightTime(flightTime);
            Course = trajectoryField.GetCourseByCurrentFlightTime(flightTime);
        }
    }
    [POCOViewModel]
    public class AirportInfo {
        public virtual GeoPoint Location { get; set; }
    }
    [POCOViewModel]
    public class FlightMapDataGenerator : BindableBase, IDisposable {
        public virtual ObservableCollection<PlaneInfo> Planes { get; set; }
        public virtual ObservableCollection<AirportInfo> Airports { get; set; }
        public virtual ObservableCollection<MapItem> ActualAirPaths { get; set; }
        public virtual PlaneInfo SelectedPlaneInfo { get; set; }
        public virtual double SpeedScale { get; set; }

        protected void OnSelectedPlaneInfoChanged() {
            Airports = SelectedPlaneInfo != null ? new ObservableCollection<AirportInfo>() {
                new AirportInfo() { Location = SelectedPlaneInfo.StartPoint },
                new AirportInfo() { Location = SelectedPlaneInfo.EndPoint }
            } : null;
            ActualAirPaths = SelectedPlaneInfo != null ? new ObservableCollection<MapItem>() {
                SelectedPlaneInfo.TrajectoryPresentation
            } : null;
        }

        const double mSecPerHour = 3600000;

        readonly DispatcherTimer timer = new DispatcherTimer();
        readonly ObservableCollection<PlaneInfo> planesInfo = new ObservableCollection<PlaneInfo>();
        DateTime lastTime;

        public FlightMapDataGenerator() {
            LoadFromXML();
            timer.Tick += new EventHandler(OnTimedEvent);
            timer.Interval = new TimeSpan(0, 0, 2);
            lastTime = DateTime.Now;
            timer.Start();
            if(Planes != null)
                SelectedPlaneInfo = Planes[1];
        }
        void LoadFromXML() {
            XDocument document = DataLoader.LoadXmlFromResources("/Data/FlightMap.xml");
            if(document != null) {
                foreach(XElement element in document.Element("Planes").Elements()) {
                    CoordPointCollection points = new CoordPointCollection();
                    foreach(XElement infoElement in element.Element("Path").Elements()) 
                        points.Add(new GeoPoint(Convert.ToDouble(infoElement.Element("Latitude").Value, CultureInfo.InvariantCulture), 
                            Convert.ToDouble(infoElement.Element("Longitude").Value, CultureInfo.InvariantCulture)));
                    
                    PlaneInfo info = ViewModelSource.Create(() => new PlaneInfo(element.Element("PlaneName").Value, element.Element("PlaneID").Value, 
                        element.Element("EndPointName").Value, element.Element("StartPointName").Value, Convert.ToInt32(element.Element("Speed").Value), 
                        Convert.ToInt32(element.Element("Altitude").Value), points));
                    info.CurrentFlightTime = Convert.ToDouble(element.Element("CurrentFlightTime").Value, CultureInfo.InvariantCulture);
                    planesInfo.Add(info);
                }
            }
            Planes = planesInfo;
        }
        void OnTimedEvent(object source, EventArgs e) {
            DateTime currentTime = DateTime.Now;
            TimeSpan interval = currentTime.Subtract(lastTime);
            foreach(PlaneInfo info in planesInfo) {
                if(!info.IsLanded)
                    info.CurrentFlightTime += SpeedScale * interval.TotalMilliseconds / mSecPerHour;
            }
            lastTime = currentTime;
        }
        public void Dispose() {
            timer.Stop();
            timer.Tick -= OnTimedEvent;
        }
    }
}
