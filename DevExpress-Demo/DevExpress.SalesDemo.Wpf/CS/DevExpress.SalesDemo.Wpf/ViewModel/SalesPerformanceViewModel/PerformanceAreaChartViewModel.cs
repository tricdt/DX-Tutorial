using DevExpress.Mvvm.POCO;
using DevExpress.SalesDemo.Model;
using System;

namespace DevExpress.SalesDemo.Wpf.ViewModel {
    public class PerformanceAreaChartViewModel : DataViewModel {
        public static PerformanceAreaChartViewModel Create(string moduleType, PerformanceViewMode mode) {
            return ViewModelSource.Create(() => new PerformanceAreaChartViewModel(moduleType, mode));
        }
        protected string ModuleType { get; private set; }
        protected PerformanceAreaChartViewModel()
            : this(Modules.Dashboard, PerformanceViewMode.Daily) {
        }
        protected PerformanceAreaChartViewModel(string moduleType, PerformanceViewMode mode) {
            ValidateMode(moduleType, mode);
            ModuleType = moduleType;
            Mode = mode;
            if(Mode == PerformanceViewMode.Daily)
                InitializeInDailyMode();
            else InitializeInMonthlyMode();
            SelectedDate = DateTime.Now;
            UpdateVolumeLables();
        }
        protected virtual void ValidateMode(string moduleType, PerformanceViewMode mode) {
            if(moduleType != Modules.Dashboard)
                throw new NotImplementedException();
        }
        protected virtual void InitializeInDailyMode() {
            ArgumentDataMember = "StartOfPeriod";
            ValueDataMember = "TotalCost";
        }
        protected virtual void InitializeInMonthlyMode() {
            InitializeInDailyMode();
        }
        protected virtual void RequestData() {
            DateTimeRange range = Mode == PerformanceViewMode.Daily ? DateTimeUtils.GetDayRange(SelectedDate) : DateTimeUtils.GetMonthRange(SelectedDate);
            GroupingPeriod groupingPeriod = Mode == PerformanceViewMode.Daily ? GroupingPeriod.Hour : GroupingPeriod.Day;
            RequestData("ChartDataSource", x => x.GetSales(range, groupingPeriod), x => ChartDataSource = x);
        }
        protected virtual void UpdateVolumeLables() {
            Action<string, DateTimeRange, Action<SalesGroup>> requestDataCore = (id, range, callback) => {
                RequestData(id, x => x.GetTotalSalesByRange(range), callback);
            };
            if(Mode == PerformanceViewMode.Daily) {
                requestDataCore("FirstSalesVolume", DateTimeUtils.GetTodayRange(), x => FirstSalesVolume = GetTotalCost(x));
                requestDataCore("SecondSalesVolume", DateTimeUtils.GetYesterdayRange(), x => SecondSalesVolume = GetTotalCost(x));
                requestDataCore("ThirdSalesVolume", DateTimeUtils.GetLastWeekRange(), x => ThirdSalesVolume = GetTotalCost(x));
            } else {
                requestDataCore("FirstSalesVolume", DateTimeUtils.GetThidMonthRange(), x => FirstSalesVolume = GetTotalCost(x));
                requestDataCore("SecondSalesVolume", DateTimeUtils.GetLastMonthRange(), x => SecondSalesVolume = GetTotalCost(x));
                requestDataCore("ThirdSalesVolume", DateTimeUtils.GetYtdRange(), x => ThirdSalesVolume = GetTotalCost(x));
            }
        }
        string GetTotalCost(SalesGroup group) {
            return group.TotalCost.ToString("$#,#,0");
        }

        public PerformanceViewMode Mode { get; private set; }
        public virtual object ChartDataSource { get; protected set; }
        public virtual string ArgumentDataMember { get; protected set; }
        public virtual string ValueDataMember { get; protected set; }
        public virtual string FirstSalesVolume { get; protected set; }
        public virtual string SecondSalesVolume { get; protected set; }
        public virtual string ThirdSalesVolume { get; protected set; }
        public virtual DateTime SelectedDate { get; protected set; }
        public virtual string SelectedDateString { get; protected set; }

        public void TimeForward() {
            SelectedDate = Mode == PerformanceViewMode.Daily ? SelectedDate.AddDays(1) : SelectedDate.AddMonths(1);
        }
        public void TimeBackward() {
            SelectedDate = Mode == PerformanceViewMode.Daily ? SelectedDate.AddDays(-1) : SelectedDate.AddMonths(-1);
        }
        public bool CanTimeForward() {
            return Mode == PerformanceViewMode.Daily ? !DateTimeUtils.IsToday(SelectedDate) : !DateTimeUtils.IsCurrentMonth(SelectedDate);
        }
        public void SetCurrentTimePeriod() {
            SelectedDate = DateTime.Now;
        }
        public void SetLastTimePeriod() {
            SelectedDate = Mode == PerformanceViewMode.Daily ? DateTime.Now.AddDays(-1) : DateTime.Now.AddMonths(-1);
        }
        protected void OnSelectedDateChanged() {
            string fomatString = Mode == PerformanceViewMode.Daily ? "d" : "MMMM, yyyy";
            SelectedDateString = SelectedDate.ToString(fomatString);
            RequestData();
        }
    }
}
