using System;
using DevExpress.VideoRent.Helpers;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public interface IMovieAddCompanyEditObjectParent : IAddVRObjectEditObjectParent { }
    public class MovieAddCompanyEditObject : AddVRObjectEditObject<MovieCompany> {
        public MovieAddCompanyEditObject(EditableObject parent, Guid parentVRObjectOid) : base(parent, parentVRObjectOid) { }
        public new IMovieAddCompanyEditObjectParent AddVRObjectEditObjectParent { get { return (IMovieAddCompanyEditObjectParent)Parent; } }
        protected override MovieCompany CreateObjectOverride() {
            return new MovieCompany(SessionHelper.GetObjectByKey<Movie>(ParentVRObjectOid, NestedSession));
        }
    }
}
