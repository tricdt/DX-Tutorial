using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DockingDemo.Helpers {
    public static class ImagesHelper {

        public static IEnumerable<string> NaturePhotos { get; private set; }
        public static string NaturePhoto1 { get { return NaturePhotos.ElementAt(0); } }
        public static string NaturePhoto2 { get { return NaturePhotos.ElementAt(1); } }
        public static string NaturePhoto3 { get { return NaturePhotos.ElementAt(2); } }
        public static string NaturePhoto4 { get { return NaturePhotos.ElementAt(3); } }
        public static string NaturePhoto5 { get { return NaturePhotos.ElementAt(4); } }
        public static string NaturePhoto6 { get { return NaturePhotos.ElementAt(5); } }
        public static string NaturePhoto7 { get { return NaturePhotos.ElementAt(6); } }
        public static string NaturePhoto8 { get { return NaturePhotos.ElementAt(7); } }
        public static string NaturePhoto9 { get { return NaturePhotos.ElementAt(8); } }
        public static string NaturePhoto10 { get { return NaturePhotos.ElementAt(9); } }
        public static string GetRandomNaturePhoto() {
            return NaturePhotos.ElementAt(rnd.Next(NaturePhotos.Count() - 1));
        }

        static ImagesHelper() {
            string natureUriPath = @"/DockingDemo;component/Images/Photos/Nature/";
            Func<string, string> getNatureImage = x => natureUriPath + x;
            var naturePhotos = new List<string>();
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
        }
        static readonly Random rnd = new Random();
    }
}
