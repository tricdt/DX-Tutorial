using System;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.LayoutControl;

namespace LayoutControlDemo {
    public partial class pageLayoutControl : LayoutControlDemoModule {
        public pageLayoutControl() {
            InitializeComponent();
        }
        protected override void HidePopupContent() {
            if(layoutItems.IsCustomization)
                layoutItems.Controller.CustomizationController.SelectedElements.Clear();
            base.HidePopupContent();
        }
        void layoutItems_IsCustomizationChanged(object sender, EventArgs e) {
            if (layoutItems.IsCustomization)
                layoutItems.Controller.CustomizationController.SelectionChanged += layoutItems_SelectionChanged;
            else
                layoutItems.Controller.CustomizationController.SelectionChanged -= layoutItems_SelectionChanged;
            if (PropertiesControl != null)
                PropertiesControl.SelectedItem = null;
        }
        void layoutItems_SelectionChanged(object sender, LayoutControlSelectionChangedEventArgs e) {
            if (e.SelectedElements.Count == 1 && !ReferenceEquals(e.SelectedElements[0], layoutItems))
                PropertiesControl.SelectedItem = e.SelectedElements[0];
            else
                PropertiesControl.SelectedItem = null;
        }
        void LayoutGroupHeaderTextEdit_EditValueChanging(object sender, EditValueChangingEventArgs e) {
            var textBox = (TextEdit)sender;
            var layoutGroup = (sender as TextEdit).DataContext as LayoutGroup;
            if (layoutGroup == null) return;
            var parentLayoutGroup = layoutGroup.Parent as LayoutGroup;
            if (textBox.EditValue != null && Equals(textBox.EditValue, e.NewValue) && !parentLayoutGroup.View.Equals(LayoutGroupView.Tabs))
                layoutGroup.View = LayoutGroupView.GroupBox;
        }
        void LayoutGroupIsCollapsibleCheckBox_Checked(object sender, RoutedEventArgs e) {
            var checkEdit = (CheckEdit)sender;
            ((LayoutGroup)checkEdit.DataContext).View = LayoutGroupView.GroupBox;
        }
        void LayoutGroupIsCollapsedCheckBox_Checked(object sender, RoutedEventArgs e) {
            var checkEdit = (CheckEdit)sender;
            var group = (LayoutGroup)checkEdit.DataContext;
            group.View = LayoutGroupView.GroupBox;
            group.IsCollapsible = true;
        }
    }

    public class LayoutControlTemplateSelector : DataTemplateSelector {
        public DataTemplate LayoutGroupTemplate { get; set; }
        public DataTemplate LayoutItemTemplate { get; set; }
        public DataTemplate DefaultTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            if(item is LayoutGroup) return LayoutGroupTemplate;
            if(item is LayoutItem) return LayoutItemTemplate;
            if(item is TextEdit || item is SampleLayoutItem) return DefaultTemplate;
            return new DataTemplate();
        }
    }
}
