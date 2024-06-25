Imports System
Imports System.Windows
Imports DevExpress.Mvvm.ModuleInjection
Imports DevExpress.Internal
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.UI
Imports DevExpress.SalesDemo.Model
Imports DevExpress.SalesDemo.Wpf.Helpers
Imports DevExpress.SalesDemo.Wpf.View
Imports DevExpress.SalesDemo.Wpf.View.Common
Imports DevExpress.SalesDemo.Wpf.ViewModel
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoCenterBase

Namespace DevExpress.SalesDemo.Wpf

    Public Partial Class App
        Inherits Application

        Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
            Call ExceptionHelper.Initialize()
            ViewModelLocator.Default = New ViewModelLocator(GetType(App).Assembly)
            ViewLocator.Default = New ViewLocator(GetType(App).Assembly)
            ApplicationThemeHelper.ApplicationThemeName = Theme.Office2013Name
            Using DataSource.Progress.Subscribe(New DataGenerationProgress())
                Call DataSource.EnsureDataProvider()
            End Using

            MyBase.OnStartup(e)
            Call InitModules()
        End Sub

        Private Shared Sub InitModules()
            Call ModuleManager.DefaultManager.Register(Regions.Navigation, New [Module](Dashboard, Function() NavigationItemViewModel.Create(Dashboard, "Sales", "Revenue" & Environment.NewLine & "Snapshots", ResourceImageHelper.GetResourceImage("Sales.png")), GetType(NavigationItemView)))
            Call ModuleManager.DefaultManager.Register(Regions.Navigation, New [Module](Products, Function() NavigationItemViewModel.Create(Products, "Products", "Revenue" & Environment.NewLine & "by Products", ResourceImageHelper.GetResourceImage("Products.png")), GetType(NavigationItemView)))
            Call ModuleManager.DefaultManager.Register(Regions.Navigation, New [Module](Sectors, Function() NavigationItemViewModel.Create(Sectors, "Sectors", "Revenue" & Environment.NewLine & "by Sectors", ResourceImageHelper.GetResourceImage("Sectors.png")), GetType(NavigationItemView)))
            Call ModuleManager.DefaultManager.Register(Regions.Navigation, New [Module](Modules.Regions, Function() NavigationItemViewModel.Create(Modules.Regions, "Regions", "Revenue" & Environment.NewLine & "by Regions", ResourceImageHelper.GetResourceImage("Regions.png")), GetType(NavigationItemView)))
            Call ModuleManager.DefaultManager.Register(Regions.Navigation, New [Module](Channels, Function() NavigationItemViewModel.Create(Channels, "Channels", "Revenue" & Environment.NewLine & "by Sales Channels", ResourceImageHelper.GetResourceImage("Channels.png")), GetType(NavigationItemView)))
            Call ModuleManager.DefaultManager.Register(Regions.Main, New [Module](Dashboard, Function() SalesDashboardViewModel.Create(), GetType(SalesDashboard)))
            Call ModuleManager.DefaultManager.Register(Regions.Main, New [Module](Products, Function() ProductsViewModel.Create(), GetType(ProductsView)))
            Call ModuleManager.DefaultManager.Register(Regions.Main, New [Module](Sectors, Function() SectorsViewModel.Create(), GetType(SectorsView)))
            Call ModuleManager.DefaultManager.Register(Regions.Main, New [Module](Modules.Regions, Function() RegionsViewModel.Create(), GetType(RegionsView)))
            Call ModuleManager.DefaultManager.Register(Regions.Main, New [Module](Channels, Function() ChannelsViewModel.Create(), GetType(ChannelsView)))
            ModuleManager.DefaultManager.Inject(Regions.Navigation, Dashboard)
            ModuleManager.DefaultManager.Inject(Regions.Navigation, Products)
            ModuleManager.DefaultManager.Inject(Regions.Navigation, Sectors)
            ModuleManager.DefaultManager.Inject(Regions.Navigation, Modules.Regions)
            ModuleManager.DefaultManager.Inject(Regions.Navigation, Channels)
            Call ModuleManager.DefaultManager.Inject(Regions.Main, Dashboard)
            Call ModuleManager.DefaultManager.Inject(Regions.Main, Products)
            Call ModuleManager.DefaultManager.Inject(Regions.Main, Sectors)
            Call ModuleManager.DefaultManager.Inject(Regions.Main, Modules.Regions)
            Call ModuleManager.DefaultManager.Inject(Regions.Main, Channels)
            ModuleManager.DefaultManager.Navigate(Regions.Navigation, Dashboard)
        End Sub
    End Class

    Friend Class DataGenerationProgress
        Implements IObserver(Of Integer)

        Private splashScreenViewModel As DXSplashScreenViewModel

        Private Sub OnCompleted() Implements IObserver(Of Integer).OnCompleted
            Call DemoRunner.CloseApplicationSplashScreen()
            splashScreenViewModel = Nothing
        End Sub

        Private Sub OnNext(ByVal value As Integer) Implements IObserver(Of Integer).OnNext
            If splashScreenViewModel Is Nothing Then
                splashScreenViewModel = DemoRunner.ShowApplicationSplashScreen(True, Sub(x)
                    x.Status = "Generating Sales..."
                    x.IsIndeterminate = False
                End Sub)
            End If

            splashScreenViewModel.Progress = value
        End Sub

        Private Sub OnError(ByVal [error] As Exception) Implements IObserver(Of Integer).OnError
            Call DemoRunner.CloseApplicationSplashScreen()
            Throw [error]
        End Sub
    End Class
End Namespace
#If CLICKONCE Or DXCORE3
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
#End If
