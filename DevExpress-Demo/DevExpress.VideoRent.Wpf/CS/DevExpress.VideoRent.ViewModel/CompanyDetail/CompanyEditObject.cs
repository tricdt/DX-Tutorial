using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public interface ICompanyEditObjectParent : IVRObjectEditObjectParent<Company> { }
    public sealed class CompanyEditObject : VRObjectEditObject<Company> {
        public CompanyEditObject(EditableObject parent, Guid companyOid)
            : base(parent, companyOid) {
            Update();
        }
        public new ICompanyEditObjectParent VideoRentEditObjectParent { get { return (ICompanyEditObjectParent)Parent; } }
    }

}
