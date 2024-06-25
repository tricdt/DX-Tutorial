using DevExpress.DevAV;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System.Collections.Generic;

namespace DevExpress.MailClient.ViewModel {

    public class ContactEditViewModel {
        public static ContactEditViewModel Create(ContactItemViewModel contact, IList<string> cities, IList<string> states) {
            return ViewModelSource.Create(() => new ContactEditViewModel(contact, cities, states));
        }

        public virtual ContactItemViewModel Contact { get; set; }
        public IList<string> States { get; private set; }
        public IList<string> Cities { get; private set; }

        protected ContactEditViewModel(ContactItemViewModel contact, IList<string> cities, IList<string> states) {
            this.Contact = contact;
            this.Cities = cities;
            this.States = states;
        }
        protected ContactEditViewModel() {
            if(this.IsInDesignMode())
                InitializeInDesignMode();
        }

        void InitializeInDesignMode() {
            var contactsModel = ContactsViewModel.Create();
            Contact = contactsModel.CurrentContact;
            Cities = contactsModel.Cities;
            States = contactsModel.States;
        }
    }
    public class PersonPrefixWithExternalMetadata {
        public static void BuildMetadata(EnumMetadataBuilder<PersonPrefix> builder) {
            builder
                .Member(PersonPrefix.Dr)
                    .ImageUri("pack://application:,,,/DevExpress.MailClient.Xpf;component/Images/Tasks/Doctor.svg")
                .EndMember()
                .Member(PersonPrefix.Miss)
                    .ImageUri("pack://application:,,,/DevExpress.MailClient.Xpf;component/Images/Tasks/Miss.svg")
                .EndMember()
                .Member(PersonPrefix.Mr)
                    .ImageUri("pack://application:,,,/DevExpress.MailClient.Xpf;component/Images/Tasks/Mr.svg")
                .EndMember()
                .Member(PersonPrefix.Mrs)
                    .ImageUri("pack://application:,,,/DevExpress.MailClient.Xpf;component/Images/Tasks/Mrs.svg")
                .EndMember()
                .Member(PersonPrefix.Ms)
                    .ImageUri("pack://application:,,,/DevExpress.MailClient.Xpf;component/Images/Tasks/Ms.svg")
                .EndMember();
        }
    }
}
