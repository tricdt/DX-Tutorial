using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PivotGridDemo {
    public enum FormatConditionType {
        DataBar
    }
    public class FormatConditionItem {
        public FormatConditionType Type { get; set; }
        public string MeasureName { get; set; }
        public string RowName { get; set; }
        public string ColumnName { get; set; }
        public string PredefinedFormatName { get; set; }
    }
}
