using System;
using System.Collections.Generic;
using DevExpress.VideoRent.Helpers;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;

namespace DevExpress.VideoRent.ViewModel {
    public class MoviesListObject : VRObjectsListObject<Movie>, IMoviesEditObjectParent, IMoviePicturesEditObjectParent {
        Dictionary<Guid, MoviePicturesEditObject> moviePicturesEditObjects = new Dictionary<Guid, MoviePicturesEditObject>();

        public MoviesListObject(Session session) : base(session) { }
        public override AllObjects<Movie> GetVideoRentObjects() {
            return new AllObjects<Movie>(Session, null, new SortProperty("[Title]", SortingDirection.Ascending));
        }
        public Movie GetVideoRentObject(Guid videoRentObjectOid) {
            return SessionHelper.GetObjectByKey<Movie>(videoRentObjectOid, Session);
        }
        internal MoviesEditObject MoviesEditObject { get { return (MoviesEditObject)ListEditObject; } }
        internal MoviesViewOptionsEditObject MoviesViewOptionsEditObject { get { return (MoviesViewOptionsEditObject)ViewOptionsEditObject; } }
        protected override EditableSubobject CreateListEditObject() {
            return new MoviesEditObject(this);
        }
        protected override EditableSubobject CreateViewOptionsEditObject() {
            return new MoviesViewOptionsEditObject(this);
        }
        #region Subobjects
        internal MoviePicturesEditObject GetMoviePicturesEditObject(Guid movieOid) {
            MoviePicturesEditObject editObject;
            if(!moviePicturesEditObjects.TryGetValue(movieOid, out editObject)) {
                editObject = new MoviePicturesEditObject(this, movieOid);
                moviePicturesEditObjects.Add(movieOid, editObject);
            }
            return editObject;
        }
        internal override IEnumerable<EditableSubobject> Subobjects {
            get {
                List<EditableSubobject> list = new List<EditableSubobject>(base.Subobjects);
                foreach(MoviePicturesEditObject moviePicturesEditObject in moviePicturesEditObjects.Values)
                    list.Add(moviePicturesEditObject);
                return list;
            }
        }
        internal override bool ReleaseSubobject(EditableSubobject editableSubobject) {
            if(base.ReleaseSubobject(editableSubobject)) return true;
            MoviePicturesEditObject moviePicturesEditObject = editableSubobject as MoviePicturesEditObject;
            if(moviePicturesEditObjects != null) {
                if(!moviePicturesEditObjects.ContainsKey(moviePicturesEditObject.VideoRentObjectOid)) return false;
                moviePicturesEditObjects.Remove(moviePicturesEditObject.VideoRentObjectOid);
                return true;
            }
            return false;
        }
        #endregion
    }
}
