using DevExpress.Mvvm.POCO;

namespace DevExpress.SalesDemo.Wpf.ViewModel {
    public class ChannelsViewModel : PageViewModel {
        public static ChannelsViewModel Create() {
            return ViewModelSource.Create(() => new ChannelsViewModel());
        }
        protected ChannelsViewModel() { }

        public virtual PerformanceLineChartViewModel PerformanceLineChartViewModel { get; protected set; }
        public virtual PieBarRangeViewModel PieGaugeRangeViewModel { get; protected set; }
        protected override void Initialize() {
            PerformanceLineChartViewModel = PerformanceLineChartViewModel.Create(Modules.Channels);
            PieGaugeRangeViewModel = PieBarRangeViewModel.Create(Modules.Channels);
        }
    }
}
