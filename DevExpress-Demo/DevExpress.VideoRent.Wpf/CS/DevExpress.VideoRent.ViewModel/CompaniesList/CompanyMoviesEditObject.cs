using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public interface ICompanyMoviesEditObjectParent : IVRObjectMainEditObjectParent<Company> { }
    public sealed class CompanyMoviesEditObject : VRObjectMainEditObject<Company> {
        public CompanyMoviesEditObject(EditableObject parent, Guid companyOid)
            : base(parent, companyOid) {
            Update();
        }
        public ICompanyMoviesEditObjectParent CompanyMoviesEditObjectParent { get { return (ICompanyMoviesEditObjectParent)Parent; } }
    }
}
