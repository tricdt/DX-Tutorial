using System;
using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using DevExpress.WindowsMailClient.Wpf.Internal;

namespace DevExpress.WindowsMailClient.Wpf.ViewModel {
    public class MailAccountCollectionViewModel : CommandViewModel {
        protected MailAccountCollectionViewModel() {
            Accounts = new ObservableCollection<MailAccountViewModel>();
        }

        public ObservableCollection<MailAccountViewModel> Accounts { get; private set; }

        public static MailAccountCollectionViewModel Create(string content, Action action, string glyph) {
            return new MailAccountCollectionViewModel { 
                Content = content, 
                Command = new DelegateCommand(action), 
                Glyph = ImageSourceHelper.CreateSvgImageSource(glyph)
            };
        }
    }
}
