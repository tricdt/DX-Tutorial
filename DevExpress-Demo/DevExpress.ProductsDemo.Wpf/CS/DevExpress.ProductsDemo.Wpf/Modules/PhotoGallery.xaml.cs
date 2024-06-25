using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using DevExpress.Map;
using DevExpress.Mvvm;
using DevExpress.Xpf.Map;

namespace ProductsDemo.Modules {
    public partial class PhotoGallery : UserControl {
        public PhotoGallery() {
            InitializeComponent();
            CitiesViewModel viewModel = new CitiesViewModel(map, Resources["citySmallIconTemplate"] as DataTemplate);
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
        CitiesViewModel ViewModel { get { return LayoutRoot.DataContext as CitiesViewModel; } }
    }    


    public class CitiesViewModel : BindableBase {
        const int MaxZoomLevelInWorldView = 7;
        const int MinZoomLevelInWorldView = 5;
        const int MinZoomLevelInCityView = 15;
        const int MaxZoomLevelInCityView = 18;

        ObservableCollection<MapCustomElement> citiesCore = null;
        CityInfo selectedCityCore = null;
        PlaceInfo selectedPlaceCore = null;
        PhotoGalleryViewType viewTypeCore = PhotoGalleryViewType.Map;
        CoordPoint centerPointCore = new GeoPoint(47.5, 2);
        Point cityPointCore = new Point(0, 0);
        double zoomLevelCore = 5;
        int minZoomLevel = MinZoomLevelInWorldView;
        int maxZoomLevel = MaxZoomLevelInWorldView;
        ObservableCollection<MapCustomElement> citySmallIconsCore = null;

        public ObservableCollection<MapCustomElement> Cities {
            get { return citiesCore; }
            private set { SetProperty(ref citiesCore, value, "Cities"); }
        }
        public ObservableCollection<MapCustomElement> CitySmallIcons {
            get { return citySmallIconsCore; }
            private set { SetProperty(ref citySmallIconsCore, value, "CitySmallIcons"); }
        }
        public CityInfo SelectedCity {
            get { return selectedCityCore; }
            set { SetProperty(ref selectedCityCore, value, "SelectedCity", SelectedItemPropertyChanged); }
        }
        public PlaceInfo SelectedPlace {
            get { return selectedPlaceCore; }
            set { SetProperty(ref selectedPlaceCore, value, "SelectedPlace", SelectedItemPropertyChanged); }
        }
        public PhotoGalleryViewType ViewType {
            get { return viewTypeCore; }
            set { SetProperty(ref viewTypeCore, value, "ViewType", ViewTypePropertyChanged); }
        }
        public CoordPoint CenterPoint {
            get { return centerPointCore; }
            set { SetProperty(ref centerPointCore, value, "CenterPoint"); }
        }
        public Point CityPoint {
            get { return cityPointCore; }
            set { SetProperty(ref cityPointCore, value, "CityPoint"); }
        }
        public double ZoomLevel {
            get { return zoomLevelCore; }
            set { SetProperty(ref zoomLevelCore, value, "ZoomLevel"); }
        }
        public int MinZoomLevel {
            get { return minZoomLevel; }
            set { SetProperty(ref minZoomLevel, value, "MinZoomLevel"); }
        }
        public int MaxZoomLevel {
            get { return maxZoomLevel; }
            set { SetProperty(ref maxZoomLevel, value, "MaxZoomLevel"); }
        }

        void SelectedItemPropertyChanged() {
            this.UpdateViewType();
        }
        void ViewTypePropertyChanged() {
            this.Update();
        }

        readonly MapControl map;

        LayerBase Layer { get { return map.Layers.Count > 0 ? map.Layers[0] : null; } }

        public CitiesViewModel(MapControl map, DataTemplate citySmallIconTemplate) {
            this.map = map;
            Cities = new ObservableCollection<MapCustomElement>();
            CitySmallIcons = new ObservableCollection<MapCustomElement>();
            LoadDataFromXML(citySmallIconTemplate);
        }
        void LoadDataFromXML(DataTemplate citySmallIconTemplate) {
            string fileName = Utils.GetRelativePath("CitiesData.xml");
            XDocument document = DataLoader.LoadXmlFromFile(fileName);
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
                    Binding binding = new Binding("ViewType") { Source = this, Converter = new ViewTypeToBoolConverter(), ConverterParameter = PhotoGalleryViewType.Map };
                    CityInformationControl city = new CityInformationControl() { CityInfo = cityInfo };
                    city.SetBinding(CityInformationControl.VisibleProperty, binding);
                    MapCustomElement mapItem = new MapCustomElement() { Content = city, Location = cityLocation };
                    mapItem.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(OnMouseLeftButtonUp);
                    mapItem.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(OnMouseLeftButtonDown);
                    city.TouchDown += OnCityTouchDown;
                    Cities.Add(mapItem);
                    CitySmallIcons.Add(new MapCustomElement() { Content = cityInfo, ContentTemplate = citySmallIconTemplate, Location = cityInfo.Location });
                }
            }
        }
        void UpdateViewType() {
            if(SelectedCity != null)
                ViewType = SelectedPlace != null ? PhotoGalleryViewType.Detail : PhotoGalleryViewType.Gallery;
            else
                ViewType = PhotoGalleryViewType.Map;
        }
        void Update() {
            switch(ViewType) {
                case PhotoGalleryViewType.Map:
                    MinZoomLevel = MinZoomLevelInWorldView;
                    MaxZoomLevel = MaxZoomLevelInWorldView;
                    ZoomLevel = 5;
                    break;
                case PhotoGalleryViewType.Gallery:
                    MinZoomLevel = MinZoomLevelInWorldView;
                    MaxZoomLevel = MaxZoomLevelInWorldView;
                    ZoomLevel = 5;
                    CityPoint = Layer.GeoToScreenPoint(SelectedCity.Location);
                    break;
                case PhotoGalleryViewType.Detail:
                    MinZoomLevel = MinZoomLevelInCityView;
                    MaxZoomLevel = MaxZoomLevelInCityView;
                    ZoomLevel = 17;
                    CenterPoint = SelectedPlace.Location;
                    break;
                default:
                    goto case PhotoGalleryViewType.Map;
            }
        }
        void SelectCity(CityInfo cityInfo) {
            SelectedPlace = null;
            SelectedCity = cityInfo;
            UpdateViewType();
        }
        void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            MapCustomElement element = sender as MapCustomElement;
            if (element != null) {
                SelectCity(((CityInformationControl)element.Content).CityInfo);
                e.Handled = true;
            }
        }
        void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if(sender is MapCustomElement)
                e.Handled = true;
        }
        void OnCityTouchDown(object sender, TouchEventArgs e) {
            SelectCity(((CityInformationControl)sender).CityInfo);
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
}
