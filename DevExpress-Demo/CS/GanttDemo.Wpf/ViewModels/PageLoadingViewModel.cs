using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Threading;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Utils;
using DevExpress.Xpf.Gantt;

namespace GanttDemo {
    [POCOViewModel]
    public class PageLoadingViewModel {
        class DataLoadingItemComparer : IComparer<DataLoadingItem> {
            public int Compare(DataLoadingItem x, DataLoadingItem y) {
                return x.FinishDate.CompareTo(y.FinishDate);
            }
        }

        const string csvFilePath = "Data\\PageLoading.csv";

        public ObservableCollection<DataLoadingItem> DataItems { get; private set; }
        public virtual DataLoadingItem LastItem { get; protected set; }
        public virtual DateTime StartTime { get; set; }
        readonly List<DataLoadingItem> sourceItems;
        readonly DispatcherTimer timer = new DispatcherTimer();
        int itemIndex = 0;

        public PageLoadingViewModel() {
            timer.Tick += TimerTick;
            DataItems = new ObservableCollection<DataLoadingItem>();
            using(var stream = ResourceUtils.GetResourceStream(csvFilePath))
                sourceItems = CSVLoader.LoadItems(stream);
            StartTime = sourceItems.Min(x => x.StartTime).AddMilliseconds(-300);
            sourceItems.Sort(new DataLoadingItemComparer());
        }

        void TimerTick(object sender, EventArgs e) {
            timer.Stop();
            DataItems.Add(sourceItems[itemIndex]);
            LastItem = sourceItems[itemIndex];
            if(itemIndex < sourceItems.Count - 1) {
                timer.Interval = sourceItems[itemIndex + 1].FinishDate - sourceItems[itemIndex].FinishDate;
                timer.Start();
            }
            itemIndex++;
        }

        public void OnModuleLoaded() {
            timer.Start();
        }
        public void OnModuleUnloaded() {
            timer.Stop();
        }
        public void ReloadPage() {
            timer.Stop();
            DataItems.Clear();
            itemIndex = 0;
            timer.Interval = sourceItems[0].Duration;
            timer.Start();
        }
    }

    public class DataLoadingItem : ImmutableObject {
        public DateTime StartTime { get; private set; }
        public TimeSpan Duration { get; private set; }
        public string Name { get; private set; }
        public string Status { get; private set; }
        public string Type { get; private set; }
        public int Size { get; private set; }
        internal DateTime FinishDate { get { return StartTime + Duration; } }

        public DataLoadingItem(DateTime startTime, TimeSpan duration, string name, string status, string type, int size) {
            this.StartTime = startTime;
            this.Duration = duration;
            this.Name = name;
            this.Status = status;
            this.Type = type;
            this.Size = size;
        }
    }

    public class DurationToStringConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        string FormatDuration(TimeSpan duration) {
            return duration.TotalSeconds > 1 ? string.Format("{0} s", duration.TotalSeconds) : string.Format("{0} ms", duration.TotalMilliseconds);
        }
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value is TimeSpan)
                return FormatDuration((TimeSpan)value);
            return null;

        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class ByteSizeToStringConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        string FormatByteSize(int size) {
            return size > 1024 ? string.Format("{0}KB", size / 1024) : string.Format("{0}B", size);
        }
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value is int)
                return FormatByteSize((int)value);
            return null;

        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
