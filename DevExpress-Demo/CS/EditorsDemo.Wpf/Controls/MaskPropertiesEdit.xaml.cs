using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Editors;
using EditorsDemo.ModuleResources;

namespace EditorsDemo {
    public partial class MaskPropertiesEdit : UserControl {
        public static readonly DependencyProperty FocusedEditorProperty;
        public static readonly DependencyProperty SelectedCardInfoProperty;
        static MaskPropertiesEdit() {
            FocusedEditorProperty = DependencyProperty.Register(nameof(FocusedEditor), typeof(TextEdit), typeof(MaskPropertiesEdit), new PropertyMetadata(null, (d, e) => ((MaskPropertiesEdit)d).UpdateMask()));
            SelectedCardInfoProperty = DependencyProperty.Register(nameof(SelectedCardInfo), typeof(CardInfo), typeof(MaskPropertiesEdit), new PropertyMetadata(null));
        }
        public TextEdit FocusedEditor {
            get { return (TextEdit)GetValue(FocusedEditorProperty); }
            set { SetValue(FocusedEditorProperty, value); }
        }

        public CardInfo SelectedCardInfo {
            get { return (CardInfo)GetValue(SelectedCardInfoProperty); }
            set { SetValue(SelectedCardInfoProperty, value); }
        }
        public MaskPropertiesEdit() {
            InitializeComponent();
            SetCustomMaskCases();
        }
        public void UpdateMask() {
            UpdateVisibilities();
            if (FocusedEditor == null) {
                cbAutoComplete.SelectedIndex = -1;
                chIgnoreMaskBlank.IsChecked = false;
                txtEditMask.EditValue = null;
                chBeep.IsChecked = false;
                cbMaskType.SelectedIndex = -1;
                txtPlaceHolder.EditValue = ' ';
                cbSaveLiteral.IsChecked = false;
                chShowPlaceHolders.IsChecked = false;
                chUseMaskAsDisplayFormat.IsChecked = false;
                chAllowNull.IsChecked = false;
                return;
            }
            cbAutoComplete.SelectedIndex = (int)FocusedEditor.MaskAutoComplete;
            chIgnoreMaskBlank.IsChecked = FocusedEditor.MaskIgnoreBlank;
            txtEditMask.EditValue = FocusedEditor.Mask;
            chBeep.IsChecked = FocusedEditor.MaskBeepOnError;
            if (FocusedEditor.MaskType == MaskType.DateTime)
                cbMaskType.SelectedIndex = 0;
            else if (FocusedEditor.MaskType == MaskType.DateTimeAdvancingCaret)
                cbMaskType.SelectedIndex = 1;
            else
                cbMaskType.SelectedIndex = -1;
            txtPlaceHolder.EditValue = Convert.ToString(FocusedEditor.MaskPlaceHolder);
            cbSaveLiteral.IsChecked = FocusedEditor.MaskSaveLiteral;
            chShowPlaceHolders.IsChecked = FocusedEditor.MaskShowPlaceHolders;
            chUseMaskAsDisplayFormat.IsChecked = FocusedEditor.MaskUseAsDisplayFormat;
            chAllowNull.IsChecked = FocusedEditor.AllowNullInput;
        }
        void UpdateVisibilities() {
            if (FocusedEditor == null)
                return;
            cbAutoComplete.Visibility = FocusedEditor.MaskType.Equals(MaskType.RegEx) ? Visibility.Visible : Visibility.Collapsed;
            lblAutoComplete.Visibility = FocusedEditor.MaskType.Equals(MaskType.RegEx) ? Visibility.Visible : Visibility.Collapsed;
            chIgnoreMaskBlank.Visibility = FocusedEditor.MaskType == MaskType.Simple || FocusedEditor.MaskType == MaskType.Regular ?
                Visibility.Visible : Visibility.Collapsed;

            cbCardType.Visibility = FocusedEditor.MaskType.Equals(MaskType.Custom) ? Visibility.Visible : Visibility.Collapsed;
            lblCardType.Visibility = FocusedEditor.MaskType.Equals(MaskType.Custom) ? Visibility.Visible : Visibility.Collapsed;
            lblEditValue.Visibility = FocusedEditor.MaskType.Equals(MaskType.Custom) ? Visibility.Collapsed : Visibility.Visible;
            lblEditMask.Visibility = FocusedEditor.MaskType.Equals(MaskType.Custom) ? Visibility.Collapsed : Visibility.Visible;
            txtEditValue.Visibility = FocusedEditor.MaskType.Equals(MaskType.Custom) ? Visibility.Collapsed : Visibility.Visible;
            txtEditMask.Visibility = FocusedEditor.MaskType.Equals(MaskType.Custom) ? Visibility.Collapsed : Visibility.Visible;
            lblMaskType.Visibility = FocusedEditor.MaskType == MaskType.DateTime ||
                FocusedEditor.MaskType == MaskType.DateTime ?
                Visibility.Visible : Visibility.Collapsed;
            cbMaskType.Visibility = FocusedEditor.MaskType == MaskType.DateTime ||
                FocusedEditor.MaskType == MaskType.DateTimeAdvancingCaret ?
                Visibility.Visible : Visibility.Collapsed;
            txtPlaceHolder.Visibility = FocusedEditor.MaskType == MaskType.Simple ||
                FocusedEditor.MaskType == MaskType.Regular ||
                FocusedEditor.MaskType.Equals(MaskType.RegEx) ?
                Visibility.Visible : Visibility.Collapsed;
            lblPlaceHolder.Visibility = FocusedEditor.MaskType == MaskType.Simple ||
                FocusedEditor.MaskType == MaskType.Regular ||
                FocusedEditor.MaskType.Equals(MaskType.RegEx) ?
                Visibility.Visible : Visibility.Collapsed;

            cbSaveLiteral.Visibility = FocusedEditor.MaskType == MaskType.Simple || FocusedEditor.MaskType == MaskType.Regular ?
                Visibility.Visible : Visibility.Collapsed;
            chShowPlaceHolders.Visibility = FocusedEditor.MaskType.Equals(MaskType.RegEx) ? Visibility.Visible : Visibility.Collapsed;
            chAllowNull.Visibility = FocusedEditor.MaskType == MaskType.DateTime || FocusedEditor.MaskType == MaskType.DateTimeAdvancingCaret ||
                FocusedEditor.MaskType == MaskType.Numeric ? Visibility.Visible : Visibility.Collapsed;
        }
        void SetCustomMaskCases() {
            cbCardType.ItemsSource = GetCardTypes();
            cbCardType.SelectedIndex = 0;
        }
        private void cbAutoComplete_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if (FocusedEditor == null)
                return;
            FocusedEditor.MaskAutoComplete = (AutoCompleteType)Enum.Parse(typeof(AutoCompleteType), (string)((ComboBoxEdit)sender).SelectedItem, true);
        }
        private void chIgnoreMaskBlank_EditValueChanged(object sender, RoutedEventArgs e) {
            if (FocusedEditor == null)
                return;
            FocusedEditor.MaskIgnoreBlank = chIgnoreMaskBlank.IsChecked.Value;
        }
        private void txtEditMask_LostFocus(object sender, RoutedEventArgs e) {
            if (FocusedEditor == null)
                return;
            string maskBackup = FocusedEditor.Mask;
            try {
                FocusedEditor.Mask = (string)txtEditMask.EditValue;
            } catch {
                MessageBox.Show("Invalid mask", "Error");
                FocusedEditor.Mask = maskBackup;
                ((TextEdit)sender).EditValue = maskBackup;
            }
        }
        private void chBeep_EditValueChanged(object sender, RoutedEventArgs e) {
            if (FocusedEditor == null)
                return;
            FocusedEditor.MaskBeepOnError = chBeep.IsChecked.Value;
        }
        private void cbMaskType_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if (FocusedEditor == null)
                return;
            FocusedEditor.MaskType = (MaskType)Enum.Parse(typeof(MaskType), (string)((ComboBoxEdit)sender).SelectedItem, true);
        }
        private void txtPlaceHolder_EditValueChanged(object sender, RoutedEventArgs e) {
            if (FocusedEditor == null)
                return;
            FocusedEditor.MaskPlaceHolder = string.IsNullOrEmpty((string)txtPlaceHolder.EditValue) ? ' ' : Convert.ToChar(txtPlaceHolder.EditValue);
        }
        private void cbSaveLiteral_EditValueChanged(object sender, RoutedEventArgs e) {
            if (FocusedEditor == null)
                return;
            FocusedEditor.MaskSaveLiteral = cbSaveLiteral.IsChecked.Value;
        }
        private void chShowPlaceHolders_EditValueChanged(object sender, RoutedEventArgs e) {
            if (FocusedEditor == null)
                return;
            FocusedEditor.MaskShowPlaceHolders = chShowPlaceHolders.IsChecked.Value;
        }
        private void chUseMaskAsDisplayFormat_EditValueChanged(object sender, RoutedEventArgs e) {
            if (FocusedEditor == null)
                return;
            FocusedEditor.MaskUseAsDisplayFormat = chUseMaskAsDisplayFormat.IsChecked.Value;
        }
        private void chAllowNull_EditValueChanged(object sender, RoutedEventArgs e) {
            if (FocusedEditor == null)
                return;
            FocusedEditor.AllowNullInput = chAllowNull.IsChecked.Value;
        }
        private void OncbCardType_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            FocusedEditor?.Clear();
            SelectedCardInfo = (CardInfo)cbCardType.SelectedItem;
            FocusedEditor?.Focus();
        }
        private List<CardInfo> GetCardTypes() {
            var list = new List<CardInfo>();
            list.Add(new CardInfo ( "Visa/MC", "XXXX XXXX XXXX XXXX" ));
            list.Add(new CardInfo ( "Amex", "XXXX XXXXXX XXXXX" ));
            list.Add(new CardInfo ( "DX Bank", "XXX XXX XXX XXX XX" ));
            return list;
        }
    }
}
