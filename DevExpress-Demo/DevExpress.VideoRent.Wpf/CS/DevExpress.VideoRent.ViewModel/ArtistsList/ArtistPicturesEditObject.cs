using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public interface IArtistPicturesEditObjectParent : IVRObjectMainEditObjectParent<Artist> { }
    public sealed class ArtistPicturesEditObject : VRObjectMainEditObject<Artist> {
        public ArtistPicturesEditObject(EditableObject parent, Guid movieOid)
            : base(parent, movieOid) {
            Update();
        }
        public IArtistPicturesEditObjectParent ArtistPicturesEditObjectParent { get { return (IArtistPicturesEditObjectParent)Parent; } }
    }
}
