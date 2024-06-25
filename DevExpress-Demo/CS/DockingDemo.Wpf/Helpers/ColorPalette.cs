using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace DockingDemo.Helpers {
    public class ColorPalette {
        static readonly List<Color> Palette;

        static ColorPalette() {
            Palette = new List<Color>() {
                ColorFromString("#a02277"),
                ColorFromString("#4668a5"),
                ColorFromString("#90a091"),
                ColorFromString("#33a4be"),
                ColorFromString("#460ea5"),
                ColorFromString("#139e79"),
                ColorFromString("#5848a5"),
                ColorFromString("#9462ae"),
                ColorFromString("#65fc03"),
                ColorFromString("#fcc003"),
            };
        }

        public static Color GetColor(int number) {
            int color = number % Palette.Count;
            return Palette[color];
        }
        static Color ColorFromString(string colorString) {
            return (Color)(ColorConverter.ConvertFromString(colorString) ?? Colors.Transparent);
        }
    }
}
