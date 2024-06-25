using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.DemosHelpers.Grid;
using DevExpress.Xpf.Editors;
using System.ComponentModel.DataAnnotations;
using DevExpress.Data.Helpers;
using System.Windows.Data;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.DemoData.Models;

namespace PropertyGridDemo {
    public class MultiEditorsList : MultiEditorsListBaseSQLite {
        public MultiEditorsList() {
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
            for(int i = 0; i < rows.Count; i++) {
                var dict = Table[i];
                dict["Category1"] = rows[i].CategoryID;
                dict["Category2"] = rows[i].CategoryID;
                dict["Brush"] = new SolidColorBrush((Color)dict["Color"]);
                dict["Date1"] = DateTime.Today;
                dict["Date2"] = DateTime.Today;
                dict["Discontinued2"] = true;
            }
        }
    }
}
