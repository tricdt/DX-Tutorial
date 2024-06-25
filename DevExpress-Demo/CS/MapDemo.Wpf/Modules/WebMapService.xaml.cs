using DevExpress.Xpf.Map;

namespace MapDemo {
    public partial class WebMapService : MapDemoModule {
        public WebMapService() {
            InitializeComponent();
        }
        void OnResponseCapabilities(object sender, CapabilitiesRespondedEventArgs e) {
            lbWmsLayers.ItemsSource = e.Layers;
        }
    }
}
