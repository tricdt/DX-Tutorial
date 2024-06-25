using DevExpress.Mvvm.POCO;
using DevExpress.SalesDemo.Model;
using System;

namespace DevExpress.SalesDemo.Wpf.ViewModel {
    public class PieBarRangeViewModel : DataViewModel {
        public static PieBarRangeViewModel Create(string moduleType) {
            return ViewModelSource.Create(() => new PieBarRangeViewModel(moduleType));
        }
        protected string ModuleType { get; private set; }
        protected PieBarRangeViewModel()
            : this(Modules.Regions) {
        }
        protected PieBarRangeViewModel(string moduleType) {
            ModuleType = moduleType;

            BarViewModel = BarViewModel.Create();
            PieViewModel = PieViewModel.Create();
            RangeViewModel = RangeViewModel.Create();
            RangeViewModel.RangeChanged += OnRangeViewModelRangeChanged;
            RangeViewModel.SelectedRangeChanged += OnRangeViewModelSelectedRangeChanged;

            RequestDataForRange();
            RequestDataForPieAndBar();
        }
        public virtual RangeViewModel RangeViewModel { get; protected set; }
        public virtual BarViewModel BarViewModel { get; protected set; }
        public virtual PieViewModel PieViewModel { get; protected set; }

        void OnRangeViewModelRangeChanged(object sender, EventArgs e) {
            RequestDataForRange();
        }
        void OnRangeViewModelSelectedRangeChanged(object sender, EventArgs e) {
            RequestDataForPieAndBar();
        }
        void RequestDataForRange() {
            if(RangeViewModel.RangeStart == null || RangeViewModel.RangeEnd == null || RangeViewModel.RangeStart >= RangeViewModel.RangeEnd)
                return;
            RequestData("RangeViewModel", x => x.GetSales(RangeViewModel.RangeStart.Value, RangeViewModel.RangeEnd.Value.AddDays(1), GroupingPeriod.Day), x => RangeViewModel.LoadData(x));
        }
        void RequestDataForPieAndBar() {
            var range = RangeViewModel.GetSelectedRange();
            if(range == null) return;
            RequestData("Data", x => {
                if(ModuleType == Modules.Regions)
                    return x.GetSalesByRegion(range.Value, GroupingPeriod.All);
                else if(ModuleType == Modules.Sectors)
                    return x.GetSalesBySector(range.Value, GroupingPeriod.All);
                else if(ModuleType == Modules.Products)
                    return x.GetSalesByProduct(range.Value, GroupingPeriod.All);
                else if(ModuleType == Modules.Channels)
                    return x.GetSalesByChannel(range.Value, GroupingPeriod.All);
                throw new NotImplementedException();
            }, x => {
                BarViewModel.LoadData(x);
                PieViewModel.LoadData(x);
            });
        }
    }
}
