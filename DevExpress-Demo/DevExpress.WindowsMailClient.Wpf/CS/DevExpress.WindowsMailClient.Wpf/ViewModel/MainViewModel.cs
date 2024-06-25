using System.Windows.Media.Imaging;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.WindowsMailClient.Wpf.Internal;
using DevExpress.Mvvm.ModuleInjection;

namespace DevExpress.WindowsMailClient.Wpf.ViewModel {
    public class MainViewModel {
        public static MainViewModel Create() {
            return ViewModelSource.Create(() => new MainViewModel()); ;
        }
        
        protected IModuleManager Manager { get { return ModuleManager.DefaultManager; } }

        protected MainViewModel() {
#if !DXCORE3
            if(!ViewModelBase.IsInDesignMode)
                RegisterJumpList();
#endif
        }

        [Command(false)]
        public void RegisterJumpList() {
            var jumpListService = this.GetRequiredService<IApplicationJumpListService>();
            jumpListService.Items.AddOrReplace("Navigation", "Mail", new BitmapImage(ImageSourceHelper.GetDXImageUri("Mail/Mail_16x16")), OpenMail);
            jumpListService.Apply();
        }

        public void OpenMail() {
            Manager.Navigate(Regions.Documents, Modules.Mail);
        }
        public void ShowAbout() {
            Xpf.About.ShowAbout(Utils.About.ProductKind.DXperienceWPF);
        }
        public void Exit() {
            System.Windows.Application.Current.MainWindow.Close();
        }
    }
}
