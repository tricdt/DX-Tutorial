using MVVMDemo.Services;
using System;
using DevExpress.Xpf.DemoBase;

namespace MVVMDemo {
    public partial class ServicesModule : MVVMDemoModule {
        protected override IShowcaseInfo[] CreateShowcases() {
            const string path = "Modules/Services";
            return new ShowcaseInfo[] {
                LoadShowcase("ISplashScreenManagerService", "401692/mvvm-framework/services/predefined-set/splashscreenmanagerservice",
                    path, new[] { typeof(SplashScreenManagerViewModel), typeof(SplashScreenManagerView) }),
                LoadShowcase("IMessageBoxService", "113933/MVVM-Framework/Services/Predefined-Set/Message-Box-Services", 
                    path + "/MessageBoxService", new[] { typeof(MessageBoxServiceViewModel), typeof(MessageBoxServiceView),  typeof(Helpers) }),
                LoadShowcase("IDialogService", "113932/MVVM-Framework/Services/Predefined-Set/Dialog-Services",
                    path, new[] { typeof(DialogServiceViewModel), typeof(DialogServiceDetailViewModel), typeof(DialogServiceView) }),
                LoadShowcase("IWindowService + ICurrentWindowService", null,
                    path, new[] { typeof(WindowServiceViewModel), typeof(WindowServiceDetailViewModel), typeof(WindowServiceView) }),
                LoadShowcase("INavigationService ", "113944/MVVM-Framework/Services/Predefined-Set/FrameNavigationService",
                    path + "/NavigationService", new[] { typeof(Services.Navigation.FrameView), typeof(Services.Navigation.NavigationDetailView), typeof(Services.Navigation.CollectionViewModel) , typeof(Services.Navigation.DetailViewModel)}),

                LoadShowcase("IDocumentManagerService - Tabbed MDI View", "18173/MVVM-Framework/Services/Predefined-Set/Document-Services/TabbedDocumentUIService",
                    path + "/DocumentManagerService", GetDocumentServiceTypes(typeof(Services.DocumentManager.TabbedView))),
                LoadShowcase("IDocumentManagerService - Windows View", "18174/MVVM-Framework/Services/Predefined-Set/Document-Services/WindowedDocumentUIService",
                    path + "/DocumentManagerService", GetDocumentServiceTypes(typeof(Services.DocumentManager.WindowedView))),
                LoadShowcase("IDocumentManagerService - Navigation Frame View", "18172/MVVM-Framework/Services/Predefined-Set/Document-Services/FrameDocumentUIService",
                    path + "/DocumentManagerService", GetDocumentServiceTypes(typeof(Services.DocumentManager.FrameView))),
                LoadShowcase("IDocumentManagerService - Docking View", "18275/MVVM-Framework/Services/Predefined-Set/Document-Services/DockingDocumentUIService",
                    path + "/DocumentManagerService", GetDocumentServiceTypes(typeof(Services.DocumentManager.DockingView))),

                LoadShowcase("IDispatcherService", "113861/MVVM-Framework/Services/Predefined-Set/DispatcherService",
                    path, new[] { typeof(DispatcherServiceViewModel), typeof(DispatcherServiceView) }),

                LoadShowcase("IOpenFileDialogService", "114757/MVVM-Framework/Services/Predefined-Set/OpenFileDialogService",
                    path, new[] { typeof(OpenFileDialogServiceViewModel), typeof(OpenFileDialogServiceView) }),
                LoadShowcase("IOpenFolderDialogService", "DevExpress.Xpf.Dialogs.DXOpenFolderDialogService.class",
                    path, new[] { typeof(OpenFolderDialogServiceViewModel), typeof(OpenFolderDialogServiceView) }),
                LoadShowcase("ISaveFileDialogService", "114760/MVVM-Framework/Services/Predefined-Set/SaveFileDialogService",
                    path, new[] { typeof(SaveFileDialogServiceViewModel), typeof(SaveFileDialogServiceView) }),
                LoadShowcase("IFolderBrowserDialogService", "114755/MVVM-Framework/Services/Predefined-Set/FolderBrowserDialogService",
                    path, new[] { typeof(FolderBrowserDialogServiceViewModel), typeof(FolderBrowserDialogServiceView) }),

                LoadShowcase("Implementing a Custom Service", "16920/MVVM-Framework/Services/How-to-create-a-Custom-Service",
                    path, new[] { typeof(CustomServiceViewModel), typeof(GridExportService), typeof(CustomServiceView) }),

                LoadShowcase("IUIObjectService", null,
                    path, new[] { typeof(UIObjectServiceViewModel), typeof(UIObjectServiceView) }),
            };
        }
        static Type[] GetDocumentServiceTypes(Type viewType) {
            return new[] {
                        viewType,
                        typeof(Services.DocumentManager.CollectionView),
                        typeof(Services.DocumentManager.DocumentDetailView),
                        typeof(Services.DocumentManager.CollectionViewModel),
                        typeof(Services.DocumentManager.DetailViewModel)
            };
        }
    }
}
