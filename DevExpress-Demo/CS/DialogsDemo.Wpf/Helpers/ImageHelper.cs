using DevExpress.Internal;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CommonDemo.Helpers {
    public static class ImageHelper {
        public static IEnumerable<PhotoData> GetJPGPhotos(string directoryPath) {
            return Directory.GetFiles(directoryPath, "*.jpg").Select(GetJPGPhoto);
        }
        public static PhotoData GetJPGPhoto(string photoPath) {
            return new PhotoData(photoPath);
        }
    }
    public class ImagesHelper {
        public static ImageSource GetWebIcon(string icon) {
            return new BitmapImage(new Uri(@"/DialogsDemo;component/Images/TabControl/" + icon, UriKind.Relative));
        }
    }
    public class PhotoData {
        public BitmapImage Photo { get; private set; }
        public string PhotoName { get; private set; }

        public PhotoData(string photoPath) {
            Photo = new BitmapImage(new Uri(photoPath));
            PhotoName = Path.GetFileNameWithoutExtension(photoPath);
        }
    }
}
