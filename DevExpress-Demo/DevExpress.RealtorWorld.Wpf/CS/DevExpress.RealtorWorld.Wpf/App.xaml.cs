using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using DevExpress.Internal;
using DevExpress.Xpf.Core;

namespace DevExpress.RealtorWorld.Xpf {
    public partial class App : Application {
        public App() {
            ApplicationThemeHelper.ApplicationThemeName = Theme.MetropolisDark.Name;
        }
        protected override void OnStartup(StartupEventArgs e) {
            ExceptionHelper.Initialize();
#if !DXCORE3
            DataDirectoryHelper.SetWebBrowserMode();
#endif
            base.OnStartup(e);
            Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata(200));
            SetCultureInfo();
        }
        static void SetCultureInfo() {
            CultureInfo demoCI = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            demoCI.NumberFormat.CurrencySymbol = "$";
            demoCI.DateTimeFormat = new DateTimeFormatInfo();
            Thread.CurrentThread.CurrentCulture = demoCI;
            CultureInfo demoUI = (CultureInfo)Thread.CurrentThread.CurrentUICulture.Clone();
            demoUI.NumberFormat.CurrencySymbol = "$";
            demoUI.DateTimeFormat = new DateTimeFormatInfo();
            Thread.CurrentThread.CurrentUICulture = demoUI;
        }
        #region DemoTester
#if CLICKONCE        
        static DispatcherTimer testTimer;
#endif
        public static void Test(ThemedWindow window) {
#if CLICKONCE
            if(testTimer == null) {
                testTimer = new DispatcherTimer() { Interval = TimeSpan.FromMinutes(1) };
                testTimer.Tick += (s, e) => {
                    testTimer.Stop();
                    Test(window);
                };
                testTimer.Start();
                return;
            }
            var testerPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                string.Format("DevExpress.Reallife.Tester.v{0}.dll", AssemblyInfo.VersionShort));
            var allowDemoTest = true;
#else            
            var frameworkPrefix = DevExpress.Xpf.Core.Utils.NetVersionDetector.IsNetCore3() ? "NETCoreDesktop" : "Framework4";
            var testerPath = string.Format(@"c:\DevExpress.Tester\{0}\DevExpress.Reallife.Tester.v{1}.dll", frameworkPrefix, AssemblyInfo.VersionShort);
            var args = Environment.GetCommandLineArgs();
            var allowDemoTest = args.Any() && args.Contains(@"/testallexcept:");
#endif
            if(File.Exists(testerPath) && allowDemoTest) {
                ExceptionHelper.IsEnabled = false;
                Assembly.LoadFrom(testerPath)
                    .GetType("DevExpress.Reallife.Tester.RealtorWorldDemoTester")
                    .GetMethod("Create")
                    .Invoke(null, new object[] { window, true });
            }
        }
        #endregion
    }
}
#if CLICKONCE || DXCORE3
namespace DevExpress.Internal.DemoLauncher {
    public static class DemoLauncherEntryPoint {
        public static Tuple<Action, System.Threading.Tasks.Task<Window>> Run() {
            var done = new System.Threading.Tasks.TaskCompletionSource<Window>();
            Action run = () => {
                var app = new DevExpress.RealtorWorld.Xpf.App();
                app.InitializeComponent();
                typeof(Application).GetField("_startupUri", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(app, null);
                app.Startup += (s, e) => {
                    var window = new DevExpress.RealtorWorld.Xpf.View.MainWindow();
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
