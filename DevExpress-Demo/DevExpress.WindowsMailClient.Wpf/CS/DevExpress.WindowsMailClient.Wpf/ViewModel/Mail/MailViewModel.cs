using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.WindowsMailClient.Wpf.Data;
using DevExpress.WindowsMailClient.Wpf.Internal;

namespace DevExpress.WindowsMailClient.Wpf.ViewModel {
    public class MailViewModel {
        protected MailViewModel() {
            Initialize();
        }

        public virtual MailAccountViewModel SelectedAccount { get; set; }
        public virtual MailFolderViewModel SelectedFolder { get; set; }
        public virtual MailMessageViewModel SelectedMessage { get; set; }
        public virtual MailMessageViewModel PreviousMessage { get; set; }
        public virtual bool ShowAccountsSplitView { get; set; }
        public virtual bool ShowMessageContent { get; set; }
        public virtual bool IsReadMessage { get; set; }

        protected virtual IMessageBoxService MessageBoxService { get { return null; } }

        public ObservableCollection<CommandViewModel> EditCommands { get; private set; }
        public ObservableCollection<CommandViewModel> ViewCommands { get; private set; }
        internal AddNewMailCommandViewModel NewMailCommand { get; private set; }
        internal MailAccountCollectionViewModel AccountsCollectionCommand { get; private set; }
        List<MailAccountSettingsViewModel> AccountsSettingsItems { get; set; }
        List<CommandViewModel> AccountSettingsCommands { get; set; }

        public static MailViewModel Create() {
            return ViewModelSource.Create(() => new MailViewModel());
        }

        public void ArchiveMessage() {
            MoveMessage(SelectedAccount.Folders, SelectedMessage, MailFolders.Archive);
            UpdateShowMessageContent();
        }
        public bool CanArchiveMessage() {
            return CanRunReadMessageOperation() && SelectedMessage.Type != MailFolders.Archive;
        }
        public bool CanCreateNewMessage() {
            return SelectedAccount != null;
        }
        public bool CanDeleteMessage() {
            return CanRunReadMessageOperation();
        }
        public bool CanDiscardMessage() {
            return CanRunEditedMessageOperation();
        }
        public bool CanForwardMessage() {
            return CanRunReadMessageOperation();
        }
        public bool CanReplyMessage() {
            return CanRunReadMessageOperation() && SelectedMessage.From != SelectedAccount.Email;
        }
        public bool CanSendMessage() {
            return CanRunEditedMessageOperation() && !string.IsNullOrEmpty(SelectedMessage.To);
        }
        public bool CanSetFlag() {
            return CanRunReadMessageOperation();
        }
        public void CreateNewMessage() {
            PreviousMessage = SelectedMessage;
            MailMessageViewModel viewMessage = MailMessageViewModel.Create(MailFolders.Drafts, SelectedAccount.Email, SelectedAccount.Email, SelectedAccount.Company);
            viewMessage.Date = DateTime.Now;
            AddMessage(viewMessage);
        }
        public void DeleteMessage() {
            if(SelectedMessage.Type == MailFolders.Deleted)
                RemoveMessage(SelectedAccount.Folders, SelectedMessage);
            else
                MoveMessage(SelectedAccount.Folders, SelectedMessage, MailFolders.Deleted);
            UpdateShowMessageContent();
        }
        public void DiscardMessage() {
            if(MessageBoxService.ShowMessage("Are you sure you want to discard this draft?", "Discard draft?", MessageButton.YesNo) != MessageResult.Yes)
                return;
            RemoveMessage(SelectedAccount.Folders, SelectedMessage);
            UpdateShowMessageContent();
        }
        public void ForwardMessage() {
            PreviousMessage = SelectedMessage;
            MailMessageViewModel viewMessage = MailMessageViewModel.Create(MailFolders.Drafts, SelectedAccount.Email, SelectedAccount.Email, SelectedAccount.Company);
            UpdateForwardMessage(SelectedMessage, viewMessage);
            AddMessage(viewMessage);
        }
        public void ReplyMessage() {
            PreviousMessage = SelectedMessage;
            MailMessageViewModel viewMessage = MailMessageViewModel.Create(MailFolders.Drafts, SelectedAccount.Email, SelectedAccount.Email, SelectedAccount.Company);
            UpdateReplyMessage(SelectedMessage, viewMessage);
            AddMessage(viewMessage);
        }
        public void SendMessage() {
            MoveMessage(SelectedAccount.Folders, SelectedMessage, MailFolders.Sent);
            UpdateShowMessageContent();
        }
        public void SetFlag() {
            
        }
        public void ShowAccountsInfo() {
            ShowAccountsSplitView = !ShowAccountsSplitView;
        }
        protected void OnSelectedAccountChanged(MailAccountViewModel oldValue) {
            if(oldValue != null) oldValue.IsSelected = false;
            if(SelectedAccount != null) { SelectedAccount.IsSelected = true; }
        }
        protected void OnSelectedFolderChanged(MailFolderViewModel oldValue) {
            if(oldValue != null) oldValue.IsSelected = false;
            if(SelectedFolder != null) {
                SelectedFolder.IsSelected = true;
                SelectedMessage = GetSelectedMessage(SelectedFolder); 
            }
            else SelectedMessage = null;
        }
        protected void OnSelectedMessageChanged(MailMessageViewModel oldValue) {
            if(SelectedMessage == null) {
                ShowMessageContent = false;
                RecalculateReadMessage(false);
            }
            else {
                ShowMessageContent = true;
                RecalculateReadMessage(SelectedMessage.IsDraft);
                if(oldValue != null && oldValue.IsUnread) {
                    oldValue.IsUnread = false;
                    if(SelectedAccount != null) SelectedAccount.UpdateMailCount();
                    if(SelectedFolder != null) SelectedFolder.UpdateMailCount();
                }
            }
        }
        protected void OnShowAccountsSplitViewChanged() {
            Messenger.Default.Send(new MailAccountsSettingsViewModelInjectedMessage(AccountsSettingsItems, AccountSettingsCommands));
        }

