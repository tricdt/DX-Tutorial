using DevExpress.Utils;
using DevExpress.Xpf.Core;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace RibbonDemo {
    public class GlyphHelper {
        public static ImageSource GetGlyph(string path) {
            if(path.EndsWith(".svg")) {
                return new DXImageExtension(path).ProvideValue(null) as ImageSource;
            } else {
                return new BitmapImage(AssemblyHelper.GetResourceUri(typeof(MVVMRibbon).Assembly, path));
            }
        }
    }
}
