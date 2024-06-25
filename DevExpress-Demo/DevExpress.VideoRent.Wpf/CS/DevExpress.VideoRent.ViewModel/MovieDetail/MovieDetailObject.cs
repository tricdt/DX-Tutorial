using System;
using System.Collections.Generic;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.Xpo;

namespace DevExpress.VideoRent.ViewModel {
    public class MovieDetailObject : VRObjectDetailObject<Movie>, IMovieEditObjectParent, IMovieItemsEditObjectParent, IMovieAddArtistEditObjectParent, IMovieAddCompanyEditObjectParent {
        MovieEditObject movieEditObject;
        MovieAddCompanyEditObject movieCompanyEditObject;
        MovieAddArtistEditObject movieArtistEditObject;
        MovieItemsEditObject movieItemsEditObject;
        MovieAddItemsEditObject movieAddItemsEditObject;

        public MovieDetailObject(Session session, Guid? movieOid) : base(session, movieOid) { }
        protected override Movie CreateNewObjectOverride() {
            return new Movie(Session);
        }
        #region Subobjects
        internal MovieEditObject MovieEditObject {
            get {
                if(movieEditObject == null)
                    movieEditObject = new MovieEditObject(this, VideoRentObjectOid);
                return movieEditObject;
            }
        }
        internal MovieAddCompanyEditObject MovieCompanyEditObject {
            get {
                if(movieCompanyEditObject == null)
                    movieCompanyEditObject = new MovieAddCompanyEditObject(this, VideoRentObjectOid);
                return movieCompanyEditObject;
            }
        }
        internal MovieAddArtistEditObject MovieArtistEditObject {
            get {
                if(movieArtistEditObject == null)
                    movieArtistEditObject = new MovieAddArtistEditObject(this, VideoRentObjectOid);
                return movieArtistEditObject;
            }
        }
        internal MovieItemsEditObject MovieItemsEditObject {
            get {
                if(movieItemsEditObject == null)
                    movieItemsEditObject = new MovieItemsEditObject(this, VideoRentObjectOid);
                return movieItemsEditObject;
            }
        }
        internal MovieAddItemsEditObject MovieAddItemsEditObject {
            get {
                if(movieAddItemsEditObject == null)
                    movieAddItemsEditObject = new MovieAddItemsEditObject(this, VideoRentObjectOid);
                return movieAddItemsEditObject;
            }
        }
        internal override IEnumerable<EditableSubobject> Subobjects {
            get {
                List<EditableSubobject> list = new List<EditableSubobject>(base.Subobjects);
                if(movieEditObject != null)
                    list.Add(movieEditObject);
                if(movieCompanyEditObject != null)
                    list.Add(movieCompanyEditObject);
                if(movieArtistEditObject != null)
                    list.Add(movieArtistEditObject);
                if(movieItemsEditObject != null)
                    list.Add(movieItemsEditObject);
                if(movieAddItemsEditObject != null)
                    list.Add(movieAddItemsEditObject);
                return list;
            }
        }
        internal override bool ReleaseSubobject(EditableSubobject editableSubobject) {
            if(base.ReleaseSubobject(editableSubobject)) return true;
            if(editableSubobject == movieEditObject) {
                movieEditObject = null;
                return true;
            }
            if(editableSubobject == movieCompanyEditObject) {
                movieCompanyEditObject = null;
                return true;
            }
            if(editableSubobject == movieArtistEditObject) {
                movieArtistEditObject = null;
                return true;
            }
            if(editableSubobject == movieItemsEditObject) {
                movieItemsEditObject = null;
                return true;
            }
            if(editableSubobject == movieAddItemsEditObject) {
                movieAddItemsEditObject = null;
                return true;
            }
            return false;
        }
        #endregion
    }
}
