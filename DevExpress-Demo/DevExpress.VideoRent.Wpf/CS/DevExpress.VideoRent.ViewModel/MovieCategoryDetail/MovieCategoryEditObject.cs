using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public interface IMovieCategoryEditObjectParent : IVRObjectEditObjectParent<MovieCategory> { }
    public sealed class MovieCategoryEditObject : VRObjectEditObject<MovieCategory> {
        public MovieCategoryEditObject(EditableObject parent, Guid categoryOid)
            : base(parent, categoryOid) {
            Update();
        }
        public new IMovieCategoryEditObjectParent VideoRentEditObjectParent { get { return (IMovieCategoryEditObjectParent)Parent; } }
    }
}
