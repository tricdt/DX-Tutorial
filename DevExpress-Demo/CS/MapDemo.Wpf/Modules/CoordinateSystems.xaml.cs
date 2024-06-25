using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using System.Xml.Linq;
using DevExpress.Map;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Map;

namespace MapDemo {
    [POCOViewModel]
    public class HotelsViewModel : BindableBase {
        public virtual CoordinateSystemType CoordinateSystemType { get; set; }
        public virtual ObservableCollection<HotelInfo> HotelInfos { get; set; }
        public virtual HotelInfo SelectedHotel { get; set; }
        public virtual double ZoomLevel { get; set; }
        public virtual int MinZoomLevel { get; set; }
        public virtual int MaxZoomLevel { get; set; }
        public virtual CoordPoint CenterPoint { get; set; }
        public virtual bool UseSprings { get; set; }

        GeoPoint mapDefaultPosition = new GeoPoint(-21.164, -175.127);
        int geoMinZoomLevel = 12;
        int geoMaxZoomLevel = 15;
        int hotelMinZoomLevel = 2;
        int hotelMaxZoomLevel = 6;
        double mapDefaultZoomLevel = 13.4;

        public HotelsViewModel() {
            CoordinateSystemType = CoordinateSystemType.Geo;
            MinZoomLevel = geoMinZoomLevel;
            MaxZoomLevel = geoMaxZoomLevel;
            ZoomLevel = mapDefaultZoomLevel;
            CenterPoint = mapDefaultPosition;
            HotelInfos = LoadDataFromXML();
            UseSprings = true;
        }

        protected void OnSelectedHotelChanged() {
          
            if (SelectedHotel == null) {
                UseSprings = false;
                MinZoomLevel = geoMinZoomLevel;
                MaxZoomLevel = geoMaxZoomLevel;
                CoordinateSystemType = CoordinateSystemType.Geo;
                CenterPoint = mapDefaultPosition;
                UseSprings = true;
                ZoomLevel = mapDefaultZoomLevel;
            }
            else {
                UseSprings = false;
                MinZoomLevel = hotelMinZoomLevel;
                MaxZoomLevel = hotelMaxZoomLevel;
                CoordinateSystemType = CoordinateSystemType.Cartesian;
                CenterPoint = new CartesianPoint(0, 0);
                UseSprings = true;
                ZoomLevel = SelectedHotel.ZoomLevel;
            }
        
        }
        ObservableCollection<HotelInfo> LoadDataFromXML() {
            XDocument document = DataLoader.LoadXmlFromResources("/Data/Hotels.xml");
            ObservableCollection<HotelInfo> infos = new ObservableCollection<HotelInfo>();
            if (document != null) {
                foreach (XElement element in document.Element("Hotels").Elements()) {
                    HotelInfo hotelInfo = new HotelInfo();
                    hotelInfo.Name = element.Element("Name").Value;
                    hotelInfo.Latitude = Convert.ToDouble(element.Element("Latitude").Value, CultureInfo.InvariantCulture);
                    hotelInfo.Longitude = Convert.ToDouble(element.Element("Longitude").Value, CultureInfo.InvariantCulture);
                    hotelInfo.ShpFileUri = new Uri(element.Element("ShpFileUri").Value, UriKind.RelativeOrAbsolute);
                    hotelInfo.ZoomLevel = Convert.ToInt32(element.Element("ZoomLevel").Value);
                    hotelInfo.ImageUri = new Uri(element.Element("ImageUri").Value, UriKind.RelativeOrAbsolute);
                    hotelInfo.HighlightedImageUri = new Uri(element.Element("HighlightedImageUri").Value, UriKind.RelativeOrAbsolute);
                    infos.Add(hotelInfo);
                }
            }
            return infos;
        }
    }

    public partial class CoordinateSystems : MapDemoModule {
        public HotelsViewModel ViewModel { get { return LayoutRoot.DataContext as HotelsViewModel; } }

        public CoordinateSystems() {
            InitializeComponent();
            LayoutRoot.DataContext = ViewModelSource.Create(() => new HotelsViewModel());
        }
        void Label_MouseDown(object sender, MouseButtonEventArgs e) {
            ViewModel.SelectedHotel = null;
        }
    }

    public class HotelInfo {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Uri ShpFileUri { get; set; }
        public string Name { get; set; }
        public int ZoomLevel { get; set; }
        public Uri ImageUri { get; set; }
        public Uri HighlightedImageUri { get; set; }
    }

    public enum CoordinateSystemType { Geo, Cartesian }
}
