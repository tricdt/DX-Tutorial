using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Xpf;
using DevExpress.Mvvm;
using DevExpress.Utils;

namespace ProductsDemo {
    public class InfoViewModel : BindableBase {
        AboutInfo aboutInfo;
        ICommand showHelpCommand;
        ICommand showGettingStartedCommand;
        ICommand showBuyNowCommand;

        public AboutInfo AboutInfo {
            get {
                if(aboutInfo == null) {
                    aboutInfo = new AboutInfo();
                    DevExpress.Internal.UserData data = DevExpress.Utils.About.Utility.GetInfoEx();                    
                    aboutInfo.LicenseState = data.IsExpired ? LicenseState.TrialExpired : data.IsTrial ? LicenseState.Trial : LicenseState.Licensed;
                    aboutInfo.ProductName = "WPF Controls";
                    aboutInfo.ProductPlatform = "Build Your Own Office";
                    aboutInfo.RegistrationCode = WpfSerialNumberProvider.GetSerial();
                    aboutInfo.Version = AssemblyInfo.Version;
                }
                return aboutInfo;
            }
        }
        public void ShowHelp() {
            DevExpress.Xpf.Core.DocumentPresenter.OpenLink(AssemblyInfo.DXLinkGetSupport);
        }
        public void ShowGettingStarted() {
            DevExpress.Xpf.Core.DocumentPresenter.OpenLink("https://go.devexpress.com/Demo_GetStarted_WPF.aspx");
        }
        public void ShowBuyNow() {
            DevExpress.Xpf.Core.DocumentPresenter.OpenLink("https://go.devexpress.com/Demo_Subscriptions_Buy.aspx");
        }
        public ICommand ShowHelpCommand {
            get {
                if(showHelpCommand == null)
                    showHelpCommand = new DelegateCommand(ShowHelp, false);
                return showHelpCommand;
            }
        }
        public ICommand ShowGettingStartedCommand {
            get {
                if(showGettingStartedCommand == null)
                    showGettingStartedCommand = new DelegateCommand(ShowGettingStarted, false);
                return showGettingStartedCommand;
            }
        }
        public ICommand ShowBuyNowCommand {
            get {
                if(showBuyNowCommand == null)
                    showBuyNowCommand = new DelegateCommand(ShowBuyNow, false);
                return showBuyNowCommand;
            }
        }
    }
}
