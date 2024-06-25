using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Utils;
using System.Collections.Generic;

namespace DevExpress.VideoRent.Resources.Helpers {
    public static class DrawingImageToImageSourceHelper {
        static readonly DependencyProperty GdiImageProperty;
        static DrawingImageToImageSourceHelper() {
            Type ownerType = typeof(ImagesHelper);
            GdiImageProperty = DependencyProperty.RegisterAttached("GdiImage", typeof(System.Drawing.Image), ownerType, new PropertyMetadata(null));
        }
        public static BitmapSource Convert(System.Drawing.Image gdiImage, System.Drawing.Image nullValue) {
            System.Drawing.Image gdiImageForConvert = gdiImage == null ? nullValue : gdiImage;
            if(gdiImageForConvert == null)
                return null;
            System.Drawing.Bitmap bitmap = gdiImageForConvert as System.Drawing.Bitmap;
            if(bitmap == null)
                throw new NotSupportedException();
            Int32Rect rect = new Int32Rect(0, 0, bitmap.Width, bitmap.Height);
            BitmapSizeOptions sizeOptions = BitmapSizeOptions.FromWidthAndHeight(bitmap.Width, bitmap.Height);
            IntPtr hbitmap = bitmap.GetHbitmap();
            BitmapSource image = Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero, rect, sizeOptions);
            DeleteObject(hbitmap);
            SetGdiImage(image, gdiImage);
            return image;
        }
        public static List<ImageSource> Convert(ImageCollection imageCollection) {
            List<ImageSource> list = new List<ImageSource>();
            foreach(System.Drawing.Image image in imageCollection.Images) {
                list.Add(Convert(image, null));
            }
            return list;
        }
        [DllImport("gdi32.dll")]
        static extern bool DeleteObject(IntPtr hObject);
        static void SetGdiImage(DependencyObject target, System.Drawing.Image gdiImage) { target.SetValue(GdiImageProperty, gdiImage); }
    }
}
