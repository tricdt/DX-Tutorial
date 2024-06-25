using System;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.PivotGrid;
using DevExpress.Xpf.Editors.Native;

namespace PivotGridDemo {
    
    public partial class FieldsCustomization : PivotGridDemoModule {
        public FieldsCustomization() {
            InitializeComponent();
        }

        protected override void ShowPopupContent() {
            base.ShowPopupContent();
            pivotGrid.ShowFieldList();
        }

        void ShowHideFieldList_Click(object sender, RoutedEventArgs e) {
            pivotGrid.IsFieldListVisible = !pivotGrid.IsFieldListVisible;
        }

        void customizationStyle_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            bool IsFieldListVisible = pivotGrid.IsFieldListVisible;
            gbAdvancedCustSettings.MaxWidth = gbCustSettings.ActualWidth;
            pivotGrid.FieldListStyle = (FieldListStyle)Enum.Parse(typeof(FieldListStyle), customizationStyle.SelectedItem.ToString());
            pivotGrid.IsFieldListVisible = IsFieldListVisible;
        }

        void OnAllowCustomizationFormChanged(object sender, EditValueChangedEventArgs e) {
            if(!IsLoaded)
                return;
            pivotGrid.IsFieldListVisible = pivotGrid.AllowCustomizationForm;
        }

        void OnCustomizationLayoutsEditValueChanged(object sender, EditValueChangedEventArgs e) {
            FieldListAllowedLayouts layout = (FieldListAllowedLayouts)customizationLayouts.SelectedItems[0];
            foreach(FieldListAllowedLayouts layout2 in customizationLayouts.SelectedItems)
                layout = layout | layout2;
            pivotGrid.FieldListAllowedLayouts = layout;
            EnsureCurrentLayoutItems(true);
        }

        void OnCustomizationLayoutsPopupContentSelectionChanged(object sender, SelectionChangedEventArgs e) {
            customizationLayouts.GetOkButton().IsEnabled = GetListBox().SelectedItems.Count > 0;
        }
        ListBox GetListBox() {
            return (ListBox)LookUpEditHelper.GetVisualClient(customizationLayouts).InnerEditor;
        }
        void OnCurrentLayoutEditValueChanged(object sender, EditValueChangedEventArgs e) {
            EnsureCurrentLayoutItems(false);
        }

        void EnsureCurrentLayoutItems(bool includeCurrent) {
            currentLayout.Items.BeginUpdate();
            currentLayout.Items.Clear();
            foreach(FieldListAllowedLayouts layout2 in customizationLayouts.SelectedItems) {
                currentLayout.Items.Add(ToFieldListAllowedLayouts(layout2));
            }
            if(includeCurrent && !currentLayout.Items.Contains(pivotGrid.FieldListLayout))
                currentLayout.Items.Add(pivotGrid.FieldListLayout);
            currentLayout.Items.EndUpdate();
        }

        FieldListLayout ToFieldListAllowedLayouts(FieldListAllowedLayouts layout) {
            switch(layout) {
                case FieldListAllowedLayouts.BottomPanelOnly1by4:
                    return FieldListLayout.BottomPanelOnly1by4;
                case FieldListAllowedLayouts.BottomPanelOnly2by2:
                    return FieldListLayout.BottomPanelOnly2by2;
                case FieldListAllowedLayouts.StackedDefault:
                    return FieldListLayout.StackedDefault;
                case FieldListAllowedLayouts.StackedSideBySide:
                    return FieldListLayout.StackedSideBySide;
                case FieldListAllowedLayouts.TopPanelOnly:
                    return FieldListLayout.TopPanelOnly;
                default:
                    return FieldListLayout.StackedDefault;
            }
        }
    }
}
