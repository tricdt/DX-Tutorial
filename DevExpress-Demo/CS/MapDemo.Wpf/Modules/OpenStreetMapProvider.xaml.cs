using System.Net;
using DevExpress.Xpf.Map;

namespace MapDemo {
    public partial class OpenStreetMapProvider : MapDemoModule {
        public OpenStreetMapProvider() {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            InitializeComponent();
        }
        void overlayLayerKind_SelectedIndexChanged(object sender, System.Windows.RoutedEventArgs e) {
            MoveMap(overlayLayerKind.SelectedIndex);
        }
        void MoveMap(int index) {
            switch(index) {
                case 0: { map.CenterPoint = new GeoPoint(50.067, 14.417); ; map.ZoomLevel = 15; break; }
                case 1: { map.CenterPoint = new GeoPoint(54.15, 11.75); map.ZoomLevel = 14; break; }
                case 2: { map.CenterPoint = new GeoPoint(41.5, 2.0); map.ZoomLevel = 11; break; }
                case 3: { map.CenterPoint = new GeoPoint(51.5, -3.2); map.ZoomLevel = 13; break; }
                case 4: { map.CenterPoint = new GeoPoint(48.85, 2.3); map.ZoomLevel = 11; break; }
            }
        }
        void OnWebRequest(object sender, MapWebRequestEventArgs e) {
            e.UserAgent = "DevExpress Map Demo";
        }
    }
}
