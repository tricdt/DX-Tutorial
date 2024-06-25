using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Threading;
using DevExpress.Mvvm;

namespace GaugesDemo {
    public class WorldTimeViewModel {
        public IEnumerable<HourLabelData> RomanLabels { get; private set; }
        public virtual bool ShowLabels { get; set; }
        public virtual bool ShowCustomLabels { get; set; }
        public virtual bool ShowRegularLabels { get; protected set; }
        public virtual DateTime NewYorkTime { get; protected set; }
        public virtual DateTime LondonTime { get; protected set; }
        public virtual DateTime MoscowTime { get; protected set; }

        readonly DispatcherTimer timer = new DispatcherTimer();

        protected WorldTimeViewModel() {
            RomanLabels = Enumerable.Range(1, 12)
                .Select(x => new HourLabelData(x))
                .ToArray();
            ShowLabels = true;
            UpdateTime();
            
            timer.Interval = TimeSpan.FromSeconds(1);
        }
        protected void OnShowLabelsChanged() {
            UpdateLabelsVisiblity();
        }
        protected void OnShowCustomLabelsChanged() {
            UpdateLabelsVisiblity();
        }
        void UpdateLabelsVisiblity() {
            foreach(HourLabelData labelData in RomanLabels) {
                labelData.Visible = ShowLabels && ShowCustomLabels;
            }
            ShowRegularLabels = ShowLabels && !ShowCustomLabels;
        }

        void OnTimedEvent(object source, EventArgs e) {
            UpdateTime();
        }
        DateTime ConvertToTimeZone(DateTime utcTime, string timeZone) {
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, TimeZoneInfo.FindSystemTimeZoneById(timeZone));
        }
        void UpdateTime() {
            DateTime nowUtc = DateTime.UtcNow;
            NewYorkTime = ConvertToTimeZone(nowUtc, "Eastern Standard Time");
            LondonTime = ConvertToTimeZone(nowUtc, "GMT Standard Time");
            MoscowTime = ConvertToTimeZone(nowUtc, "Russian Standard Time");
        }
        public void Start() {
            timer.Tick += OnTimedEvent;
            timer.Start();
        }
        public void Stop() {
            timer.Stop();
            timer.Tick -= OnTimedEvent;
        }
    }

    public class HourLabelData : BindableBase {
        public int Hour { get; private set; }
        public string RomanHour { get; private set; }
        public bool Visible {
            get { return GetProperty(() => Visible); }
            set { SetProperty(() => Visible, value); }
        }

        public HourLabelData(int hour) {
            Hour = hour;
            RomanHour = romanHours[hour - 1];
        }
        static string[] romanHours = new string[] { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII" };
    }

    public abstract class DateTimeToUnitConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        protected abstract double ConvertCore(DateTime dateTime);

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value is DateTime)
                return ConvertCore((DateTime)value);
            return null;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class DateTimeToSecondConverter : DateTimeToUnitConverter {
        protected override double ConvertCore(DateTime dateTime) { return (double)dateTime.Second / 60.0 * 12.0; }
    }
    public class DateTimeToMinuteConverter : DateTimeToUnitConverter {
        protected override double ConvertCore(DateTime dateTime) { return (double)dateTime.Minute / 60.0 * 12.0; }
    }
    public class DateTimeToHourConverter : DateTimeToUnitConverter {
        protected override double ConvertCore(DateTime dateTime) { return dateTime.Hour % 12; }
    }
}
