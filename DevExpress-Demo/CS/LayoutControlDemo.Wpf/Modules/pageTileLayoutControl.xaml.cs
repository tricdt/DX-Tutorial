using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using DevExpress.Xpf.LayoutControl;

namespace LayoutControlDemo {
    public partial class pageTileLayoutControl : LayoutControlDemoModule {
        public pageTileLayoutControl() {
            InitializeComponent();
        }

        public IEnumerable<int> Numbers {
            get {
                var result = new List<int>();
                for (int i = 9; i >= 0; i--)
                    result.Add(i);
                return result;
            }
        }
    }

    public class StringCollection : List<string> { }

    public class TimeSpanToSecondsConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return ((TimeSpan)value).TotalSeconds;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return TimeSpan.FromSeconds((double)value);
        }
    }

    public class ScalablePaddingConverter : IValueConverter {
        public ScalablePaddingConverter() {
            MinPadding = 35;
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var controlHeight = (double)value;
            double desiredContentHeight = 3 * Tile.LargeHeight + 2 * TileLayoutControl.DefaultItemSpace + 20;  
            double paddingY = Math.Floor(Math.Max(0d, controlHeight - desiredContentHeight) / 2d);
            paddingY = Math.Max(MinPadding, Math.Min(paddingY, TileLayoutControl.DefaultPadding.Top));
            double relativePadding = (paddingY - MinPadding) / (TileLayoutControl.DefaultPadding.Top - MinPadding);
            double paddingX = Math.Floor(MinPadding + relativePadding * (TileLayoutControl.DefaultPadding.Left - MinPadding));
            return new Thickness(paddingX, paddingY, paddingX, paddingY);
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        public double MinPadding { get; set; }
    }
}
