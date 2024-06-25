using System;
using System.Windows;
using System.Globalization;
using System.Collections.Generic;
using System.Windows.Data;
using DevExpress.Xpf.Charts.RangeControlClient;
using DevExpress.Xpf.Editors;

namespace EditorsDemo {
    public partial class ChartClientForRangeControlModule : EditorsDemoModule {
        const int dataCount = 100;
        public ChartClientModel ChartClientModel { get; set; }

        public ChartClientForRangeControlModule() {
            InitializeComponent();
            lbDTGridAlignment.SelectedIndex = 0;
            lbTSGridAlignment.SelectedIndex = 0;
            ChartClientModel = new ChartClientModel();
            ChartClientModel.NumericItemsSource = GenerateNumericData();
            ChartClientModel.DateTimeItemsSource = GenerateDateTimeData();
            ChartClientModel.TimeSpanItemsSource = GenerateTimeSpanData();
            this.DataContext = this;
        }
        double[] GenerateNumericData() {
            double[] data = new double[dataCount];
            Random rand = new Random();
            double value = 0;
            for (int i = 0; i < dataCount; i++) {
                value += (rand.NextDouble() - 0.5);
                data[i] = value;
            }
            return data;
        }
        List<DataSourceItem> GenerateDateTimeData() {
            List<DataSourceItem> data = new List<DataSourceItem>();
            DateTime now = DateTime.Now.Date;
            Random rand = new Random();
            double value = 0;
            for (int i = 0; i < dataCount; i++) {
                now = now.AddDays(1);
                value += (rand.NextDouble() - 0.5);
                data.Add(new DataSourceItem() { Argument = now, Value = value + Math.Sin(i * 0.6) });
            }
            return data;
        }
        List<DataSourceItem> GenerateTimeSpanData() {
            List<DataSourceItem> data = new List<DataSourceItem>();
            Random rand = new Random();
            double value = 0;
            for (int i = 0; i < dataCount; i++) {
                value += (rand.NextDouble() - 0.5);
                data.Add(new DataSourceItem() { Argument = TimeSpan.FromMinutes(i * 30), Value = value + Math.Sin(i * 0.6) });
            }
            return data;
        }
    }
    public class ChartClientModel : FrameworkElement {
        public static readonly DependencyProperty NumericItemsSourceProperty =
            DependencyProperty.Register("NumericItemsSource", typeof(object), typeof(ChartClientModel),
            new PropertyMetadata(null));
        public static readonly DependencyProperty DateTimeItemsSourceProperty =
            DependencyProperty.Register("DateTimeItemsSource", typeof(object), typeof(ChartClientModel),
            new PropertyMetadata(null));
        public static readonly DependencyProperty TimeSpanItemsSourceProperty =
            DependencyProperty.Register("TimeSpanItemsSource", typeof(object), typeof(ChartClientModel),
            new PropertyMetadata(null));
        public static readonly DependencyProperty MinimumDateTimeGridSpacingProperty =
            DependencyProperty.Register("MinimumDateTimeGridSpacing", typeof(double), typeof(ChartClientModel),
            new PropertyMetadata(1.0));
        public static readonly DependencyProperty MiddleDateTimeGridSpacingProperty =
            DependencyProperty.Register("MiddleDateTimeGridSpacing", typeof(double), typeof(ChartClientModel),
            new PropertyMetadata(3.0));
        public static readonly DependencyProperty MaximumDateTimeGridSpacingProperty =
            DependencyProperty.Register("MaximumDateTimeGridSpacing", typeof(double), typeof(ChartClientModel),
            new PropertyMetadata(5.0));
        public static readonly DependencyProperty MinimumTimeSpanGridSpacingProperty =
            DependencyProperty.Register("MinimumTimeSpanGridSpacing", typeof(double), typeof(ChartClientModel),
            new PropertyMetadata(1.0));
        public static readonly DependencyProperty MiddleTimeSpanGridSpacingProperty =
            DependencyProperty.Register("MiddleTimeSpanGridSpacing", typeof(double), typeof(ChartClientModel),
            new PropertyMetadata(3.0));
        public static readonly DependencyProperty MaximumTimeSpanGridSpacingProperty =
            DependencyProperty.Register("MaximumTimeSpanGridSpacing", typeof(double), typeof(ChartClientModel),
            new PropertyMetadata(5.0));
        public static readonly DependencyProperty DateTimeGridAlignmentProperty;
        public static readonly DependencyProperty TimeSpanGridAlignmentProperty;
        public static readonly DependencyProperty DateTimeGridSpacingVisibilityProperty =
            DependencyProperty.Register("DateTimeGridSpacingVisibility", typeof(Visibility), typeof(ChartClientModel),
           new PropertyMetadata(Visibility.Collapsed));
        public static readonly DependencyProperty TimeSpanGridSpacingVisibilityProperty =
            DependencyProperty.Register("TimeSpanGridSpacingVisibility", typeof(Visibility), typeof(ChartClientModel),
           new PropertyMetadata(Visibility.Collapsed));

