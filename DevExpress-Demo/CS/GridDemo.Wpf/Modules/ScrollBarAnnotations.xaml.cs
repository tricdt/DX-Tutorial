using DevExpress.Xpf.Grid;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace GridDemo {
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/ScrollBarAnnotationsModeConverter.(cs)")]
    public partial class ScrollBarAnnotations : GridDemoModule {    
        public ScrollBarAnnotations() {         
            InitializeComponent();
            listAnnotaions.SelectAll();          
        }

        #region Validation
        void view_ValidateCell(object sender, DevExpress.Xpf.Grid.GridCellValidationEventArgs e) {
            if(e.CellValue != null) {
                switch(e.Column.FieldName) {
                case "Quantity":
                    int quantity = int.Parse(e.CellValue.ToString());
                    if(quantity >= 100) {
                        e.IsValid = false;
                        e.SetError("Quantity is greater than or equals to 100");
                    }
                    break;
                case "Discount":
                    double discount = double.Parse(e.CellValue.ToString());
                    if(discount > 0.21) {
                        e.IsValid = false;
                        e.SetError("Discount is greater than 21%");
                    }
                    break;
                case "UnitPrice":
                    double val = double.Parse(e.CellValue.ToString());
                    if(val < 2.5) {
                        e.IsValid = false;
                        e.SetError("Unit Price is less than 2.5");
                    }
                    break;
                default:
                    return;
                }
            }
        }
        #endregion

        #region Custom Scroll Bar Annotation implementation
        HashSet<int> changedRows = new HashSet<int>();

        void view_ScrollBarAnnotationsCreating(object sender, ScrollBarAnnotationsCreatingEventArgs e) {
            ScrollBarAnnotationInfo info = new ScrollBarAnnotationInfo(Brushes.Green, ScrollBarAnnotationAlignment.Left, 4, 3);
            e.CustomScrollBarAnnotations = new List<ScrollBarAnnotationRowInfo>(changedRows.Select(x => new ScrollBarAnnotationRowInfo(grid.GetRowHandleByListIndex(x), info)));
        }

        void view_RowUpdated(object sender, RowEventArgs e) {
            changedRows.Add(grid.GetListIndexByRowHandle(e.RowHandle));          
        }
        #endregion      
    }
}
