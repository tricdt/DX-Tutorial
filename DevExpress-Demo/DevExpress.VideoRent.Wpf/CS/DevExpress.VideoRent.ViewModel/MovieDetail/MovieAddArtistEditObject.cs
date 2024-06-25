using System;
using DevExpress.VideoRent.Helpers;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public interface IMovieAddArtistEditObjectParent : IAddVRObjectEditObjectParent { }
    public class MovieAddArtistEditObject : AddVRObjectEditObject<MovieArtist> {
        public MovieAddArtistEditObject(EditableObject parent, Guid parentVRObjectOid) : base(parent, parentVRObjectOid) { }
        public new IMovieAddArtistEditObjectParent AddVRObjectEditObjectParent { get { return (IMovieAddArtistEditObjectParent)Parent; } }
        protected override MovieArtist CreateObjectOverride() {
            return new MovieArtist(SessionHelper.GetObjectByKey<Movie>(ParentVRObjectOid, NestedSession));
        }
    }
}
