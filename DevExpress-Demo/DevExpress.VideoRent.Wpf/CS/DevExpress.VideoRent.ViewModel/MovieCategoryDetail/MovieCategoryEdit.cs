using DevExpress.VideoRent.ViewModel.ViewModelBase;
using System.Collections.Generic;
using System.ComponentModel;
using System;

namespace DevExpress.VideoRent.ViewModel {
    public class MovieCategoryEdit : ModuleObjectEdit {
        MovieCategoryEditData anotherCategories;

        public MovieCategoryEdit(MovieCategoryEditObject editObject, ModuleObjectDetail detail) : base(editObject, detail) { }
        public MovieCategoryEditObject MovieCategoryEditObject { get { return (MovieCategoryEditObject)EditObject; } }
        public MovieCategoryEditData AnotherCategories {
            get { return anotherCategories; }
            private set { SetValue<MovieCategoryEditData>("AnotherCategories", ref anotherCategories, value, true); }
        }
        protected override void OnEditObjectBeforeUpdate(object sender, EventArgs e) {
            base.OnEditObjectBeforeUpdate(sender, e);
            AnotherCategories = null;
        }
        protected override void OnEditObjectUpdated(object sender, EventArgs e) {
            base.OnEditObjectUpdated(sender, e);
            AnotherCategories = new MovieCategoryEditData(MovieCategoryEditObject.VideoRentObject.Session, MovieCategoryEditObject.VideoRentObjectOid);
        }
        protected override void DisposeManaged() {
            AnotherCategories = null;
            base.DisposeManaged();
        }
    }
}
