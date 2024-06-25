using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoCenterBase;
using System;
using System.Windows;

namespace DevExpress.StockMarketTrader {
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {            
            ApplicationThemeHelper.ApplicationThemeName = Theme.Office2019ColorfulName;
            DemoRunner.ShowApplicationSplashScreen();
            base.OnStartup(e);
            DevExpress.Utils.About.UAlgo.Default.DoEventObject(DevExpress.Utils.About.UAlgo.kDemo, DevExpress.Utils.About.UAlgo.pWPF, this); 
        }
    }
}
#if CLICKONCE || DXCORE3
namespace DevExpress.Internal.DemoLauncher {
    public static class DemoLauncherEntryPoint {
        public static Tuple<Action, System.Threading.Tasks.Task<Window>> Run() {
            var done = new System.Threading.Tasks.TaskCompletionSource<Window>();
            Action run = () => {
                var app = new StockMarketTrader.App();
                app.InitializeComponent();
                typeof(Application).GetField("_startupUri", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(app, null);
                app.Startup += (s, e) => {
                    var window = new DevExpress.StockMarketTrader.MainWindow();
                    window.Show();
                    app.MainWindow = window;
                    done.SetResult(window);
                };
                app.Run();
            };
            return Tuple.Create(run, done.Task);
        }
    }
}
#endif
