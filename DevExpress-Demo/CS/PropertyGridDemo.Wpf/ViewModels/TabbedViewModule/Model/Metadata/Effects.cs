using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyGridDemo.Metadata {
    public static class MirrorOptionsMetadata {
        public static void BuildMetadata(MetadataBuilder<MirrorOptions> builder) {
            builder.Property(x => x.Show).PropertyGridEditor("MirrorOptions.Show");
            builder.Property(x => x.MirrorLength).PropertyGridEditor("MirrorOptions.MirrorLength").LocatedAt(0);
        }
    }
    public static class LabelOptionsMetadata {
        public static void BuildMetadata(MetadataBuilder<LabelOptions> builder) {
            builder.Property(x => x.ShowLabel).PropertyGridEditor("LabelOptions.ShowLabel").LocatedAt(0);
        }
    }
    public static class VisibleLabelOptionsMetadata {
        public static void BuildMetadata(MetadataBuilder<VisibleLabelOptions> builder) {
            builder.Property(x => x.Position).PropertyGridEditor("VisibleLabelOptions.Position");
        }
    }
}
