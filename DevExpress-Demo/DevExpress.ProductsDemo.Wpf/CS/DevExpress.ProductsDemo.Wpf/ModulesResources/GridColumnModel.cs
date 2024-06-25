using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.Mvvm;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using DevExpress.Xpf.Grid;

namespace ProductsDemo.Modules {
    public class GridColumnModel : BindableBase {
        public string Name { get; set; }
        public string DisplayName { get; set; }

        bool isGrouped;
        public bool IsGrouped {
            get { return isGrouped; }
            set {
                if(isGrouped == value)
                    return;
                isGrouped = value;
                RaisePropertiesChanged("IsGrouped");
            }
        }

        GridColumnWidth width = Double.NaN;
        public GridColumnWidth Width {
            get { return width; }
            set {
                width = value;
                RaisePropertiesChanged("Width");
            }
        }
        public System.Windows.HorizontalAlignment HorizontalHeaderContentAlignment { get; set; }
        DefaultBoolean allowFiltering = DefaultBoolean.True;
        public DefaultBoolean AllowFiltering {
            get { return allowFiltering; }
            set { allowFiltering = value; }
        }
        DefaultBoolean allowSorting = DefaultBoolean.True;
        public DefaultBoolean AllowSorting {
            get { return allowSorting; }
            set { allowSorting = value; }
        }
        public DefaultBoolean AllowEditing { get; set; }
        public ColumnGroupInterval GroupInterval { get; set; }
        public bool FixedWidth { get; set; }
        public string Mask { get; set; }

        ColumnSortOrder sortOrder;
        public ColumnSortOrder SortOrder { 
            get { return sortOrder; }
            set {
                sortOrder = value;
                RaisePropertiesChanged("SortOrder");
            }
        }

        int index;
        public int Index {
            get { return index; }
            set {
                index = value;
                RaisePropertiesChanged("Index");
            }
        }

        bool isVisible = true;
        public bool IsVisible {
            get { return isVisible; }
            set {
                isVisible = value;
                RaisePropertiesChanged("IsVisible");
            }
        }
    }
}
