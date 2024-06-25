using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.VideoRent.Resources;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class MovieCategoryDetail : VRObjectDetail<MovieCategory> {
        MovieCategoryEdit movieCategoryEdit;
        ObservableCollection<MovieCategoryPriceEdit> categoryPriceEdits;

        public MovieCategoryDetail(MovieCategoryDetailObject editObject)
            : base(editObject) {
            MovieCategoryEdit = new MovieCategoryEdit(VRObjectDetailEditObject.MovieCategoryEditObject, this);
            CategoryPriceEdits = GetMovieCategoryPriceEdits();
        }
        public new MovieCategoryDetailObject VRObjectDetailEditObject { get { return (MovieCategoryDetailObject)EditObject; } }
        #region Edits
        public MovieCategoryEdit MovieCategoryEdit {
            get { return movieCategoryEdit; }
            private set { SetValue<MovieCategoryEdit>("MovieCategoryEdit", ref movieCategoryEdit, value); }
        }
        public ObservableCollection<MovieCategoryPriceEdit> CategoryPriceEdits {
            get { return categoryPriceEdits; }
            set { SetValue<ObservableCollection<MovieCategoryPriceEdit>>("CategoryPriceEdits", ref categoryPriceEdits, value); }
        }
        protected override IEnumerable<ModuleObjectEdit> ModuleObjectEdits {
            get {
                List<ModuleObjectEdit> list = new List<ModuleObjectEdit>(base.ModuleObjectEdits);
                if(MovieCategoryEdit != null)
                    list.Add(MovieCategoryEdit);
                foreach(MovieCategoryPriceEdit priceEdit in CategoryPriceEdits)
                    list.Add(priceEdit);
                return list;
            }
        }
        #endregion
        protected override void OnEditObjectReloaded(object sender, EventArgs e) {
            base.OnEditObjectReloaded(sender, e);
            TitleDraft = VRObjectDetailEditObject.WasCreatedNewObject ? ConstStrings.Get("NewMovieCategory") : VRObjectDetailEditObject.MovieCategoryEditObject.VideoRentObject.Name;
        }
        ObservableCollection<MovieCategoryPriceEdit> GetMovieCategoryPriceEdits() {
            ObservableCollection<MovieCategoryPriceEdit> toReturn = new ObservableCollection<MovieCategoryPriceEdit>();
            foreach(MovieCategoryPrice price in MovieCategoryEdit.MovieCategoryEditObject.VideoRentObject.Prices) {
                toReturn.Add(new MovieCategoryPriceEdit(VRObjectDetailEditObject.GetMovieCategoryPriceEditObjects(price.Oid), this));
            }
            return toReturn;
        }
    }
}