        protected static void DateTimeGridAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ChartClientModel model = d as ChartClientModel;
            if (model != null && e.NewValue != null) {
                DateTimeGridAlignment gridAlignment = (DateTimeGridAlignment)(((ListBoxEditItem)(e.NewValue)).Tag);
                model.DateTimeGridSpacingVisibility = gridAlignment.Equals(DateTimeGridAlignment.Auto) ? Visibility.Collapsed : Visibility.Visible;
                model.UpdateGridSpacing(gridAlignment);
            }
        }
        protected static void TimeSpanGridAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ChartClientModel model = d as ChartClientModel;
            if (model != null && e.NewValue != null) {
                TimeSpanGridAlignment gridAlignment = (TimeSpanGridAlignment)(((ListBoxEditItem)(e.NewValue)).Tag);
                model.TimeSpanGridSpacingVisibility = gridAlignment.Equals(TimeSpanGridAlignment.Auto) ? Visibility.Collapsed : Visibility.Visible;
                model.UpdateGridSpacing(gridAlignment);
            }
        }

        static ChartClientModel() {
            DateTimeGridAlignmentProperty = DependencyProperty.Register("DateTimeGridAlignment", typeof(object), typeof(ChartClientModel), new PropertyMetadata(DateTimeGridAlignment.Day, DateTimeGridAlignmentChanged));
            TimeSpanGridAlignmentProperty = DependencyProperty.Register("TimeSpanGridAlignment", typeof(object), typeof(ChartClientModel), new PropertyMetadata(TimeSpanGridAlignment.Hour, TimeSpanGridAlignmentChanged));
        }

        public double MinimumDateTimeGridSpacing {
            get { return (double)GetValue(MinimumDateTimeGridSpacingProperty); }
            set { SetValue(MinimumDateTimeGridSpacingProperty, value); }
        }
        public double MiddleDateTimeGridSpacing {
            get { return (double)GetValue(MiddleDateTimeGridSpacingProperty); }
            set { SetValue(MiddleDateTimeGridSpacingProperty, value); }
        }
        public double MaximumDateTimeGridSpacing {
            get { return (double)GetValue(MaximumDateTimeGridSpacingProperty); }
            set { SetValue(MaximumDateTimeGridSpacingProperty, value); }
        }
        public double MinimumTimeSpanGridSpacing {
            get { return (double)GetValue(MinimumTimeSpanGridSpacingProperty); }
            set { SetValue(MinimumTimeSpanGridSpacingProperty, value); }
        }
        public double MiddleTimeSpanGridSpacing {
            get { return (double)GetValue(MiddleTimeSpanGridSpacingProperty); }
            set { SetValue(MiddleTimeSpanGridSpacingProperty, value); }
        }
        public double MaximumTimeSpanGridSpacing {
            get { return (double)GetValue(MaximumTimeSpanGridSpacingProperty); }
            set { SetValue(MaximumTimeSpanGridSpacingProperty, value); }
        }
        public object NumericItemsSource {
            get { return GetValue(NumericItemsSourceProperty); }
            set { SetValue(NumericItemsSourceProperty, value); }
        }
        public object DateTimeItemsSource {
            get { return GetValue(DateTimeItemsSourceProperty); }
            set { SetValue(DateTimeItemsSourceProperty, value); }
        }
        public object TimeSpanItemsSource {
            get { return GetValue(TimeSpanItemsSourceProperty); }
            set { SetValue(TimeSpanItemsSourceProperty, value); }
        }
        public Visibility DateTimeGridSpacingVisibility {
            get { return (Visibility)GetValue(DateTimeGridSpacingVisibilityProperty); }
            set { SetValue(DateTimeGridSpacingVisibilityProperty, value); }
        }
        public Visibility TimeSpanGridSpacingVisibility {
            get { return (Visibility)GetValue(TimeSpanGridSpacingVisibilityProperty); }
            set { SetValue(TimeSpanGridSpacingVisibilityProperty, value); }
        }

        void UpdateGridSpacing(DateTimeGridAlignment gridAlignment) {
            MinimumDateTimeGridSpacing = GetMinimumGridSpacing(gridAlignment);
            MaximumDateTimeGridSpacing = GetMaximumGridSpacing(gridAlignment);
            MiddleDateTimeGridSpacing = (MinimumDateTimeGridSpacing + MaximumDateTimeGridSpacing) / 2;
        }
        void UpdateGridSpacing(TimeSpanGridAlignment gridAlignment) {
            MinimumTimeSpanGridSpacing = GetMinimumGridSpacing(gridAlignment);
            MaximumTimeSpanGridSpacing = GetMaximumGridSpacing(gridAlignment);
            MiddleTimeSpanGridSpacing = (MinimumTimeSpanGridSpacing + MaximumTimeSpanGridSpacing) / 2;
        }
        double GetMaximumGridSpacing(DateTimeGridAlignment gridAlignment) {
            switch (gridAlignment) {
                case DateTimeGridAlignment.Day:
                    return 15;
                case DateTimeGridAlignment.Month:
                    return 3;
                case DateTimeGridAlignment.Week:
                    return 6;
                case DateTimeGridAlignment.Auto:
                default:
                    return 5;
            }
        }
        double GetMinimumGridSpacing(DateTimeGridAlignment gridAlignment) {
            switch (gridAlignment) {
                case DateTimeGridAlignment.Day:
                    return 5;
                case DateTimeGridAlignment.Month:
                    return 1;
                case DateTimeGridAlignment.Week:
                    return 2;
                case DateTimeGridAlignment.Auto:
                default:
                    return 1;
            }
        }
        double GetMaximumGridSpacing(TimeSpanGridAlignment gridAlignment) {
            switch (gridAlignment) {
                case TimeSpanGridAlignment.Hour:
                    return 12;
                case TimeSpanGridAlignment.Minute:
                    return 720;
                case TimeSpanGridAlignment.Auto:
                default:
                    return 1;
            }
        }
        double GetMinimumGridSpacing(TimeSpanGridAlignment gridAlignment) {
            switch (gridAlignment) {
                case TimeSpanGridAlignment.Hour:
                    return 4;
                case TimeSpanGridAlignment.Minute:
                    return 240;
                case TimeSpanGridAlignment.Auto:
                default:
                    return 1;
            }
        }
    }
    public class DataSourceItem {
        public object Argument { get; set; }
        public object Value { get; set; }
    }
    public enum ChartViewType {
        Area,
        Bar,
        Line
    }
    public class ChartViewTypeConverter : IValueConverter {
        RangeControlClientView Parse(string type) {
            if (type == "Area")
                return new RangeControlClientAreaView();
            if (type == "Bar")
                return new RangeControlClientBarView();
            if (type == "Line")
                return new RangeControlClientLineView();
            return null;
        }
        RangeControlClientView Parse(ChartViewType type) {
            if (type == ChartViewType.Area)
                return new RangeControlClientAreaView();
            if (type == ChartViewType.Bar)
                return new RangeControlClientBarView();
            if (type == ChartViewType.Line)
                return new RangeControlClientLineView();
            return null;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is string)
                return Parse(value as string);
            if (value is ChartViewType)
                return Parse((ChartViewType)value);
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter is RangeControlClientAreaView)
                return ChartViewType.Area;
            if (parameter is RangeControlClientBarView)
                return ChartViewType.Bar;
            if (parameter is RangeControlClientLineView)
                return ChartViewType.Line;
            return string.Empty;
        }
    }
}
