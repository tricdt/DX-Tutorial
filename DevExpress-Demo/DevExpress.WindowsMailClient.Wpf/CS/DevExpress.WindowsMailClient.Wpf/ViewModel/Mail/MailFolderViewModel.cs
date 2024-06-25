using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using DevExpress.Mvvm.POCO;

namespace DevExpress.WindowsMailClient.Wpf.ViewModel {
    public class MailFolderViewModel : ContentViewModel {
        readonly ObservableCollection<MailMessageViewModel> items;
        Action<MailFolderViewModel> onSelect;

        protected MailFolderViewModel() {
            items = new ObservableCollection<MailMessageViewModel>();
            items.CollectionChanged += ItemsOnCollectionChanged;
        }

        public virtual string Header { get; set; }
        public bool IsFavorite { get; set; }
        public virtual bool IsSelected { get; set; }
        public ObservableCollection<MailMessageViewModel> Items { get { return items; } }
        public int MailCount { get { return Name == Data.MailFolders.Drafts ? Items.Count : Items.Cast<MailMessageViewModel>().Count(x => x.IsUnread); } }
        public virtual string Name { get; set; }

        public static MailFolderViewModel Create(string company, string name, bool isFavorite, Action<MailFolderViewModel> onSelect) {
            return 
                ViewModelSource.Create(() => new MailFolderViewModel { 
                    Name = name, 
                    Header = string.Format("{0} - {1}", name, company), 
                    Content = name, 
                    onSelect = onSelect, 
                    IsFavorite = isFavorite 
                });
        }

        public void UpdateMailCount() {
            RaisePropertyChanged("MailCount");
        }
        protected void OnIsSelectedChanged(bool oldValue) {
            if (IsSelected && onSelect != null)
                onSelect(this);
        }
        void ItemsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            UpdateMailCount();
        }
    }
}
