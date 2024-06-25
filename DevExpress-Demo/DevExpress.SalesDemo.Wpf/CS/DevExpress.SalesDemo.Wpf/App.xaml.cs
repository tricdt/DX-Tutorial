using System;
using System.Windows;
using DevExpress.Mvvm.ModuleInjection;
using DevExpress.Internal;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using DevExpress.SalesDemo.Model;
using DevExpress.SalesDemo.Wpf.Helpers;
using DevExpress.SalesDemo.Wpf.View;
using DevExpress.SalesDemo.Wpf.View.Common;
using DevExpress.SalesDemo.Wpf.ViewModel;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoCenterBase;

namespace DevExpress.SalesDemo.Wpf {
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            ExceptionHelper.Initialize();
            ViewModelLocator.Default = new ViewModelLocator(typeof(App).Assembly);
            ViewLocator.Default = new ViewLocator(typeof(App).Assembly);
            ApplicationThemeHelper.ApplicationThemeName = Theme.Office2013Name;
            using(DataSource.Progress.Subscribe(new DataGenerationProgress()))
                DataSource.EnsureDataProvider();
            base.OnStartup(e);
            InitModules();
        }
        static void InitModules() {
            ModuleManager.DefaultManager.Register(
                Regions.Navigation, 
                new Mvvm.ModuleInjection.Module(Modules.Dashboard, 
                    () => NavigationItemViewModel.Create(Modules.Dashboard, "Sales", "Revenue" + Environment.NewLine + "Snapshots", ResourceImageHelper.GetResourceImage("Sales.png")),
                    typeof(NavigationItemView)));
            ModuleManager.DefaultManager.Register(
                Regions.Navigation, 
                new Mvvm.ModuleInjection.Module(Modules.Products,
                    () => NavigationItemViewModel.Create(Modules.Products, "Products", "Revenue" + Environment.NewLine + "by Products", ResourceImageHelper.GetResourceImage("Products.png")),
                    typeof(NavigationItemView)));
            ModuleManager.DefaultManager.Register(
                Regions.Navigation,
                new Mvvm.ModuleInjection.Module(Modules.Sectors,
                    () => NavigationItemViewModel.Create(Modules.Sectors, "Sectors", "Revenue" + Environment.NewLine + "by Sectors", ResourceImageHelper.GetResourceImage("Sectors.png")),
                    typeof(NavigationItemView)));
            ModuleManager.DefaultManager.Register(
                Regions.Navigation,
                new Mvvm.ModuleInjection.Module(Modules.Regions,
                    () => NavigationItemViewModel.Create(Modules.Regions, "Regions", "Revenue" + Environment.NewLine + "by Regions", ResourceImageHelper.GetResourceImage("Regions.png")),
                    typeof(NavigationItemView)));
            ModuleManager.DefaultManager.Register(
                Regions.Navigation,
                new Mvvm.ModuleInjection.Module(Modules.Channels,
                    () => NavigationItemViewModel.Create(Modules.Channels, "Channels", "Revenue" + Environment.NewLine + "by Sales Channels", ResourceImageHelper.GetResourceImage("Channels.png")),
                    typeof(NavigationItemView)));

            ModuleManager.DefaultManager.Register(Regions.Main, new Mvvm.ModuleInjection.Module(Modules.Dashboard, () => SalesDashboardViewModel.Create(), typeof(SalesDashboard)));
            ModuleManager.DefaultManager.Register(Regions.Main, new Mvvm.ModuleInjection.Module(Modules.Products, () => ProductsViewModel.Create(), typeof(ProductsView)));
            ModuleManager.DefaultManager.Register(Regions.Main, new Mvvm.ModuleInjection.Module(Modules.Sectors, () => SectorsViewModel.Create(), typeof(SectorsView)));
            ModuleManager.DefaultManager.Register(Regions.Main, new Mvvm.ModuleInjection.Module(Modules.Regions, () => RegionsViewModel.Create(), typeof(RegionsView)));
            ModuleManager.DefaultManager.Register(Regions.Main, new Mvvm.ModuleInjection.Module(Modules.Channels, () => ChannelsViewModel.Create(), typeof(ChannelsView)));

            ModuleManager.DefaultManager.Inject(Regions.Navigation, Modules.Dashboard);
            ModuleManager.DefaultManager.Inject(Regions.Navigation, Modules.Products);
            ModuleManager.DefaultManager.Inject(Regions.Navigation, Modules.Sectors);
            ModuleManager.DefaultManager.Inject(Regions.Navigation, Modules.Regions);
            ModuleManager.DefaultManager.Inject(Regions.Navigation, Modules.Channels);

            ModuleManager.DefaultManager.Inject(Regions.Main, Modules.Dashboard);
            ModuleManager.DefaultManager.Inject(Regions.Main, Modules.Products);
            ModuleManager.DefaultManager.Inject(Regions.Main, Modules.Sectors);
            ModuleManager.DefaultManager.Inject(Regions.Main, Modules.Regions);
            ModuleManager.DefaultManager.Inject(Regions.Main, Modules.Channels);

            ModuleManager.DefaultManager.Navigate(Regions.Navigation, Modules.Dashboard);
        }
    }
    class DataGenerationProgress : IObserver<int> {
        DXSplashScreenViewModel splashScreenViewModel;
        void IObserver<int>.OnCompleted() {
            DemoRunner.CloseApplicationSplashScreen();
            splashScreenViewModel = null;
        }
        void IObserver<int>.OnNext(int value) {
            if (splashScreenViewModel == null) {
                splashScreenViewModel = DemoRunner.ShowApplicationSplashScreen(true, x => {
                    x.Status = "Generating Sales...";
                    x.IsIndeterminate = false;
                });
            }
            splashScreenViewModel.Progress = value;
        }
        void IObserver<int>.OnError(Exception error) {
            DemoRunner.CloseApplicationSplashScreen();
            throw error; 
        }
    }
}
#if CLICKONCE || DXCORE3
namespace DevExpress.Internal.DemoLauncher {
    public static class DemoLauncherEntryPoint {
        public static Tuple<Action, System.Threading.Tasks.Task<Window>> Run() {
            var done = new System.Threading.Tasks.TaskCompletionSource<Window>();
            Action run = () => {
                var app = new DevExpress.SalesDemo.Wpf.App();
                app.InitializeComponent();
                typeof(Application).GetField("_startupUri", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(app, null);
                app.Startup += (s, e) => {
                    var window = new DevExpress.SalesDemo.Wpf.MainWindow();
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
