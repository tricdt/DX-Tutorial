using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace NavigationDemo.Utils {
    public class FilterBase {
        public virtual string Name { get { return string.Empty; } }
        public System.Windows.Media.Imaging.BitmapImage ApplyFilter(string uri, System.Windows.Size? size = null) {
            var image = size.HasValue ? ApplyMatrix(uri, Matrix, new Size((int)size.Value.Width, (int)size.Value.Height)) 
                : ApplyMatrix(uri, Matrix);
            return CreateWPFImage(image);
        }
        protected virtual ColorMatrix Matrix { get { return new ColorMatrix(); } }

        protected Image ApplyMatrix(string uri, ColorMatrix matrix, Size? size = null) {               
            Bitmap src = size != null ? new Bitmap(Image.FromFile(uri), size.Value) : new Bitmap(uri);
            Bitmap dest = new Bitmap(src.Width, src.Height, PixelFormat.Format32bppArgb);
            using(Graphics graphics = Graphics.FromImage(dest)) {
                ImageAttributes bmpAttributes = new ImageAttributes();
                bmpAttributes.SetColorMatrix(matrix);
                graphics.DrawImage(src, new Rectangle(0, 0, src.Width, src.Height), 0, 0, src.Width, src.Height, GraphicsUnit.Pixel, bmpAttributes);
            }
            src.Dispose();
            return dest;
        }

        protected System.Windows.Media.Imaging.BitmapImage CreateWPFImage(Image image) {
            var bitmapImage = new System.Windows.Media.Imaging.BitmapImage();
            bitmapImage.BeginInit();
            var stream = new MemoryStream();
            image.Save(stream, ImageFormat.Bmp);
            stream.Seek(0, SeekOrigin.Begin);
            bitmapImage.StreamSource = stream;
            bitmapImage.EndInit();
            return bitmapImage;
        }

    }
    public class PolaroidFilter : FilterBase {
        public override string Name { get { return "Polaroid"; } }
        protected override ColorMatrix Matrix {
            get {
                return new ColorMatrix(new float[][] {
                    new float[] {1.438f, -0.062f, -0.062f, 0, 0},
                    new float[] {-0.122f, 1.378f, -0.122f, 0, 0},
                    new float[] {0.016f, -0.016f, 1.438f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0.03f, 0.05f, -0.2f, 0, 1}
                });
            }
        }
    }
    public class GrayScaleFilter : FilterBase {
        public override string Name { get { return "GrayScale"; } }
        protected override ColorMatrix Matrix {
            get {
                return new ColorMatrix(new float[][] {
                    new float[] {.3f, .3f, .3f, 0, 0},
                    new float[] {.59f, .59f, .59f, 0, 0},
                    new float[] {.11f, .11f, .11f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                });
            }
        }
    }
    public class NegativeFilter : FilterBase {
        public override string Name { get { return "Negative"; } }
        protected override ColorMatrix Matrix {
            get {
                return new ColorMatrix(new float[][] {
                    new float[] {-1, 0, 0, 0, 0},
                    new float[] {0, -1, 0, 0, 0},
                    new float[] {0, 0, -1, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {1, 1, 1, 0, 1}
                });
            }
        }
    }
    public class SepiaFilter : FilterBase {
        public override string Name { get { return "Sepia"; } }
        protected override ColorMatrix Matrix {
            get {
                return new ColorMatrix(new float[][] {
                    new float[] {.393f, .349f, .272f, 0, 0},
                    new float[] {.769f, .686f, .534f, 0, 0},
                    new float[] {.189f, .168f, .131f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                });
            }
        }
    }
    public class BGRFilter : FilterBase {
        public override string Name { get { return "BGR"; } }
        protected override ColorMatrix Matrix {
            get {
                return new ColorMatrix(new float[][] {
                    new float[] {0, 0, 1, 0, 0},
                    new float[] {0, 1, 0, 0, 0},
                    new float[] {1, 0, 0, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                });
            }
        }
    }
    public class GBRFilter : FilterBase {
        public override string Name { get { return "GBR"; } }
        protected override ColorMatrix Matrix {
            get {
                return new ColorMatrix(new float[][] {
                    new float[] {0, 1, 0, 0, 0},
                    new float[] {0, 0, 1, 0, 0},
                    new float[] {1, 0, 0, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                });
            }
        }
    }
    public class UniversalFilter : FilterBase {
        public UniversalFilter() {
            r = g = b = 0;
        }
        int r, g, b;
        public System.Windows.Media.Imaging.BitmapImage ApplyFilter(string uri, int r, int g, int b) {
            this.r = r;
            this.g = g;
            this.b = b;
            var image = ApplyMatrix(uri, Matrix);
            return CreateWPFImage(image);
        }
        public override string Name { get { return "Universal"; } }
        protected override ColorMatrix Matrix {
            get {
                return new ColorMatrix(new float[][] {
                    new float[] {1 + (float)(r / 255.0f), 0, 0, 0, 0},
                    new float[] {0, 1 + (float)(g/ 255.0f), 0, 0, 0},
                    new float[] {0, 0, 1 + (float)(b/ 255.0f), 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0.1f, 0.1f, 0.1f, 0, 1}
                });
            }
        }
    }
    public class ContrastFilter : FilterBase {
        public ContrastFilter() {
            scale = translate = 0;
        }
        float scale;
        float translate;
        public override string Name { get { return "Contrast"; } }
        public System.Windows.Media.Imaging.BitmapImage ApplyFilter(string uri, int val) {
            scale = val;
            translate = (-.5f * scale + .5f) * 255.0f;
            var image = ApplyMatrix(uri, Matrix);
            return CreateWPFImage(image);
        }
        protected override ColorMatrix Matrix {
            get {
                return new ColorMatrix(new float[][] {
                    new float[] {1 + scale / 100, 0, 0, 0, translate},
                    new float[] {0, 1 + scale / 100, 0, 0, translate},
                    new float[] {0, 0, 1 + scale / 100, 0, translate},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                });
            }
        }
    }
}
