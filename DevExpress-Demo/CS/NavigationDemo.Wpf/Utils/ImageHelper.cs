using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Internal;
using System.Collections.ObjectModel;

namespace NavigationDemo.Utils {
    public static class ImageHelper {
        static readonly List<string> EmployeeDepartmentImages = new List<string> { "administration", "inventory", "manufacturing", "quality", "research", "sales" };
        public static Uri GetEmployeeDepartmentImage(string departmentName) {
            var imageName = EmployeeDepartmentImages
                .FirstOrDefault(x => departmentName.ToLower().Contains(x));
            if(string.IsNullOrEmpty(imageName))
                return null;
            return new Uri("/NavigationDemo;component/Images/Departments/" + imageName + ".svg", UriKind.RelativeOrAbsolute);
        }
    }
    public class PhotoInfo {
        public PhotoInfo(string uri, int rating) {
            Image = new BitmapImage(new Uri(uri));
            Dimension = string.Format("{0}x{1}", Image.Width, Image.Height);
            Size = string.Format("{0} KB", (new FileInfo(uri).Length / 1024).ToString());
            Name = Path.GetFileName(uri);
            Uri = uri;
            Rating = rating;
        }
        public ImageSource Image { get; private set; }
        public string Uri { get; private set; }
        public string Dimension { get; private set; }
        public string Name { get; private set; }
        public string Size { get; private set; }
        public virtual int Rating { get; set; }
    }
}
