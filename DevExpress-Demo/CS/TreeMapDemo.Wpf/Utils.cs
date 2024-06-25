using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;

namespace TreeMapDemo {
    public static class DataLoader {
        static Stream GetStream(string fileName) {
            Uri uri = GetResourceUri(fileName);
            return Application.GetResourceStream(uri).Stream;
        }
        public static Uri GetResourceUri(string fileName) {
            fileName = "/TreeMapDemo;component" + fileName;
            return new Uri(fileName, UriKind.RelativeOrAbsolute);
        }
        public static XDocument LoadXDocumentFromResources(string fileName) {
            try {
                return XDocument.Load(GetStream(fileName));
            }
            catch {
                return null;
            }
        }
        public static XmlDocument LoadXmlDocumentFromResources(string fileName) {
            XmlDocument document = new XmlDocument();
            try {
                document.Load(GetStream(fileName));
                return document;
            }
            catch {
                return null;
            }
        }
        public static DataTable CreateDataSet(string fileName) {
            if (!string.IsNullOrWhiteSpace(fileName)) {
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(GetStream(fileName));
                if (dataSet.Tables.Count > 0)
                    return dataSet.Tables[0];
            }
            return null;
        }
    }

    public class LeafVerticalStackPanel : Panel {
        protected override Size MeasureOverride(Size availableSize) {
            foreach (UIElement child in Children)
                child.Measure(availableSize);
            return availableSize;
        }
        protected override Size ArrangeOverride(Size finalSize) {
            Rect freeRect = new Rect(new Point(0, 0), finalSize);
            foreach (UIElement child in Children) {
                FrameworkElement element = child as FrameworkElement;
                if (element != null) {
                    Size size = element.DesiredSize;
                    double horzPosition = element.HorizontalAlignment == HorizontalAlignment.Right ? freeRect.Right - size.Width : freeRect.Left;
                    double vertPosition = element.VerticalAlignment == VerticalAlignment.Bottom ? freeRect.Bottom - size.Height : freeRect.Top;
                    Rect locationRect = new Rect(new Point(horzPosition, vertPosition), size);
                    if (freeRect.Contains(locationRect) && freeRect.Height > locationRect.Height &&
                            freeRect.Width >= (element is StackPanel ? locationRect.Width : locationRect.Width + element.Margin.Left)) {
                        element.Visibility = Visibility.Visible;
                        element.Arrange(locationRect);
                        if (element.VerticalAlignment == VerticalAlignment.Bottom)
                            freeRect = new Rect(new Point(freeRect.Left, freeRect.Top), new Size(freeRect.Width, freeRect.Height - size.Height));
                        else
                            freeRect = new Rect(new Point(freeRect.Left, freeRect.Top + size.Height), new Point(freeRect.Right, freeRect.Bottom));
                    }
                    else {
                        element.Visibility = Visibility.Hidden;
                        break;
                    }
                }
            }
            return finalSize;
        }
    }

