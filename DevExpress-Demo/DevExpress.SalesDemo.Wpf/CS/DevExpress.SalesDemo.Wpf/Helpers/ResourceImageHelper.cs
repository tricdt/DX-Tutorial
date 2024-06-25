using DevExpress.SalesDemo.Model;
using System.Windows.Media.Imaging;

namespace DevExpress.SalesDemo.Wpf.Helpers {
    public static class ResourceImageHelper {
        public static BitmapImage GetResourceImage(string image) {
            BitmapImage res = new BitmapImage();
            res.BeginInit();
            res.StreamSource = ModelAssemblyHelper.GetResourceStream(image);
            res.EndInit();
            res.Freeze();
            return res;
        }
    }
}
