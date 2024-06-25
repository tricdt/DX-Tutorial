using System;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Core;

namespace DevExpress.RealtorWorld.Xpf.View {
    public partial class MainWindow : ThemedWindow {
        public MainWindow() {
            App.Test(this);
            InitializeComponent();
            DevExpress.Utils.About.UAlgo.Default.DoEventObject(DevExpress.Utils.About.UAlgo.kDemo, DevExpress.Utils.About.UAlgo.pWPF, this); 
        }
    }
}
