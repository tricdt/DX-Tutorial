using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PropertyGridDemo.Metadata {
    public static class FillOptionsMetadata {
        public static void BuildMetadata(MetadataBuilder<FillOptions> builder) {
            builder.Property(x => x.Result).Hidden();
            builder.Property(x => x.FillType).PropertyGridEditor("FillOptions.FillType").LocatedAt(0);            
        }
    }
    public static class SolidFillOptionsMetadata {
        public static void BuildMetadata(MetadataBuilder<SolidFillOptions> builder) {
            builder.Property(x => x.Color).PropertyGridEditor("CommonBorderAndFillOptions.Color");
            builder.Property(x => x.Opacity).PropertyGridEditor("CommonBorderAndFillOptions.Opacity");
        }
    }
    public static class PictureFillOptionsMetadata {
        public static void BuildMetadata(MetadataBuilder<PictureFillOptions> builder) {
            builder.Property(x => x.Picture).PropertyGridEditor("PictureFillOptions.Picture");
            builder.Property(x => x.Opacity).PropertyGridEditor("CommonBorderAndFillOptions.Opacity");
        }
    }
}
