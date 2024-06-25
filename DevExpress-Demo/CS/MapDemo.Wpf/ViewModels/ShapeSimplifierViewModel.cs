using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;
using DevExpress.Map;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Map;

namespace MapDemo {
    public enum ActiveShapeType {
        Polygon, Polyline
    }

    [POCOViewModel]
    public class ShapeSimplifierViewModel {
        public static int MaxToleranceValue = 280;

        readonly DispatcherTimer timer;
        int toleranceDelta = -1;

        public virtual double Tolerance { get; set; }
        public virtual ActiveShapeType ActiveShape { get; set; }
        public virtual MapItemStorage PolygonData { get; set; }
        public virtual MapItemStorage PolylineData { get; set; }
        public virtual MapItemStorage EtalonData { get; set; }
        public virtual string Status { get; set; }
        public virtual bool AutoMode { get; set; }
        public virtual ICommand Simplify { get; set; }

        public ShapeSimplifierViewModel() {
            Tolerance = MaxToleranceValue;
            this.timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(30), IsEnabled = true };
            this.timer.Tick += OnTimerTick;
            AutoMode = true;

            ShapefileDataAdapter adapter = new ShapefileDataAdapter() { FileUri = new ShapefileWorldResources().IcelandFileUri };
            adapter.LoadData();

            MapPolyline line = new MapPolyline() { StrokeStyle = new StrokeStyle() { Thickness = 3 } };
            line.Points.Assign(((ISupportCoordPoints)adapter.DisplayItems.First()).Points);
            MapItemStorage storage = new MapItemStorage();
            storage.Items.Add(line);
            PolylineData = storage;

            MapItemStorage polygonData = new MapItemStorage();
            polygonData.Items.Add(adapter.DisplayItems.First());
            PolygonData = polygonData;

            MapPolyline etalonLine = new MapPolyline();
            etalonLine.Points.Assign(((ISupportCoordPoints)adapter.DisplayItems.First()).Points);
            storage = new MapItemStorage();
            storage.Items.Add(etalonLine);
            EtalonData = storage;
        }

        protected void OnToleranceChanged() {
            SimplifyItems();
        }
        protected void OnActiveShapeChanged() {
            SimplifyItems();
        }
        protected void OnAutoModeChanged() {
            timer.IsEnabled = AutoMode;
        }
        void OnTimerTick(object sender, EventArgs e) {
            this.toleranceDelta = Tolerance == MaxToleranceValue ? -1 : 
                Tolerance == 0 ? 1 : this.toleranceDelta;
            Tolerance =  Tolerance + this.toleranceDelta;
        }
        double CalculateTolerance(double value) {
            return Math.Max(Math.Round(100 * Math.Abs(Math.Pow(value / MaxToleranceValue, 5)), 4), 0.001);
        }
        void SimplifyItems() {
            if(Simplify != null) {
                double tolerance = CalculateTolerance(Tolerance);
                Simplify.Execute(new object[] { ActiveShape == ActiveShapeType.Polygon ? PolygonData.DisplayItems : PolylineData.Items, tolerance });
                UpdateCounterText(tolerance);
            }
        }
        void UpdateCounterText(double tolerance) {
            MapDataAdapterBase actualStorage = ActiveShape == ActiveShapeType.Polygon ?  (MapDataAdapterBase)PolygonData : PolylineData;
            ISupportCoordPoints shape = actualStorage.DisplayItems.ElementAt(0) as ISupportCoordPoints;
            if(shape != null)
                Status = string.Format("{0} points, {1:0.000}% tolerance", shape.Points.Count - 2, tolerance);
        }
    }

    public class ActiveShapeTypeToLayerVisibilityConverter : MarkupExtension, IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value.Equals(parameter) ? Visibility.Visible : Visibility.Collapsed;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
