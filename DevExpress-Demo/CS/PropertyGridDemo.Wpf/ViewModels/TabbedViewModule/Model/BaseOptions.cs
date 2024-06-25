using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyGridDemo {
    [MetadataType(typeof(Metadata.BaseOptionsMetadata))]
    public abstract class BaseOptions : ISupportParentViewModel {
        public BaseOptions() { }
        public BaseOptions(SeriesOptions root) {
            this.root = root;
        }
        SeriesOptions root;
        object ISupportParentViewModel.ParentViewModel {
            get { return root; }
            set { root = (SeriesOptions)value; }
        }
    }
}
