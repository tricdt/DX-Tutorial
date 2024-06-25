using System.Collections.Generic;

namespace DevExpress.WindowsMailClient.Wpf.ViewModel {
    public class MailAccountsSettingsViewModelInjectedMessage {
        public MailAccountsSettingsViewModelInjectedMessage(IEnumerable<MailAccountSettingsViewModel> accounts, IEnumerable<CommandViewModel> commands) {
            Accounts = accounts;
            Commands = commands;
        }
        public IEnumerable<MailAccountSettingsViewModel> Accounts { get; private set; }
        public IEnumerable<CommandViewModel> Commands { get; private set; }
    }
}
