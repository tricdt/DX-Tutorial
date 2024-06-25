using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.Mvvm.POCO;

namespace NavigationDemo {
    public class NavigationViewModelBase { }

    public class ContactsNavigationViewModel : NavigationViewModelBase {
        public static ContactsNavigationViewModel Create() {
            return ViewModelSource.Create(() => new ContactsNavigationViewModel());
        }
        public virtual IList<ContactItem> Contacts { get; set; }

        protected ContactsNavigationViewModel() {
            Contacts = new List<ContactItem>(NavigationPaneData.ContactsData);
        }
    }
}
