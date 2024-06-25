using System;
using System.Collections.Generic;
using DevExpress.Data.Filtering;
using DevExpress.VideoRent.Helpers;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;

namespace DevExpress.VideoRent.ViewModel {
    public class MovieCategoriesListObject : VRObjectsListObject<MovieCategory>, IMovieCategoriesEditObjectParent, IMovieCategoryEditObjectParent, IMovieCategoryPriceEditObjectParent {
        MovieCategoriesEditObject movieCategoriesEditObject;
        Dictionary<Guid, MovieCategoryEditObject> movieCategoryEditObjects = new Dictionary<Guid, MovieCategoryEditObject>();
        Dictionary<Guid, MovieCategoryPriceEditObject> movieCategoryPriceEditObjects = new Dictionary<Guid, MovieCategoryPriceEditObject>();

        public MovieCategoriesListObject(Session session) : base(session) { }
        public MovieCategory GetVideoRentObject(Guid categoryOid) {
            XPCollection<MovieCategory> categories = new XPCollection<MovieCategory>(MovieCategoriesEditObject.VideoRentObjects, CriteriaOperator.Parse("Oid = ?", categoryOid));
            return categories.Count == 0 ? null : categories[0];
        }
        public MovieCategoryPrice GetCategoryPrice(Guid categoryPriceOid) {
            return SessionHelper.GetObjectByKey<MovieCategoryPrice>(categoryPriceOid, Session);
        }
        public override AllObjects<MovieCategory> GetVideoRentObjects() {
            return new AllObjects<MovieCategory>(Session, null, new SortProperty("[IsDefault]", SortingDirection.Descending), new SortProperty("[Name]", SortingDirection.Ascending));
        }
        #region Subobjects
        internal MovieCategoriesEditObject MovieCategoriesEditObject {
            get {
                if(movieCategoriesEditObject == null)
                    movieCategoriesEditObject = new MovieCategoriesEditObject(this);
                return movieCategoriesEditObject;
            }
        }
        internal MovieCategoryEditObject GetMovieCategoryEditObject(Guid categoryOid) {
            MovieCategoryEditObject movieCategoryEditObject;
            if(!movieCategoryEditObjects.TryGetValue(categoryOid, out movieCategoryEditObject)) {
                movieCategoryEditObject = new MovieCategoryEditObject(this, categoryOid);
                movieCategoryEditObjects.Add(categoryOid, movieCategoryEditObject);
            }
            return movieCategoryEditObject;
        }
        internal MovieCategoryPriceEditObject GetMovieCategoryPriceEditObject(Guid categoryPriceOid) {
            MovieCategoryPriceEditObject movieCategoryPriceEditObject;
            if(!movieCategoryPriceEditObjects.TryGetValue(categoryPriceOid, out movieCategoryPriceEditObject)) {
                movieCategoryPriceEditObject = new MovieCategoryPriceEditObject(this, categoryPriceOid);
                movieCategoryPriceEditObjects.Add(categoryPriceOid, movieCategoryPriceEditObject);
            }
            return movieCategoryPriceEditObject;
        }
        internal override IEnumerable<EditableSubobject> Subobjects {
            get {
                List<EditableSubobject> subobjects = new List<EditableSubobject>(base.Subobjects);
                if(movieCategoriesEditObject != null)
                    subobjects.Add(movieCategoriesEditObject);
                foreach(MovieCategoryEditObject movieCategoryEditObject in movieCategoryEditObjects.Values)
                    subobjects.Add(movieCategoryEditObject);
                foreach(MovieCategoryPriceEditObject movieCategoryPriceEditObject in movieCategoryPriceEditObjects.Values)
                    subobjects.Add(movieCategoryPriceEditObject);
                return subobjects;
            }
        }
        internal override bool ReleaseSubobject(EditableSubobject editableSubobject) {
            if(base.ReleaseSubobject(editableSubobject)) return true;
            if(editableSubobject == movieCategoriesEditObject) {
                movieCategoriesEditObject = null;
                return true;
            }
            MovieCategoryEditObject movieCategoryEditObject = editableSubobject as MovieCategoryEditObject;
            if(movieCategoryEditObject != null) {
                movieCategoryEditObjects.Remove(movieCategoryEditObject.VideoRentObjectOid);
                return true;
            }
            MovieCategoryPriceEditObject movieCategoryPriceEditObject = editableSubobject as MovieCategoryPriceEditObject;
            if(movieCategoryPriceEditObject != null) {
                movieCategoryPriceEditObjects.Remove(movieCategoryPriceEditObject.CategoryPriceOid);
                return true;
            }
            return false;
        }
        #endregion
    }
}
