using DevExpress.DemoData.Models;
using DevExpress.Mvvm;
using DevExpress.Utils;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.DemosHelpers.Grid;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace GridDemo {
    [DevExpress.Xpf.DemoBase.CodeFile("Descriptions/DataEditorTypesDescriptions.xaml")]
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/DataEditorTypesTemplates.xaml")]
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/DataEditorTypesClasses.(cs)")]
    public partial class DataEditorTypes : GridDemoModule {
        MultiEditorsList list;
        public DataEditorTypes() {
            DataContext = this;
            ButtonEditClickCommand = new DelegateCommand<object>(PART_Editor_DefaultButtonClick);
            InitializeComponent();
            UpdateDescription();
            AssignDataSource();
        }
        void AssignDataSource() {
            list = new MultiEditorsList();

            MultiEditorsTemplateSelector templateSelector = new MultiEditorsTemplateSelector();

            foreach(PropertyDescriptor propertyDescriptor in ((ITypedList)list).GetItemProperties(null)) {
                GridColumn column = new GridColumn() { FieldName = propertyDescriptor.Name };
                if(column.FieldName == "Field") {
                    column.AllowEditing = DefaultBoolean.False;
                    column.Fixed = FixedStyle.Left;
                }
                if(column.FieldName == "TemplateName") {
                    column.Visible = false;
                    column.ShowInColumnChooser = false;
                }
                if(column.FieldName == "EditorType") {
                    column.Fixed = FixedStyle.Right;
                    column.AllowEditing = DefaultBoolean.False;
                    column.Width = 200;
                }
                if(column.FieldName != "Field" && column.FieldName != "EditorType")
                    column.CellTemplateSelector = templateSelector;
                grid.Columns.Add(column);
            }
            grid.ItemsSource = list;
        }

        void PART_Editor_DefaultButtonClick(object param) {
            ListBoxEdit listBoxEdit = new ListBoxEdit() { ItemsSource = NWindContext.Create().CountriesArray, ShowBorder = false };
            listBoxEdit.EditValue = grid.GetCellValue(grid.View.FocusedRowHandle, (GridColumn)grid.CurrentColumn);
            DialogClosedDelegate closedHandler = delegate(bool? dialogResult) {
                if(dialogResult == true) {
                    grid.View.ShowEditor();
                    grid.View.ActiveEditor.EditValue = listBoxEdit.EditValue;
                }
            };

            FloatingContainer.ShowDialogContent(listBoxEdit, grid.View.ActiveEditor, new Size(400, 300), new FloatingContainerParameters() { ClosedDelegate = closedHandler, Title = "Select Country", ContainerFocusable = false });
        }
        void TableView_ShowGridMenu(object sender, GridMenuEventArgs e) {
            if(e.MenuType == GridMenuType.Column) {
                e.Customizations.Add(new RemoveBarItemAndLinkAction() { ItemName = DefaultColumnMenuItemNames.GroupBox });
                e.Customizations.Add(new RemoveBarItemAndLinkAction() { ItemName = DefaultColumnMenuItemNames.SortAscending });
                e.Customizations.Add(new RemoveBarItemAndLinkAction() { ItemName = DefaultColumnMenuItemNames.SortDescending });
                e.Customizations.Add(new RemoveBarItemAndLinkAction() { ItemName = DefaultColumnMenuItemNames.ClearSorting });
                e.Customizations.Add(new RemoveBarItemAndLinkAction() { ItemName = DefaultColumnMenuItemNames.GroupColumn });
            }
        }

        void TableView_FocusedRowChanged(object sender, FocusedRowHandleChangedEventArgs e) {
            UpdateDescription();
        }
        string lastDescription;
        void UpdateDescription() {
            if(descriptionRichTextBox == null)
                return;
            BlockCollection blocks =
            descriptionRichTextBox.Document.Blocks;
            if(grid.View.FocusedRowHandle == GridControl.InvalidRowHandle)
                return;
            string newDescription = list.FieldDescriptions[grid.View.FocusedRowHandle].TemplateName + "Description";
            if(newDescription == lastDescription)
                return;
            lastDescription = newDescription;
            ContentControl control = new ContentControl() { Template = Resources[newDescription] as ControlTemplate };
            control.ApplyTemplate();
            ParagraphContainer container = VisualTreeHelper.GetChild(control, 0) as ParagraphContainer;

            blocks.Clear();
            blocks.Add(container.Paragraph);
        }
        void TableView_ShowingEditor(object sender, ShowingEditorEventArgs e) {
            e.Cancel = list.FieldDescriptions[grid.View.FocusedRowHandle].TemplateName == "ProgressBarEdit";
        }
        public ICommand ButtonEditClickCommand { get; private set; }
    }
}