    public class StringToImagePathConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string stringValue = value as string;
            if (stringValue != null)
                return DataLoader.GetResourceUri("/Images/" + stringValue + ".png");
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }

    public class CountToTextConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (System.Convert.ToInt32(value) == 1)
                return value.ToString() + " medal";
            return value.ToString() + " medals";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }

    public class EnergyTypeToBrushConverter : IValueConverter {
        Color GetColor(string typeName) {
            switch (typeName) {
                case "Nuclear": return Color.FromRgb(0x9D, 0x90, 0xA0);
                case "Oil": return Color.FromRgb(0x2A, 0x5F, 0x8E);
                case "Natural Gas": return Color.FromRgb(0x62, 0x9D, 0xD1);
                case "Hydro Electric": return Color.FromRgb(0x29, 0x7F, 0xD5);
                case "Coal": return Color.FromRgb(0x4A, 0x66, 0xAC);
                default: return Colors.Transparent;
            }
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is string)
                return new SolidColorBrush(GetColor((string)value));
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }

    public class WidthToVisibilityConverter : MarkupExtension, IValueConverter {
        const int imageWidth = 24;

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return Convert.ToInt32(value) >= imageWidth ? Visibility.Visible : Visibility.Collapsed;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }

    public class HeatmapDataSourceGenerator {
        const int offset = 15;
        const int speedRange = 7;
        const int velocity = 10;
        public const int SizeX = 600, SizeY = 300;

        Random random = new Random(100);

        int[,] speed;
        int[,] values;

        int min = 0;
        int delta = 0;

        int LengthX { get { return SizeY + offset; } }
        int LengthY { get { return SizeX + offset; } }

        public HeatmapDataSourceGenerator() {
            InitializeMatrix();
        }

        void InitializeMatrix() {
            speed = new int[SizeY + offset, SizeX + offset];
            values = new int[SizeY + offset, SizeX + offset];
            values[0, 0] = 0;
            speed[0, 0] = 0;
            int minLength = Math.Min(LengthX, LengthY);
            for (int i = 1; i < minLength; i++) {
                values[i, 0] = values[i - 1, 0] + speed[i - 1, 0];
                values[0, i] = values[0, i - 1] + speed[0, i - 1];
                speed[i, 0] = speed[i - 1, 0] + random.Next(0 - speedRange, 1 + speedRange);
                speed[0, i] = speed[0, i - 1] + random.Next(0 - speedRange, 1 + speedRange);
            }
            for (int i = minLength; i < LengthX; i++) {
                values[i, 0] = values[i - 1, 0] + speed[i - 1, 0];
                speed[i, 0] = speed[i - 1, 0] + random.Next(0 - speedRange, 1 + speedRange); 
            }
            for (int i = minLength; i < LengthY; i++) {
                values[0, i] = values[0, i - 1] + speed[0, i - 1];
                speed[0, i] = speed[0, i - 1] + random.Next(0 - speedRange, 1 + speedRange);
            }
            for (int i = 1; i < LengthX; i++) {
                for (int j = 1; j < LengthY; j++) {
                    values[i, j] = (values[i - 1, j - 1] + speed[i - 1, j - 1] + speed[i - 1, j] + speed[i, j - 1]) / 2;
                    speed[i, j] = (speed[i - 1, j] + speed[i, j - 1]) / 2 + random.Next(0 - speedRange, 1 + speedRange);
                }
            }
            for (int i = 0; i < LengthX; i++) {
                for (int j = 0; j < LengthY; j++) {
                    if (values[i, j] > delta)
                        delta = values[i, j];
                    if (values[i, j] < min)
                        min = values[i, j];
                }
            }
            delta -= min;
        }

        void UpdateMatrix() {
            for (int i = 0; i < LengthX; i++) {
                for (int j = 0; j < LengthY - velocity; j++) {
                    values[i, j] = values[i, j + velocity];
                    speed[i, j] = speed[i, j + velocity];
                }
            }
            for (int i = LengthY - velocity; i < LengthY; i++) {
                values[0, i] = values[0, i - 1] + speed[0, i - 1];
                speed[0, i] = speed[0, i - 1] + random.Next(0 - speedRange, 1 + speedRange);
                for (int j = 1; j < LengthX; j++) {
                    values[j, i] = (values[j - 1, i - 1] + speed[j - 1, i - 1] + speed[j, i - 1] + speed[j - 1, i]) / 2;
                    speed[j, i] = (speed[j, i - 1] + speed[j - 1, i]) / 2 + random.Next(0 - speedRange, 1 + speedRange);
                }
            }
        }

        public static int[] GetArray(int size) {
            return new int[size].Select((v, i) => i).ToArray();
        }

        public double[,] GetMatrix() {
            UpdateMatrix();
            double[,] matrix = new double[SizeY, SizeX];
            for (int i = 0; i < SizeY; i++)
                for (int j = 0; j < SizeX; j++)
                    matrix[i, j] = (values[i + offset, j + offset] - min) * 16777216.0 / delta - 8388608;
            return matrix;
        }
    }
}
