using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Utils;
using System.Windows;
using System.Windows.Documents;

namespace BarsDemo {
    public partial class ContextMenu: BarsDemoModule {
        public ButtonSwitcher MenuButton {
            get { return (ButtonSwitcher)GetValue(MenuButtonProperty); }
            set { SetValue(MenuButtonProperty, value); }
        }
        public static readonly DependencyProperty MenuButtonProperty =
            DependencyPropertyManager.Register("MenuButton", typeof(ButtonSwitcher), typeof(ContextMenu), new FrameworkPropertyMetadata());
        public ContextMenu() {
            InitializeComponent();
            ModuleLoaded += OnModuleLoaded;
            CheckMouseSwitcher();
            edit.ContextMenu = null;
        }

        void OnModuleLoaded(object sender, RoutedEventArgs e) {
            edit.SetFocus();
        }

        private void OnRadioButtonClick(object sender, RoutedEventArgs e) {
            CheckMouseSwitcher();
        }
        private void CheckMouseSwitcher() {
            if((bool)Left.IsChecked)
                MenuButton = ButtonSwitcher.LeftButton;
            if((bool)LeftRight.IsChecked)
                MenuButton = ButtonSwitcher.LeftRightButton;
            if((bool)Right.IsChecked)
                MenuButton = ButtonSwitcher.RightButton;
            UpdateText(MenuButton);
        }
        private void UpdateText(ButtonSwitcher MenuButton) {
            string text = string.Empty;
            switch(MenuButton) {
                case ButtonSwitcher.LeftButton:
                    text = "Left click here to show a context menu";
                    break;
                case ButtonSwitcher.LeftRightButton:
                    text = "Left or right click here to show a context menu";
                    break;
                case ButtonSwitcher.RightButton:
                    text = "Right click here to show a context menu";
                    break;             
            }
            edit.Clear();
            edit.Document.Blocks.Add(new Paragraph(new Run(text)));
        }
    }
}
