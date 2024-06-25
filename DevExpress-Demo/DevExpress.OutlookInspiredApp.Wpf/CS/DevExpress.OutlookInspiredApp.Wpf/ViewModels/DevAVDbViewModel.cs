using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using DevExpress.DevAV.Common.ViewModel;
using DevExpress.DevAV.DevAVDbDataModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.ViewModel;
using DevExpress.Xpf.Core;

namespace DevExpress.DevAV.ViewModels {
    /// <summary>
    /// Represents the root POCO view model for the DevAVDb data model.
    /// </summary>
    public partial class DevAVDbViewModel : DocumentsViewModel<DevAVDbModuleDescription, IDevAVDbUnitOfWork> {
        static bool IsDebug;
        const string TablesGroup = "Tables";

        /// <summary>
        /// Creates a new instance of DevAVDbViewModel as a POCO view model.
        /// </summary>
        public static DevAVDbViewModel Create() {
            return ViewModelSource.Create(() => new DevAVDbViewModel());
        }

        /// <summary>
        /// Initializes a new instance of the DevAVDbViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the DevAVDbViewModel type without the POCO proxy factory.
        /// </summary>
        protected DevAVDbViewModel()
            : base(UnitOfWorkSource.GetUnitOfWorkFactory()) {
            SetIsDebug();
            NavigationPaneVisibility = NavigationPaneVisibility.Normal;
            CreateNotificationsInfo();
            MetadataLocator.Default = MetadataLocator.Create()
                .AddMetadata<EmployeeTaskStatusWithExternalMetadata>()
                .AddMetadata<EmployeeTaskPriorityWithExternalMetadata>()
                .AddMetadata<PersonPrefixWithExternalMetadata>();
        }

        protected override DevAVDbModuleDescription[] CreateModules() {
#if !DXCORE3
            return new DevAVDbModuleDescription[] {
                new DevAVDbModuleDescription("Employees", "EmployeeCollectionView", TablesGroup, FiltersSettings.GetEmployeesFilterTree(this), GetPeekCollectionViewModelFactory(x => x.Employees)),
                new DevAVDbModuleDescription("Tasks", "TaskCollectionView", TablesGroup, FiltersSettings.GetTasksFilterTree(this)),
                new DevAVDbModuleDescription("Customers", "CustomerCollectionView", TablesGroup, FiltersSettings.GetCustomersFilterTree(this), GetPeekCollectionViewModelFactory(x => x.Customers)),
                new DevAVDbModuleDescription("Products", "ProductCollectionView", TablesGroup, FiltersSettings.GetProductsFilterTree(this), GetPeekCollectionViewModelFactory(x => x.Products)),
                new DevAVDbModuleDescription("Sales", "OrderCollectionView", TablesGroup, FiltersSettings.GetSalesFilterTree(this)),
                new DevAVDbModuleDescription("Opportunities", "QuoteCollectionView", TablesGroup, FiltersSettings.GetOpportunitiesFilterTree(this)),
            };
#else
            return new DevAVDbModuleDescription[] {
                new DevAVDbModuleDescription("Employees", "EmployeeCollectionView", TablesGroup, FiltersSettings.GetEmployeesFilterTree(this), GetPeekCollectionViewModelFactory(x => x.Employees)),
                new DevAVDbModuleDescription("Customers", "CustomerCollectionView", TablesGroup, FiltersSettings.GetCustomersFilterTree(this), GetPeekCollectionViewModelFactory(x => x.Customers)),
                new DevAVDbModuleDescription("Products", "ProductCollectionView", TablesGroup, FiltersSettings.GetProductsFilterTree(this), GetPeekCollectionViewModelFactory(x => x.Products)),
                new DevAVDbModuleDescription("Sales", "OrderCollectionView", TablesGroup, FiltersSettings.GetSalesFilterTree(this)),
                new DevAVDbModuleDescription("Opportunities", "QuoteCollectionView", TablesGroup, FiltersSettings.GetOpportunitiesFilterTree(this)),
            };
#endif
        }
        Workspace defaultWorkspace = new Workspace();
        LinksViewModel linksViewModel;

        public override void OnLoaded(DevAVDbModuleDescription module) {
            base.OnLoaded(module);
            RegisterJumpList();
            var timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 20);
            if(!IsDebug)
                timer.Tick += OnTimerTick;
            timer.Start();
        }

        public override void SaveLogicalLayout() { }
        public override bool RestoreLogicalLayout() {
            return false;
        }

        protected override void OnActiveModuleChanged(DevAVDbModuleDescription oldModule) {
            base.OnActiveModuleChanged(oldModule);
            if(ActiveModule != null && ActiveModule.FilterTreeViewModel != null)
                ActiveModule.FilterTreeViewModel.SetViewModel(DocumentManagerService.ActiveDocument.Content);
            var title = Convert.ToString(DocumentManagerService.ActiveDocument.Title);
            MainWindowService.Title = title + " - DevAV";
            Logger.Log(string.Format("OutlookInspiredApp: Select module: {0}", title.ToUpper()));
            FinishModuleChangingDispatcherService.BeginInvoke(() => {
                UpdateWorkspace(oldModule, ActiveModule);
            });
        }
        [Conditional("DEBUG")]
        void SetIsDebug() {
            IsDebug = true;
        }

