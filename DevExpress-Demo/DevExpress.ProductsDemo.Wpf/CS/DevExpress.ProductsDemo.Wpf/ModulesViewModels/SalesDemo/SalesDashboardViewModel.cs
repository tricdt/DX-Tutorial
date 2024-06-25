using DevExpress.Xpf.Charts;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using DevExpress.SalesDemo.Model;

namespace ProductsDemo {
    public class SalesDashboardViewModel : NavigationModule {
        const string SalesPerformanceArgumentDataMember = "StartOfPeriod";
        const string SalesPerformanceValueDataMember = "TotalCost";
        const string FirstPartOfFiscalYearHeder = "YEAR ";
        const string SalesVolumeForDayFormatString = "$#,#,0";
        const string SalesVolumeForWeekFormatString = "$#,#,0";
        const string SalesVolumeForMonthFormatString = "$#,#,0";
        const string SalesVolumeForYearFormatString = "$#,#,0";
        const string FirstPartOfSalesForecastDashboardGroupHeader = "SALES FORECAST ";

        DateTime selectedDay = DateTime.Now;
        DateTime selectedMonth = DateTime.Now;
        IEnumerable<SalesGroup> salesBySector;
        decimal ytdSalesVolume;
        decimal ytdSalesForecast;
        decimal lastYearSalesVolume;
        decimal annualSalesFirstRangeEnd;
        decimal annualSalesSecondRangeEnd;
        decimal annualSalesThirdRangeEnd;
        string lastYearHeder;
        string salesForecastDashboardGroupHeader;
        SalesPerformanceViewModel dailySalesPerformanceViewModel;
        SalesPerformanceViewModel monthlySalesPerformanceViewModel;

        public DateTime SelectedDay {
            get { return selectedDay; }
            private set { SetProperty(ref selectedDay, value, "SelectedDay", OnSelectedDayChanged); }
        }
        public DateTime SelectedMonth {
            get { return selectedMonth; }
            private set { SetProperty(ref selectedMonth, value, "SelectedMonth", OnSelectedMonthChanged); }
        }
        public decimal YTDSalesVolume {
            get { return ytdSalesVolume; }
            
            set { SetProperty(ref ytdSalesVolume, value, "YTDSalesVolume"); }
        }
        public decimal YTDSalesForecast {
            get { return ytdSalesForecast; }
            private set { SetProperty(ref ytdSalesForecast, value, "YTDSalesForecast"); }
        }
        public decimal LastYearSalesVolume {
            get { return lastYearSalesVolume; }
            private set { SetProperty(ref lastYearSalesVolume, value, "LastYearSalesVolume"); }
        }
        public decimal AnnualSalesFirstRangeEnd {
            get { return annualSalesFirstRangeEnd; }
            private set { SetProperty(ref annualSalesFirstRangeEnd, value, "AnnualSalesFirstRangeEnd"); }
        }
        public decimal AnnualSalesSecondRangeEnd {
            get { return annualSalesSecondRangeEnd; }
            private set { SetProperty(ref annualSalesSecondRangeEnd, value, "AnnualSalesSecondRangeEnd"); }
        }
        public decimal AnnualSalesThirdRangeEnd {
            get { return annualSalesThirdRangeEnd; }
            private set { SetProperty(ref annualSalesThirdRangeEnd, value, "AnnualSalesThirdRangeEnd"); }
        }
        public string LastYearHeader {
            get { return lastYearHeder; }
            private set { SetProperty(ref lastYearHeder, value, "LastYearHeader"); }
        }
        public string SalesForecastDashboardGroupHeader {
            get { return salesForecastDashboardGroupHeader; }
            private set { SetProperty(ref salesForecastDashboardGroupHeader, value, "SalesForecastDashboardGroupHeader"); }
        }
        public IEnumerable<SalesGroup> SalesBySector {
            get { return salesBySector; }
            private set { SetProperty(ref salesBySector, value, "SalesBySector"); }
        }
        public SalesPerformanceViewModel DailySalesPerformanceViewModel {
            get { return dailySalesPerformanceViewModel; }
            private set { SetProperty(ref dailySalesPerformanceViewModel, value, "DailySalesPerformanceViewModel"); }
        }
        public SalesPerformanceViewModel MonthlySalesPerformanceViewModel {
            get { return monthlySalesPerformanceViewModel; }
            private set { SetProperty(ref monthlySalesPerformanceViewModel, value, "MonthlySalesPerformanceViewModel"); }
        }
        public override string Caption { get { return "Sales"; } }
        public override string Description { get { return "Revenue" + Environment.NewLine + "Snapshots"; } }
        public override BitmapImage Glyph { get { return ResourceImageHelper.GetResourceImage("Sales.png"); } }

