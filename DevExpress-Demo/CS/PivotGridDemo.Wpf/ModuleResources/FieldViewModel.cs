using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.PivotGrid;
using DevExpress.Mvvm.POCO;

namespace PivotGridDemo {
    [POCOViewModel]
    public class FieldViewModel {
        public static FieldViewModel Create(string caption, string name, FieldArea area, int areaIndex) {
            return ViewModelSource.Create(() => new FieldViewModel(caption, name, area, areaIndex));
        }
        protected FieldViewModel(string caption, string name, FieldArea area, int areaIndex) {
            Visible = true;
            Caption = caption;
            Name = name;
            Area = area;
            AreaIndex = areaIndex;
        }

        public virtual FieldArea Area { get; set; }
        public virtual int AreaIndex { get; set; }
        public virtual string Caption { get; set; }
        public virtual FieldGroupInterval GroupInterval { get; set; }
        public virtual string Name { get; set; }
        public virtual string SortByFieldName { get; set; }
        public virtual FieldSortOrder SortOrder { get; set; }
        public virtual Decimal TopValueCount { get; set; }
        public virtual bool TopValueShowOthers { get; set; }
        public virtual bool Visible { get; set; }
        public virtual double Width { get; set; }
    }
}
