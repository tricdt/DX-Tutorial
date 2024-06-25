using DevExpress.DemoData.Models;
using DevExpress.Utils;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.DemosHelpers.Grid;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using DevExpress.Xpf.DemoBase;

namespace EditorsDemo {
    [CodeFile("ModuleResources/DataEditorTypesDescriptions.xaml")]
    [CodeFile("ModuleResources/DataEditorTypesTemplates.xaml")]
    [CodeFile("ModuleResources/DataEditorTypesClasses.(cs)")]
    public partial class InplaceEditorsTreeListModule : EditorsDemoModule {
        TreeListMultiEditorsList list;
        public InplaceEditorsTreeListModule() {
            InitializeComponent();
            UpdateDescription();
            AssignDataSource();
            treeListView.ExpandAllNodes();
        }
        void AssignDataSource() {
            list = new TreeListMultiEditorsList();

            TreeListMultiEditorsTemplateSelector templateSelector = new TreeListMultiEditorsTemplateSelector();

            foreach (PropertyDescriptor propertyDescriptor in ((ITypedList)list).GetItemProperties(null)) {
                TreeListColumn column = new TreeListColumn() { FieldName = propertyDescriptor.Name };
                if (column.FieldName == "Field") {
                    column.AllowEditing = DefaultBoolean.False;
                    column.Fixed = FixedStyle.Right;
                }
                if (column.FieldName == "TemplateName") {
                    column.Visible = false;
                    column.ShowInColumnChooser = false;
                }
                if (column.FieldName == "EditorType") {
                    column.Fixed = FixedStyle.Left;
                    column.AllowEditing = DefaultBoolean.False;
                    column.Width = 200;
                }
                if (column.FieldName == "Id") {
                    column.Visible = false;
                    column.ShowInColumnChooser = false;
                }
                if (column.FieldName == "ParentId") {
                    column.Visible = false;
                    column.ShowInColumnChooser = false;
                }

                if (column.FieldName != "Field" && column.FieldName != "EditorType")
                    column.CellTemplateSelector = templateSelector;
                treeList.Columns.Add(column);
            }
            treeList.ItemsSource = list;
        }
        void PART_Editor_DefaultButtonClick(object sender, RoutedEventArgs e) {
            ListBoxEdit listBoxEdit = new ListBoxEdit() { ItemsSource = NWindContext.Create().CountriesArray, ShowBorder = false };
            listBoxEdit.EditValue = treeList.GetCellValue(treeListView.FocusedRowHandle, (TreeListColumn)treeList.CurrentColumn);
            DialogClosedDelegate closedHandler = delegate (bool? dialogResult) {
                if (dialogResult == true) {
                    treeListView.ShowEditor();
                    treeListView.ActiveEditor.EditValue = listBoxEdit.EditValue;
                }
            };

            FloatingContainer.ShowDialogContent(listBoxEdit, treeListView.ActiveEditor, new Size(400, 300), new FloatingContainerParameters() { ClosedDelegate = closedHandler, Title = "Select Country", ContainerFocusable = false });
        }
        string lastDescription;
        void UpdateDescription() {
            if (descriptionRichTextBox == null)
                return;
            BlockCollection blocks =
           descriptionRichTextBox.Document.Blocks;
            if (treeListView.FocusedRowHandle == GridControl.InvalidRowHandle)
                return;
            string newDescription = list.FieldDescriptions[treeListView.FocusedRowHandle].TemplateName + "Description";
            if (newDescription == lastDescription)
                return;
            lastDescription = newDescription;
            ContentControl control = new ContentControl() { Template = Resources[newDescription] as ControlTemplate };
            control.ApplyTemplate();
            ParagraphContainer container = VisualTreeHelper.GetChild(control, 0) as ParagraphContainer;

            blocks.Clear();
            blocks.Add(container.Paragraph);


        }
        void TableView_ShowingEditor(object sender, DevExpress.Xpf.Grid.TreeList.TreeListShowingEditorEventArgs e) {
            e.Cancel = list.FieldDescriptions[treeListView.FocusedRowHandle].TemplateName == "ProgressBarEdit";
        }
        void CurrentItemChanged(object sender, CurrentItemChangedEventArgs e) {
            UpdateDescription();
        }
    }
    public class TreeListMultiEditorsList : MultiEditorsListBaseSQLite {
        protected override void CreateColumnCollection() {
            MultiEditorsListPropertyDescriptorSQLite[] pds = new MultiEditorsListPropertyDescriptorSQLite[Table.Count + 5];
            pds[0] = new MultiEditorsListPropertyDescriptorSQLite(this, 0, "EditorType", true);
            for (int n = 1; n < Table.Count + 1; n++)
                pds[n] = new MultiEditorsListPropertyDescriptorSQLite(this, n, "Product #" + n, false);

            pds[Table.Count + 1] = new MultiEditorsListPropertyDescriptorSQLite(this, Table.Count + 1, "Field", true);
            pds[Table.Count + 2] = new MultiEditorsListPropertyDescriptorSQLite(this, Table.Count + 2, "TemplateName", true);
            pds[Table.Count + 3] = new MultiEditorsListPropertyDescriptorSQLite(this, Table.Count + 3, "Id", true);
            pds[Table.Count + 4] = new MultiEditorsListPropertyDescriptorSQLite(this, Table.Count + 4, "ParentId", true);
            ColumnCollection = new PropertyDescriptorCollection(pds);
        }
        public override object GetPropertyValue(int rowIndex, int columnIndex) {
            if (columnIndex == 0)
                return FieldDescriptions[rowIndex].EditorDisplayName;
            if (columnIndex == Table.Count + 1)
                return FieldDescriptions[rowIndex].ColumnName;
            if (columnIndex == Table.Count + 2)
                return FieldDescriptions[rowIndex].TemplateName;
            if (columnIndex == Table.Count + 3)
                return FieldDescriptions[rowIndex].Id;
            if (columnIndex == Table.Count + 4)
                return FieldDescriptions[rowIndex].ParentId;
            return Table[columnIndex - 1][FieldDescriptions[rowIndex].ColumnName];
        }
    }
    public class TreeListMultiEditorsTemplateSelector : DataTemplateSelector {
        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            GridCellData data = (GridCellData)item;
            TreeListControl grid = (TreeListControl)data.View.DataControl;
            string editorType = grid.GetCellValue(data.RowData.RowHandle.Value, "TemplateName") as string;
            return string.IsNullOrEmpty(editorType) ? null : (DataTemplate)grid.Resources[editorType];
        }
    }
}

