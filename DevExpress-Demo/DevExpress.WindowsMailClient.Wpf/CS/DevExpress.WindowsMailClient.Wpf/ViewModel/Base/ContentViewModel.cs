using System.Windows.Media;
using DevExpress.Mvvm;

namespace DevExpress.WindowsMailClient.Wpf.ViewModel {
    public abstract class ContentViewModel : BindableBase {
        protected ContentViewModel() { }
        public virtual object Content { get; set; }
        public virtual ImageSource Glyph { get; set; }
    }
}
