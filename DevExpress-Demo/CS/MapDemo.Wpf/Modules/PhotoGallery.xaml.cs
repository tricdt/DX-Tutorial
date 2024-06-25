using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Map;
using DevExpress.Map;

namespace MapDemo {
    public partial class PhotoGallery : MapDemoModule {
        CitiesViewModel ViewModel { get { return LayoutRoot.DataContext as CitiesViewModel; } }

        public PhotoGallery() {
            InitializeComponent();
            CitiesViewModel viewModel = ViewModelSource.Create(() => new CitiesViewModel(map));
            LayoutRoot.DataContext = viewModel;
            placePointer.Content = viewModel;
        }
        void GalleryItemClick(object sender, RoutedEventArgs e) {
            PhotoGalleryItemControl item = sender as PhotoGalleryItemControl;
            if(item != null)
                ViewModel.SelectedPlace = item.DataContext as PlaceInfo;
        }
        void OnGalleryClose(object sender, RoutedEventArgs e) {
            ViewModel.SelectedCity = null;
        }
        void OnBackClick(object sender, RoutedEventArgs e) {
            ViewModel.SelectedCity = null;
        }
        void photoGallery_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            ViewModel.SelectedCity = null;
        }
        void placeControl_ShowNextSight(object sender, RoutedEventArgs e) {
            ViewModel.ShowNextSight();
        }
        void placeControl_ShowPreviousSight(object sender, RoutedEventArgs e) {
            ViewModel.ShowPrevSight();
        }
    }

    public enum ViewType {
        Map,
        Gallery,
        Detail
    }

    [POCOViewModel]
    public class CitiesViewModel : BindableBase {
        const int InitialZoomLevel = 5;
        const int MaxZoomLevelInWorldView = 7;
        const int MinZoomLevelInWorldView = 5;
        const int MinZoomLevelInCityView = 15;
        const int MaxZoomLevelInCityView = 18;
        
        public virtual ObservableCollection<CityInfo> Cities { get; set; }
        public virtual ObservableCollection<CityInfo> CitiesMini { get; set; }
        public virtual CityInfo SelectedCity { get; set; }
        protected void OnSelectedCityChanged() {
            SelectedPlace = null;
            UpdateViewType();
        }
        public virtual PlaceInfo SelectedPlace { get; set; }
        protected void OnSelectedPlaceChanged() {
            UpdateViewType();
        }
        public virtual ViewType ViewType { get; set; }
        public virtual CoordPoint CenterPoint { get; set; }
        public virtual Point CityPoint { get; set; }
        public virtual double ZoomLevel { get; set; }
        public virtual int MinZoomLevel { get; set; }
        public virtual int MaxZoomLevel { get; set; }
        readonly MapControl map;

        LayerBase Layer { get { return map.Layers.Count > 0 ? map.Layers[0] : null; } }

        public CitiesViewModel(MapControl map) {
            this.map = map;
            Cities = new ObservableCollection<CityInfo>();
            CitiesMini = new ObservableCollection<CityInfo>();
            ZoomLevel = InitialZoomLevel;
            MinZoomLevel = MinZoomLevelInWorldView;
            MaxZoomLevel = MaxZoomLevelInWorldView;
            ViewType = ViewType.Map;
            CenterPoint = new GeoPoint(47.5, 2);
            CityPoint = new Point(0, 0);
            LoadDataFromXML();
        }
        void LoadDataFromXML() {
            XDocument document = DataLoader.LoadXmlFromResources("/Data/CitiesData.xml");
            if(document != null) {
                foreach(XElement element in document.Element("Cities").Elements()) {
                    string cityName = element.Element("CityName").Value;
                    GeoPoint cityLocation = new GeoPoint(Convert.ToDouble(element.Element("Latitude").Value, CultureInfo.InvariantCulture),
                        Convert.ToDouble(element.Element("Longitude").Value, CultureInfo.InvariantCulture));
                    CityInfo cityInfo = new CityInfo(cityName, cityLocation);
                    foreach(XElement placeElement in element.Element("Places").Elements()) {
                        string placeName = placeElement.Element("Name").Value;
                        GeoPoint placeLocation = new GeoPoint(Convert.ToDouble(placeElement.Element("Latitude").Value, CultureInfo.InvariantCulture),
                            Convert.ToDouble(placeElement.Element("Longitude").Value, CultureInfo.InvariantCulture));
                        string placeDescription = placeElement.Element("Description").Value;
                        Uri placeImageUri = new Uri(placeElement.Element("ImageUri").Value, UriKind.RelativeOrAbsolute);
                        cityInfo.Places.Add(new PlaceInfo(placeName, cityName, placeLocation, placeDescription, new BitmapImage(placeImageUri)));
                    }
                    Cities.Add(cityInfo);
                    CitiesMini.Add(cityInfo);
                }
            }
        }
        void UpdateViewType() {
            if(SelectedCity != null)
                ViewType = SelectedPlace != null ? ViewType.Detail : ViewType.Gallery;
            else
                ViewType = ViewType.Map;
        }
        protected void OnViewTypeChanged() {
            switch(ViewType) {
                case ViewType.Map:
                    MinZoomLevel = MinZoomLevelInWorldView;
                    MaxZoomLevel = MaxZoomLevelInWorldView;
                    ZoomLevel = 5;
                    break;
                case ViewType.Gallery:
                    MinZoomLevel = MinZoomLevelInWorldView;
                    MaxZoomLevel = MaxZoomLevelInWorldView;
                    ZoomLevel = 5;
                    CityPoint = Layer.GeoToScreenPoint(SelectedCity.Location);
                    break;
                case ViewType.Detail:                  
                    MinZoomLevel = MinZoomLevelInCityView;
                    MaxZoomLevel = MaxZoomLevelInCityView;
                    ZoomLevel = 17;
                    CenterPoint = SelectedPlace.Location;
                    break;
                default:
                    goto case ViewType.Map;
            }
        }
        public void ShowNextSight() {
            if(SelectedCity != null && SelectedPlace != null) {
                int index = SelectedCity.Places.IndexOf(SelectedPlace) + 1;
                SelectedPlace = index < SelectedCity.Places.Count ? SelectedCity.Places[index] : SelectedCity.Places[0];
                CenterPoint = SelectedPlace.Location;
            }
        }
        public void ShowPrevSight() {
            if(SelectedCity != null && SelectedPlace != null) {
                int index = SelectedCity.Places.IndexOf(SelectedPlace) - 1;
                SelectedPlace = index < 0 ? SelectedCity.Places[SelectedCity.Places.Count - 1] : SelectedCity.Places[index];
                CenterPoint = SelectedPlace.Location;
            }
        }
    }

    public class PlaceInfo {
        readonly string name;
        readonly string cityName;
        readonly GeoPoint location;
        readonly string description;
        readonly BitmapImage imageSource;

        public string Name { get { return name; } }
        public string CityName { get { return cityName; } }
        public GeoPoint Location { get { return location; } }
        public string Description { get { return description; } }
        public BitmapImage ImageSource { get { return imageSource; } }

        public PlaceInfo(string name, string cityName, GeoPoint location, string description, BitmapImage imageSource) {
            this.name = name;
            this.cityName = cityName;
            this.location = location;
            this.description = description;
            this.imageSource = imageSource;
        }
    }

    public class CityInfo {
        readonly string name;
        readonly GeoPoint location;
        readonly List<PlaceInfo> places = new List<PlaceInfo>();

        public string Name { get { return name; } }
        public GeoPoint Location { get { return location; } }
        public List<PlaceInfo> Places { get { return places; } }

        public CityInfo(string name, GeoPoint location) {
            this.name = name;
            this.location = location;
        }
    }
}
