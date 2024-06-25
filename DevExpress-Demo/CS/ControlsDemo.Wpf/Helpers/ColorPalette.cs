using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace ControlsDemo.Helpers {
    public class ColorPalette {
        readonly static List<Color> Palette;
        static ColorPalette() {
            Palette = new List<Color>() {
                (Color)ColorConverter.ConvertFromString("#4668a5"),
                (Color)ColorConverter.ConvertFromString("#a54671"),
                (Color)ColorConverter.ConvertFromString("#49a4be"),
                (Color)ColorConverter.ConvertFromString("#469ea5"),
                (Color)ColorConverter.ConvertFromString("#5848a5"),
                (Color)ColorConverter.ConvertFromString("#9462ae"),
                (Color)ColorConverter.ConvertFromString("#fcc653"),
            };
        }
        public static Color GetColor(int number) {
            if(number >= Palette.Count) {
                int coef = number / (Palette.Count - 1);
                number -= coef * (Palette.Count - 1);
            }
            return Palette[(int)Math.Max(0, number)];
        }
        public static Color GetColor(int number, byte opacity) {
            var res = GetColor(number);
            res.A = opacity;
            return res;
        }
    }
    public class ColorPaletteConverter : IValueConverter {
        public byte Opacity { get; set; }
        public ColorPaletteConverter() {
            Opacity = 255;
        }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value is int)
                return ColorPalette.GetColor((int)value, Opacity);
            return Colors.Transparent;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
    public class ColorPaletteConverterExtension : MarkupExtension {
        public byte Opacity { get; set; }
        public ColorPaletteConverterExtension() {
            Opacity = 255;
        }
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return new ColorPaletteConverter() { Opacity = Opacity };
        }
    }
}
