using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using DevExpress.Map;
using DevExpress.Xpf.Map;

namespace MapDemo {
    public enum TemperatureScale { Fahrenheit, Celsius };

    public class DemoValuesProvider {
        public string DevexpressBingKey { get { return BingKeyProvider.BingKey; } }
        public IEnumerable<BingMapKind> BingMapKinds { get { return new BingMapKind[] { BingMapKind.Area, BingMapKind.Road, BingMapKind.Hybrid,
            BingMapKind.RoadLight, BingMapKind.RoadGray, BingMapKind.RoadDark }; } }
        public IEnumerable<OpenStreetMapKind> OSMBaseLayers { get { return new OpenStreetMapKind[] { OpenStreetMapKind.Basic, OpenStreetMapKind.CycleMap, OpenStreetMapKind.Hot, OpenStreetMapKind.Transport }; } }
        public IEnumerable<object> OSMOverlayLayers { get { return new object[] { "None", OpenStreetMapKind.SeaMarks, OpenStreetMapKind.HikingRoutes, OpenStreetMapKind.CyclingRoutes, OpenStreetMapKind.PublicTransport }; } }
        public IEnumerable<string> ShapeMapTypes { get { return new string[] { "GDP", "Population", "Political" }; } }
        public IEnumerable<string> ShapefileMapTypes { get { return new string[] { "World", "Africa", "South America", "North America", "Australia", "Eurasia" }; } }

        public IEnumerable<TemperatureScale> TemperatureUnit { get { return new TemperatureScale[] { TemperatureScale.Celsius, TemperatureScale.Fahrenheit }; } }
        public IEnumerable<MarkerType> BubbleMarkerTypes { get { return new MarkerType[] { MarkerType.Circle, MarkerType.Cross, MarkerType.Diamond, MarkerType.Hexagon,
                                                                MarkerType.InvertedTriangle, MarkerType.Triangle, MarkerType.Pentagon, MarkerType.Plus,
                                                                MarkerType.Square, MarkerType.Star5, MarkerType.Star6, MarkerType.Star8 }; } }
        public IEnumerable<BingRouteOptimization> RouteOptimizations { get { return new BingRouteOptimization[] { BingRouteOptimization.MinimizeTimeWithTraffic, BingRouteOptimization.MinimizeTime, BingRouteOptimization.MinimizeDistance }; } }
        public IEnumerable<BingTrafficIncidentType> TrafficIncidentTypes { get { return new BingTrafficIncidentType[] { BingTrafficIncidentType.Accident, BingTrafficIncidentType.Congestion,
            BingTrafficIncidentType.DisabledVehicle, BingTrafficIncidentType.MassTransit, BingTrafficIncidentType.Miscellaneous, BingTrafficIncidentType.OtherNews, BingTrafficIncidentType.PlannedEvent,
            BingTrafficIncidentType.RoadHazard, BingTrafficIncidentType.Construction, BingTrafficIncidentType.Alert, BingTrafficIncidentType.Weather }; } }


        static List<Projection> projections = null;
        public List<Projection> Projections {
            get {
                if(projections == null)
                    projections = PopulateProjectionData();
                return projections;
            }
        }
        public Projection DefaultProjection { get { return Projections[12]; } }

