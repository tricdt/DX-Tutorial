using System.Windows.Media.Imaging;
using DevExpress.Xpf.Map;

namespace ProductsDemo {
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
}
