using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
#if SL
using DevExpress.Xpf.Drawing;
#else
using System.Drawing;
#endif

namespace DevExpress.VideoRent.ViewModel {
    public class MoviePicturesEdit : VRObjectPicturesEdit<Movie> {
        public MoviePicturesEdit(MoviePicturesEditObject editObject, ModuleObjectDetail detail) : base(editObject, detail) { }
        public MoviePicturesEditObject MoviePicturesEditObject { get { return (MoviePicturesEditObject)EditObject; } }
        protected override void AddPicture(Image picture) {
            MoviePicturesEditObject.VideoRentObject.AddPicture(picture);
        }
    }
}
