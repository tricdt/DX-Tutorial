using DevExpress.Xpf.Charts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyGridDemo {    
    [MetadataType(typeof(Metadata.MirrorOptionsMetadata))]
    public class MirrorOptions {
        public MirrorOptions() {
            MirrorLength = 50;
        }
        public virtual double MirrorLength { get; set; }
        public virtual bool Show { get; set; }
        public virtual double ActualLength { get; set; }

        protected void OnMirrorLengthChanged() {
            UpdateActualLength();
        }        
        protected void OnShowChanged() {
            UpdateActualLength();
        }
        void UpdateActualLength() {
            if (Show)
                ActualLength = MirrorLength;
            else
                ActualLength = 0d;            
        }
    }
    [MetadataType(typeof(Metadata.LabelOptionsMetadata))]
    public class LabelOptions {
        public virtual bool ShowLabel { get; set; }
        public virtual Bar2DLabelPosition Position { get; set; }
    }
    [MetadataType(typeof(Metadata.VisibleLabelOptionsMetadata))]
    public class VisibleLabelOptions : LabelOptions {        
    }
}
