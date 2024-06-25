using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace DevExpress.WindowsMailClient.Wpf.ViewModel {
    public class MailAccountViewModel : CommandViewModel {
        public static MailAccountViewModel Create(string company, string email, string foldersName, string foldersHeader, string glyph) {
            return ViewModelSource.Create(() => new MailAccountViewModel() {
                Company = company,
                Email = email,
                FolderCollection = MailFolderCollectionViewModel.Create(foldersName, foldersHeader, glyph),
            });
        }

        protected MailAccountViewModel() {
            Command = new DelegateCommand(SelectDefaultFolder);
        }

        public virtual string Company { get; set; }
        public MailFolderViewModel DefaultFolder { get { return Folders.FirstOrDefault(); }}
        public virtual string Email { get; set; }
        public MailFolderCollectionViewModel FolderCollection { get; private set; }
        public ObservableCollection<MailFolderViewModel> Folders {
            get { return FolderCollection.Folders; }
        }
        public virtual bool IsSelected { get; set; }
        public int MailCount {
            get { return DefaultFolder != null ? DefaultFolder.MailCount : 0; }
        }
                
        public MailFolderViewModel FindFolder(string folderName) {
            return Folders.FirstOrDefault(f => f.Name == folderName);
        }
        public void UpdateMailCount() {
            RaisePropertyChanged("MailCount");
        }
        protected void OnIsSelectedChanged(bool oldValue) {
            SelectDefaultFolder();
        }
        void SelectDefaultFolder() {
            if(DefaultFolder != null) DefaultFolder.IsSelected = IsSelected;
        }
    }
}