        static List<Projection> PopulateProjectionData() {
            Projection LAEAParent = new Projection() { Name = "Lambert Azimuthal Equal Area", PrjInstance = null };
            return new List<Projection>()  {
            new Projection() { Name = "Spherical Mercator", PrjInstance = new SphericalMercatorProjection() },
            new Projection() { Name = "Equal Area", PrjInstance = new EqualAreaProjection()},
            new Projection() { Name = "Equirectangular", PrjInstance = new EquirectangularProjection()},
            new Projection() { Name = "Elliptical Mercator", PrjInstance = new EllipticalMercatorProjection()},
            new Projection() { Name = "Miller", PrjInstance = new MillerProjection()},
            new Projection() { Name = "Equidistant", PrjInstance = new EquidistantProjection() },
            new Projection() { Name = "Lambert Cylindrical Equal Area", PrjInstance = new LambertCylindricalEqualAreaProjection() },
            LAEAParent,
            new Projection() { Name = "ETRS89", PrjInstance = new Etrs89LambertAzimuthalEqualAreaProjection(), ParentPrjName = LAEAParent.Name},
            new Projection() { Name = "North Pole", PrjInstance = new NorthPole(), ParentPrjName = LAEAParent.Name},
            new Projection() { Name = "South Pole", PrjInstance = new SouthPole(), ParentPrjName = LAEAParent.Name},
            new Projection() { Name = "Braun Stereographic", PrjInstance = new BraunStereographicProjection() },
            new Projection() { Name = "Kavrayskiy VII", PrjInstance = new KavrayskiyProjection() },
            new Projection() { Name = "Sinusoidal", PrjInstance = new SinusoidalProjection() },
            new Projection() { Name = "EPSG:4326", PrjInstance = new EPSG4326Projection()},
            };
        }
    }

    public static class DemoUtils {
        public static IEnumerable<MapControl> FindMap(DependencyObject obj) {
            if(obj != null) {
                for(int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++) {
                    DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                    if(child != null && child is MapControl) {
                        yield return (MapControl)child;
                    }
                    foreach(MapControl childOfChild in FindMap(child)) {
                        yield return childOfChild;
                    }
                }
            }
        }
    }

    public static class DataLoader {
        static Stream GetStream(string fileName) {
            fileName = "/MapDemo;component" + fileName;
            Uri uri = new Uri(fileName, UriKind.RelativeOrAbsolute);
            return Application.GetResourceStream(uri).Stream;
        }

        public static XDocument LoadXmlFromResources(string fileName) {
            try {
                return XDocument.Load(GetStream(fileName));
            } catch {
                return null;
            }
        }
        public static Stream LoadStreamFromResources(string fileName) {
            try {
                return GetStream(fileName);
            } catch {
                return null;
            }
        }
    }

