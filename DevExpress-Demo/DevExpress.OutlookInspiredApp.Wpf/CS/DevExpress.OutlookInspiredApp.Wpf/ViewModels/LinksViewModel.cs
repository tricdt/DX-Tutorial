using DevExpress.Xpf.Core;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;

namespace DevExpress.DevAV.ViewModels {
    public class LinksViewModel {
        public static LinksViewModel Create() {
            return ViewModelSource.Create(() => new LinksViewModel());
        }
        protected LinksViewModel() { }

        public void GettingStarted() {
            DocumentPresenter.OpenLink("https://go.devexpress.com/Demo_GetStarted_WPF.aspx");
        }
        public void GetSupport() {
            DocumentPresenter.OpenLink(AssemblyInfo.DXLinkGetSupport);
        }
        public void BuyNow() {
            DocumentPresenter.OpenLink("https://go.devexpress.com/Demo_Subscriptions_Buy.aspx");
        }
        public void UniversalSubscription() {
            DocumentPresenter.OpenLink("https://go.devexpress.com/Demo_UniversalSubscription.aspx");
        }
        public void About() {
            var application = System.Windows.Application.Current;
            DevExpress.Xpf.Core.Native.AboutHelper.ShowAbout(new[] { DevExpress.Utils.About.ProductKind.DXperienceWPF }, "Outlook Inspired App", application == null ? null : application.MainWindow);
        }
    }
}
