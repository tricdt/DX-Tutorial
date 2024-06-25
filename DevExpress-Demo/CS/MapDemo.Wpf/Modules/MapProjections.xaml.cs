using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using DevExpress.Demos.DayAndNightLineCalculator;
using DevExpress.Map;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Map;

namespace MapDemo {
    public partial class MapProjections : MapDemoModule {
        readonly DayAndNightViewModel viewModel;

        DayAndNightViewModel ViewModel { get { return viewModel; } }

        public MapProjections() {
            InitializeComponent();
            this.viewModel = ViewModelSource.Create(() => new DayAndNightViewModel(Map));
            this.DataContext = viewModel;
        }
        void Button_Click(object sender, RoutedEventArgs e) {
            ViewModel.SetCurrentDateTime();
        }
        void ButtonBackwardClick(object sender, RoutedEventArgs e) {
            ViewModel.SetPreviousDateTime();
        }
        void ButtonForwardClick(object sender, RoutedEventArgs e) {
            ViewModel.SetNextDateTime();
        }
        void OnProjectionChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e) {
            if(ViewModel != null)
                Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => ViewModel.Update()));
        }
    }
    [POCOViewModel]
    public class DayAndNightViewModel : BindableBase {
        const double DiscreteHoursStep = 0.5;
        const double SteadilyHoursStep = 24.5;
        readonly MapControl map;
        readonly MapItemStorage gridData = new MapItemStorage();
        readonly List<MapItem> excludeFromSouth = new List<MapItem>();
        DispatcherTimer timer;
       
        protected MapControl Map { get { return map; } }
        public virtual GeoPoint SunPosition { get; set; }
        public virtual GeoPoint MoonPosition { get; set; }
        public virtual CoordPointCollection DayAndNightLineVertices { get; set; }
        public virtual bool IsSteady { get; set; }
        public virtual object DataContext { get; set; }
        public MapItemStorage GridData { get { return gridData; } }

        protected void OnIsSteadyChanged() {
            this.timer.IsEnabled = IsSteady;
        }
        public virtual DateTime ActualDateTime { get; set; }
        protected void OnActualDateTimeChanged() {
            UpdateDayAndNightLine();
        }

        public DayAndNightViewModel(MapControl map) {
            this.map = map;
            InitializeTimer();
            Map.Layers[0].DataLoaded += DayAndNightViewModel_DataLoaded;
            Map.Layers[0].Unloaded += DayAndNightViewModel_Unloaded;
            IsSteady = true;
            SunPosition = new GeoPoint();
            MoonPosition = new GeoPoint();
            DayAndNightLineVertices = new CoordPointCollection();
            ActualDateTime = DateTime.UtcNow;
            GenerateGrid();
        }

        void DayAndNightViewModel_DataLoaded(object sender, DataLoadedEventArgs e) {
            foreach(MapShape item in ((MapItemsLoadedEventArgs)e).Items) {
                MapBounds bounds = item.GetBounds();
                if(bounds.Width > 359.0 && bounds.Bottom < -89.9)
                    excludeFromSouth.Add(item);
            }
            ZoomToFit();
        }

        void InitializeTimer() {
            this.timer = new DispatcherTimer {
                Interval = TimeSpan.FromMilliseconds(100),
                IsEnabled = true
            };
            this.timer.Tick += OnTimerTick;
        }
        void UpdateDayAndNightLine() {
            double[] sun3DPosition = DayAndNightLineCalculator.CalculateSunPosition(ActualDateTime);
            GeoPoint sunPosition = new GeoPoint(sun3DPosition[1], sun3DPosition[0]);
            GeoPoint moonPosition = GetOppositePoint(sunPosition);
            CoordPointCollection dayAndNightLineVertices = GetDayAndNightLineVertices(sunPosition, 0.1);
            bool isNorthNight = DayAndNightLineCalculator.CalculateIsNorthNight(sun3DPosition);
            if(isNorthNight)
                AddNorthContour(dayAndNightLineVertices);
            else
                AddSouthContour(dayAndNightLineVertices);
            SunPosition = sunPosition;
            MoonPosition = moonPosition;
            DayAndNightLineVertices = dayAndNightLineVertices;
        }
        GeoPoint GetOppositePoint(GeoPoint sunLocation) {
            double lat = -sunLocation.Latitude;
            double lon = sunLocation.Longitude + 180;
            if(lon > 180)
                lon -= 360;
            return new GeoPoint(lat, lon);
        }
        CoordPointCollection GetDayAndNightLineVertices(GeoPoint sunLocation, double step) {
            CoordPointCollection result = new CoordPointCollection();
            IList<double> latitudes = DayAndNightLineCalculator.GetDayAndNightLineLatitudes(sunLocation.Latitude, sunLocation.Longitude, step);
            double lon = -180;
            foreach(double lat in latitudes) {
                result.Add(new GeoPoint(lat, lon));
                lon += step;
            }
            return result;
        }
        void AddNorthContour(CoordPointCollection dayAndNightLineVertices) {
            double initLat = Math.Ceiling(((GeoPoint)dayAndNightLineVertices[dayAndNightLineVertices.Count - 1]).Latitude);
            for(double latForward = initLat; latForward <= 90.0; latForward++)
                dayAndNightLineVertices.Add(new GeoPoint(latForward, 180));
            for(double lon = 180; lon >= -180; lon--)
                dayAndNightLineVertices.Add(new GeoPoint(90, lon));
            initLat = Math.Ceiling(((GeoPoint)dayAndNightLineVertices[0]).Latitude);
            for(double latBackward = 90; latBackward >= initLat; latBackward--)
                dayAndNightLineVertices.Add(new GeoPoint(latBackward, -180));
        }
        void AddSouthContour(CoordPointCollection dayAndNightLineVertices) {
            double initLat = Math.Ceiling(((GeoPoint)dayAndNightLineVertices[dayAndNightLineVertices.Count - 1]).Latitude);
            for(double lat = initLat; lat >= -90.0; lat--)
                dayAndNightLineVertices.Add(new GeoPoint(lat, 180));
            for(double lon = 180; lon >= -180; lon--)
                dayAndNightLineVertices.Add(new GeoPoint(-90, lon));
            initLat = Math.Ceiling(((GeoPoint)dayAndNightLineVertices[0]).Latitude);
            for(double lat = -90; lat <= initLat; lat++)
                dayAndNightLineVertices.Add(new GeoPoint(lat, -180));
        }
        DateTime GetNextDateTime(DateTime dt) {
            return dt.AddHours(IsSteady ? SteadilyHoursStep : DiscreteHoursStep);
        }
        DateTime GetPreviousDateTime(DateTime dt) {
            return dt.AddHours(-DiscreteHoursStep);
        }
        void GenerateGrid() {
            Brush gridBrush = new SolidColorBrush(Color.FromArgb(50, 255, 255, 255));
            gridBrush.Freeze();
            GenerateLatitudes(gridData, gridBrush);
            GenerateLongitudes(gridData, gridBrush);
        }
        void SetVisibleEx(bool value) {
            this.excludeFromSouth.ForEach((item) => item.Visible = value);
        }
        void ZoomToFit() {
            Map.EnableZooming = true;
            Map.ZoomToFitLayerItems(0.3);
            Map.EnableZooming = false;
        }
        void OnTimerTick(object sender, EventArgs e) {
            ActualDateTime = GetNextDateTime(ActualDateTime);
        }
        void DayAndNightViewModel_Unloaded(object sender, RoutedEventArgs e) {
            this.timer.Stop();
            this.timer.Tick -= OnTimerTick;
        }
        static void GenerateLatitudes(MapItemStorage gridData, Brush gridColor) {
            for(double i = -90; i <= 90; i += 10) {
                CoordPointCollection points = new CoordPointCollection();
                for(int x = -180; x <= 180; x++)
                    points.Add(new GeoPoint(i, x));
                MapPolyline line = new MapPolyline() {
                    Points = points,
                    StrokeStyle = new StrokeStyle() { Thickness = 1 },
                    Stroke = gridColor,
                    IsGeodesic = false
                };
                gridData.Items.Add(line);
            }
        }
        static void GenerateLongitudes(MapItemStorage gridData, Brush gridColor) {
            for(double i = -180; i <= 180; i += 10) {
                CoordPointCollection points = new CoordPointCollection();
                for(int y = -90; y <= 90; y++)
                    points.Add(new GeoPoint(y, i));
                MapPolyline line = new MapPolyline() {
                    Points = points,
                    StrokeStyle = new StrokeStyle() { Thickness = 1 },
                    Stroke = gridColor,
                    IsGeodesic = false
                };
                gridData.Items.Add(line);
            }
        }
        public void SetCurrentDateTime() {
            IsSteady = false;
            ActualDateTime = DateTime.UtcNow;
        }
        public void SetPreviousDateTime() {
            IsSteady = false;
            ActualDateTime = GetPreviousDateTime(ActualDateTime);
        }
        public void SetNextDateTime() {
            IsSteady = false;
            ActualDateTime = GetNextDateTime(ActualDateTime);
        }
        public void Update() {
            var cs = (GeoMapCoordinateSystem)Map.CoordinateSystem;
            SetVisibleEx(!(cs.Projection is NorthPole));
            ZoomToFit();
            if(cs.Projection is LambertAzimuthalEqualAreaProjectionBase)
                Map.CenterPoint = new GeoPoint(((LambertAzimuthalEqualAreaProjectionBase)cs.Projection).OriginLatitude,
                                               ((LambertAzimuthalEqualAreaProjectionBase)cs.Projection).CentralMeridian);
        }
    }

    public class Projection {
        public string Name { get; set; }
        public ProjectionBase PrjInstance { get; set; }
        public string ParentPrjName { get; set; }
    }

    public class SouthPole : LambertAzimuthalEqualAreaProjectionBase {
        protected override bool IsPredefined { get { return false; } }
        public SouthPole() {
            OriginLatitude = -90.0;
            BoundingBox = new MapBounds(-180.0, -90.0, 180.0, 0.0);
        }
        public override GeoPoint MapUnitToGeoPoint(MapUnit mapPoint) {
            GeoPoint res = base.MapUnitToGeoPoint(mapPoint);
            if(mapPoint.X > 0.5 && mapPoint.Y > 0.5)
                res = new GeoPoint(res.GetY(), 180.0 + res.GetX());
            if(mapPoint.X <= 0.5 && mapPoint.Y > 0.5)
                res = new GeoPoint(res.GetY(), res.GetX() - 180.0);
            return res;
        }
    }

    public class NorthPole : LambertAzimuthalEqualAreaProjectionBase {
        protected override bool IsPredefined { get { return false; } }
        public NorthPole() {
            OriginLatitude = 90.0;
            BoundingBox = new MapBounds(-180.0, 0.0, 180.0, 90.0);
        }
        public override GeoPoint MapUnitToGeoPoint(MapUnit mapPoint) {
            GeoPoint res = base.MapUnitToGeoPoint(mapPoint);
            if(mapPoint.X >= 0.5 && mapPoint.Y < 0.5)
                res = new GeoPoint(res.GetY(), 180.0 + res.GetX());
            if(mapPoint.X < 0.5 && mapPoint.Y < 0.5)
                res = new GeoPoint(res.GetY(), res.GetX() - 180.0);
            return res;
        }
    }
}
