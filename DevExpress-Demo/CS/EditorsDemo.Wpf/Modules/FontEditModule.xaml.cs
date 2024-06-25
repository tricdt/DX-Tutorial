using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Utils;

namespace EditorsDemo {
    
    
    
    public partial class FontEditModule : EditorsDemoModule {
        Color selectedColor = Colors.Black;
        Color SelectedColor {
            get { return this.selectedColor; }
            set {
                this.selectedColor = value;
                if (this.richControl != null)
                    this.richControl.TextColor = value;
            }
        }
        public FontEditModule() {
            InitializeComponent();
            Loaded += OnLoaded;
            richControl.Text = "The DXEditors Library offers a collection of advanced data editors available for use within data entry forms, option editors and data-aware controls. Our editors provide seamless integration with the rest of our product line, including the data grid and toolbar-menu controls. When it comes to data input and representation, the DevExpress Data Editors Library is unmatched in providing the same level of customization and flexibility.";
        }
        [DefaultValue(false)]
        bool IsInUpdate { get; set; }
        void OnLoaded(object sender, RoutedEventArgs e) {
            ((ComboBoxEditSettings)eFontSize.EditSettings).ItemsSource = FontSizes.Sizes;
            UpdateBarsValues();
            richControl.Focus();
        }
        void eFontFamily_EditValueChanged(object sender, RoutedEventArgs e) {
            if (IsInUpdate)
                return;
            richControl.TextFontFamily = eFontFamily.EditValue;
            FocusRichControl();
        }
        void eFontSize_EditValueChanged(object sender, RoutedEventArgs e) {
            if (IsInUpdate)
                return;
            richControl.TextFontSize = eFontSize.EditValue;
            FocusRichControl();
        }
        void richControl_SelectionChanged(object sender, RoutedEventArgs e) {
            UpdateBarsValues();
        }
        void UpdateBarsValues() {
            IsInUpdate = true;
            eFontFamily.EditValue = richControl.TextFontFamily;
            eFontSize.EditValue = richControl.TextFontSize;
            TextAlignment textAlignment = this.richControl.GetTextAlignment();
            this.bLeft.IsChecked = object.Equals(textAlignment, TextAlignment.Left);
            this.bCenter.IsChecked = object.Equals(textAlignment, TextAlignment.Center);
            this.bRight.IsChecked = object.Equals(textAlignment,TextAlignment.Right);
            this.bBold.IsChecked = this.richControl.TextIsBold;
            this.bItalic.IsChecked = this.richControl.TextIsItalic;
            this.bUnderline.IsChecked = this.richControl.TextIsUnderline;
            IsInUpdate = false;
        }
        void FocusRichControl() {
            richControl.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new ThreadStart(() => richControl.Focus()));
        }

        private void fontColorChooser_ColorChanged(object sender, EventArgs e) {
            if (IsInUpdate)
                return;
            SelectedColor = ((ColorChooser)sender).Color;
            FocusRichControl();
        }

        private void bBold_ItemClick(object sender, ItemClickEventArgs e) {
            if (IsInUpdate)
                return;
            richControl.TextIsBold = (bool)this.bBold.IsChecked;
            FocusRichControl();
        }

        private void bItalic_ItemClick(object sender, ItemClickEventArgs e) {
            if (IsInUpdate)
                return;
            richControl.TextIsItalic = (bool)this.bItalic.IsChecked;
            FocusRichControl();
        }

        private void bUnderline_ItemClick(object sender, ItemClickEventArgs e) {
            if (IsInUpdate)
                return;
            richControl.TextIsUnderline = (bool)this.bUnderline.IsChecked;
            FocusRichControl();
        }

        private void bLeft_CheckedChanged(object sender, ItemClickEventArgs e) {
            if (IsInUpdate || richControl == null)
                return;
            if ((bool)((BarCheckItem)sender).IsChecked) 
                richControl.ToggleTextAlignmentLeft();
            FocusRichControl();
        }

        private void bCenter_CheckedChanged(object sender, ItemClickEventArgs e) {
            if (IsInUpdate || richControl == null)
                return;
            if ((bool)((BarCheckItem)sender).IsChecked) 
                richControl.ToggleTextAlignmentCenter();
            FocusRichControl();
        }

        private void bRight_CheckedChanged(object sender, ItemClickEventArgs e) {
            if (IsInUpdate || richControl == null)
                return;
            if ((bool)((BarCheckItem)sender).IsChecked) 
                richControl.ToggleTextAlignmentRight();
            FocusRichControl();
        }

        private void bFontColor_ItemClick(object sender, ItemClickEventArgs e) {
            if (IsInUpdate)
                return;
            richControl.TextColor = SelectedColor;
            FocusRichControl();
        }
    }
}
