using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace DevExpress.VideoRent.Resources.Helpers {
    public static class ColorMatrixHelper {
        public static Image InvertImage(Image image) {
            ColorMatrix colorMatrix = new ColorMatrix(new float[][] {
                new float[] { -1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                new float[] { 0.0f, -1.0f, 0.0f, 0.0f, 0.0f},
                new float[] { 0.0f, 0.0f, -1.0f, 0.0f, 0.0f},
                new float[] { 0.0f, 0.0f, 0.0f, 1.0f, 0.0f},
                new float[] { 1.0f, 1.0f, 1.0f, 0.0f, 1.0f}
            });
            return ApplyColorMatrixToImage(image, colorMatrix);
        }
        public static Image GrayscaleImage(Image image) {
            ColorMatrix colorMatrix = new ColorMatrix(new float[][] {
                new float[] { 0.333f, 0.333f, 0.333f, 0.0f, 0.0f },
                new float[] { 0.333f, 0.333f, 0.333f, 0.0f, 0.0f },
                new float[] { 0.333f, 0.333f, 0.333f, 0.0f, 0.0f },
                new float[] { 0.0f, 0.0f, 0.0f, 1.0f, 0.0f },
                new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 1.0f }
            });
            return ApplyColorMatrixToImage(image, colorMatrix);
        }
        public static Image ColorizeImage(Image image, Color color) {
            float r = (float)color.R / 255.0f;
            float g = (float)color.G / 255.0f;
            float b = (float)color.B / 255.0f;
            float a = (float)color.A / 255.0f;
            ColorMatrix colorMatrix = new ColorMatrix(new float[][] {
                new float[] { r, g, b, 0.0f, 0.0f},
                new float[] { r, g, b, 0.0f, 0.0f},
                new float[] { r, g, b, 0.0f, 0.0f},
                new float[] { 0.0f, 0.0f, 0.0f, a, 0.0f},
                new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 1.0f}
            });
            return ApplyColorMatrixToImage(image, colorMatrix);
        }
        public static Image ApplyColorMatrixToImage(Image image, ColorMatrix colorMatrix) {
            int width = image.Width;
            int height = image.Height;
            Bitmap bitmap = new Bitmap(width, height);
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.DrawImage(image, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel, attributes);
            return bitmap;
        }
    }
}
