using DevExpress.Mvvm.POCO;
using DevExpress.SalesDemo.Model;
using System;

namespace DevExpress.SalesDemo.Wpf.ViewModel {
    public class BarViewModel : DataViewModel {
        public static BarViewModel Create() {
            return ViewModelSource.Create(() => new BarViewModel());
        }
        protected BarViewModel() {
            PieArgumentDataMember = "GroupName";
            PieValueDataMember = "TotalCost";
            if(this.IsInDesignMode())
                OnInitializeInDesignMode();
        }

        public virtual object PieDataSource { get; protected set; }
        public virtual string PieArgumentDataMember { get; protected set; }
        public virtual string PieValueDataMember { get; protected set; }
        public void LoadData(object data) {
            PieDataSource = data;
        }
        void OnInitializeInDesignMode() {
            DateTime today = DateTime.Today;
            DateTime monthAgo = today.AddMonths(-1);
            int ThirdOfMonth = 10;
            var rangeStart = monthAgo;
            var rangeEnd = today;
            var selectedRangeStart = monthAgo.AddDays(ThirdOfMonth);
            var selectedRangeEnd = monthAgo.AddDays(2 * ThirdOfMonth);
            RequestData("PieDataSource", x => x.GetSalesBySector(selectedRangeStart, selectedRangeEnd, GroupingPeriod.All), x => PieDataSource = x);
        }
    }
}
