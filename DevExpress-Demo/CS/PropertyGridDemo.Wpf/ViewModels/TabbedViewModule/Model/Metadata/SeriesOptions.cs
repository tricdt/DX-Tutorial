using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Charts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyGridDemo.Metadata {
    public static class SeriesOptionsMetadata {
        public static void BuildMetadata(MetadataBuilder<SeriesOptions> builder) {
            builder.Property(x => x.FillType).PropertyGridEditor("FillType");
            builder.Property(x => x.BorderType).PropertyGridEditor("BorderType");
            builder.Property(x => x.ShowLabel).PropertyGridEditor("ShowLabel");
            builder.Property(x => x.Fill).PropertyGridEditor("CommonTemplates.BoldHeaderWithoutEditor");
            builder.Property(x => x.Border).PropertyGridEditor("CommonTemplates.BoldHeaderWithoutEditor");
            builder.Property(x => x.Mirror).PropertyGridEditor("CommonTemplates.BoldHeaderWithoutEditor");
            builder.Property(x => x.Label).PropertyGridEditor("CommonTemplates.BoldHeaderWithoutEditor");
        }
    }
    public static class CommonSeriesOptionsMetadata {
        public static void BuildMetadata(MetadataBuilder<CommonSeriesOptions> builder) {
            builder.PropertyGridEditor("Options");
            builder.Property(x => x.Model).PropertyGridEditor("Options.Model");
        }
    }
}
