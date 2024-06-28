using DevExpress.Mvvm;
namespace MyDX_Demo.Module.MVVM_Demo.BindableBaseDemo
{
    public class BindablePropertiesViewModel:BindableBase
    {
        public string FirstName
        {
            get { return GetValue<string>(); }
            set { SetValue(value, changedCallback: NotifyFullNameChanged); }
        }

        public string LastName
        {
            get { return GetValue<string>(); }
            set { SetValue(value, changedCallback: NotifyFullNameChanged); }
        }

        public string FullName { get { return FirstName + " " + LastName; } }

        void NotifyFullNameChanged()
        {
            RaisePropertyChanged(() => FullName);
        }
    }
}
