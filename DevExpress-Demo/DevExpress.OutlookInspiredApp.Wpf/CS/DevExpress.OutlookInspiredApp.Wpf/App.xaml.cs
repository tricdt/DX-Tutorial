using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Linq;
using System.Windows.Media.Animation;
using System.Runtime.InteropServices;
using DevExpress.Images;
using DevExpress.Internal;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoCenterBase;
using System.Windows.Threading;

namespace DevExpress.DevAV {
    public partial class App : Application {
        static IDisposable singleInstanceApplicationGuard;
        static App() {
#if DXCORE3
            ApplicationThemeHelper.UpdateApplicationThemeName(); 
#endif
            bool exit;
            singleInstanceApplicationGuard = DevAVDataDirectoryHelper.SingleInstanceApplicationGuard("DevExpressWpfOutlookInspiredApp", out exit);
            if(exit) {
                Environment.Exit(0);
            }
            DemoRunner.ShowApplicationSplashScreen();
        }
        protected override void OnStartup(StartupEventArgs e) {
            ExceptionHelper.Initialize();
            AppDomain.CurrentDomain.AssemblyResolve += OnCurrentDomainAssemblyResolve;
            DevAVDataDirectoryHelper.LocalPrefix = "WpfOutlookInspiredApp";
#if !DXCORE3
            ServiceContainer.Default.RegisterService(new ApplicationJumpListService());
#endif
            AssemblyResolver.Subscribe();
            Theme.RegisterPredefinedPaletteThemes();
            ImagesAssemblyLoader.Load();
            Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata(200));
            base.OnStartup(e);
            ViewLocator.Default = new ViewLocator(typeof(App).Assembly);
            Theme.TouchlineDark.ShowInThemeSelector = false;
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
        static Assembly OnCurrentDomainAssemblyResolve(object sender, ResolveEventArgs args) {
            string partialName = DevExpress.Utils.AssemblyHelper.GetPartialName(args.Name).ToLower();
            if(partialName == "entityframework" || partialName == "system.data.sqlite" || partialName == "system.data.sqlite.ef6") {
                string path = Path.Combine(Path.GetDirectoryName(typeof(App).Assembly.Location), "..\\..\\bin", partialName + ".dll");
                return Assembly.LoadFrom(path);
            }
            return null;
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
            var clickOnceTest = true;
#else
            var frameworkPrefix = Xpf.Core.Utils.NetVersionDetector.IsNetCore3() ? "NETCoreDesktop" : "Framework4";
            var testerPath = string.Format(@"c:\DevExpress.Tester\{0}\DevExpress.Reallife.Tester.v{1}.dll", frameworkPrefix, AssemblyInfo.VersionShort);
            var args = Environment.GetCommandLineArgs();
            var allowDemoTest = args.Any() && args.Contains(@"/testallexcept:");
            var clickOnceTest = false;
#endif
            if(File.Exists(testerPath) && allowDemoTest) {
                ExceptionHelper.IsEnabled = false;
                Assembly.LoadFrom(testerPath)
                    .GetType("DevExpress.Reallife.Tester.OutlookInspiredAppDemoTester")
                    .GetMethod("Create")
                    .Invoke(null, new object[] { window, true, clickOnceTest });
            }
        }
#endregion
        public static string ApplicationID {
            get { return string.Format("Components_{0}_Demo_Center_OutlookInspired_{0}", AssemblyInfo.VersionShort.Replace(".", "_")); }
        }
    }
    [Guid("86882B9F-1EAE-41D9-B9CF-BD7ACCA51523"), ComVisible(true)]
    public class OutlookInspiredAppNotificationActivator : ToastNotificationActivator {
        public override void OnActivate(string arguments, System.Collections.Generic.Dictionary<string, string> data) {
        }
    }
}
#if CLICKONCE || DXCORE3
namespace DevExpress.Internal.DemoLauncher {
    public static class DemoLauncherEntryPoint {
        public static Tuple<Action, System.Threading.Tasks.Task<Window>> Run() {
            var done = new System.Threading.Tasks.TaskCompletionSource<Window>();
            Action run = () => {
                var app = new DevExpress.DevAV.App();
                app.InitializeComponent();
                typeof(Application).GetField("_startupUri", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(app, null);
                app.Startup += (s, e) => {
                    var window = new DevExpress.DevAV.MainWindow();
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
