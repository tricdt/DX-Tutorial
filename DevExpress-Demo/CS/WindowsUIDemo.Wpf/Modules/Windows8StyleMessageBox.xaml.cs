using System;
using System.Windows;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.WindowsUI;
namespace CommonDemo {
    public partial class Windows8StyleMessageBox : DemoModule {
        public Windows8StyleMessageBox() {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e) {
            WinUIMessageBox.Show(
                contentEdit.DisplayText,
                captionEdit.DisplayText,
                (MessageBoxButton)buttons.EditValue,
                (MessageBoxImage)icons.EditValue,
                MessageBoxResult.None, MessageBoxOptions.None
                );
        }
    }
}
