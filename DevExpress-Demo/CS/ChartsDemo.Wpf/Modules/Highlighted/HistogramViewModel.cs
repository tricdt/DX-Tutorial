using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {
    [POCOViewModel]
    public class HistogramViewModel {
        public static HistogramViewModel Create() {
            return ViewModelSource.Create(() => new HistogramViewModel());
        }

        public virtual Palette Palette { get; set; }
        public virtual IntervalDivisionMode IntervalDivisionMode { get; set; }
        public virtual SolidColorBrush Cluster1Brush { get; protected set; }
        public virtual SolidColorBrush Cluster2Brush { get; protected set; }
        public virtual SolidColorBrush Cluster3Brush { get; protected set; }

        public HistogramViewModel() {
            Palette = new OfficePalette();
            IntervalDivisionMode = IntervalDivisionMode.Width;
        }
        protected void OnIntervalDivisionModeChanged() {
            switch (IntervalDivisionMode) {
                case IntervalDivisionMode.Auto:
                    break;
                case IntervalDivisionMode.Width:
                    break;
                case IntervalDivisionMode.Count:
                    break;
                default:
                    throw new InvalidEnumArgumentException(string.Format("The {0} enum value is unknown", IntervalDivisionMode));
            }
        }
        protected void OnPaletteChanged() {
            if (Palette == null)
                return;
            Cluster1Brush = new SolidColorBrush(Color.FromArgb(100, Palette[0].R, Palette[0].G, Palette[0].B));
            Cluster2Brush = new SolidColorBrush(Color.FromArgb(100, Palette[1].R, Palette[1].G, Palette[1].B));
            Cluster3Brush = new SolidColorBrush(Color.FromArgb(100, Palette[2].R, Palette[2].G, Palette[2].B));
        }
    }

    class IntervalDivisionModeToScaleOptionsConverter : IValueConverter {
        public IntervalNumericScaleOptions AutoIntervalNumericScaleOptions { get; set; }
        public WidthIntervalNumericScaleOptions WidthIntervalNumericScaleOptions { get; set; }
        public CountIntervalNumericScaleOptions CountIntervalNumericScaleOptions { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value.GetType() != typeof(IntervalDivisionMode))
                return value;
            IntervalDivisionMode scaleMode = (IntervalDivisionMode)value;
            switch (scaleMode) {
                case IntervalDivisionMode.Auto:
                    return AutoIntervalNumericScaleOptions;
                case IntervalDivisionMode.Width:
                    return WidthIntervalNumericScaleOptions;
                case IntervalDivisionMode.Count:
                    return CountIntervalNumericScaleOptions;
                default:
                    throw new InvalidEnumArgumentException(string.Format("The {0} enum value is unknown", scaleMode));
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (!(targetType == typeof(IntervalDivisionMode)))
                return value;
            if (value is WidthIntervalNumericScaleOptions)
                return IntervalDivisionMode.Width;
            if (value is CountIntervalNumericScaleOptions)
                return IntervalDivisionMode.Count;
            if (value is IntervalNumericScaleOptions)
                return IntervalDivisionMode.Auto;
            throw new ArgumentException("An instance of the IntervalNumericScaleOptions, WidthIntervalNumericScaleOptions or CountIntervalNumericScaleOptions class is expected.");
        }
    }
}
