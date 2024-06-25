using System;
using System.Collections.Generic;
using DevExpress.VideoRent.Helpers;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.Xpo;

namespace DevExpress.VideoRent.ViewModel {
    public class MovieCategoryDetailObject : VRObjectDetailObject<MovieCategory>, IMovieCategoryEditObjectParent, IMovieCategoryPriceEditObjectParent {
        MovieCategoryEditObject movieCategoryEditObject;
        Dictionary<Guid, MovieCategoryPriceEditObject> movieCategoryPriceEditObjects = new Dictionary<Guid, MovieCategoryPriceEditObject>();

        public MovieCategoryDetailObject(Session session, Guid? categoryOid) : base(session, categoryOid) { }
        public MovieCategoryPrice GetCategoryPrice(Guid categoryPriceOid) {
            return SessionHelper.GetObjectByKey<MovieCategoryPrice>(categoryPriceOid, Session);
        }
        protected override MovieCategory CreateNewObjectOverride() {
            MovieCategory category;
            using(NewNameGenerator<MovieCategory> generator = new NewNameGenerator<MovieCategory>(Session)) {
                category = new MovieCategory(Session, generator.RetrieveNewName());
            }
            return category;
        }
        #region Subobjects
        internal MovieCategoryEditObject MovieCategoryEditObject {
            get {
                if(movieCategoryEditObject == null)
                    movieCategoryEditObject = new MovieCategoryEditObject(this, VideoRentObjectOid);
                return movieCategoryEditObject;
            }
        }
        internal MovieCategoryPriceEditObject GetMovieCategoryPriceEditObjects(Guid priceOid) {
            MovieCategoryPriceEditObject movieCategoryPriceEditObject;
            if(!movieCategoryPriceEditObjects.TryGetValue(priceOid, out movieCategoryPriceEditObject)) {
                movieCategoryPriceEditObject = new MovieCategoryPriceEditObject(this, priceOid);
                movieCategoryPriceEditObjects.Add(priceOid, movieCategoryPriceEditObject);
            }
            return movieCategoryPriceEditObject;
        }
        internal override IEnumerable<EditableSubobject> Subobjects {
            get {
                List<EditableSubobject> subobjects = new List<EditableSubobject>();
                if(movieCategoryEditObject != null)
                    subobjects.Add(movieCategoryEditObject);
                foreach(MovieCategoryPriceEditObject priceEditObject in movieCategoryPriceEditObjects.Values)
                    subobjects.Add(priceEditObject);
                return subobjects;
            }
        }
        internal override bool ReleaseSubobject(EditableSubobject editableSubobject) {
            if(base.ReleaseSubobject(editableSubobject)) return true;
            if(editableSubobject == movieCategoryEditObject) {
                movieCategoryEditObject = null;
                return true;
            }
            MovieCategoryPriceEditObject priceEditObject = editableSubobject as MovieCategoryPriceEditObject;
            if(priceEditObject != null) {
                movieCategoryPriceEditObjects.Remove(priceEditObject.CategoryPriceOid);
                return true;
            }
            return false;
        }
        #endregion
        List<Guid> FillPriceOidsList(Guid guid) {
            MovieCategory currentCategory = SessionHelper.GetObjectByKey<MovieCategory>(guid, Session);
            List<Guid> toReturn = new List<Guid>();
            foreach(MovieCategoryPrice item in currentCategory.Prices) {
                toReturn.Add(item.Oid);
            }
            return toReturn;
        }
    }
}