    public class DoubleToTimeSpanConvert : IValueConverter {
        #region IValueConvector implementation
        object IValueConverter.Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            double doubleValue = 3600 * (double)value;
            return new TimeSpan(0, 0, (int)Math.Ceiling(doubleValue));
        }
        object IValueConverter.ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
        #endregion
    }
    public class SelectedLayerToVisibilityConverter : IValueConverter {
        #region IValueConverter implementation
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value is string ? Visibility.Collapsed : Visibility.Visible;
        }
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
        #endregion
    }
    public class SelectedLayerToKindConverter : IValueConverter {
        #region IValueConverter implementation
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value is string ? OpenStreetMapKind.SeaMarks : value;
        }
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
        #endregion
    }
    public class PlaneInfoToPathVisibilityConverter : IValueConverter {
        #region IValueConverter implementation
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value == parameter;
        }
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
        #endregion
    }

    public class RangeColor {
        readonly int rangeMin;
        readonly int rangeMax;
        readonly Color fill;

        public int RangeMin {
            get { return rangeMin; }
        }
        public int RangeMax {
            get { return rangeMax; }
        }
        public Color Fill {
            get { return fill; }
        }

        public RangeColor(int rangeMin, int rangeMax, Color fill) {
            this.rangeMin = rangeMin;
            this.rangeMax = rangeMax;
            this.fill = fill;
        }
    }

    public class ViewTypeToBoolConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(targetType == typeof(bool) && value is ViewType && parameter is ViewType)
                return (ViewType)value == (ViewType)parameter;
            return false;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value is bool)
                if(targetType == typeof(ViewType)) {
                    return (bool)value ? ViewType.Gallery : ViewType.Map;
                }
            return null;
        }
    }

    public class ViewTypeToVisibilityConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (targetType == typeof(Visibility) && value is ViewType && parameter is ViewType)
                return (ViewType)value == (ViewType)parameter ? Visibility.Visible : Visibility.Hidden;
            return Visibility.Hidden;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is Visibility)
                if (targetType == typeof(ViewType)) {
                    return (Visibility)value == Visibility.Visible ? ViewType.Gallery : ViewType.Map;
                }
            return null;
        }
    }
    public class CoordinateSystemTypeToVisibilityConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(targetType == typeof(Visibility) && value is CoordinateSystemType && parameter is CoordinateSystemType)
                return (CoordinateSystemType)value == (CoordinateSystemType)parameter ? Visibility.Visible : Visibility.Hidden;
            return Visibility.Hidden;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value is Visibility)
                if(targetType == typeof(CoordinateSystemType))
                    return (Visibility)value == Visibility.Visible ? CoordinateSystemType.Geo : CoordinateSystemType.Cartesian;
            return null;
        }
    }
    public class DoubleToRenderTransforOffsetConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (targetType == typeof(double) && value is double) {
                double doubleValue = (double)value;
                if (parameter is double)
                    return doubleValue / (double)parameter;
                return 0;
            }
            return null;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
    public class CoordinateSystemTypeToCoordinateSystemConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (targetType == typeof(MapCoordinateSystem) && value is CoordinateSystemType && (CoordinateSystemType)value == CoordinateSystemType.Cartesian)
                return new CartesianMapCoordinateSystem();
            return new GeoMapCoordinateSystem();
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value is MapCoordinateSystem && targetType == typeof(CoordinateSystemType))
                return value is GeoMapCoordinateSystem ? CoordinateSystemType.Geo : CoordinateSystemType.Cartesian;
            return null;
        }
    }
    public class ItemToTextConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value is MapItem) {
                MapPath path = value as MapPath;
                if(path != null)
                    return HotelRoomTooltipHelper.CalculateTitle(path);
                ShapeTitle title = value as ShapeTitle;
                if(title != null)
                    return HotelRoomTooltipHelper.CalculateTitle(title.MapShape);
            }
            return null;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
    public class ItemToImageSourceConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value is MapItem) {
                ShapeTitle title = value as ShapeTitle;
                return HotelRoomTooltipHelper.GetItemImageSource(title != null ? title.MapShape : (MapItem)value);
            }
            return null;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
    public class ItemToImageVisibilityConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is MapItem && targetType == typeof(Visibility)) {
                ShapeTitle title = value as ShapeTitle;
                string imageSource = HotelRoomTooltipHelper.GetItemImageSource(title != null ? title.MapShape : (MapItem)value);
                return string.IsNullOrWhiteSpace(imageSource) ? Visibility.Collapsed : Visibility.Visible;
            }
            return null;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
    public class CountToMatrixTransformConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            List<MapItem> itemsList = value as List<MapItem>;
            double count = itemsList != null ? itemsList.Count : 1.0;
            double scaleFactor = Math.Log10(count / 5.0) * 0.02 + 0.05;
            double offsetKoefX = 318;
            double offsetKoefY = -455;
            return new MatrixTransform(scaleFactor, 0, 0, scaleFactor, scaleFactor * offsetKoefX, scaleFactor * offsetKoefY).Matrix;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
    public class CountToTextConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            List<MapItem> itemsList = value as List<MapItem>;
            int count = itemsList != null ? itemsList.Count : 1;
            return string.Format("Cluster contains {0} item(s)", count);
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
    public class MapTypeToVisibilityConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value is int && targetType == typeof(Visibility) && parameter is string) {
                int index = (int)value;
                string mapType = (string)parameter;
                if(index == 1 && mapType == "population")
                    return Visibility.Visible;
                if(index == 0 && mapType == "gdp")
                    return Visibility.Visible;
                return Visibility.Collapsed;
            }
            return null;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
    public class ProviderNameToImageVisibilityConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value is ProviderName && targetType == typeof(Visibility)) {
                return (ProviderName)value == ProviderName.Bing ? Visibility.Visible : Visibility.Collapsed;
            }
            return null;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
    public class ProviderNameToCopyrightTextConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value is ProviderName) {
                ProviderName providerName = (ProviderName)value;
                if(providerName == ProviderName.Bing)
                    return "Copyright © " + DateTime.Now.Year + " Microsoft and its suppliers. All rights reserved.";
                if(providerName == ProviderName.Osm)
                    return "© OpenStreetMap contributors";
                return null;
            }
            return null;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
    public class BoolToCircularScrollingConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value is bool && targetType == typeof(CircularScrollingMode)) {
                return (bool)value ? CircularScrollingMode.TilesAndVectorItems : CircularScrollingMode.None;
            }
            return null;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
    public class SelectedItemToVisibilityConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(targetType == typeof(Visibility))
                return value == null ? Visibility.Hidden : Visibility.Visible;
            return null;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
    public class CountryToFlagConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value is string && targetType == typeof(ImageSource)) {
                string name = (string)value;
                if(string.IsNullOrEmpty(name))
                    return null;
                return "..\\Images\\Flags\\" + name + ".png";
            }
            return null;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class ShapefileWorldResources {
        public Uri CountriesFileUri { get { return new Uri("/MapDemo;component/Data/Shapefiles/Maps/Countries.shp", UriKind.RelativeOrAbsolute); } }
        public Uri AfricaFileUri { get { return new Uri("/MapDemo;component/Data/Shapefiles/Maps/Africa.shp", UriKind.RelativeOrAbsolute); } }
        public Uri SouthAmericaFileUri { get { return new Uri("/MapDemo;component/Data/Shapefiles/Maps/SouthAmerica.shp", UriKind.RelativeOrAbsolute); } }
        public Uri NorthAmericaFileUri { get { return new Uri("/MapDemo;component/Data/Shapefiles/Maps/NorthAmerica.shp", UriKind.RelativeOrAbsolute); } }
        public Uri AustraliaFileUri { get { return new Uri("/MapDemo;component/Data/Shapefiles/Maps/Australia.shp", UriKind.RelativeOrAbsolute); } }
        public Uri EurasiaFileUri { get { return new Uri("/MapDemo;component/Data/Shapefiles/Maps/Eurasia.shp", UriKind.RelativeOrAbsolute); } }
        public Uri IcelandFileUri { get { return new Uri("/MapDemo;component/Data/Shapefiles/Iceland.shp", UriKind.RelativeOrAbsolute); } }

        public ShapefileWorldResources() {
        }
    }
    
    public class PhotoGalleryResources {
        public ImageSource CityInformationControlSource { get { return new BitmapImage(new Uri("/MapDemo;component/Images/PhotoGallery/CityInformationControl.png", UriKind.RelativeOrAbsolute)); } }
        public ImageSource LabelControlImageSource { get { return new BitmapImage(new Uri("/MapDemo;component/Images/PhotoGallery/Label.png", UriKind.RelativeOrAbsolute)); } }
        public ImageSource PlaceInfoControlPrevImageSource { get { return GetSvgImage("pack://application:,,,/MapDemo;component/Images/PhotoGallery/PrevPlace.svg", new Size(16, 16)); } }
        public ImageSource PlaceInfoControlNextImageSource { get { return GetSvgImage("pack://application:,,,/MapDemo;component/Images/PhotoGallery/NextPlace.svg", new Size(16, 16)); } }

        public PhotoGalleryResources() {
        }
        static ImageSource GetSvgImage(string imagePath, Size imageSize) {
            var extension = new DevExpress.Xpf.Core.SvgImageSourceExtension() { Uri = new Uri(imagePath), Size = imageSize };
            return (ImageSource)extension.ProvideValue(null);
        }

    }
    public static class HotelRoomTooltipHelper {
        #region inner class
        class HotelImagesGenerator {
            class PathsIndexPair {
                public string[] Paths { get; set; }
                public int Index { get; set; }
            }

            static readonly string[] Categories = new string[] { "Restaurant", "MeetingRoom", "Bathroom", "Bedroom", "Outofdoors", "ServiceRoom", "Pool", "Lobby" };
            const string basePath = "/MapDemo;component/";

            int hotelIndex = 0;
            List<PathsIndexPair> filesWithIndices = new List<PathsIndexPair>();

            public int HotelIndex {
                get { return hotelIndex; }
                set {
                    hotelIndex = value;
                    UpdateIndices();
                }
            }

            public HotelImagesGenerator() {
                foreach(string category in Categories)
                    filesWithIndices.Add(new PathsIndexPair() { Index = 0, Paths = GetAvailableFiles(category) });
            }
            void UpdateIndices() {
                filesWithIndices[0].Index = hotelIndex * 2;
                filesWithIndices[1].Index = 0;
                filesWithIndices[2].Index = hotelIndex * 4;
                filesWithIndices[6].Index = hotelIndex;
            }
            string[] GetAvailableFiles(string category) {
                var asm = Assembly.GetExecutingAssembly();
                string resName = asm.GetName().Name + ".g.resources";
                using(var stream = asm.GetManifestResourceStream(resName))
                using(var reader = new ResourceReader(stream)) {
                    return reader.Cast<DictionaryEntry>().Select(entry => (string)entry.Key).Where(entry => entry.StartsWith("images/hotels/" + category.ToLowerInvariant())).ToArray();
                }
            }
            string GetImagePath(int category, string name, int roomCat) {
                if(category == 4)
                    filesWithIndices[3].Index = roomCat;
                return GetCategoryImagePath(filesWithIndices[category - 1]);
            }
            string GetCategoryImagePath(PathsIndexPair pathsWithIndex) {
                if(pathsWithIndex.Paths.Length == 0)
                    return null;
                int index = pathsWithIndex.Index % pathsWithIndex.Paths.Length;
                pathsWithIndex.Index++;
                return pathsWithIndex.Paths[index];
            }
            public string GetItemImagePath(MapItem item) {
                string imagePath = GetImagePath((int)item.Attributes["CATEGORY"].Value, item.Attributes["NAME"].Value.ToString(), (int)item.Attributes["ROOMCAT"].Value);
                if(imagePath == null)
                    return null;
                string totalPath = basePath + imagePath;
                item.Attributes.Add(new MapItemAttribute() { Name = "IMAGESOURCE", Value = totalPath });
                return totalPath;
            }
        }
        #endregion

        static HotelImagesGenerator imagesGenerator = new HotelImagesGenerator();

        public static string CalculateTitle(MapItem item) {
            int category = (int)item.Attributes["CATEGORY"].Value;
            string text = (string)item.Attributes["NAME"].Value;
            return category == 4 ? string.Format("Room: {0}", text) : text;
        }
        public static string GetItemImageSource(MapItem item) {
            if(item == null)
                return null;
            MapItemAttribute attr = item.Attributes["IMAGESOURCE"];
            return attr != null ? (string)attr.Value : imagesGenerator.GetItemImagePath(item);
        }
        public static void UpdateHotelIndex(int index) {
            imagesGenerator.HotelIndex = index;
        }
    }
    public static class MapArrowsDemoHelper {
        static List<WindDataItem> dataItems;
        public static List<WindDataItem> DataItems {
            get {
                if(dataItems == null) {
                    dataItems = new List<WindDataItem>();
                    try {
                        using(StreamReader reader = new StreamReader(DataLoader.LoadStreamFromResources("/Data/windData.csv"))) {
                            while(!reader.EndOfStream) {
                                var values = reader.ReadLine().Split(' ');
                                dataItems.Add(new WindDataItem(double.Parse(values[1], CultureInfo.InvariantCulture),
                                    double.Parse(values[2], CultureInfo.InvariantCulture),
                                    double.Parse(values[3], CultureInfo.InvariantCulture),
                                    double.Parse(values[4], CultureInfo.InvariantCulture)));
                            }
                        }
                    }
                    catch {
                        throw new Exception("It's impossible to load wind data");
                    }
                }
                return dataItems;
            }
        }
    }
    public class WindDataItem {
        const double ArrowLength = 70000;

        public CoordPoint P1 { get; private set; }
        public CoordPoint P2 { get; private set; }
        public double Speed { get; private set; }

        public WindDataItem(double latitude, double longitude, double direction, double speed) {
            P1 = new GeoPoint(latitude, longitude);
            P2 = GeoUtils.CalculateDestinationPoint(new GeoPoint(latitude, longitude), ArrowLength, direction);
            Speed = speed;
        }
    }
}
