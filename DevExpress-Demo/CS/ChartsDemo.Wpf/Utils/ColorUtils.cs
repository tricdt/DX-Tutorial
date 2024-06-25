using System;
using System.Linq;
using System.Windows.Media;
using DevExpress.Xpf.Core;

namespace ChartsDemo {

    static class ColorUtils {
        #region class ColorHSL
        class ColorHSL {
            const float minLuminance = 0.0f;
            const float maxLuminance = 1.0f;

            readonly float _hue;
            readonly float _saturation;
            float _luminance;

            public float Luminance { get { return this._luminance; } set { this._luminance = value; } }

            public float MinLuminance { get { return Math.Min(minLuminance, Luminance * 0.9f); } }
            public float MaxLuminance { get { return Math.Max(maxLuminance, Luminance + (1.0f - Luminance) * 0.15f); } }

            public ColorHSL(float hue, float saturation, float luminance) {
                this._hue = hue;
                this._saturation = saturation;
                this._luminance = luminance;
            }
            byte GetComponent(float q, float p, float t) {
                const float oneDivSix = 1.0f / 6.0f;
                const float twoDivThree = 2.0f / 3.0f;
                while (t < 0.0f)
                    t += 1.0f;
                while (t > 1.0f)
                    t -= 1.0f;
                float result;
                if (t < oneDivSix)
                    result = p + ((q - p) * 6.0f * t);
                else if (t < 0.5f)
                    result = q;
                else if (t < twoDivThree)
                    result = p + ((q - p) * (twoDivThree - t) * 6.0f);
                else
                    result = p;
                return (byte)Math.Round(result * 255.0f);
            }
            internal Color ToColor() {
                const float oneDivThree = 1.0f / 3.0f;
                float q = this._luminance < 0.5f ? (Luminance * (1.0f + this._saturation)) : this._luminance + this._saturation - (this._luminance * this._saturation);
                float p = 2.0f * Luminance - q;
                float hueScaled = this._hue / 360.0f;
                return Color.FromArgb(255, GetComponent(q, p, hueScaled + oneDivThree),
                    GetComponent(q, p, hueScaled), GetComponent(q, p, hueScaled - oneDivThree));
            }
        }
        #endregion

        static string[] darkThemeNames = new string[] {
            Theme.MetropolisDark.Name,
            Theme.TouchlineDark.Name,
            Theme.Office2016BlackSE.Name,
            Theme.Office2016Black.Name
        };

        static ColorHSL ToColorHSL(Color color) {
            return new ColorHSL(color.GetHue(), color.GetSaturation(), color.GetBrightness());
        }
        static byte MixChannel(byte fromValue, byte toValue, double ratio) {
            return (byte)(fromValue * (1.0 - ratio) + toValue * ratio);
        }

        internal static Color ChangeColorLuminance(Color color, float ratio) {
            ColorHSL colorHSL = ToColorHSL(color);
            colorHSL.Luminance = Math.Min(0.95f, ratio);
            return colorHSL.ToColor();
        }

        internal static Color ColorizeSeaIceSeries(Color color, int seriesIndex, string theme) {
            float ratio;
            int hundredth = seriesIndex / 100;
            if (!darkThemeNames.Contains(theme))
                ratio = seriesIndex <= 10 ? 0.2f + (float)Math.Ceiling(seriesIndex / 2d) * 0.09f : 0.7f + hundredth; 
            else
                ratio = seriesIndex <= 10 ? 1.0f - (float)Math.Ceiling(seriesIndex / 2d) * 0.09f : 0.5f - hundredth; 
            return ChangeColorLuminance(color, ratio);
        }

        internal static Color InterpolateColors(Color fromUnit, Color toUnit, double ratio) {
            return Color.FromArgb(MixChannel(fromUnit.A, toUnit.A, ratio),
                                  MixChannel(fromUnit.R, toUnit.R, ratio),
                                  MixChannel(fromUnit.G, toUnit.G, ratio),
                                  MixChannel(fromUnit.B, toUnit.B, ratio));
        }

    }


    public static class ColorExtensions {
        public static float GetHue(this System.Windows.Media.Color c) {
            return System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B).GetHue();
        }

        public static float GetBrightness(this System.Windows.Media.Color c) {
            return System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B).GetBrightness();
        }

        public static float GetSaturation(this System.Windows.Media.Color c) {
            return System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B).GetSaturation();
        }
    }

}
