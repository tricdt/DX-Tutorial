using System;
using System.Windows;
using DevExpress.Xpf.Core;

namespace DevExpress.VideoRent.Wpf {
    public partial class StartUpSplashScreenWindow : Window, ISplashScreen {
        public StartUpSplashScreenWindow() {
            InitializeComponent();
            this.board.Completed += OnAnimationCompleted;
            Footer_Text.Text = "Copyright © 1998-" + DateTime.Now.Year;
        }
        public void Progress(double value) {
            progressBar.Value = value;
        }
        public void CloseSplashScreen() {
            this.board.Begin(this);
        }
        public void SetProgressState(bool isIndeterminate) {
            progressBar.IsIndeterminate = isIndeterminate;
        }
        void OnAnimationCompleted(object sender, EventArgs e) {
            this.board.Completed -= OnAnimationCompleted;
            this.Close();
        }
    }
}
