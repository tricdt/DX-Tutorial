using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public interface ICompaniesEditObjectParent : IVRObjectsEditObjectParent<Company> { }
    public sealed class CompaniesEditObject : VRObjectsEditObject<Company> {
        public CompaniesEditObject(EditableObject parent)
            : base(parent) {
            Update();
        }
        public new ICompaniesEditObjectParent VRObjectsEditObjectParent { get { return (ICompaniesEditObjectParent)Parent; } }
    }
}
