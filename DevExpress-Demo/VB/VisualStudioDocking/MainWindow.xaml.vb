Imports System
Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoCenterBase

Namespace VisualStudioDocking

    Public Partial Class MainWindow
        Inherits ThemedWindow

        Private Shared Sub ShowSplashScreen()
            Dim viewModel = New DXSplashScreenViewModel() With {.Title = "Visual Studio Inspired UI Demo", .Subtitle = "Powered by DevExpress", .Logo = New Uri(String.Format("pack://application:,,,/DevExpress.Xpf.DemoBase.v{0};component/DemoLauncher/Images/Logo.svg", AssemblyInfo.VersionShort))}
            Call SplashScreenManager.Create(Function() New DockingSplashScreenWindow(), viewModel).ShowOnStartup()
        End Sub

        Public Sub New()
            ApplicationThemeHelper.ApplicationThemeName = Theme.VS2019Light.Name
            Call ShowSplashScreen()
            Call DemoRunner.SubscribeThemeChanging()
            Theme.CachePaletteThemes = True
            Call Theme.RegisterPredefinedPaletteThemes()
            InitializeComponent()
        End Sub
    End Class

    Public Class CodeViewPresenter
        Inherits DevExpress.Xpf.DemoBase.Helpers.Internal.CodeViewPresenter

    End Class
End Namespace
