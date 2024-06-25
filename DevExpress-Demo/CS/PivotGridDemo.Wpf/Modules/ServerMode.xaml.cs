using System;
using System.Diagnostics;
using System.Windows;

namespace PivotGridDemo {   

#if !DXCORE3
    [DevExpress.Xpf.DemoBase.CodeFile("Helpers/ServerMode/ServerModeViewModel.(cs)")]
    [DevExpress.Xpf.DemoBase.CodeFile("Helpers/ServerMode/OrderDataContext.(cs)")]
    [DevExpress.Xpf.DemoBase.CodeFile("Helpers/ServerMode/ServerModeViewModelBase.(cs)")]
    public partial class ServerMode : PivotGridDemoModule {
#else
    [DevExpress.Xpf.DemoBase.CodeFile("Helpers/ServerMode/ServerModeViewModelBase.(cs)")]
    [DevExpress.Xpf.DemoBase.CodeFile("Helpers/ServerMode/ServerModeViewModelNetCore.(cs)")]
    [DevExpress.Xpf.DemoBase.CodeFile("Helpers/ServerMode/OrderDataContextNetCore.(cs)")]
    public partial class ServerMode : PivotGridDemoModule {
#endif

        readonly Stopwatch timer = new Stopwatch();
        DateTime asyncCompleted = DateTime.Now;

        public ServerMode() {
            InitializeComponent();
        }

        void pivotGrid_AsyncOperationStarting(object sender, RoutedEventArgs e) {
            tbTimeTaken.Text = "...";
            if(!timer.IsRunning)
                if((DateTime.Now - asyncCompleted).TotalMilliseconds < 100)
                    timer.Start();
                else
                    timer.Restart();
        }

        void pivotGrid_AsyncOperationCompleted(object sender, RoutedEventArgs e) {
            timer.Stop();
            tbTimeTaken.Text = timer.ElapsedMilliseconds.ToString();
            asyncCompleted = DateTime.Now;
        }
    }
}
