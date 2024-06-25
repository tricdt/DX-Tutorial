using DevExpress.WindowsMailClient.Wpf.Internal;
using System.Collections.ObjectModel;

namespace DevExpress.WindowsMailClient.Wpf.ViewModel {
    public class MailFolderCollectionViewModel : ContentViewModel {
        protected MailFolderCollectionViewModel() {
            Folders = new ObservableCollection<MailFolderViewModel>();
        }

        public ObservableCollection<MailFolderViewModel> Folders { get; private set; }
        public string Header { get; set; }

        public static MailFolderCollectionViewModel Create(string content, string header, string glyph) {
            return new MailFolderCollectionViewModel { 
                Content = content, 
                Header = header, 
                Glyph = ImageSourceHelper.CreateSvgImageSource(glyph) 
            };
        }
    }
}
