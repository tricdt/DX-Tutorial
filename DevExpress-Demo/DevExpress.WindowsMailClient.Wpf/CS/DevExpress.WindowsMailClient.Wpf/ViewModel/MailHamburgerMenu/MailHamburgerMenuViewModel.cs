using DevExpress.Mvvm;
using DevExpress.Mvvm.ModuleInjection;
using DevExpress.Mvvm.POCO;
using System.Collections.Generic;

namespace DevExpress.WindowsMailClient.Wpf.ViewModel {
    public class MailHamburgerMenuViewModel {
        public static MailHamburgerMenuViewModel Create() {
            return ViewModelSource.Create(() => new MailHamburgerMenuViewModel());
        }
        IList<object> menuItems;
        protected MailHamburgerMenuViewModel() {
            if(this.IsInDesignMode()) {
                ContentViewModel = MailViewModel.Create();
            } else {
                ModuleManager.DefaultManager.GetEvents(Regions.Documents).ViewModelCreated += OnDocumentsRegionViewModelCreated;
                ContentViewModel = ModuleManager.DefaultManager.GetRegion(Regions.Documents).GetViewModel(Modules.Mail);
                CreateMenuItems();
            }
        }
        public virtual object ContentViewModel { get; set; }
        public object ItemsSource {
            get {
                if (menuItems == null)
                    CreateMenuItems();
                return menuItems;
            }
        }

        void CreateMenuItems() {
            MailViewModel mailVM = ContentViewModel as MailViewModel;
            if (mailVM == null)
                return;
            menuItems = new List<object>();
            menuItems.Add(mailVM.NewMailCommand);
            menuItems.Add(new EmptyItemViewModel());
            menuItems.Add(mailVM.AccountsCollectionCommand);
            menuItems.Add(new EmptyItemViewModel());
            menuItems.Add(mailVM.SelectedAccount.FolderCollection);
        }
        void OnDocumentsRegionViewModelCreated(object sender, ViewModelCreatedEventArgs e) {
            if(e.ViewModelKey == Modules.Mail)
                ContentViewModel = e.ViewModel;
        }
    }
}
