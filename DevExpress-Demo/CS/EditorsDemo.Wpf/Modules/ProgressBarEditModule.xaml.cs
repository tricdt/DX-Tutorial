using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;
using System.Windows.Markup;
using EditorsDemo.Utils;

namespace EditorsDemo {
    public partial class ProgressBarEditModule : EditorsDemoModule {
        public ProgressBarEditModule() {
            InitializeComponent();
            cbContentSettings.ItemsSource = new List<ContentDisplayMode> {ContentDisplayMode.None, ContentDisplayMode.Value};
            ComboBoxEdit.SetupComboBoxEnumItemSource<Orientation, Orientation>(cbOrientation);
            cbOrientation.EditValue = Orientation.Horizontal;
            SetupOrientation();
            DataContext = new OutlookViewModel();
        }
        void SetupOrientation() {
            if(object.Equals(cbOrientation.EditValue, Orientation.Horizontal)) {
                editor.Width = 400;
                editor.ClearValue(FrameworkElement.HeightProperty);
                editor.Orientation = Orientation.Horizontal;
            }
            else {
                editor.Height = 400;
                editor.ClearValue(FrameworkElement.WidthProperty);
                editor.Orientation = Orientation.Vertical;
            }
        }
        private void cbBarStyle_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            if(cbBarStyle.SelectedIndex == 0)
                editor.StyleSettings = new ProgressBarStyleSettings();
            else
                editor.StyleSettings = new ProgressBarMarqueeStyleSettings();
        }
        private void CheckEdit_Checked(object sender, RoutedEventArgs e) {
            if(string.IsNullOrEmpty(cbDisplayFormat.EditValue as string) || cbDisplayFormat.SelectedIndex == 0)
                cbDisplayFormat.SelectedIndex = 1;
        }
        private void CheckEdit_Unchecked(object sender, RoutedEventArgs e) {
            if (cbDisplayFormat.SelectedIndex == 1)
                cbDisplayFormat.SelectedIndex = 0;
        }

        private void cbDisplayFormat_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            try {
                editor.DisplayFormatString = (string)e.NewValue;
            }
            catch {
                editor.DisplayFormatString = "";
            }
        }

        private void cbOrientation_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            SetupOrientation();
        }
    }
}
