using DevExpress.Mvvm;

namespace MVVMDemo.BindableBaseDemo {
    public class BindablePropertiesViaFieldsViewModel : BindableBase {
        string _FirstName;
        public string FirstName {
            get { return _FirstName; }
            set { SetValue(ref _FirstName, value, changedCallback: NotifyFullNameChanged); }
        }

        string _LastName;
        public string LastName {
            get { return _LastName; }
            set { SetValue(ref _LastName, value, changedCallback: NotifyFullNameChanged); }
        }

        public string FullName { get { return FirstName + " " + LastName; } }

        void NotifyFullNameChanged() {
            RaisePropertyChanged(() => FullName);
        }

    }
}
