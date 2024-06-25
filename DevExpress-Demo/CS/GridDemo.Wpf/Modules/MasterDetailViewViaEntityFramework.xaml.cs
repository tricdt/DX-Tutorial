using DevExpress.DemoData.Models;
#if DXCORE3
using Microsoft.EntityFrameworkCore;
#else
using System.Data.Entity;
#endif

namespace GridDemo {
    public partial class MasterDetailViewViaEntityFramework : GridDemoModule {
        public MasterDetailViewViaEntityFramework() {
            InitializeComponent();
            var customers = new NWindContext().Customers;
            customers.Load();
            grid.ItemsSource = customers.Local;
        }
    }
}
