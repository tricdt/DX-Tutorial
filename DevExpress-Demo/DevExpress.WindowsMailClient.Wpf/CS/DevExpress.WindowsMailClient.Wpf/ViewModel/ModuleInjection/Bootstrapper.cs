using DevExpress.Mvvm.ModuleInjection;
using DevExpress.WindowsMailClient.Wpf.View;
using DevExpress.WindowsMailClient.Wpf.ViewModel;

namespace DevExpress.WindowsMailClient.Wpf {
    public class Bootstrapper {
        public virtual void Run() {
            RegisterModules();
            InjectModules();
            ConfigureNavigation();
        }
        protected IModuleManager Manager { get { return ModuleManager.DefaultManager; } }
        protected virtual void RegisterModules() {
            Manager.Register(Regions.Documents, new Module(Modules.Mail, ViewModel.MailViewModel.Create, typeof(MailView)));
            Manager.Register(Regions.Navigation, new Module(Modules.Mail, MailHamburgerMenuViewModel.Create));
            Manager.Register(Regions.Settings, new Module(Modules.MailAccountSettings, MailAccountsSettingsViewModel.Create, typeof(MailAccountSettingsView)));
        }
        protected virtual bool RestoreState() { 
            return false;
        }
        protected virtual void InjectModules() {
            Manager.Inject(Regions.Documents, Modules.Mail);
            Manager.Inject(Regions.Navigation, Modules.Mail);
            Manager.Inject(Regions.Settings, Modules.MailAccountSettings);
        }
        protected virtual void ConfigureNavigation() {
            Manager.GetEvents(Regions.Navigation).Navigation += OnNavigation;
            Manager.GetEvents(Regions.Documents).Navigation += OnDocumentsNavigation;
        }
        void OnNavigation(object sender, NavigationEventArgs e) {
            if (e.NewViewModelKey == null) return;
            Manager.InjectOrNavigate(Regions.Documents, e.NewViewModelKey);
        }
        void OnDocumentsNavigation(object sender, NavigationEventArgs e) {
            Manager.Navigate(Regions.Navigation, e.NewViewModelKey);
        }
    }
}