        void Initialize() {
            NewMailCommand = AddNewMailCommandViewModel.Create("New Mail", CreateNewMessage, "Add.svg");
            AccountsSettingsItems = new List<MailAccountSettingsViewModel>();
            AccountSettingsCommands = new List<CommandViewModel>() { CommandViewModel.Create("Add Account", "Add.svg", () => { }, () => { return true; }) };
            AccountsCollectionCommand = MailAccountCollectionViewModel.Create("Accounts", ShowAccountsInfo, "Customer.svg");
            ObservableCollection<MailAccountViewModel> accountsCommand = AccountsCollectionCommand.Accounts;
            foreach (MailMessage dataMessage in CreateDataProvider().GetItems()) {
                string currentEmail = dataMessage.Email;
                string currentCompany = dataMessage.Company;
                MailAccountViewModel account = accountsCommand.FirstOrDefault(a => a.Email == currentEmail && a.Company == currentCompany);
                if (account == null) {
                    account = MailAccountViewModel.Create(currentCompany, currentEmail, "Folders", "All Folders", "Folders.svg");
                    account.Folders.Add(CreateFolder(currentCompany, MailFolders.Inbox));
                    account.Folders.Add(CreateFolder(currentCompany, MailFolders.Archive, false));
                    account.Folders.Add(CreateFolder(currentCompany, MailFolders.Deleted));
                    account.Folders.Add(CreateFolder(currentCompany, MailFolders.Drafts));
                    account.Folders.Add(CreateFolder(currentCompany, MailFolders.Sent));
                    accountsCommand.Add(account);
                    AccountsSettingsItems.Add(MailAccountSettingsViewModel.Create(currentCompany, currentEmail, "Mail.svg"));
                }
                string currentType = dataMessage.Type;
                MailFolderViewModel folder = account.FindFolder(currentType);
                if (folder == null) {
                    folder = CreateFolder(currentCompany, currentType);
                    account.Folders.Add(folder);
                }
                MailMessageViewModel viewMessage = MailMessageViewModel.Create(currentType, dataMessage.From, currentEmail, currentCompany);
                UpdateMailMessage(dataMessage, viewMessage);
                folder.Items.Add(viewMessage);
            }
            ViewCommands = new ObservableCollection<CommandViewModel>() {
                CommandViewModel.Create("Set flag", "Set_flag.svg", SetFlag, CanSetFlag),
                CommandViewModel.Create("Delete", "Delete.svg", DeleteMessage, CanDeleteMessage),
                CommandViewModel.Create("Archive", "Archive.svg", ArchiveMessage, CanArchiveMessage),
                CommandViewModel.Create("Forward", "Forward.svg", ForwardMessage, CanForwardMessage),
                CommandViewModel.Create("Reply", "Reply.svg", ReplyMessage, CanReplyMessage),
            };
            EditCommands = new ObservableCollection<CommandViewModel>() {
                CommandViewModel.Create("Send", "Send.svg", SendMessage, CanSendMessage),
                CommandViewModel.Create("Discard", "Delete.svg", DiscardMessage, CanDiscardMessage),
            };
            SelectedAccount = accountsCommand[0];
        }
        MailDataProviderBase CreateDataProvider() {
            return ViewModelBase.IsInDesignMode ? (MailDataProviderBase)new DesignTimeMailDataProvider() : new XmlMailDataProvider();
        }
        MailFolderViewModel CreateFolder(string company, string name, bool isFavorite = true) {
            return MailFolderViewModel.Create(company, name, isFavorite, OnSelectFolder);
        }
        void OnSelectFolder(MailFolderViewModel folder) {
            if (!folder.IsSelected && SelectedFolder == folder) SelectedFolder = null;
            if (folder.IsSelected) SelectedFolder = folder;
        }
        void AddMessage(MailMessageViewModel message) {
            AddMessageCore(SelectedAccount.Folders, message);
            RecalculateReadMessage(message.IsDraft);
            SelectedMessage = message;
        }
        void AddMessageCore(IEnumerable<MailFolderViewModel> folders, MailMessageViewModel message) {
            MailFolderViewModel folder = folders.First(f => f.Name == message.Type);
            if(folder != null)
                folder.Items.Add(message);
        }
        bool CanRunEditedMessageOperation() {
            return !IsReadMessage && ShowMessageContent && SelectedMessage != null;
        }
        bool CanRunReadMessageOperation() {
            return IsReadMessage && ShowMessageContent && SelectedMessage != null;
        }
        MailMessageViewModel GetSelectedMessage(MailFolderViewModel folder) {
            
            return folder.Items.Count > 0 ? folder.Items[folder.Items.Count - 1] : null;
        }
        void MoveMessage(IEnumerable<MailFolderViewModel> folders, MailMessageViewModel message, string targetType) {
            RemoveMessage(folders, message);
            message.Type = targetType;
            AddMessageCore(folders, message);
        }
        void RecalculateReadMessage(bool IsDraft) {
            IsReadMessage = !IsDraft;
        }
        void RemoveMessage(IEnumerable<MailFolderViewModel> folders, MailMessageViewModel message) {
            MailFolderViewModel folder = folders.First(f => f.Name == message.Type);
            if(folder != null)
                folder.Items.Remove(message);
        }
        void UpdateForwardMessage(MailMessageViewModel source, MailMessageViewModel target) {
            target.Date = DateTime.Now;
            target.Subject = MailMessageHelper.CreateForwardSubject(source.Subject);
            target.Text = MailMessageHelper.CreateForwardText(source.From, source.To, source.Subject, source.Text);
        }
        void UpdateMailMessage(MailMessage source, MailMessageViewModel target) {
            target.Date = source.Date;
            target.To = source.To;
            target.Subject = source.Subject;
            target.Text = source.Text;
            target.IsUnread = source.IsUnread;
        }
        void UpdateReplyMessage(MailMessageViewModel source, MailMessageViewModel target) {
            target.Date = DateTime.Now;
            target.Subject = MailMessageHelper.CreateReplySubject(source.Subject);
            target.To = MailMessageHelper.CreateReplyAddress(source.From);
            target.Text = MailMessageHelper.CreateReplyText(source.From, source.To, source.Subject, source.Text, source.Date);
        }
        void UpdateShowMessageContent() {
            if(SelectedMessage == null) {
                ShowMessageContent = false;
                RecalculateReadMessage(false);
            }
            else if(SelectedFolder.Name != SelectedMessage.Type) {
                SelectedMessage = PreviousMessage;
                ShowMessageContent = true;
                RecalculateReadMessage(false);
            }
            else {
                ShowMessageContent = true;
                RecalculateReadMessage(SelectedMessage.IsDraft);
            }
        }
    }
}
