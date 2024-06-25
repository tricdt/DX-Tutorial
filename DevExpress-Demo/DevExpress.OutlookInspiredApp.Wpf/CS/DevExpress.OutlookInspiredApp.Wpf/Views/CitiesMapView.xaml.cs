using DevExpress.DevAV.ViewModels;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Map;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.DevAV.Views {
    public partial class CitiesMapView : UserControl {
        public CitiesMapView() {
            InitializeComponent();
        }

        void OnWebRequest(object sender, DevExpress.Xpf.Map.MapWebRequestEventArgs e) {
            e.UserAgent = "DevExpress WPF Map Control Main Demo";
        }
    }
}
