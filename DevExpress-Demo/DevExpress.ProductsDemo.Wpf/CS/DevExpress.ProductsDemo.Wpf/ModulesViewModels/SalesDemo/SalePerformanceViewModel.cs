using DevExpress.Xpf.Charts;
using DevExpress.Mvvm;
using System;
using System.Windows.Input;
using DevExpress.SalesDemo.Model;

namespace ProductsDemo {
    public enum SalesPerformanceViewMode {
        Daily,
        Monthly
    }

    public class SalesPerformanceViewModel : ViewModelBase {
        SalesPerformanceViewMode mode;
        DateTime selectedDate = DateTime.Now;
        object chartDataSource;
        string selectedDateString;
        string argumentDataMember;
        string valueDataMember;
        string secondButtonText;
        string thirdButtonText;
        string firstSalesVolumeHeader;
        string secondSalesVolumeHeader;
        string thirdSalesVolumeHeader;
        string firstSalesVolume;
        string secondSalesVolume;
        string thirdSalesVolume;
        string axisXLabelFormatString;
        string areaSeriesCrosshairLabelPattern;
        bool areaSeriesVisible;
        bool barSeriesVisible;
        bool chartSideMarginsEnabled;
        DateTimeMeasureUnit dateTimeMeasureUnit;
        DateTimeGridAlignment dateTimeGridAlignment;
        double gridSpacing;
        int axisXMinorCount;
        ICommand timeForwardCommand;
        ICommand timeBackwardCommand;
        ICommand setCurrentTimePeriodCommand;
        ICommand setLastTimePeriodCommand;

        public SalesPerformanceViewMode Mode {
            get { return mode; }
            set { SetProperty(ref mode, value, "Mode", OnModePropertyChanged); }
        }
        public DateTime SelectedDate {
            get { return selectedDate; }
            set { SetProperty(ref selectedDate, value, "SelectedDate", OnSelectedDatePropertyChanged); }
        }
        public string SelectedDateString {
            get { return selectedDateString; }
            private set { SetProperty(ref selectedDateString, value, "SelectedDateString"); }
        }
        public object ChartDataSource {
            get { return chartDataSource; }
            set { SetProperty(ref chartDataSource, value, "ChartDataSource"); }
        }
        public string ArgumentDataMember {
            get { return argumentDataMember; }
            set { SetProperty(ref argumentDataMember, value, "ArgumentDataMember"); }
        }
        public string ValueDataMember {
            get { return valueDataMember; }
            set { SetProperty(ref valueDataMember, value, "ValueDataMember"); }
        }
        public string SecondButtonText {
            get { return secondButtonText; }
            private set { SetProperty(ref secondButtonText, value, "SecondButtonText"); }
        }
        public string ThirdButtonText {
            get { return thirdButtonText; }
            private set { SetProperty(ref thirdButtonText, value, "ThirdButtonText"); }
        }
        public string FirstSalesVolumeHeader {
            get { return firstSalesVolumeHeader; }
            private set { SetProperty(ref firstSalesVolumeHeader, value, "FirstSalesVolumeHeader"); }
        }
        public string SecondSalesVolumeHeader {
            get { return secondSalesVolumeHeader; }
            private set { SetProperty(ref secondSalesVolumeHeader, value, "SecondSalesVolumeHeader"); }
        }
        public string ThirdSalesVolumeHeader {
            get { return thirdSalesVolumeHeader; }
            private set { SetProperty(ref thirdSalesVolumeHeader, value, "ThirdSalesVolumeHeader"); }
        }
        public string FirstSalesVolume {
            get { return firstSalesVolume; }
            set { SetProperty(ref firstSalesVolume, value, "FirstSalesVolume"); }
        }
        public string SecondSalesVolume {
            get { return secondSalesVolume; }
            set { SetProperty(ref secondSalesVolume, value, "SecondSalesVolume"); }
        }
        public string ThirdSalesVolume {
            get { return thirdSalesVolume; }
            set { SetProperty(ref thirdSalesVolume, value, "ThirdSalesVolume"); }
        }
        public string AxisXLabelFormatString {
            get { return axisXLabelFormatString; }
            set { SetProperty(ref axisXLabelFormatString, value, "AxisXLabelFormatString"); }
        }
        public string AreaSeriesCrosshairLabelPattern {
            get { return areaSeriesCrosshairLabelPattern; }
            set { SetProperty(ref areaSeriesCrosshairLabelPattern, value, "AreaSeriesCrosshairLabelPattern"); }
        }
        public bool AreaSeriesVisible {
            get { return areaSeriesVisible; }
            set { SetProperty(ref areaSeriesVisible, value, "AreaSeriesVisible", OnSeriesVisibilityChanged); }
        }
        public bool BarSeriesVisible {
            get { return barSeriesVisible; }
            set { SetProperty(ref barSeriesVisible, value, "BarSeriesVisible", OnSeriesVisibilityChanged); }
        }
        public bool ChartSideMarginsEnabled {
            get { return chartSideMarginsEnabled; }
            set { SetProperty(ref chartSideMarginsEnabled, value, "ChartSideMarginsEnabled"); }
        }
        public DateTimeMeasureUnit DateTimeMeasureUnit {
            get { return dateTimeMeasureUnit; }
            set { SetProperty(ref dateTimeMeasureUnit, value, "DateTimeMeasureUnit"); }
        }
        public DateTimeGridAlignment DateTimeGridAlignment {
            get { return dateTimeGridAlignment; }
            set { SetProperty(ref dateTimeGridAlignment, value, "DateTimeGridAlignment"); }
        }
        public double AxisXGridSpacing {
            get { return gridSpacing; }
            set { SetProperty(ref gridSpacing, value, "AxisXGridSpacing"); }
        }
        public int AxisXMinorCount {
            get { return axisXMinorCount; }
            set { SetProperty(ref axisXMinorCount, value, "AxisXMinorCount"); }
        }
        public ICommand TimeForwardCommand {
            get { return timeForwardCommand; }
            private set { SetProperty(ref timeForwardCommand, value, "TimeForwardCommand"); }
        }
        public ICommand TimeBackwardCommand {
            get { return timeBackwardCommand; }
            private set { SetProperty(ref timeBackwardCommand, value, "TimeBackwardCommand"); }
        }
        public ICommand SetCurrentTimePeriodCommand {
            get { return setCurrentTimePeriodCommand; }
            set { SetProperty(ref setCurrentTimePeriodCommand, value, "SetCurrentTimePeriodCommand"); }
        }
        public ICommand SetLastTimePeriodCommand {
            get { return setLastTimePeriodCommand; }
            set { SetProperty(ref setLastTimePeriodCommand, value, "SetLastTimePeriodCommand"); }
        }

