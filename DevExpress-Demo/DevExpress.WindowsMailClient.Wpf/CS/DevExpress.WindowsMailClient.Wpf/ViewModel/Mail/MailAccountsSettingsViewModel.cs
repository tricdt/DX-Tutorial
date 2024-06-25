using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.POCO;
using System.Collections.Generic;

namespace DevExpress.WindowsMailClient.Wpf.ViewModel {
    public class MailAccountsSettingsViewModel {
        public static MailAccountsSettingsViewModel Create() {
            return ViewModelSource.Create(() => new MailAccountsSettingsViewModel());
        }

        protected MailAccountsSettingsViewModel() {
            Accounts = new DXObservableCollection<MailAccountSettingsViewModel>();
            Commands = new DXObservableCollection<CommandViewModel>();
            Messenger.Default.Register<MailAccountsSettingsViewModelInjectedMessage>(this, OnSettingsViewModelInjectedMessage);
        }

        public DXObservableCollection<MailAccountSettingsViewModel> Accounts { get; private set; }
        public DXObservableCollection<CommandViewModel> Commands { get; private set; }
       
        void OnSettingsViewModelInjectedMessage(MailAccountsSettingsViewModelInjectedMessage obj) {
            AddItems(Accounts, obj.Accounts);
            AddItems(Commands, obj.Commands);
        }

        void AddItems<T>(DXObservableCollection<T> target, IEnumerable<T> source) {
            target.Clear();
            if (source != null)
                target.AddRange(source);
        }
    }
}
