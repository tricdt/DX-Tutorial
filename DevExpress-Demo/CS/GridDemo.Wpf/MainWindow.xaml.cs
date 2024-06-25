using System;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.DemoData.Models;
using DevExpress.DemoData.Models.Vehicles;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.Helpers;

namespace GridDemo {
    public partial class MainWindow : SidebarWindow {
        public MainWindow() {
            InitializeComponent();
            SetValue(ScrollBarExtensions.AllowMiddleMouseScrollingProperty, true);
#if DXCORE3
            System.Data.Common.DbProviderFactories.RegisterFactory("Microsoft.Data.Sqlite", Microsoft.Data.Sqlite.SqliteFactory.Instance);
#endif

        }
        static MainWindow() {
            new TaskFactory().StartNew(() => DevExpress.DemoData.Models.NWindContext.Preload())
                    .ContinueWith(t => DevExpress.DemoData.Models.Vehicles.VehiclesContext.Preload(), TaskContinuationOptions.ExecuteSynchronously);
        }
    }
}
