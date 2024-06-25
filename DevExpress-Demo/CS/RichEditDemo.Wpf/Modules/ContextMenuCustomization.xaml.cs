using DevExpress.Xpf.Bars;
using DevExpress.Xpf.RichEdit;

namespace RichEditDemo {
    public partial class ContextMenuCustomization : RichEditDemoModule {
        public ContextMenuCustomization() {
            InitializeComponent();
        }

        void RichEditControl_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e) {
            if ((e.MenuType & RichEditMenuType.Text) != 0) {
                if (ceBoldText.IsChecked == true)
                    e.Customizations.Add(new BarButtonItem() { Command = RichEditUICommand.FormatFontBold, Content = "Custom Bold Text", CommandParameter = richEdit });
                if (ceClearFormatting.IsChecked == true)
                    e.Customizations.Add(new BarButtonItem() { Command = RichEditUICommand.FormatClearFormatting, Content = "Custom Clear Formatting", CommandParameter = richEdit });
            }
            if((e.MenuType & RichEditMenuType.TableCell) != 0) {
                if (ceShowGridlines.IsChecked == true)
                    e.Customizations.Add(new BarButtonItem() { Command = RichEditUICommand.TableToggleShowGridlines, Content = "Custom Show Gridlines", CommandParameter = richEdit });
            }
        }
    }
}
