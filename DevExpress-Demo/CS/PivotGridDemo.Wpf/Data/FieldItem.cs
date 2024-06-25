using DevExpress.Xpf.PivotGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PivotGridDemo {
    public class FieldItem {
        public FieldItem() {
            ExpandedInFieldsGroup = true;
            SummaryType = FieldSummaryType.Sum;
            ValueFormat = null;
            CellFormat = null;
            GroupName = null;
            Width = double.NaN;
        }
        public string Name { get; set; }
        public DataBinding DataBinding { get; set; }
        public FieldArea Area { get; set; }
        public double Width { get; set; }
        public string Caption { get; set; }
        public string GroupName { get; set; }
        public bool ExpandedInFieldsGroup { get; set; }
        public string ValueFormat { get; set; }
        public FieldSummaryType SummaryType { get; set; }
        public string CellFormat { get; set; }
    }
}
