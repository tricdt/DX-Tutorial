using DevExpress.Diagram.Core;
using DevExpress.Diagram.Demos;
using DevExpress.Xpf.Diagram;

namespace DiagramDemo {
    public partial class ExpandAndCollapseOrgChartModule : DiagramDemoModule {
        public ExpandAndCollapseOrgChartModule() {
            InitializeComponent();
            orgChartBehavior.ItemsSource = EmployeesData.GetOrgChartEmployees();
        }
        void OnItemsGenerated(object sender, DiagramItemsGeneratedEventArgs e) {
            foreach(var item in e.GeneratedItems) {
                item.ThemeStyleId = OrgChartHelpers.GetStyleID(item);
            }
        }
    }
}
