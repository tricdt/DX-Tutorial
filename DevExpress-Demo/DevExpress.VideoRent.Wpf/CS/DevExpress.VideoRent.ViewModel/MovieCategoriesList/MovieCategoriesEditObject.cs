using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public interface IMovieCategoriesEditObjectParent : IVRObjectsEditObjectParent<MovieCategory> { }
    public sealed class MovieCategoriesEditObject : VRObjectsEditObject<MovieCategory> {
        public MovieCategoriesEditObject(EditableObject parent)
            : base(parent) {
            Update();
        }
        public new IMovieCategoriesEditObjectParent VRObjectsEditObjectParent { get { return (IMovieCategoriesEditObjectParent)Parent; } }
    }
}
