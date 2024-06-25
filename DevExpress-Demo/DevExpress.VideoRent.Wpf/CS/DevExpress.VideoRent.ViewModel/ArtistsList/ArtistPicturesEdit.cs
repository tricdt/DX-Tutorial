using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
#if SL
using DevExpress.Xpf.Drawing;
#else
using System.Drawing;
#endif

namespace DevExpress.VideoRent.ViewModel {
    public class ArtistPicturesEdit : VRObjectPicturesEdit<Artist> {
        public ArtistPicturesEdit(ArtistPicturesEditObject editObject, ModuleObjectDetail detail) : base(editObject, detail) { }
        public ArtistPicturesEditObject ArtistPicturesEditObject { get { return (ArtistPicturesEditObject)EditObject; } }
        protected override void AddPicture(Image picture) {
            ArtistPicturesEditObject.VideoRentObject.AddPicture(picture);
        }
    }
}
