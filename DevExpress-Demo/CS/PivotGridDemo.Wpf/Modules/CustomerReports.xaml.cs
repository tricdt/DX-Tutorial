using System.ComponentModel.DataAnnotations;

namespace PivotGridDemo {
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/CustomerReportsViewModel.(cs)")]
    public partial class CustomerReports : PivotGridDemoModule {
        public CustomerReports() {
            InitializeComponent();
        }
    }

    public enum CustomerReportType {
        Customers,
        [Display(Name = "Products (filtering)")]
        Products,
        [Display(Name = "Top 2 Products")]
        Top2Products,
        [Display(Name = "Top 10 Customers")]
        Top10Customers
    }

}
