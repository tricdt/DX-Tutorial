using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using DevExpress.MailClient.Helpers;
using DevExpress.Xpf.RichEdit;
using DevExpress.Xpf.SpellChecker;
using DevExpress.XtraRichEdit.SpellChecker;
using DevExpress.XtraSpellChecker;
using DevExpress.XtraSpellChecker.Native;
using System.ComponentModel;
using DevExpress.Mvvm;
using ContentSpellChecker = DevExpress.Xpf.SpellChecker.SpellChecker;

namespace DevExpress.MailClient.View {
    public partial class NewMessageContent : UserControl {
        public ContentSpellChecker SpellChecker { get; set; }

        public NewMessageContent() {
            if(!DesignerProperties.GetIsInDesignMode(this)) {
                SpellChecker = CreateSpellChecker();
            }
            InitializeComponent();
        }
        
        void OnRichEditLoaded(object sender, RoutedEventArgs e) {
            richEdit.SpellChecker = SpellChecker;
        }
        ContentSpellChecker CreateSpellChecker() {
            var spellChecker = new ContentSpellChecker();
            spellChecker.Culture = new CultureInfo("en-US");
            spellChecker.SpellCheckMode = DevExpress.XtraSpellChecker.SpellCheckMode.AsYouType;

            SpellCheckTextControllersManager.Default.RegisterClass(typeof(RichEditControl), typeof(RichEditSpellCheckController));
            return spellChecker;
        }
    }
}
