using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using DevExpress.Xpf.Core;

namespace PropertyGridDemo {
    public static class ImageHelper {
        public static ImageSource GetSvgImage(string imageName) {
            var extension = new SvgImageSourceExtension() { Uri = new Uri(string.Format("pack://application:,,,/PropertyGridDemo;component/Images/{0}.svg", imageName)), Size = new System.Windows.Size(16, 16) };
            return (ImageSource)extension.ProvideValue(null);
        }
    }
}
