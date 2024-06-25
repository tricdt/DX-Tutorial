using System;
using DevExpress.VideoRent.Helpers;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public interface ICompanyAddMovieEditObjectParent : IAddVRObjectEditObjectParent { }
    public class CompanyAddMovieEditObject : AddVRObjectEditObject<MovieCompany> {
        public CompanyAddMovieEditObject(EditableObject parent, Guid companyOid) : base(parent, companyOid) { }
        public new ICompanyAddMovieEditObjectParent AddVRObjectEditObjectParent { get { return (ICompanyAddMovieEditObjectParent)Parent; } }
        protected override MovieCompany CreateObjectOverride() {
            return new MovieCompany(SessionHelper.GetObjectByKey<Company>(ParentVRObjectOid, NestedSession));
        }
    }
}
