using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace ControlsDemo {
    [CodeFile("Modules/SplashScreenManager/SplashScreenMainViewModel.(cs)")]
    [CodeFile("Modules/SplashScreenManager/SplashScreenManagerModule.xaml")]
    [CodeFile("Modules/SplashScreenManager/SplashScreenManagerModule.xaml.(cs)")]
    public partial class SplashScreenManagerModule : DemoModule {
        public SplashScreenMainViewModel ViewModel { get; private set; }
        bool cancelOperation = false;

        public SplashScreenManagerModule() {
            InitializeComponent();
            ViewModel = (SplashScreenMainViewModel)DataContext;
            ModuleUnloaded += OnModuleUnloaded;
        }

        void OnModuleUnloaded(object sender, RoutedEventArgs e) {
            cancelOperation = true;
            SplashScreenManager.CloseAll();
        }
        void OnShowHyperlinkClick(object sender, RoutedEventArgs e) {
            ShowSplashScreen();
        }
        void OnCloseHyperlinkClick(object sender, RoutedEventArgs e) {
            SplashScreenManager.CloseAll();
        }

        async void ShowSplashScreen() {
            bool isFluent = ViewModel.SplashScreenType.Equals(PredefinedSplashScreenType.Fluent);
            var viewModel = new DXSplashScreenViewModel() {
                Logo = new Uri(String.Format(@"pack://application:,,,/DevExpress.Xpf.DemoBase.v{0};component/DemoLauncher/Images/Logo.svg", AssemblyInfo.VersionShort)),
                Status = "Starting...",
                Title = isFluent ? "Fluent Splash Screen" : "Themed Splash Screen",
                Subtitle = "Powered by DevExpress",
                Copyright = GetCopyright(),
                IsIndeterminate = isFluent
            };
            SplashScreenManager manager; 
            if(isFluent)
                manager = SplashScreenManager.CreateFluent(viewModel);
            else if(ViewModel.SplashScreenType.Equals(PredefinedSplashScreenType.Themed))
                manager = SplashScreenManager.CreateThemed(viewModel);      
            else
                manager = SplashScreenManager.CreateWaitIndicator(viewModel);

            cancelOperation = false;
            manager.Show(ViewModel.ShowDelay, ViewModel.MinDuration, this, ViewModel.StartupLocation, ViewModel.TrackOwnerPosition, ViewModel.InputBlock);
            for(int i = 0; i <= 100; i++) {
                if(i == 50)
                    viewModel.Status = "Loading data...";
                else if(i == 80)
                    viewModel.Status = "Finishing...";

                viewModel.Progress = i;
                if(cancelOperation)
                    break;

                await Task.Delay(60);
            }
            manager.Close();
        }
        string GetCopyright() {
            return AssemblyInfo.AssemblyCopyright + (AssemblyInfo.AssemblyCopyright.Contains("All rights reserved") ? "" : "\nAll rights reserved");
        }
    }
}
