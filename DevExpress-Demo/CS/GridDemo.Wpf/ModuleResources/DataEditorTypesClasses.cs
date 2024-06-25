using DevExpress.Xpf.DemoBase.DemosHelpers.Grid;
using DevExpress.Xpf.Grid;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace GridDemo {
    public class MultiEditorsTemplateSelector : DataTemplateSelector {
        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            GridCellData data = (GridCellData)item;
            GridControl grid = ((GridViewBase)data.View).Grid;
            string editorType = grid.GetCellValue(data.RowData.RowHandle.Value, "TemplateName") as string;
            return string.IsNullOrEmpty(editorType) ? null : (DataTemplate)grid.Resources[editorType];
        }
    }
    public class MultiEditorsList : MultiEditorsListBaseSQLite {
        protected override void CreateColumnCollection() {
            var pds = new MultiEditorsListPropertyDescriptorSQLite[Table.Count + 3];
            pds[0] = new MultiEditorsListPropertyDescriptorSQLite(this, 0, "Field", true);
            for(int n = 1; n < Table.Count + 1; n++) {
                pds[n] = new MultiEditorsListPropertyDescriptorSQLite(this, n, "Product #" + n, false);
            }
            pds[Table.Count + 1] = new MultiEditorsListPropertyDescriptorSQLite(this, Table.Count + 1, "EditorType", true);
            pds[Table.Count + 2] = new MultiEditorsListPropertyDescriptorSQLite(this, Table.Count + 2, "TemplateName", true);
            ColumnCollection = new PropertyDescriptorCollection(pds);
        }
        public override object GetPropertyValue(int rowIndex, int columnIndex) {
            if(columnIndex == 0)
                return FieldDescriptions[rowIndex].ColumnName;
            if(columnIndex == Table.Count + 1)
                return FieldDescriptions[rowIndex].EditorDisplayName;
            if(columnIndex == Table.Count + 2)
                return FieldDescriptions[rowIndex].TemplateName;
            return Table[columnIndex - 1][FieldDescriptions[rowIndex].ColumnName];
        }
    }
}
