using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public interface IMoviePicturesEditObjectParent : IVRObjectMainEditObjectParent<Movie> { }
    public sealed class MoviePicturesEditObject : VRObjectMainEditObject<Movie> {
        public MoviePicturesEditObject(EditableObject parent, Guid movieOid)
            : base(parent, movieOid) {
            Update();
        }
        public IMoviePicturesEditObjectParent MoviePicturesEditObjectParent { get { return (IMoviePicturesEditObjectParent)Parent; } }
    }
}
