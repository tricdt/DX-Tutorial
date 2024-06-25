using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public interface IMoviesEditObjectParent : IVRObjectsEditObjectParent<Movie> { }
    public sealed class MoviesEditObject : VRObjectsEditObject<Movie> {
        public MoviesEditObject(EditableObject parent)
            : base(parent) {
            Update();
        }
        public new IMoviesEditObjectParent VRObjectsEditObjectParent { get { return (IMoviesEditObjectParent)Parent; } }
    }
}