        const string ResourcePathHeader = @"pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/";
        List<Tuple<string, string, Action<Task<NotificationResult>>>> Notifications;
        int notificationIndex = 0;

        void OnTimerTick(object sender, EventArgs e) {
            var info = Notifications[notificationIndex];
            var timer = (DispatcherTimer)sender;
            NotificationService.CreatePredefinedNotification("DevAV Tips & Tricks", info.Item1, "", new BitmapImage(new Uri(ResourcePathHeader + info.Item2)))
                .ShowAsync()
                .ContinueWith(info.Item3, TaskScheduler.FromCurrentSynchronizationContext());
            if(++notificationIndex < Notifications.Count)
                timer.Interval = new TimeSpan(0, 0, 60);
            else
                timer.Stop();
        }

        IDocumentManagerService SignleObjectDocumentManagerService { get { return this.GetService<IDocumentManagerService>("SignleObjectDocumentManagerService"); } }

        IWorkspaceService WorkspaceService { get { return this.GetRequiredService<IWorkspaceService>(); } }

        IMainWindowService MainWindowService { get { return this.GetRequiredService<IMainWindowService>(); } }

        IDispatcherService FinishModuleChangingDispatcherService { get { return this.GetRequiredService<IDispatcherService>("FinishModuleChangingDispatcherService"); } }

        INotificationService NotificationService { get { return this.GetRequiredService<INotificationService>(); } }

        void UpdateWorkspace(DevAVDbModuleDescription oldModule, DevAVDbModuleDescription newModule) {
            Workspace oldWorkspace = WorkspaceService.SaveWorkspace();
            Workspace newWorkspace = new Workspace();
            try {
                if(oldModule != null)
                    oldModule.Workspace = oldWorkspace;
                if(newModule != null)
                    newWorkspace = newModule.Workspace ?? defaultWorkspace;
            } finally {
                WorkspaceService.RestoreWorkspace(newWorkspace);
            }
        }

        public LinksViewModel LinksViewModel {
            get {
                if(linksViewModel == null)
                    linksViewModel = LinksViewModel.Create();
                return linksViewModel;
            }
        }

        void RegisterJumpList() {
#if !CLICKONCE && !DXCORE3
            IApplicationJumpListService jumpListService = this.GetRequiredService<IApplicationJumpListService>();
            jumpListService.Items.AddOrReplace("Modules", "New Employee", EmployeeIcon, () => { SignleObjectDocumentManagerService.ShowNewEntityDocument<Employee>(this); });
            jumpListService.Items.AddOrReplace("Modules", "Customers", CustomerIcon, () => { Show(Modules.Where(m => m.DocumentType == "CustomerCollectionView").First()); });
            jumpListService.Items.AddOrReplace("Modules", "Opportunities", OpportunitiesIcon, () => { Show(Modules.Where(m => m.DocumentType == "QuoteCollectionView").First()); });
            jumpListService.Apply();
#endif
        }

        void CreateNotificationsInfo() {
            Notifications = new List<Tuple<string, string, Action<Task<NotificationResult>>>>();
            Notifications.Add(new Tuple<string, string, Action<Task<NotificationResult>>>("Become a UI Superhero, check out our WYSIWYG Reporting in the Sales Module",
                "reports_96x96.png", x => OnNotificationShown(x, Modules.Where(m => m.DocumentType == "OrderCollectionView").First())));
            Notifications.Add(new Tuple<string, string, Action<Task<NotificationResult>>>("Become a UI Superhero, explore PDF-documents with DevExpress PDF Viewer",
                "pdf_96x96.png", x => OnNotificationShown(x, Modules.Where(m => m.DocumentType == "ProductCollectionView").First())));
        }
        void OnNotificationShown(Task<NotificationResult> task, DevAVDbModuleDescription module) {
            try {
                if(task.Result == NotificationResult.Activated && module != null)
                    Show(module);
            } catch(AggregateException) { }
        }

        ImageSource EmployeeIcon { get { return (ImageSource)new SvgImageSourceExtension() { Uri = new Uri(ResourcePathHeader + "NewEmploye.svg") }.ProvideValue(null); } }
        ImageSource CustomerIcon { get { return (ImageSource)new SvgImageSourceExtension() { Uri = new Uri(ResourcePathHeader + "NewCustomer.svg") }.ProvideValue(null); } }
        ImageSource OpportunitiesIcon { get { return (ImageSource)new SvgImageSourceExtension() { Uri = new Uri(ResourcePathHeader + "NewOpportunities.svg") }.ProvideValue(null); } }
    }

    public partial class DevAVDbModuleDescription : ModuleDescription<DevAVDbModuleDescription> {
        public DevAVDbModuleDescription(string title, string documentType, string group, IFilterTreeViewModel filterTreeViewModel, Func<DevAVDbModuleDescription, object> peekCollectionViewModelFactory = null)
            : base(title, documentType, group, peekCollectionViewModelFactory) {
            ImageSource = (ImageSource)new SvgImageSourceExtension() { Uri = new Uri(string.Format(@"pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Modules/{0}.svg", title)), Size = new System.Windows.Size(24, 24) }
            .ProvideValue(null);
            FilterTreeViewModel = filterTreeViewModel;
        }

        public Workspace Workspace { get; set; }

        public ImageSource ImageSource { get; private set; }

        public IFilterTreeViewModel FilterTreeViewModel { get; private set; }
    }
}
