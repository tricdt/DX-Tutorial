using System.Collections.Generic;
using DevExpress.Data.Filtering;
using DevExpress.Xpf.Grid;

namespace GridDemo {
    public partial class ExcelStyleFiltering : GridDemoModule {
        public ExcelStyleFiltering() {
            InitializeComponent();
            grid.ItemsSource = OrderDataGenerator.GenerateVehicleOrders(10000);
        }
    }
}
