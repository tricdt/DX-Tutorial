using System;
using System.Collections.Generic;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.Xpo;

namespace DevExpress.VideoRent.ViewModel {
    public class ArtistDetailObject : VRObjectDetailObject<Artist>, IArtistEditObjectParent, IArtistAddMovieEditObjectParent {
        ArtistEditObject artistEditObject;
        ArtistAddMovieEditObject artistMovieEditObject;

        public ArtistDetailObject(Session session, Guid? artistOid) : base(session, artistOid) { }
        #region Subobjects
        internal ArtistEditObject ArtistEditObject {
            get {
                if(artistEditObject == null)
                    artistEditObject = new ArtistEditObject(this, VideoRentObjectOid);
                return artistEditObject;
            }
        }
        internal ArtistAddMovieEditObject ArtistMovieEditObject {
            get {
                if(artistMovieEditObject == null)
                    artistMovieEditObject = new ArtistAddMovieEditObject(this, VideoRentObjectOid);
                return artistMovieEditObject;
            }
        }
        internal override IEnumerable<EditableSubobject> Subobjects {
            get {
                List<EditableSubobject> list = new List<EditableSubobject>(base.Subobjects);
                if(artistEditObject != null)
                    list.Add(artistEditObject);
                if(artistMovieEditObject != null)
                    list.Add(artistMovieEditObject);
                return list;
            }
        }
        internal override bool ReleaseSubobject(EditableSubobject editableSubobject) {
            if(base.ReleaseSubobject(editableSubobject)) return true;
            if(editableSubobject == artistMovieEditObject) {
                artistMovieEditObject = null;
                return true;
            }
            if(editableSubobject == artistEditObject) {
                artistEditObject = null;
                return true;
            }
            return false;
        }
        #endregion
        protected override Artist CreateNewObjectOverride() {
            return new Artist(Session);
        }
    }
}
