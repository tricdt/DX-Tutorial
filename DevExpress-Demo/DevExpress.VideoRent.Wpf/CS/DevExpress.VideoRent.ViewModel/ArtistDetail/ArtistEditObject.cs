using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public interface IArtistEditObjectParent : IVRObjectEditObjectParent<Artist> { }
    public sealed class ArtistEditObject : VRObjectEditObject<Artist> {
        public ArtistEditObject(EditableObject parent, Guid artistOid)
            : base(parent, artistOid) {
            Update();
        }
        public new IArtistEditObjectParent VideoRentEditObjectParent { get { return (IArtistEditObjectParent)Parent; } }
    }
}
