using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public interface IMovieCategoryPriceEditObjectParent {
        MovieCategoryPrice GetCategoryPrice(Guid categoryOid);
    }
    public sealed class MovieCategoryPriceEditObject : EditableSubobject {
        Guid categoryPriceOid;
        MovieCategoryPrice categoryPrice;

        public MovieCategoryPriceEditObject(EditableObject parent, Guid categoryPriceOid)
            : base(parent) {
            CategoryPriceOid = categoryPriceOid;
            Update();
        }
        public IMovieCategoryPriceEditObjectParent MovieCategoryPriceEditObjectParent { get { return (IMovieCategoryPriceEditObjectParent)Parent; } }
        public Guid CategoryPriceOid {
            get { return categoryPriceOid; }
            private set { SetValue<Guid>("CategoryPriceOid", ref categoryPriceOid, value); }
        }
        public MovieCategoryPrice CategoryPrice {
            get { return categoryPrice; }
            private set { SetValue<MovieCategoryPrice>("CategoryPrice", ref categoryPrice, value); }
        }
        protected override void UpdateOverride() {
            CategoryPrice = MovieCategoryPriceEditObjectParent.GetCategoryPrice(categoryPriceOid);
        }
        protected override void DisposeManaged() {
            CategoryPrice = null;
            base.DisposeManaged();
        }
    }
}
