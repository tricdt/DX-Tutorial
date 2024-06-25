using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PropertyGridDemo.Metadata {
    public static class BorderOptionsDataMetadata {
        public static void BuildMetadata(MetadataBuilder<BorderOptionsData> builder) {

        }
    }
    public static class BorderOptionsMetadata {
        public static void BuildMetadata(MetadataBuilder<BorderOptions> builder) {
            builder.Property(x => x.Data).Hidden();
            builder.Property(x => x.BorderType).PropertyGridEditor("BorderOptions.BorderType").LocatedAt(0);
        }
    }
    public static class SolidBorderOptionsMetadata {
        public static void BuildMetadata(MetadataBuilder<SolidBorderOptions> builder) {
            builder.Property(x => x.Color).PropertyGridEditor("CommonBorderAndFillOptions.Color");
            builder.Property(x => x.Opacity).PropertyGridEditor("CommonBorderAndFillOptions.Opacity");
            builder.Property(x => x.Thickness).PropertyGridEditor("SolidBorderOptions.Thickness");
            builder.Property(x => x.DashStyle).PropertyGridEditor("SolidBorderOptions.DashStyle");
        }
    }
}
