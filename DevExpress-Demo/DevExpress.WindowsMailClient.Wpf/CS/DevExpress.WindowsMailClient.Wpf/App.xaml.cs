using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.UI.ModuleInjection;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.WindowsUI;
using DevExpress.WindowsMailClient.Wpf.Internal;
using DevExpress.Xpf.DemoCenterBase;

namespace DevExpress.WindowsMailClient.Wpf {
    public partial class App : Application {
        Bootstrapper bootstrapper;
        protected override void OnStartup(StartupEventArgs e) {
            AppDomain.CurrentDomain.AssemblyResolve += OnCurrentDomainAssemblyResolve;
            SetCultureInfo();
            StrategyManager.Default.RegisterStrategy<HamburgerMenu, HamburgerMenuStrategy>();
#if !DXCORE3
            ServiceContainer.Default.RegisterService(new ApplicationJumpListService());            
#endif
            ApplicationThemeHelper.ApplicationThemeName = Theme.Office2019ColorfulName;
            DemoRunner.ShowApplicationSplashScreen();
            ViewLocator.Default = new ViewLocator(typeof(App).Assembly);
            bootstrapper = new Bootstrapper();
            bootstrapper.Run();
            ThemedWindow.UseNativeWindow = true;
            base.OnStartup(e);
        }
        static Assembly OnCurrentDomainAssemblyResolve(object sender, ResolveEventArgs args) {
            string partialName = DevExpress.Utils.AssemblyHelper.GetPartialName(args.Name).ToLower();
            if(partialName == "entityframework" || partialName == "system.data.sqlite" || partialName == "system.data.sqlite.ef6") {
                string path = Path.Combine(Path.GetDirectoryName(typeof(App).Assembly.Location), "..\\..\\bin", partialName + ".dll");
                return Assembly.LoadFrom(path);
            }
            return null;
        }
        protected override void OnExit(ExitEventArgs e) {
            ApplicationThemeHelper.SaveApplicationThemeName();
            base.OnExit(e);
        }
        static void SetCultureInfo() {
            CultureInfo demoCI = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            demoCI.DateTimeFormat = new DateTimeFormatInfo();
            Thread.CurrentThread.CurrentCulture = demoCI;
            CultureInfo demoUI = (CultureInfo)Thread.CurrentThread.CurrentUICulture.Clone();
            demoUI.DateTimeFormat = new DateTimeFormatInfo();
            Thread.CurrentThread.CurrentUICulture = demoUI;
        }
    }
}
#if CLICKONCE || DXCORE3
namespace DevExpress.Internal.DemoLauncher {
    public static class DemoLauncherEntryPoint {
        public static Tuple<Action, System.Threading.Tasks.Task<Window>> Run() {
            var done = new System.Threading.Tasks.TaskCompletionSource<Window>();
            Action run = () => {
                var app = new DevExpress.WindowsMailClient.Wpf.App();
                app.InitializeComponent();
                typeof(Application).GetField("_startupUri", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(app, null);
                app.Startup += (s, e) => {
                    var window = new WindowsMailClient.Wpf.View.MainWindow();
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
