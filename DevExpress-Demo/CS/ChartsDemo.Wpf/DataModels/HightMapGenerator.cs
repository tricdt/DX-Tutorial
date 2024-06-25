using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Threading;
using DevExpress.Utils;

namespace ChartsDemo {

    public class HeightMapGenerator {
        public DoubleCollection MapValues { get; private set; }
        public DoubleCollection MapX { get; private set; }
        public DoubleCollection MapY { get; private set; }
        public static Uri HeightMapUri { get { return AssemblyHelper.GetResourceUri(typeof(RegularGridDataTab).Assembly, "/Data/Heightmap.jpg"); } }

        public HeightMapGenerator() {
            GenerateMap(HeightMapUri);
        }

        void GenerateMap(Uri uri) {
            ImageData ImageData = new ImageData(uri);
            PixelColor[,] pixels = ImageData.GetPixels();

            int countX = pixels.GetLength(0);
            int countY = pixels.GetLength(1);

            int startX = 0;
            int startY = 0;
            int gridStep = 100;
            double minY = -300;
            double maxY = 3000;

            DoubleCollection mapX = new DoubleCollection(countX);
            DoubleCollection mapY = new DoubleCollection(countY);
            DoubleCollection values = new DoubleCollection(countX * countY);
            bool fullY = false;
            for (int i = 0; i < countX; i++) {
                mapX.Add(startX + i * gridStep);
                for (int j = 0; j < countY; j++) {
                    if (!fullY)
                        mapY.Add(startY + j * gridStep);
                    double value = GetMapValue(pixels[i, j], minY, maxY);
                    values.Add(value);

                }
                fullY = true;
            }
            MapY = mapY;
            MapX = mapX;
            MapValues = values;
        }

        double GetMapValue(PixelColor color, double min, double max) {
            double normalizedValue = color.Gray / 255d; 
            return min + normalizedValue * (max - min);
        }
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PixelColor {
        public byte Blue;
        public byte Green;
        public byte Red;
        public byte Alpha;

        
        int BlueInt { get { return this.Blue; } }
        int GreenInt { get { return this.Green; } }
        int RedInt { get { return this.Red; } }

        public byte Gray {
            get { return (byte)((BlueInt + GreenInt + RedInt) / 3); }
        }
    }


    public class ImageData {
        readonly StreamResourceInfo streamResourceInfo;

        public ImageData(Uri uri) {
            this.streamResourceInfo = Application.GetResourceStream(uri);
        }
        PixelColor[,] GetPixels(BitmapSource source) {
            if (source.Format != PixelFormats.Bgra32)
                source = new FormatConvertedBitmap(source, PixelFormats.Bgra32, null, 0);
            PixelColor[,] result = new PixelColor[source.PixelWidth, source.PixelHeight];
            int stride = source.PixelWidth * (source.Format.BitsPerPixel / 8);
            CopyPixels(source, result, stride, 0);
            return result;
        }
        void CopyPixels(BitmapSource source, PixelColor[,] pixels, int stride, int offset) {
            var height = source.PixelHeight;
            var width = source.PixelWidth;
            var pixelBytes = new byte[height * width * 4];
            source.CopyPixels(pixelBytes, stride, 0);
            int y0 = offset / width;
            int x0 = offset - width * y0;
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    pixels[x + x0, y + y0] = new PixelColor {
                        Blue = pixelBytes[(y * width + x) * 4 + 0],
                        Green = pixelBytes[(y * width + x) * 4 + 1],
                        Red = pixelBytes[(y * width + x) * 4 + 2],
                        Alpha = pixelBytes[(y * width + x) * 4 + 3],
                    };
        }
        void DoEvents() {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { }));
        }
        public PixelColor[,] GetPixels() {
            PixelColor[,] pixels = new PixelColor[0, 0];
            try {
                BitmapImage source = new BitmapImage();
                source.BeginInit();
                source.StreamSource = this.streamResourceInfo.Stream;
                source.EndInit();
                while (source.IsDownloading) {
                    DoEvents();
                }
                pixels = GetPixels(source);
            }
            catch {
            }
            return pixels;
        }
    }

}
