using System;
using DevExpress.VideoRent.Helpers;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public interface IArtistAddMovieEditObjectParent : IAddVRObjectEditObjectParent { }
    public class ArtistAddMovieEditObject : AddVRObjectEditObject<MovieArtist> {
        public ArtistAddMovieEditObject(EditableObject parent, Guid parentVRObjectOid): base(parent, parentVRObjectOid) { }
        public new IArtistAddMovieEditObjectParent AddVRObjectEditObjectParent { get { return (IArtistAddMovieEditObjectParent)Parent; } }
        protected override MovieArtist CreateObjectOverride() {
            return new MovieArtist(SessionHelper.GetObjectByKey<Artist>(ParentVRObjectOid, NestedSession));
        }
    }
}
