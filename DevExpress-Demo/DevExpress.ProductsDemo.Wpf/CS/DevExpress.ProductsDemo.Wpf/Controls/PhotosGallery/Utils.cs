using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Xml.Linq;

namespace ProductsDemo {
    public class DemoValuesProvider {
        public const string DevExpressBingKey = DevExpress.Map.Native.DXBingKeyVerifier.BingKeyWpfProductsDemo;
    }

        public static class DataLoader {
        public static XDocument LoadXmlFromResources(string fileName) {
            try {
                fileName = "/DevExpress.ProductsDemo.Wpf;component" + fileName;
                Uri uri = new Uri(fileName, UriKind.RelativeOrAbsolute);
                StreamResourceInfo info = Application.GetResourceStream(uri);
                return XDocument.Load(info.Stream);
            } catch {
                return null;
            }
        }
        public static XDocument LoadXmlFromFile(string fileName) {
            try {
                return XDocument.Load(fileName);
            }
            catch {
                return null;
            }
        }
    }

    public class PhotoGalleryResources {
        public BitmapImage CityInformationControlSource { get { return new BitmapImage(new Uri("/DevExpress.ProductsDemo.Wpf;component/Images/PhotoGallery/CityInformationControl.png", UriKind.RelativeOrAbsolute)); } }
        public BitmapImage LabelControlImageSource { get { return new BitmapImage(new Uri("/DevExpress.ProductsDemo.Wpf;component/Images/PhotoGallery/Label.png", UriKind.RelativeOrAbsolute)); } }
        public BitmapImage PlaceInfoControlPrevImageSource { get { return new BitmapImage(new Uri("/DevExpress.ProductsDemo.Wpf;component/Images/PhotoGallery/PrevPlace.png", UriKind.RelativeOrAbsolute)); } }
        public BitmapImage PlaceInfoControlNextImageSource { get { return new BitmapImage(new Uri("/DevExpress.ProductsDemo.Wpf;component/Images/PhotoGallery/NextPlace.png", UriKind.RelativeOrAbsolute)); } }

        public PhotoGalleryResources() {
        }
    }
}
