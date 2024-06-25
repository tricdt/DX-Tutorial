using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;
using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GridDemo {
    
    
    
    [DevExpress.Xpf.DemoBase.CodeFile("Controls/Converters.(cs)")]
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/GridCellMultiColumnLookupEditorTemplates.xaml")]
    public partial class CellEditors : GridDemoModule {
        public CellEditors() {
            InitializeComponent();
            ComboBoxEditSettings settings = new ComboBoxEditSettings() { IsTextEditable = false };
            ComboBoxEdit.SetupComboBoxSettingsEnumItemSource<Priority>(settings);
            colPriority.EditSettings = settings;
            grid.ItemsSource = OutlookDataGenerator.CreateOutlookDataTable(1000);
            booleanColumnEditorListBox.EditValueChanged += new EditValueChangedEventHandler(booleanColumnEditorListBox_EditValueChanged);
            editorButtonShowModeListBox.EditValueChanged += new EditValueChangedEventHandler(editorButtonShowModeListBox_EditValueChanged);
            viewsListBox.EditValueChanged += new EditValueChangedEventHandler(viewsListBox_SelectionChanged);
            alternativeDisplayTemplateCheckBox.IsChecked = true;
            alternativeEditTemplateCheckBox.IsChecked = true;
            editorShowModeCombobox.EditValueChanged += new EditValueChangedEventHandler(editorShowModeCombobox_EditValueChanged);

        }
#region options

        void editorShowModeCombobox_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            UpdateEditorShowMode();
        }
        void viewsListBox_SelectionChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            if(viewsListBox.SelectedIndex == 0) {
                grid.View = (GridViewBase)FindResource("columnView");
            }
            if(viewsListBox.SelectedIndex == 1) {
                grid.View = (GridViewBase)FindResource("cardView");
            }
            UpdateEditorButtonShowMode();
            UpdateEditorShowMode();
        }
        void booleanColumnEditorListBox_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            if(booleanColumnEditorListBox.SelectedIndex == 0) {
                colHasAttachment.EditSettings = null;
            }
            if(booleanColumnEditorListBox.SelectedIndex == 1) {
                colHasAttachment.EditSettings = new TextEditSettings();
            }
            if(booleanColumnEditorListBox.SelectedIndex == 2) {
                colHasAttachment.EditSettings = new ComboBoxEditSettings() { ItemsSource = new bool[] { true, false } };
            }
        }
        void editorButtonShowModeListBox_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            UpdateEditorButtonShowMode();
        }
        void UpdateEditorButtonShowMode() {
            if(editorButtonShowModeListBox.SelectedIndex == 0) {
                grid.View.EditorButtonShowMode = EditorButtonShowMode.ShowOnlyInEditor;
            }
            if(editorButtonShowModeListBox.SelectedIndex == 1) {
                grid.View.EditorButtonShowMode = EditorButtonShowMode.ShowForFocusedCell;
            }
            if(editorButtonShowModeListBox.SelectedIndex == 2) {
                grid.View.EditorButtonShowMode = EditorButtonShowMode.ShowForFocusedRow;
            }
            if(editorButtonShowModeListBox.SelectedIndex == 3) {
                grid.View.EditorButtonShowMode = EditorButtonShowMode.ShowAlways;
            }
        }
        void UpdateEditorShowMode() {
            if(editorShowModeCombobox.SelectedIndex == 0) {
                grid.View.EditorShowMode = EditorShowMode.MouseDown;
            }
            if(editorShowModeCombobox.SelectedIndex == 1) {
                grid.View.EditorShowMode = EditorShowMode.MouseDownFocused;
            }
            if(editorShowModeCombobox.SelectedIndex == 2) {
                grid.View.EditorShowMode = EditorShowMode.MouseUp;
            }
            if(editorShowModeCombobox.SelectedIndex == 3) {
                grid.View.EditorShowMode = EditorShowMode.MouseUpFocused;
            }
        }
        void colHoursActive_Validate(object sender, GridCellValidationEventArgs e) {
            double value = Convert.ToDouble(e.Value, e.Culture);
            if(value <= 0 || 1000 < value) {
                e.SetError("The Hours Active value must be greater than zero and less than or equal to 1000", ErrorType.Default);
            }
        }
#endregion
#region colHoursActive options
        private void alternativeDisplayTemplateCheckBox_Checked(object sender, RoutedEventArgs e) {
            colHoursActive.CellDisplayTemplate = (DataTemplate)Resources["alternativeHoursActiveDisplayTemplate"];
        }
        private void alternativeDisplayTemplateCheckBox_Unchecked(object sender, RoutedEventArgs e) {
            colHoursActive.CellDisplayTemplate = null;
        }
        private void alternativeEditTemplateCheckBox_Checked(object sender, RoutedEventArgs e) {
            colHoursActive.CellEditTemplate = (DataTemplate)Resources["alternativeHoursActiveEditTemplate"];
        }
        private void alternativeEditTemplateCheckBox_Unchecked(object sender, RoutedEventArgs e) {
            colHoursActive.CellEditTemplate = null;
        }
#endregion
#region custom cell editors events
        void TableView_GetIsEditorActivationAction(object sender, GetIsEditorActivationActionEventArgs e) {
            if(e.Column.FieldName == "HoursActive" && e.ActivationAction == ActivationAction.KeyDown) {
                e.IsActivationAction = IsSliderCommand(e.KeyDownEventArgs.Key) && ((Keyboard.Modifiers == ModifierKeys.None) || (Keyboard.Modifiers == ModifierKeys.Control));
            }
        }
        void TableView_ProcessEditorActivationAction(object sender, ProcessEditorActivationActionEventArgs e) {
            if(e.Column.FieldName == "HoursActive" && e.ActivationAction == ActivationAction.KeyDown) {
                e.RaiseEventAgain = IsSliderCommand(e.KeyDownEventArgs.Key);
            }
            if(e.Column.FieldName == "HoursActive" && e.ActivationAction == ActivationAction.MouseLeftButtonDown) {
                e.RaiseEventAgain = true;
            }
        }
        bool IsSliderCommand(Key key) {
            switch(key) {
                case Key.Add:
                case Key.Subtract:
                case Key.OemPlus:
                case Key.OemMinus:
                    return true;
                default:
                    return false;
            }
        }
        #endregion
    }
}
