using DevExpress.Mvvm.POCO;
using DevExpress.WindowsMailClient.Wpf.Internal;
using System.Windows.Media;

namespace DevExpress.WindowsMailClient.Wpf.ViewModel {
    public class MailAccountSettingsViewModel {
        protected MailAccountSettingsViewModel() { }
                
        public virtual string Company { get; set; }
        public virtual string Email { get; set; }
        public virtual ImageSource Glyph { get; set; }

        public static MailAccountSettingsViewModel Create(string company, string email, string glyph) {
            return ViewModelSource.Create(() => new MailAccountSettingsViewModel() {
                Company = company,
                Email = email,
                Glyph = ImageSourceHelper.CreateSvgImageSource(glyph)
            });
        }
    }
}
