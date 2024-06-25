using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Accordion;

namespace NavigationDemo {
    public class NavigationPaneViewModel {
        protected IDocumentManagerService DocumentManagerService { get { return this.GetRequiredService<IDocumentManagerService>(); } }

        public virtual ObservableCollection<GroupDescription> Groups { get; set; }
        public virtual GroupDescription SelectedGroup { get; set; }
        public virtual bool OnStartup { get; set; }

        ContactsNavigationViewModel contactsNavigationViewModel;

        public void OnLoaded() {
            if(OnStartup)
                OnSelectedItemChanged(new AccordionSelectedItemChangedEventArgs(null, null, Groups.Single(x => x.Id == NavigationId.Mail).Items.First()));
        }
        public void OnSelectedItemChanged(AccordionSelectedItemChangedEventArgs e) {
            if(e.OldItem != null)
                OnStartup = false;
            var newItem = e.NewItem as ItemDescription;
            if(newItem == null)
                return;
            var document = DocumentManagerService.CreateDocument(GetDocumentType(newItem.Id), GetDocumentParameter(newItem.Id), this);
            document.DestroyOnClose = true;
            document.Show();
        }
        public NavigationPaneViewModel() {
            OnStartup = true;
            Groups = CreateGroups();
            SelectedGroup = Groups[0];
        }
        ObservableCollection<GroupDescription> CreateGroups() {
            var groups = new ObservableCollection<GroupDescription>();
            contactsNavigationViewModel = ContactsNavigationViewModel.Create();

            groups.Add(GroupDescription.Create(NavigationId.Mail, items: new ItemDescription[] {
                ItemDescription.Create(NavigationId.Inbox),
                ItemDescription.Create(NavigationId.Outbox),
                ItemDescription.Create(NavigationId.SentItems, "Sent Items"),
                ItemDescription.Create(NavigationId.DeletedItems, "Deleted Items"),
                ItemDescription.Create(NavigationId.Drafts),
            }));
            groups.Add(GroupDescription.Create(NavigationId.Calendar, items: new ItemDescription[] { ItemDescription.Create(NavigationId.Calendar_Content) }));
            groups.Add(GroupDescription.Create(NavigationId.Contacts, items: new ItemDescription[] { ItemDescription.Create(NavigationId.Contacts_Content,
                navigationViewModel: contactsNavigationViewModel) }));
            groups.Add(GroupDescription.Create(NavigationId.Tasks, items: new ItemDescription[] { ItemDescription.Create(NavigationId.Tasks) }));
            groups.Add(GroupDescription.Create(NavigationId.Notes, items: new ItemDescription[] { ItemDescription.Create(NavigationId.Notes) }));
            groups.Add(GroupDescription.Create(NavigationId.FolderList, "Folder List", items: new ItemDescription[] {
                ItemDescription.Create(NavigationId.PersonalFolders, "Personal Folders", new ItemDescription[]{
                    ItemDescription.Create(NavigationId.Contacts),
                    ItemDescription.Create(NavigationId.DeletedItems, "Deleted Items"),
                    ItemDescription.Create(NavigationId.Drafts),
                    ItemDescription.Create(NavigationId.Inbox),
                    ItemDescription.Create(NavigationId.Journal),
                    ItemDescription.Create(NavigationId.Notes),
                    ItemDescription.Create(NavigationId.Outbox),
                    ItemDescription.Create(NavigationId.SentItems, "Sent Items"),
                    ItemDescription.Create(NavigationId.Tasks)
                })
            }, showChildrenExpandButton: true));
            groups.Add(GroupDescription.Create(NavigationId.Shortcuts, items: new ItemDescription[] { ItemDescription.Create(NavigationId.Shortcuts) }));
            groups.Add(GroupDescription.Create(NavigationId.Journal, items: new ItemDescription[] { ItemDescription.Create(NavigationId.Journal) }));
            return groups;
        }

        string GetDocumentType(NavigationId itemId) {
            var resultId = itemId;
            if(MailGroupContainsItem(itemId))
                resultId = NavigationId.Mail;
            else if(itemId == NavigationId.Calendar_Content || itemId == NavigationId.PersonalFolders)
                return "Info";
            else if(itemId == NavigationId.Contacts_Content)
                return "Contacts";
            return resultId.ToString();
        }
        object GetDocumentParameter(NavigationId itemId) {
            if(MailGroupContainsItem(itemId))
                return itemId;
            else if(itemId == NavigationId.Calendar_Content || itemId == NavigationId.PersonalFolders)
                return string.Empty;
            else if(itemId == NavigationId.Contacts || itemId == NavigationId.Contacts_Content)
                return contactsNavigationViewModel;
            return null;
        }
        bool MailGroupContainsItem(NavigationId itemId) {
            return Groups.Single(x => x.Id == NavigationId.Mail).Items.SingleOrDefault(y => y.Id == itemId) != null;
        }
    }

    public class GroupDescription : ItemDescriptionBase {
        public static GroupDescription Create(NavigationId id, string title = null, IList<ItemDescription> items = null, bool showChildrenExpandButton = false) {
            return ViewModelSource.Create(() => new GroupDescription(id, title, items, showChildrenExpandButton));
        }
        protected GroupDescription(NavigationId id, string title, IList<ItemDescription> items, bool showChildrenExpandButton) : base(id, title, items) {
            Icon = NavigationPaneData.GetIcon(Id.ToString());
            if(Items.Any())
                SelectedItem = Items.First();
            foreach(var item in Items)
                item.ShowExpandButton = showChildrenExpandButton;
        }
        public virtual ItemDescription SelectedItem { get; set; }
    }

    public class ItemDescription : ItemDescriptionBase {
        public static ItemDescription Create(NavigationId id, string title = null, IList<ItemDescription> items = null, NavigationViewModelBase navigationViewModel = null) {
            return ViewModelSource.Create(() => new ItemDescription(id, title, items, navigationViewModel));
        }
        protected ItemDescription(NavigationId id, string title, IList<ItemDescription> items, NavigationViewModelBase navigationViewModel)
            : base(id, title, items, navigationViewModel) {
            if(Id != NavigationId.Calendar_Content && Id != NavigationId.Contacts_Content)
                Icon = NavigationPaneData.GetIcon(Id.ToString());
        }
    }
    public class ItemDescriptionBase {
        protected ItemDescriptionBase(NavigationId id, string title, IList<ItemDescription> items, NavigationViewModelBase navigationViewModel = null) {
            Id = id;
            Title = title ?? Id.ToString();
            Items = new List<ItemDescription>(items ?? Enumerable.Empty<ItemDescription>());
            ShowExpandButton = true;
            NavigationViewModel = navigationViewModel;
        }
        public NavigationId Id { get; protected set; }
        public string Title { get; protected set; }
        public Uri Icon { get; protected set; }
        public virtual bool ShowExpandButton { get; set; }

        public NavigationViewModelBase NavigationViewModel { get; protected set; }

        public virtual IList<ItemDescription> Items { get; set; }
    }

    public enum NavigationId {
        Mail,
        Inbox,
        Outbox,
        SentItems,
        DeletedItems,
        Drafts,
        Calendar,
        Calendar_Content,
        Contacts,
        Contacts_Content,
        Tasks,
        Notes,
        FolderList,
        PersonalFolders,
        Shortcuts,
        Journal
    }
}
