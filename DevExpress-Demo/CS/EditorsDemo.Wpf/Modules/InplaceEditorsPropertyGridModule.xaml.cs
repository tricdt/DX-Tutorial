using System;
using System.ComponentModel;
using System.Windows.Media;
using DevExpress.Data.Helpers;
using DevExpress.DemoData.Models;
using DevExpress.Xpf.DemoBase;
using System.Linq;

namespace EditorsDemo {
    [CodeFile("ModuleResources/DynamicallyAssignDataEditorsResources.xaml")]
    public partial class InplaceEditorsPropertyGridModule : EditorsDemoModule {
        public InplaceEditorsPropertyGridModule() {
            InitializeComponent();
        }
    }
    public class PropertyGridMultiEditorsList : MultiEditorsListBaseSQLite {
        public PropertyGridMultiEditorsList() {
        }
        public object GetValue(string propertyName) {
            return Table[0][MasterDetailHelper.SplitPascalCaseString(propertyName)];
        }

        public object this[int index] { get { return Table[0]; } }
        protected override void CreateColumnCollection() {
            var pds = new MultiEditorsListPropertyDescriptorSQLite[1];
            pds[0] = new MultiEditorsListPropertyDescriptorSQLite(this, 0, "Product #" + 0, false);
            ColumnCollection = new PropertyDescriptorCollection(pds);
        }
        public override object GetPropertyValue(int rowIndex, int columnIndex) {
            throw new NotSupportedException();
        }
        protected override void CreateDataTable() {
            base.CreateDataTable();
            var rows = NWindContext.Create().Products.ToList();
            for (int i = 0; i < rows.Count; i++) {
                var dict = Table[i];
                dict["Category1"] = rows[i].CategoryID;
                dict["Category2"] = rows[i].CategoryID;
                dict["Brush"] = new SolidColorBrush((Color)dict["Color"]);
                dict["Date1"] = DateTime.Today;
                dict["Date2"] = DateTime.Today;
                dict["Discontinued2"] = true;
                dict["Product"] = rows[i].ProductName;
            }
        }
    }
}
