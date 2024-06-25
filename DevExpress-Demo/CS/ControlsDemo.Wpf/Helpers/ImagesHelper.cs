using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CommonDemo.Helpers {
    public static class ImagesHelper {
        public static IEnumerable<ImageSource> LasVegasPhotos { get; private set; }
        public static ImageSource LasVegasPhoto1 { get { return LasVegasPhotos.ElementAt(0); } }
        public static ImageSource LasVegasPhoto2 { get { return LasVegasPhotos.ElementAt(1); } }
        public static ImageSource LasVegasPhoto3 { get { return LasVegasPhotos.ElementAt(2); } }
        public static ImageSource LasVegasPhoto4 { get { return LasVegasPhotos.ElementAt(3); } }
        public static ImageSource GetRandomLasVegasPhoto() {
            return LasVegasPhotos.ElementAt(rnd.Next(LasVegasPhotos.Count() - 1));
        }

        public static IEnumerable<ImageSource> NaturePhotos { get; private set; }
        public static ImageSource NaturePhoto1 { get { return NaturePhotos.ElementAt(0); } }
        public static ImageSource NaturePhoto2 { get { return NaturePhotos.ElementAt(1); } }
        public static ImageSource NaturePhoto3 { get { return NaturePhotos.ElementAt(2); } }
        public static ImageSource NaturePhoto4 { get { return NaturePhotos.ElementAt(3); } }
        public static ImageSource NaturePhoto5 { get { return NaturePhotos.ElementAt(4); } }
        public static ImageSource NaturePhoto6 { get { return NaturePhotos.ElementAt(5); } }
        public static ImageSource NaturePhoto7 { get { return NaturePhotos.ElementAt(6); } }
        public static ImageSource NaturePhoto8 { get { return NaturePhotos.ElementAt(7); } }
        public static ImageSource NaturePhoto9 { get { return NaturePhotos.ElementAt(8); } }
        public static ImageSource NaturePhoto10 { get { return NaturePhotos.ElementAt(9); } }
        public static ImageSource GetRandomNaturePhoto() {
            return NaturePhotos.ElementAt(rnd.Next(NaturePhotos.Count() - 1));
        }

        public static ImageSource GroupIcon { get; private set; }

        public static ImageSource GetWebIcon(string icon) {
            return new BitmapImage(new Uri(@"/ControlsDemo;component/Images/TabControl/" + icon, UriKind.Relative));
        }
        public static ImageSource GetSvgImage(string imagePath, Size imageSize) {
            var extension = new SvgImageSourceExtension() { Uri = new Uri(imagePath), Size = imageSize };
            return (ImageSource)extension.ProvideValue(null);
        }
        static ImagesHelper() {
            string lasVegasUriPath = @"/ControlsDemo;component/Images/Photos/Las Vegas/";
            Func<string, ImageSource> getLasVegasImage = x => new BitmapImage(new Uri(lasVegasUriPath + x, UriKind.Relative));
            var lasVegasPhotos = new List<ImageSource>();
            lasVegasPhotos.Add(getLasVegasImage("DSC_4405.jpg"));
            lasVegasPhotos.Add(getLasVegasImage("DSC_4498.jpg"));
            lasVegasPhotos.Add(getLasVegasImage("DSC_4647.jpg"));
            lasVegasPhotos.Add(getLasVegasImage("DSC_4721.jpg"));
            lasVegasPhotos.Add(getLasVegasImage("IMG_1216.jpg"));
            lasVegasPhotos.Add(getLasVegasImage("IMG_1280.jpg"));
            lasVegasPhotos.Add(getLasVegasImage("IMG_1285.jpg"));
            lasVegasPhotos.Add(getLasVegasImage("IMG_1321.jpg"));
            lasVegasPhotos.Add(getLasVegasImage("IMG_1327.jpg"));
            lasVegasPhotos.Add(getLasVegasImage("IMG_1345.jpg"));
            LasVegasPhotos = lasVegasPhotos;

            string natureUriPath = @"/ControlsDemo;component/Images/Photos/Nature/";
            Func<string, ImageSource> getNatureImage = x => new BitmapImage(new Uri(natureUriPath + x, UriKind.Relative));
            var naturePhotos = new List<ImageSource>();
            naturePhotos.Add(getNatureImage("01.JPG"));
            naturePhotos.Add(getNatureImage("02.JPG"));
            naturePhotos.Add(getNatureImage("03.JPG"));
            naturePhotos.Add(getNatureImage("04.JPG"));
            naturePhotos.Add(getNatureImage("05.JPG"));
            naturePhotos.Add(getNatureImage("06.JPG"));
            naturePhotos.Add(getNatureImage("07.JPG"));
            naturePhotos.Add(getNatureImage("08.JPG"));
            naturePhotos.Add(getNatureImage("09.JPG"));
            naturePhotos.Add(getNatureImage("10.JPG"));
            NaturePhotos = naturePhotos;

            GroupIcon = GetSvgImage("pack://application:,,,/ControlsDemo;component/Images/TabControl/GroupIcon.svg", new Size(24, 24));
        }
        static readonly Random rnd = new Random();
    }
}
