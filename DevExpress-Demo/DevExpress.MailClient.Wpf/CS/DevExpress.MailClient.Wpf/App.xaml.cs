using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using DevExpress.MailClient.Utils;
using DevExpress.MailClient.View;
using DevExpress.MailClient.ViewModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.ModuleInjection;
using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.UI.ModuleInjection;
using DevExpress.Xpf.Accordion;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoCenterBase;

namespace DevExpress.MailClient.Xpf {
    public partial class App : Application {
        Bootstrapper bootstrapper;
        protected override void OnStartup(StartupEventArgs e) {
            AppDomain.CurrentDomain.AssemblyResolve += OnCurrentDomainAssemblyResolve;
            SetCultureInfo();
            StrategyManager.Default.RegisterStrategy<AccordionControl, AccordionControlStrategy>();
#if !DXCORE3
            ServiceContainer.Default.RegisterService(new ApplicationJumpListService());
#endif
            DevExpress.Xpf.DemoBase.AssemblyResolver.Subscribe();
            Theme.RegisterPredefinedPaletteThemes();
            ApplicationThemeHelper.ApplicationThemeName = Theme.Office2019ColorfulName;
            DemoRunner.ShowApplicationSplashScreen();
            ViewLocator.Default = new ViewLocator(typeof(App).Assembly);
            bootstrapper = new Bootstrapper();
            bootstrapper.Run();
            base.OnStartup(e);
        }
        static System.Reflection.Assembly OnCurrentDomainAssemblyResolve(object sender, ResolveEventArgs args) {
            string partialName = DevExpress.Utils.AssemblyHelper.GetPartialName(args.Name).ToLower();
            if(partialName == "entityframework" || partialName == "system.data.sqlite" || partialName == "system.data.sqlite.ef6") {
                string path = Path.Combine(Path.GetDirectoryName(typeof(App).Assembly.Location), "..\\..\\bin", partialName + ".dll");
                return System.Reflection.Assembly.LoadFrom(path);
            }
            return null;
        }
        protected override void OnExit(ExitEventArgs e) {
            ApplicationThemeHelper.SaveApplicationThemeName();
            base.OnExit(e);
        }
        static void SetCultureInfo() {
            CultureInfo demoCI = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            demoCI.DateTimeFormat = new DateTimeFormatInfo();
            Thread.CurrentThread.CurrentCulture = demoCI;
            CultureInfo demoUI = (CultureInfo)Thread.CurrentThread.CurrentUICulture.Clone();
            demoUI.DateTimeFormat = new DateTimeFormatInfo();
            Thread.CurrentThread.CurrentUICulture = demoUI;
        }
    }

    public class Bootstrapper {
        public virtual void Run() {
            RegisterModules();
            InjectModules();
            ConfigureNavigation();
        }
        protected IModuleManager Manager { get { return ModuleManager.DefaultManager; } }
        protected virtual void RegisterModules() {
            Manager.Register(Regions.Documents, new Module(Modules.Mail, () => MailViewModel.Create(), typeof(MailView)));
            Manager.Register(Regions.Documents, new Module(Modules.Tasks, () => TasksViewModel.Create(), typeof(TasksView)));
            Manager.Register(Regions.Documents, new Module(Modules.Calendar, () => CalendarViewModel.Create(), typeof(CalendarView)));
            Manager.Register(Regions.Documents, new Module(Modules.Contacts, () => ContactsViewModel.Create(), typeof(ContactsView)));

            Manager.Register(Regions.Navigation, new Module(Modules.Mail, () => MailNavigationViewModel.Create(), typeof(NavigationMailView)));
            Manager.Register(Regions.Navigation, new Module(Modules.Tasks, () => TasksNavigationViewModel.Create(), typeof(NavigationTasksView)));
            Manager.Register(Regions.Navigation, new Module(Modules.Calendar, () => CalendarNavigationViewModel.Create(), typeof(NavigationCalendarView)));
            Manager.Register(Regions.Navigation, new Module(Modules.Contacts, () => ContactsNavigationViewModel.Create(), typeof(NavigationContactsView)));
        }
        protected virtual bool RestoreState() {
            return false;
        }
        protected virtual void InjectModules() {
            Manager.Inject(Regions.Documents, Modules.Mail);
            Manager.Inject(Regions.Documents, Modules.Tasks);
            Manager.Inject(Regions.Documents, Modules.Calendar);
            Manager.Inject(Regions.Documents, Modules.Contacts);

            Manager.Inject(Regions.Navigation, Modules.Mail);
            Manager.Inject(Regions.Navigation, Modules.Tasks);
            Manager.Inject(Regions.Navigation, Modules.Calendar);
            Manager.Inject(Regions.Navigation, Modules.Contacts);
        }
        protected virtual void ConfigureNavigation() {
            Manager.GetEvents(Regions.Navigation).Navigation += OnNavigation;
            Manager.GetEvents(Regions.Documents).Navigation += OnDocumentsNavigation;
        }
        void OnNavigation(object sender, NavigationEventArgs e) {
            if(e.NewViewModelKey == null) return;
            Manager.InjectOrNavigate(Regions.Documents, e.NewViewModelKey);
        }
        void OnDocumentsNavigation(object sender, NavigationEventArgs e) {
            Manager.Navigate(Regions.Navigation, e.NewViewModelKey);
        }
    }
}
#if CLICKONCE || DXCORE3
namespace DevExpress.Internal.DemoLauncher {
    public static class DemoLauncherEntryPoint {
        public static Tuple<Action, System.Threading.Tasks.Task<Window>> Run() {
            var done = new System.Threading.Tasks.TaskCompletionSource<Window>();
            Action run = () => {
                var app = new DevExpress.MailClient.Xpf.App();
                app.InitializeComponent();
                typeof(Application).GetField("_startupUri", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(app, null);
                app.Startup += (s, e) => {
                    var window = new MainWindow();
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
#endif
