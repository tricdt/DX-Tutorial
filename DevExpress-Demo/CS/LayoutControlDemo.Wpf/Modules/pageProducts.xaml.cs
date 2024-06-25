using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace LayoutControlDemo {
    public partial class pageProducts : LayoutControlDemoModule {
        private int _SelectedPackIndex = -1;

        public pageProducts() {
            InitializeComponent();
            var DisabledProductBrush = (Brush)Resources["DisabledProductBrush"];
            RegisterName("DisabledProductBrush", DisabledProductBrush);
        }

        protected void UpdateProducts() {
            var DisabledProductBrushAnimation = (Storyboard)Resources["DisabledProductBrushAnimation"];
            DisabledProductBrushAnimation.Stop();

            string packName = "Pack" + SelectedPackIndex.ToString();
            var EnabledProductBrush = Resources["EnabledProductBrush"] as Brush;
            var DisabledProductBrush = Resources["DisabledProductBrush"] as Brush;

            foreach (FrameworkElement element in layoutProducts.GetVisibleChildren()) {
                var textBlock = element as TextBlock;
                if (textBlock == null || textBlock.Tag == null)
                    continue;
                bool isEnabled = SelectedPackIndex == -1 || ((string)textBlock.Tag).Contains(packName);
                textBlock.Foreground = isEnabled ? EnabledProductBrush : DisabledProductBrush;
            }

            DisabledProductBrushAnimation.Begin();
        }

        protected int SelectedPackIndex {
            get { return _SelectedPackIndex; }
            set {
                if(_SelectedPackIndex != value) {
                    _SelectedPackIndex = value;
                    UpdateProducts();
                }
            }
        }

        void PackTextBlock_RequestNavigate(object sender, RequestNavigateEventArgs e) {
            if(BrowserInteropHelper.IsBrowserHosted) return;
            try {
                Process.Start(new ProcessStartInfo { FileName = e.Uri.AbsoluteUri, UseShellExecute = true });
            }
            catch {
            }
            e.Handled = true;
        }
        void PackTextBlock_MouseEnter(object sender, MouseEventArgs e) {
            System.Windows.Documents.Hyperlink hyperlink = sender as System.Windows.Documents.Hyperlink;
            hyperlink.TextDecorations = TextDecorations.Underline;
            if(hyperlink != null) SelectedPackIndex = int.Parse(hyperlink.Name[hyperlink.Name.Length - 1].ToString());
        }
        void PackTextBlock_MouseLeave(object sender, MouseEventArgs e) {
            System.Windows.Documents.Hyperlink hyperlink = sender as System.Windows.Documents.Hyperlink;
            hyperlink.TextDecorations = null;
            SelectedPackIndex = -1;
        }
    }
}
