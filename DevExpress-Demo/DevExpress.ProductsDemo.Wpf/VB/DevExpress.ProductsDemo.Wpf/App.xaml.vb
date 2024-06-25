Imports System
Imports System.Reflection
Imports System.Windows
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Core
Imports DevExpress.Internal
Imports DevExpress.Xpf.DemoCenterBase

Namespace ProductsDemo

    Public Partial Class App
        Inherits Application

        Public Sub New()
            AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf OnCurrentDomainAssemblyResolve
            Call ExceptionHelper.Initialize()
            DevExpress.Images.ImagesAssemblyLoader.Load()
            DevExpress.Xpf.DemoBase.AssemblyResolver.Subscribe()
            ApplicationThemeHelper.ApplicationThemeName = Theme.Office2019ColorfulName
            Call DemoRunner.ShowApplicationSplashScreen()
            Call Theme.RegisterPredefinedPaletteThemes()
        End Sub

        Private Shared Function OnCurrentDomainAssemblyResolve(ByVal sender As Object, ByVal args As ResolveEventArgs) As Assembly
            Dim partialName As String = DevExpress.Utils.AssemblyHelper.GetPartialName(args.Name).ToLower()
            If Equals(partialName, "entityframework") OrElse Equals(partialName, "system.data.sqlite") OrElse Equals(partialName, "system.data.sqlite.ef6") Then
                Dim path As String = IO.Path.Combine(IO.Path.GetDirectoryName(GetType(App).Assembly.Location), "..\..\bin", partialName & ".dll")
                Return Assembly.LoadFrom(path)
            End If

            Return Nothing
        End Function

        Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
            Call ServiceContainer.Default.RegisterService(New ApplicationJumpListService())
            MyBase.OnStartup(e)
        End Sub
    End Class
End Namespace
#If CLICKONCE Or DXCORE3
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
#End If
