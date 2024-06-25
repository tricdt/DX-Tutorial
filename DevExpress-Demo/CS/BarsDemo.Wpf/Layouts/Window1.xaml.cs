using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Docking;
using System.Windows;
using System.Windows.Controls;

namespace BarsDemo {
    
    
    
    public partial class FontSettingsView : UserControl {
        public FontSettingsView() {
            InitializeComponent();
        }        
    }
    public class FontSettingsModel {
        public virtual bool IsBold { get; set; }
        public virtual bool IsItalic { get; set; }
        public virtual bool IsUnderline { get; set; }
        public virtual FontWeight Weight { get; set; }
        public virtual TextDecorationCollection Decorations { get; set; }
        public virtual FontStyle Style { get; set; }
        public void OnIsBoldChanged() {
            Weight = IsBold ? FontWeights.Bold : FontWeights.Normal;
        }
        public void OnIsItalicChanged() {
            Style = IsItalic ? FontStyles.Italic : FontStyles.Normal;
        }
        public void OnIsUnderlineChanged() {
            Decorations = IsUnderline ? TextDecorations.Underline : new TextDecorationCollection();
        }
    }
}
