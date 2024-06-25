using System;
using System.Windows;
using DevExpress.Xpf.Core;

namespace DevExpress.VideoRent.Wpf {
    public partial class ApplyingThemeSplashScreenWindow : Window, ISplashScreen {
        double value;

        public ApplyingThemeSplashScreenWindow() {
            InitializeComponent();
        }
        public void Progress(double value) {
            this.value = value;
            uxStatus.Text = this.value.ToString();
        }
        public void CloseSplashScreen() { Close(); }
        public void SetProgressState(bool isIndeterminate) {
            uxStatus.Text = isIndeterminate ? string.Empty : this.value.ToString();
        }
    }
}