        public SalesPerformanceViewModel() {
            OnModePropertyChanged();
            TimeForwardCommand = new DelegateCommand(OnTimeForwardCommandExecuted, CanExecuteTimeForward);
            TimeBackwardCommand = new DelegateCommand(OnTimeBackwardCommandExecuted);
            SetCurrentTimePeriodCommand = new DelegateCommand(OnSetCurrentTimePeriodCommandExecuted);
            SetLastTimePeriodCommand = new DelegateCommand(OnSetLastTimePeriodCommandExecuted);
        }

        void OnModePropertyChanged() {
            SecondButtonText = Mode == SalesPerformanceViewMode.Daily ? "Yesterday" : "Last Month";
            ThirdButtonText = Mode == SalesPerformanceViewMode.Daily ? "Today" : "This Month";
            FirstSalesVolumeHeader = Mode == SalesPerformanceViewMode.Daily ? "TODAY" : "THIS MONTH";
            SecondSalesVolumeHeader = Mode == SalesPerformanceViewMode.Daily ? "YESTERDAY" : "LAST MONTH";
            ThirdSalesVolumeHeader = Mode == SalesPerformanceViewMode.Daily ? "LAST WEEK" : "YTD";
            OnSelectedDatePropertyChanged();
        }
        void OnSelectedDatePropertyChanged() {
            string fomatString = Mode == SalesPerformanceViewMode.Daily ? "d" : "MMMM, yyyy";
            SelectedDateString = SelectedDate.ToString(fomatString);
        }
        void OnSeriesVisibilityChanged() {
            if(AreaSeriesVisible && !BarSeriesVisible)
                ChartSideMarginsEnabled = false;
            else
                ChartSideMarginsEnabled = true;
        }
        bool IsCurrentMonth(DateTime date) {
            DateTime now = DateTime.Now;
            return now.Year == date.Year && now.Month == date.Month;
        }
        bool IsToday(DateTime date) {
            DateTime now = DateTime.Now;
            return now.Year == date.Year && now.Month == date.Month && now.Day == date.Day;
        }
        bool CanExecuteTimeForward() {
            if(Mode == SalesPerformanceViewMode.Daily)
                return !IsToday(SelectedDate);
            else
                return !IsCurrentMonth(SelectedDate);
        }
        void OnTimeForwardCommandExecuted() {
            if(Mode == SalesPerformanceViewMode.Daily)
                SelectedDate = SelectedDate.AddDays(1);
            else
                SelectedDate = SelectedDate.AddMonths(1);
        }
        void OnTimeBackwardCommandExecuted() {
            if(Mode == SalesPerformanceViewMode.Daily)
                SelectedDate = SelectedDate.AddDays(-1);
            else
                SelectedDate = SelectedDate.AddMonths(-1);
        }
        void OnSetCurrentTimePeriodCommandExecuted() {
            SelectedDate = DateTime.Now;
        }
        void OnSetLastTimePeriodCommandExecuted() {
            if(Mode == SalesPerformanceViewMode.Daily)
                SelectedDate = DateTime.Now.AddDays(-1);
            else
                SelectedDate = DateTime.Now.AddMonths(-1);
        }

        protected override void OnInitializeInDesignMode() {
            base.OnInitializeInDesignMode();
            OnModePropertyChanged();
            SelectedDate = DateTime.Now;
            IDataProvider dataProvider = new SampleDataProvider();
            FirstSalesVolume = "1.1M";
            SecondSalesVolume = "2.2M";
            ThirdSalesVolume = "12.3M";
            ValueDataMember = "TotalCost";
            ArgumentDataMember = "StartOfPeriod";
            ChartDataSource = dataProvider.GetSales(DateTime.Today.AddMonths(-1), DateTime.Today, GroupingPeriod.Day);
            AreaSeriesVisible = true;
        }
    }
}
