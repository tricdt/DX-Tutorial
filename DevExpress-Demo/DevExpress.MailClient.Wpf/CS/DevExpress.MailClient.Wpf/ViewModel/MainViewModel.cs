using System.Windows.Media.Imaging;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System.Text.RegularExpressions;
using DevExpress.MailClient.Helpers;
using DevExpress.Mvvm.ModuleInjection;
using System.Windows.Media;
using DevExpress.Xpf.Core;

namespace DevExpress.MailClient.ViewModel {
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
            MetadataLocator.Default = MetadataLocator.Create().AddMetadata<PersonPrefixWithExternalMetadata>();
        }
        [Command(false)]
        public void RegisterJumpList() {
            var jumpListService = this.GetRequiredService<IApplicationJumpListService>();
            jumpListService.Items.AddOrReplace("Navigation", "Mail", FilePathHelper.GetDXImageSource("Outlook Inspired/Glyph_Mail"), OpenMail);
            jumpListService.Items.AddOrReplace("Navigation", "Calendar", FilePathHelper.GetDXImageSource("Scheduling/ListViewAppointment"), OpenCalendar);
            jumpListService.Items.AddOrReplace("Navigation", "Contacts", FilePathHelper.GetDXImageSource("Business Objects/BO_Contact"), OpenContacts);
            jumpListService.Items.AddOrReplace("Navigation", "Tasks", FilePathHelper.GetDXImageSource("Outlook Inspired/Task"), OpenTasks);
            jumpListService.Items.AddOrReplace("Tasks", "New Mail", FilePathHelper.GetDXImageSource("RichEdit/NewComment"), OpenMailAndCreateNewMessage);
            jumpListService.Apply();
        }

        public void OpenMail() {
            Manager.Navigate(Regions.Documents, Modules.Mail);
        }
        public void OpenCalendar() {
            Manager.Navigate(Regions.Documents, Modules.Calendar);
        }
        public void OpenContacts() {
            Manager.Navigate(Regions.Documents, Modules.Contacts);
        }
        public void OpenTasks() {
            Manager.Navigate(Regions.Documents, Modules.Tasks);
        }
        public void ShowAbout() {
            DevExpress.Xpf.About.ShowAbout(DevExpress.Utils.About.ProductKind.DXperienceWPF);
        }
        public void Exit() {
            System.Windows.Application.Current.MainWindow.Close();
        }
        void OpenMailAndCreateNewMessage() {
            OpenMail();
            (Manager.GetRegion(Regions.Documents).GetViewModel(Modules.Mail) as MailViewModel).CreateNewMessage();
        }
    }
}
