using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public interface IFindCustomerEditObjectParent : IVRObjectsEditObjectParent<Customer> { }
    public sealed class FindCustomerEditObject : VRObjectsEditObject<Customer> {
        public FindCustomerEditObject(EditableObject parent)
            : base(parent) {
            Update();
        }
        public new IFindCustomerEditObjectParent VRObjectsEditObjectParent { get { return (IFindCustomerEditObjectParent)Parent; } }
    }

}
