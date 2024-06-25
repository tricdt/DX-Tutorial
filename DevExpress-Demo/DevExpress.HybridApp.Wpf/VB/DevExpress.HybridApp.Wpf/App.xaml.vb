Imports System
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Threading
Imports System.Windows
Imports System.Windows.Media.Animation
Imports DevExpress.Images
Imports DevExpress.Internal
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoCenterBase
Imports DevExpress.Xpf.Editors

Namespace DevExpress.DevAV

    Public Partial Class App
        Inherits Application

        Private Shared singleInstanceApplicationGuard As IDisposable

        Public Sub New()
            CompatibilitySettings.UseLegacyColumnFilterPopup = True
            ApplicationThemeHelper.ApplicationThemeName = Theme.HybridApp.Name
        End Sub

        Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
            Call ExceptionHelper.Initialize()
            ClearAutomationEventsHelper.IsEnabled = False
            AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf OnCurrentDomainAssemblyResolve
            DevAVDataDirectoryHelper.LocalPrefix = "WpfHybridApp"
#If Not DXCORE3
            Call ServiceContainer.Default.RegisterService(New ApplicationJumpListService())
#End If
            Call ImagesAssemblyLoader.Load()
            Call Timeline.DesiredFrameRateProperty.OverrideMetadata(GetType(Timeline), New FrameworkPropertyMetadata(200))
            MyBase.OnStartup(e)
            ViewLocator.Default = New ViewLocator(GetType(App).Assembly)
            Dim [exit] As Boolean
            singleInstanceApplicationGuard = DevAVDataDirectoryHelper.SingleInstanceApplicationGuard("DevExpressWpfHybridApp", [exit])
            If [exit] Then
                Shutdown()
                Return
            End If

            Call SetCultureInfo()
            Call SetLocalization()
            Call DemoRunner.ShowApplicationSplashScreen()
        End Sub

        Private Shared Sub SetCultureInfo()
            Dim demoCI As CultureInfo = CType(Thread.CurrentThread.CurrentCulture.Clone(), CultureInfo)
            demoCI.NumberFormat.CurrencySymbol = "$"
            demoCI.DateTimeFormat = New DateTimeFormatInfo()
            Thread.CurrentThread.CurrentCulture = demoCI
            Dim demoUI As CultureInfo = CType(Thread.CurrentThread.CurrentUICulture.Clone(), CultureInfo)
            demoUI.NumberFormat.CurrencySymbol = "$"
            demoUI.DateTimeFormat = New DateTimeFormatInfo()
            Thread.CurrentThread.CurrentUICulture = demoUI
        End Sub

        Private Shared Sub SetLocalization()
            EditorLocalizer.Active = New HybridAppEditorLocalizer()
        End Sub

        Private Shared Function OnCurrentDomainAssemblyResolve(ByVal sender As Object, ByVal args As ResolveEventArgs) As Assembly
            Dim partialName As String = Utils.AssemblyHelper.GetPartialName(args.Name).ToLower()
            If Equals(partialName, "entityframework") OrElse Equals(partialName, "system.data.sqlite") OrElse Equals(partialName, "system.data.sqlite.ef6") Then
                Dim path As String = IO.Path.Combine(IO.Path.GetDirectoryName(GetType(App).Assembly.Location), "..\..\bin", partialName & ".dll")
                Return Assembly.LoadFrom(path)
            End If

            Return Nothing
        End Function

#Region "DemoTester"
#If CLICKONCE
        static DispatcherTimer testTimer;
#End If
        Public Shared Sub Test(ByVal window As ThemedWindow)
#If CLICKONCE
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
#Else
            Dim frameworkPrefix = If(Xpf.Core.Utils.NetVersionDetector.IsNetCore3(), "NETCoreDesktop", "Framework4")
            Dim testerPath = String.Format("c:\DevExpress.Tester\{0}\DevExpress.Reallife.Tester.v{1}.dll", frameworkPrefix, AssemblyInfo.VersionShort)
            Dim args = Environment.GetCommandLineArgs()
            Dim allowDemoTest = args.Any() AndAlso args.Contains("/testallexcept:")
#End If
            If File.Exists(testerPath) AndAlso allowDemoTest Then
                ExceptionHelper.IsEnabled = False
                Call Assembly.LoadFrom(testerPath).GetType("DevExpress.Reallife.Tester.HybridAppDemoTester").GetMethod("Create").Invoke(Nothing, New Object() {window, True})
            End If
        End Sub
#End Region
    End Class

    Public Class HybridAppEditorLocalizer
        Inherits EditorLocalizer

        Protected Overrides Sub PopulateStringTable()
            MyBase.PopulateStringTable()
            AddString(EditorStringId.LookUpSearch, "Enter text to search...")
        End Sub
    End Class
End Namespace
#If CLICKONCE Or DXCORE3
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
#End If
