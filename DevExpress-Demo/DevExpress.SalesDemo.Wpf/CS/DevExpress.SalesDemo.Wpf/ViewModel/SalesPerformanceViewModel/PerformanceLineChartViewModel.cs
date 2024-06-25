using DevExpress.Mvvm.POCO;
using DevExpress.SalesDemo.Model;
using System;
using System.Collections.Generic;

namespace DevExpress.SalesDemo.Wpf.ViewModel {
    public class PerformanceLineChartViewModel : DataViewModel {
        public static PerformanceLineChartViewModel Create(string moduleType) {
            return ViewModelSource.Create(() => new PerformanceLineChartViewModel(moduleType));
        }
        protected string ModuleType { get; private set; }
        protected PerformanceLineChartViewModel()
            : this(Modules.Channels) {
        }
        protected PerformanceLineChartViewModel(string moduleType) {
            ModuleType = moduleType;
            SelectedDate = DateTime.Now;
        }
        protected virtual void RequestData() {
            if(ModuleType == Modules.Channels) {
                DateTimeRange todayRange = DateTimeUtils.GetDayRange(SelectedDate);
                RequestData("ChartDataSource", x => x.GetSalesByChannel(todayRange, GroupingPeriod.Hour), x => ChartDataSource = x);
                RequestData("TotalSalesBySector", x => x.GetSalesByChannel(todayRange, GroupingPeriod.All), x => {
                    TotalSalesBySector = x;
                    decimal total = 0M;
                    foreach(SalesGroup grp in TotalSalesBySector)
                        total += grp.TotalCost;
                    TotalSalesVolumeForDay = total;
                });
                return;
            }
            throw new NotImplementedException();
        }

        public PerformanceViewMode Mode { get { return PerformanceViewMode.Daily; } }
        public virtual object ChartDataSource { get; protected set; }
        public virtual DateTime SelectedDate { get; protected set; }
        public virtual string SelectedDateString { get; protected set; }
        public virtual IEnumerable<SalesGroup> TotalSalesBySector { get; protected set; }
        public virtual decimal TotalSalesVolumeForDay { get; protected set; }
        
        public void TimeForward() {
            SelectedDate = SelectedDate.AddDays(1);
        }
        public void TimeBackward() {
            SelectedDate = SelectedDate.AddDays(-1);
        }
        public bool CanTimeForward() {
            return !DateTimeUtils.IsToday(SelectedDate);
        }
        public void SetCurrentTimePeriod() {
            throw new NotImplementedException();
        }
        public void SetLastTimePeriod() {
            throw new NotImplementedException();
        }
        protected void OnSelectedDateChanged() {
            SelectedDateString = SelectedDate.ToString("D");
            RequestData();
        }
    }
}
