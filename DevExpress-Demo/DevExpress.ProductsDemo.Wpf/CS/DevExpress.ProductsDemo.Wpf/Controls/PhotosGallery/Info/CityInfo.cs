using System.Collections.Generic;
using DevExpress.Xpf.Map;

namespace ProductsDemo {
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
