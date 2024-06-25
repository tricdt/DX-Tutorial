using System;
using System.IO;
using System.Reflection;
using System.Windows;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoLauncher;
using DevExpress.Xpf.PdfViewer;
using DevExpress.Internal;
using DevExpress.Xpf.DemoCenterBase;

namespace ProductsDemo {
    public partial class App : Application {
        public App() {
            AppDomain.CurrentDomain.AssemblyResolve += OnCurrentDomainAssemblyResolve;
            ExceptionHelper.Initialize();
            DevExpress.Images.ImagesAssemblyLoader.Load();
            DevExpress.Xpf.DemoBase.AssemblyResolver.Subscribe();
            ApplicationThemeHelper.ApplicationThemeName = Theme.Office2019ColorfulName;
            DemoRunner.ShowApplicationSplashScreen();
            Theme.RegisterPredefinedPaletteThemes();
        }
        static Assembly OnCurrentDomainAssemblyResolve(object sender, ResolveEventArgs args) {
            string partialName = DevExpress.Utils.AssemblyHelper.GetPartialName(args.Name).ToLower();
            if(partialName == "entityframework" || partialName == "system.data.sqlite" || partialName == "system.data.sqlite.ef6") {
                string path = Path.Combine(Path.GetDirectoryName(typeof(App).Assembly.Location), "..\\..\\bin", partialName + ".dll");
                return Assembly.LoadFrom(path);
            }
            return null;
        }
        protected override void OnStartup(StartupEventArgs e) {
            ServiceContainer.Default.RegisterService(new ApplicationJumpListService());
            base.OnStartup(e);
        }
    }
}
#if CLICKONCE || DXCORE3
namespace DevExpress.Internal.DemoLauncher {
    public static class DemoLauncherEntryPoint {
        public static Tuple<Action, System.Threading.Tasks.Task<Window>> Run() {
            var done = new System.Threading.Tasks.TaskCompletionSource<Window>();
            Action run = () => {
                var app = new ProductsDemo.App();
                app.InitializeComponent();
                typeof(Application).GetField("_startupUri", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(app, null);
                app.Startup += (s, e) => {
                    var window = new ProductsDemo.MainWindow();
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
