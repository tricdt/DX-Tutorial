using DevExpress.Mvvm.POCO;

namespace DevExpress.SalesDemo.Wpf.ViewModel {
    public class RegionsViewModel : PageViewModel {
        public static RegionsViewModel Create() {
            return ViewModelSource.Create(() => new RegionsViewModel());
        }
        protected RegionsViewModel() { }

        public virtual PerformanceBarChartViewModel DailySalesByRegionViewModel { get; protected set; }
        public virtual PerformanceBarChartViewModel UnitSalesByRegionViewModel { get; protected set; }
        public virtual PieBarRangeViewModel PieGaugeRangeViewModel { get; protected set; }
        protected override void Initialize() {
            DailySalesByRegionViewModel = PerformanceBarChartViewModel.Create(Modules.Regions, PerformanceViewMode.Daily);
            UnitSalesByRegionViewModel = PerformanceBarChartViewModel.Create(Modules.Regions, PerformanceViewMode.Monthly);
            PieGaugeRangeViewModel = PieBarRangeViewModel.Create(Modules.Regions);
        }
    }
}
