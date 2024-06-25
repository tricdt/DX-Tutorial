using System;
using System.Collections.Generic;
using DevExpress.VideoRent.Helpers;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;

namespace DevExpress.VideoRent.ViewModel {
    public class ArtistsListObject : VRObjectsListObject<Artist>, IArtistsEditObjectParent, IArtistPicturesEditObjectParent {
        Dictionary<Guid, ArtistPicturesEditObject> artistPicturesEditObjects = new Dictionary<Guid, ArtistPicturesEditObject>();

        public ArtistsListObject(Session session) : base(session) { }
        public override AllObjects<Artist> GetVideoRentObjects() {
            return new AllObjects<Artist>(Session, null, new SortProperty("[LastName]", SortingDirection.Ascending), new SortProperty("[FirstName]", SortingDirection.Ascending));
        }
        public Artist GetVideoRentObject(Guid videoRentObjectOid) {
            return SessionHelper.GetObjectByKey<Artist>(videoRentObjectOid, Session);
        }
        internal ArtistsEditObject ArtistsEditObject { get { return (ArtistsEditObject)ListEditObject; } }
        internal ArtistsViewOptionsEditObject ArtistsViewOptionsEditObject { get { return (ArtistsViewOptionsEditObject)ViewOptionsEditObject; } }
        #region Subobjects
        internal ArtistPicturesEditObject GetArtistPicturesEditObject(Guid artistOid) {
            ArtistPicturesEditObject editObject;
            if(!artistPicturesEditObjects.TryGetValue(artistOid, out editObject)) {
                editObject = new ArtistPicturesEditObject(this, artistOid);
                artistPicturesEditObjects.Add(artistOid, editObject);
            }
            return editObject;
        }
        internal override IEnumerable<EditableSubobject> Subobjects {
            get {
                List<EditableSubobject> list = new List<EditableSubobject>(base.Subobjects);
                foreach(ArtistPicturesEditObject artistPicturesEditObject in artistPicturesEditObjects.Values)
                    list.Add(artistPicturesEditObject);
                return list;
            }
        }
        internal override bool ReleaseSubobject(EditableSubobject editableSubobject) {
            if(base.ReleaseSubobject(editableSubobject)) return true;
            ArtistPicturesEditObject artistPicturesEditObject = editableSubobject as ArtistPicturesEditObject;
            if(artistPicturesEditObject != null) {
                if(!artistPicturesEditObjects.ContainsKey(artistPicturesEditObject.VideoRentObjectOid)) return false;
                artistPicturesEditObjects.Remove(artistPicturesEditObject.VideoRentObjectOid);
                return true;
            }
            return false;
        }
        #endregion
        protected override EditableSubobject CreateListEditObject() {
            return new ArtistsEditObject(this);
        }
        protected override EditableSubobject CreateViewOptionsEditObject() {
            return new ArtistsViewOptionsEditObject(this);
        }
    }
}