        protected override void SaveAndClearData() {
            SaveAndClearPropertyValue(ref lastYearSalesVolume, "LastYearSalesVolume", 0.0M);
            SaveAndClearPropertyValue(ref ytdSalesVolume, "YTDSalesVolume", 0.0M);
            SaveAndClearPropertyValue(ref ytdSalesForecast, "YTDSalesForecast", 0.0M);
            SaveAndClearPropertyValue(ref salesBySector, "SalesBySector");
            SavePropertyValue<object>(DailySalesPerformanceViewModel.ChartDataSource, "DailySalesPerformanceViewModel.ChartDataSource");
            DailySalesPerformanceViewModel.ChartDataSource = null;
            SavePropertyValue<object>(MonthlySalesPerformanceViewModel.ChartDataSource, "MonthlySalesPerformanceViewModel.ChartDataSource");
            MonthlySalesPerformanceViewModel.ChartDataSource = null;
        }
        protected override void RestoreData() {
            RestorePropertyValue(out lastYearSalesVolume, "LastYearSalesVolume", true);
            RestorePropertyValue(out ytdSalesVolume, "YTDSalesVolume", true);
            RestorePropertyValue(out ytdSalesForecast, "YTDSalesForecast", true);
            RestorePropertyValue(out salesBySector, "SalesBySector", true);
            object dailySalesPerformanceViewModelChartDataSource = null;
            RestorePropertyValue<object>(out dailySalesPerformanceViewModelChartDataSource, "DailySalesPerformanceViewModel.ChartDataSource", false);
            DailySalesPerformanceViewModel.ChartDataSource = dailySalesPerformanceViewModelChartDataSource;
            object monthlySalesPerformanceViewModelChartDataSource = null;
            RestorePropertyValue<object>(out monthlySalesPerformanceViewModelChartDataSource, "MonthlySalesPerformanceViewModel.ChartDataSource", false);
            MonthlySalesPerformanceViewModel.ChartDataSource = monthlySalesPerformanceViewModelChartDataSource;
        }
        protected override void InitializeData() {
            DateTimeRange currentYearRange = DateTimeUtils.GetYtdRange();
            YTDSalesVolume = DataProvider.GetTotalSalesByRange(currentYearRange.Start, currentYearRange.End).TotalCost;
            YTDSalesForecast = SalesForecastMaker.GetYtdForecast(YTDSalesVolume);
            DateTimeRange lastYearRange = DateTimeUtils.GetLastYearRange();
            LastYearSalesVolume = DataProvider.GetTotalSalesByRange(lastYearRange.Start, lastYearRange.End).TotalCost;
            LastYearHeader = FirstPartOfFiscalYearHeder + DateTimeUtils.GetLastYear();
            SalesForecastDashboardGroupHeader = FirstPartOfSalesForecastDashboardGroupHeader + "(" + DateTimeUtils.GetCurrentYear().ToString() + ")";
            SalesBySector = DataProvider.GetSalesBySector(new DateTime(), DateTime.Now, GroupingPeriod.All);
            DecimalRange badSalesRange = SalesRangeProvider.GetBadSalesRange();
            DecimalRange normalSalesRange = SalesRangeProvider.GetNormalSalesRange();
            DecimalRange goodSalesRange = SalesRangeProvider.GetGoodSalesRange();
            AnnualSalesFirstRangeEnd = badSalesRange.End;
            AnnualSalesSecondRangeEnd = normalSalesRange.End;
            AnnualSalesThirdRangeEnd = goodSalesRange.End;
            InitializeDailySalesPerformanceViewModel();
            InitializeMonthlySalesPerformanceViewModel();
        }
        void InitializeDailySalesPerformanceViewModel() {
            DailySalesPerformanceViewModel = new SalesPerformanceViewModel() {
                Mode = SalesPerformanceViewMode.Daily,
                AreaSeriesVisible = true,
                ArgumentDataMember = SalesPerformanceArgumentDataMember,
                ValueDataMember = SalesPerformanceValueDataMember,
                DateTimeMeasureUnit = DateTimeMeasureUnit.Hour,
                DateTimeGridAlignment = DateTimeGridAlignment.Hour,
                AxisXGridSpacing = 2,
                AxisXMinorCount = 1,
                AxisXLabelFormatString = "t",
                AreaSeriesCrosshairLabelPattern = "{A:t}:  ${V:###,###,###}"
            };
            DateTimeRange todayRange = DateTimeUtils.GetDayRange(DateTime.Now);
            SalesGroup todaySalesGroup = DataProvider.GetTotalSalesByRange(todayRange.Start, todayRange.End);
            DailySalesPerformanceViewModel.FirstSalesVolume = todaySalesGroup.TotalCost.ToString(SalesVolumeForDayFormatString);
            DateTimeRange yesterdayRange = DateTimeUtils.GetDayRange(DateTime.Now.AddDays(-1));
            SalesGroup yesterdaySalesGroup = DataProvider.GetTotalSalesByRange(yesterdayRange.Start, yesterdayRange.End);
            DailySalesPerformanceViewModel.SecondSalesVolume = yesterdaySalesGroup.TotalCost.ToString(SalesVolumeForDayFormatString);
            DateTimeRange lastWeekRange = DateTimeUtils.GetLastWeekRange();
            SalesGroup lastWeekSalesGroup = DataProvider.GetTotalSalesByRange(lastWeekRange.Start, lastWeekRange.End);
            DailySalesPerformanceViewModel.ThirdSalesVolume = lastWeekSalesGroup.TotalCost.ToString(SalesVolumeForWeekFormatString);
            DailySalesPerformanceViewModel.PropertyChanged += OnDailySalesPerformanceViewModelPropertyChanged;
            RequestDataForDaylySalesPerformanceChart();
        }
        void InitializeMonthlySalesPerformanceViewModel() {
            MonthlySalesPerformanceViewModel = new SalesPerformanceViewModel() {
                Mode = SalesPerformanceViewMode.Monthly,
                AreaSeriesVisible = true,
                ArgumentDataMember = SalesPerformanceArgumentDataMember,
                ValueDataMember = SalesPerformanceValueDataMember,
                DateTimeMeasureUnit = DateTimeMeasureUnit.Day,
                DateTimeGridAlignment = DateTimeGridAlignment.Day,
                AxisXGridSpacing = 3,
                AxisXMinorCount = 2,
                AxisXLabelFormatString = " d",
                AreaSeriesCrosshairLabelPattern = "{A:d}:  ${V:###,###,###}"
            };
            DateTimeRange thisMonthRange = DateTimeUtils.GetMonthRange(DateTime.Now);
            SalesGroup thisMonthSalesGroup = DataProvider.GetTotalSalesByRange(thisMonthRange.Start, thisMonthRange.End);
            MonthlySalesPerformanceViewModel.FirstSalesVolume = thisMonthSalesGroup.TotalCost.ToString(SalesVolumeForMonthFormatString);
            DateTimeRange lastMonthRange = DateTimeUtils.GetMonthRange(DateTime.Now.AddMonths(-1));
            SalesGroup lastMonthSalesGroup = DataProvider.GetTotalSalesByRange(lastMonthRange.Start, lastMonthRange.End);
            MonthlySalesPerformanceViewModel.SecondSalesVolume = lastMonthSalesGroup.TotalCost.ToString(SalesVolumeForMonthFormatString);
            MonthlySalesPerformanceViewModel.ThirdSalesVolume = YTDSalesVolume.ToString(SalesVolumeForYearFormatString);
            MonthlySalesPerformanceViewModel.PropertyChanged += OnMonthlySalesPerformanceViewModelPropertyChanged;
            RequestDataForMonthlySalesPerformanceChart();
        }
        void RequestDataForDaylySalesPerformanceChart() {
            DateTime start = new DateTime(SelectedDay.Year, SelectedDay.Month, SelectedDay.Day);
            DateTime end = start.AddDays(1);
            DailySalesPerformanceViewModel.ChartDataSource = DataProvider.GetSales(start, end, GroupingPeriod.Hour);
        }
        void RequestDataForMonthlySalesPerformanceChart() {
            DateTime start = new DateTime(SelectedMonth.Year, SelectedMonth.Month, 1);
            int daysInMonth = DateTime.DaysInMonth(SelectedMonth.Year, SelectedMonth.Month);
            DateTime end = new DateTime(SelectedMonth.Year, SelectedMonth.Month, daysInMonth);
            MonthlySalesPerformanceViewModel.ChartDataSource = DataProvider.GetSales(start, end, GroupingPeriod.Day);
        }
        void OnDailySalesPerformanceViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if(e.PropertyName == "SelectedDate")
                SelectedDay = DailySalesPerformanceViewModel.SelectedDate;
        }
        void OnMonthlySalesPerformanceViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if(e.PropertyName == "SelectedDate")
                SelectedMonth = MonthlySalesPerformanceViewModel.SelectedDate;
        }
        void OnSelectedDayChanged() {
            DateTimeRange day = DateTimeUtils.GetDayRange(SelectedDay);
            DailySalesPerformanceViewModel.ChartDataSource = DataProvider.GetSales(day.Start, day.End, GroupingPeriod.Hour);
        }
        void OnSelectedMonthChanged() {
            DateTimeRange month = DateTimeUtils.GetMonthRange(SelectedMonth);
            MonthlySalesPerformanceViewModel.ChartDataSource = DataProvider.GetSales(month.Start, month.End, GroupingPeriod.Day);
        }
    }
}
