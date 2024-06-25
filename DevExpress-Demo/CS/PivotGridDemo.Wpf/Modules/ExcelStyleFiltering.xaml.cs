using System.Windows;
using System.Windows.Controls;

namespace PivotGridDemo {
    public partial class ExcelStyleFiltering : PivotGridDemoModule {
        public ExcelStyleFiltering() {
            InitializeComponent();
            pivot.CollapseAllColumns();
        }
    }
}
