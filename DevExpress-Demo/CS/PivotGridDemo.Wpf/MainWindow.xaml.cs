using System;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpf.DemoBase.Helpers;

namespace PivotGridDemo {
    public partial class MainWindow : SidebarWindow {
        public MainWindow() {
#if DXCORE3
            System.Data.Common.DbProviderFactories.RegisterFactory("Microsoft.Data.Sqlite", Microsoft.Data.Sqlite.SqliteFactory.Instance);
#endif
            InitializeComponent();
        }
        static MainWindow() {
            new TaskFactory().StartNew(() => DevExpress.DemoData.Models.NWindContext.Preload());
        }
    }
}
