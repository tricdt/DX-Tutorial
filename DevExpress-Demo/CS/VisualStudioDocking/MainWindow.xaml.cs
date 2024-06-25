using System;
using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoCenterBase;

namespace VisualStudioDocking {
    public partial class MainWindow : ThemedWindow {
        static void ShowSplashScreen() {
            var viewModel = new DXSplashScreenViewModel() {
                Title = "Visual Studio Inspired UI Demo",
                Subtitle = "Powered by DevExpress",
                Logo = new Uri(string.Format(@"pack://application:,,,/DevExpress.Xpf.DemoBase.v{0};component/DemoLauncher/Images/Logo.svg", AssemblyInfo.VersionShort))
            };
            SplashScreenManager.Create(() => new DockingSplashScreenWindow(), viewModel).ShowOnStartup();
        }
        public MainWindow() {
            ApplicationThemeHelper.ApplicationThemeName = Theme.VS2019Light.Name;
            ShowSplashScreen();
            DemoRunner.SubscribeThemeChanging();
            Theme.CachePaletteThemes = true;
            Theme.RegisterPredefinedPaletteThemes();
            InitializeComponent();
        }
    }
    public class CodeViewPresenter : DevExpress.Xpf.DemoBase.Helpers.Internal.CodeViewPresenter { }
}
