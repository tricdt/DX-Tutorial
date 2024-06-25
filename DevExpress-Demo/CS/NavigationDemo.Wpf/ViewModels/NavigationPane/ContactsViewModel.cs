using DevExpress.Mvvm;
using System.Collections.Generic;

namespace NavigationDemo {
    public class ContactsViewModel : ViewModelBase {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }

        public virtual void AddContact() {
            var viewModel = (ContactsNavigationViewModel)Parameter;
            var contacts = new List<ContactItem>(viewModel.Contacts);
            contacts.Add(new ContactItem() { FirstName = FirstName, LastName = LastName, Email = Email });
            viewModel.Contacts = contacts;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
        }
        public virtual bool CanAddContact() {
            return !(string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(FirstName));
        }
    }
}
