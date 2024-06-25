using DevExpress.Xpf.Charts;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace DevExpress.SalesDemo.Wpf.Converters {
    public class PaletteToBrushConverter : IValueConverter {
        public CustomPalette Palette { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var index = (int)value;
            if (index >= Palette.Count)
                index = 0;
            SolidColorBrush result = new SolidColorBrush(Palette[index]);
            return result;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
