using DevExpress.Mvvm.POCO;

namespace DevExpress.SalesDemo.Wpf.ViewModel {
    public class SalesDashboardViewModel : PageViewModel {
        public static SalesDashboardViewModel Create() {
            return ViewModelSource.Create(() => new SalesDashboardViewModel());
        }
        protected SalesDashboardViewModel() { }

        public virtual SalesSummaryViewModel SalesSummaryViewModel { get; protected set; }
        public virtual PerformanceAreaChartViewModel DailySalesPerformanceViewModel { get; protected set; }
        public virtual PerformanceAreaChartViewModel MonthlySalesPerformanceViewModel { get; protected set; }
        protected override void Initialize() {
            SalesSummaryViewModel = SalesSummaryViewModel.Create();
            DailySalesPerformanceViewModel = PerformanceAreaChartViewModel.Create(Modules.Dashboard, PerformanceViewMode.Daily);
            MonthlySalesPerformanceViewModel = PerformanceAreaChartViewModel.Create(Modules.Dashboard, PerformanceViewMode.Monthly);
        }
    }
}
