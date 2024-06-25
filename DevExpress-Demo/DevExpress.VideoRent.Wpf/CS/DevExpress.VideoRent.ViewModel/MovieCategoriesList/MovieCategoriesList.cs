using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using System.Collections.ObjectModel;

namespace DevExpress.VideoRent.ViewModel {
    public class MovieCategoriesList : VRObjectsListBase<MovieCategory> {
        MovieCategoriesEdit movieCategoriesEdit;
        MovieCategoryEdit movieCategoryEdit;
        ObservableCollection<MovieCategoryPriceEdit> categoryPriceEdits;
        ChartPaletteEditData chartPaletteEditData;
        ChartMarker2DKindEditData chartMarker2DKindEditData;

        public MovieCategoriesList(MovieCategoriesListObject editObject) : this(editObject, null) { }
        public MovieCategoriesList(MovieCategoriesListObject editObject, object tag)
            : base(editObject, tag) {
            ChartPaletteEditData = new ChartPaletteEditData();
            ChartMarker2DKindEditData = new ChartMarker2DKindEditData();
            CategoryPriceEdits = new ObservableCollection<MovieCategoryPriceEdit>();
            MovieCategoriesEdit = new MovieCategoriesEdit(VRObjectsListObject.MovieCategoriesEditObject, this);
            MovieCategoriesEdit.CurrentRecordChanged += OnMovieCategoriesEditCurrentRecordChanged;
            OnMovieCategoriesEditCurrentRecordChanged(MovieCategoriesEdit, EventArgs.Empty);
        }
        public new MovieCategoriesListObject VRObjectsListObject { get { return (MovieCategoriesListObject)EditObject; } }
        #region Edits
        public MovieCategoriesEdit MovieCategoriesEdit {
            get { return movieCategoriesEdit; }
            private set { SetValue<MovieCategoriesEdit>("MovieCategoriesEdit", ref movieCategoriesEdit, value); }
        }
        public MovieCategoryEdit MovieCategoryEdit {
            get { return movieCategoryEdit; }
            set { SetValue<MovieCategoryEdit>("MovieCategoryEdit", ref movieCategoryEdit, value, true); }
        }
        public ObservableCollection<MovieCategoryPriceEdit> CategoryPriceEdits {
            get { return categoryPriceEdits; }
            set { SetValue<ObservableCollection<MovieCategoryPriceEdit>>("CategoryPriceEdits", ref categoryPriceEdits, value); }
        }
        protected override IEnumerable<ModuleObjectEdit> ModuleObjectEdits {
            get {
                List<ModuleObjectEdit> list = new List<ModuleObjectEdit>(base.ModuleObjectEdits);
                if(MovieCategoriesEdit != null)
                    list.Add(MovieCategoriesEdit);
                if(MovieCategoryEdit != null)
                    list.Add(MovieCategoryEdit);
                foreach(MovieCategoryPriceEdit priceEdit in CategoryPriceEdits)
                    list.Add(priceEdit);
                return list;
            }
        }
        #endregion
        public ChartPaletteEditData ChartPaletteEditData {
            get { return chartPaletteEditData; }
            set { SetValue<ChartPaletteEditData>("ChartPaletteEditData", ref chartPaletteEditData, value); }
        }
        public ChartMarker2DKindEditData ChartMarker2DKindEditData {
            get { return chartMarker2DKindEditData; }
            set { SetValue<ChartMarker2DKindEditData>("ChartMarker2DKindEditData", ref chartMarker2DKindEditData, value); }
        }
        public MovieCategory CreateCategory() {
            XPCollection<MovieCategory> categories = MovieCategoriesEdit.VRObjectsEditObject.VideoRentObjects;
            MovieCategory category;
            using(NewNameGenerator<MovieCategory> generator = new NewNameGenerator<MovieCategory>(categories)) {
                category = new MovieCategory(categories.Session, generator.RetrieveNewName());
            }
            categories.Add(category);
            MovieCategoriesEdit.CurrentRecord = category;
            return category;
        }
        void OnMovieCategoriesEditCurrentRecordChanged(object sender, EventArgs e) {
            List<MovieCategoryPriceEdit> priceEdits = null;
            if(MovieCategoriesEdit.CurrentRecord == null) {
                MovieCategoryEdit = null;
            } else {
                Guid categoryOid = MovieCategoriesEdit.CurrentRecord.Oid;
                MovieCategoryEdit = CreateMovieCategoryEdit(out priceEdits);
            }
            foreach(MovieCategoryPriceEdit oldPriceEdit in CategoryPriceEdits)
                oldPriceEdit.Dispose();
            CategoryPriceEdits.Clear();
            if(priceEdits != null) {
                foreach(MovieCategoryPriceEdit newPriceEdit in priceEdits)
                    CategoryPriceEdits.Add(newPriceEdit);
            }
        }
        MovieCategoryEdit CreateMovieCategoryEdit(out List<MovieCategoryPriceEdit> priceEdits) {
            MovieCategoryEditObject editObject = VRObjectsListObject.GetMovieCategoryEditObject(MovieCategoriesEdit.CurrentRecord.Oid);
            if(MovieCategoryEdit != null && MovieCategoryEdit.EditObject == editObject) {
                priceEdits = null;
                return MovieCategoryEdit;
            }
            MovieCategoryEdit edit = new MovieCategoryEdit(editObject, this);
            priceEdits = new List<MovieCategoryPriceEdit>();
            XPCollection<MovieCategoryPrice> prices = new XPCollection<MovieCategoryPrice>(MovieCategoriesEdit.CurrentRecord.Prices);
            prices.Sorting.Add(new SortProperty("[Format]", SortingDirection.Ascending));
            foreach(MovieCategoryPrice price in prices) {
                priceEdits.Add(new MovieCategoryPriceEdit(VRObjectsListObject.GetMovieCategoryPriceEditObject(price.Oid), this));
            }
            return edit;
        }
        #region Commands
        public Action<object> CommandAdd { get { return DoCommandAdd; } }
        void DoCommandAdd(object p) { CreateCategory(); }
        #endregion
    }
}
