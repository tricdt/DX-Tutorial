using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public interface IArtistsEditObjectParent : IVRObjectsEditObjectParent<Artist> { }
    public sealed class ArtistsEditObject : VRObjectsEditObject<Artist> {
        public ArtistsEditObject(EditableObject parent)
            : base(parent) {
            Update();
        }
        public new IArtistsEditObjectParent VRObjectsEditObjectParent { get { return (IArtistsEditObjectParent)Parent; } }
    }

}
