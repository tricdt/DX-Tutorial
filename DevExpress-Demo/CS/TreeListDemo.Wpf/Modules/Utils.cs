using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using DevExpress.Xpf.Core;

namespace TreeListDemo {
    public static class ImageHelper {
        public static ImageSource GetSvgImage(string imageName) {
            var extension = new SvgImageSourceExtension() { Uri = new Uri(string.Format("pack://application:,,,/TreeListDemo;component/Images/{0}.svg", imageName)), Size = new System.Windows.Size(16, 16) };
            return (ImageSource)extension.ProvideValue(null);
        }
    }
}
