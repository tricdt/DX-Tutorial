using DevExpress.Mvvm.POCO;

namespace DevExpress.SalesDemo.Wpf.ViewModel {
    public class ProductsViewModel : PageViewModel {
        public static ProductsViewModel Create() {
            return ViewModelSource.Create(() => new ProductsViewModel());
        }
        protected ProductsViewModel() { }

        public virtual PerformanceBarChartViewModel DailySalesByProductViewModel { get; protected set; }
        public virtual PerformanceBarChartViewModel UnitSalesByProductViewModel { get; protected set; }
        public virtual PieBarRangeViewModel PieGaugeRangeViewModel { get; protected set; }
        protected override void Initialize() {
            DailySalesByProductViewModel = PerformanceBarChartViewModel.Create(Modules.Products, PerformanceViewMode.Daily);
            UnitSalesByProductViewModel = PerformanceBarChartViewModel.Create(Modules.Products, PerformanceViewMode.Monthly);
            PieGaugeRangeViewModel = PieBarRangeViewModel.Create(Modules.Products);
        }
    }
}
