using DevExpress.SalesDemo.Model;
using DevExpress.SalesDemo.Wpf;
using DevExpress.Xpf.Core;
using System.Windows;

namespace DevExpress.SalesDemo.Wpf {
    public partial class MainWindow : ThemedWindow {
        public MainWindow() {
            DevExpress.Utils.About.UAlgo.Default.DoEventObject(DevExpress.Utils.About.UAlgo.kDemo, DevExpress.Utils.About.UAlgo.pWPF, this); 
            InitializeComponent();
        }
    }
}
