namespace MapDemo {
    public partial class MultipleLayers : MapDemoModule {
        public MultipleLayers() {
            InitializeComponent();
        }

        void OnWebRequest(object sender, DevExpress.Xpf.Map.MapWebRequestEventArgs e) {
            e.UserAgent = "DevExpress WPF Map Control Main Demo";
        }
    }
}
