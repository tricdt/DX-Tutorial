using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDX_Demo.Module.MVVM_Demo.ServicesDemo
{
    public class SplashScreenManagerViewModel
    {
        public async void ShowSplashScreen(string serviceName)
        {
            ISplashScreenManagerService splashScreenService = this.GetRequiredService<ISplashScreenManagerService>(serviceName);
            splashScreenService.ViewModel = CreateViewModel();
            splashScreenService.Show();
            await Task.Delay(3000);
            splashScreenService.Close();
        }
        DXSplashScreenViewModel CreateViewModel()
        {
            return new DXSplashScreenViewModel()
            {
                Logo = new Uri(@"pack://application:,,,/DevExpress.Xpf.DemoBase.v20.1;component/DemoLauncher/Images/Logo.svg"),
                Status = "Initializing...",
                Subtitle = "Powered by DevExpress",
                Copyright = GetCopyright(),
                IsIndeterminate = true
            };
        }
        string GetCopyright()
        {
            return AssemblyInfo.AssemblyCopyright + (AssemblyInfo.AssemblyCopyright.Contains("All rights reserved") ? "" : "\nAll rights reserved");
        }
    }
}
