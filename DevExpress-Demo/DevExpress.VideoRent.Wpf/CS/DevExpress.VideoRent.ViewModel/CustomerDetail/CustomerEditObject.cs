using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public interface ICustomerEditObjectParent : IVRObjectEditObjectParent<Customer> { }
    public sealed class CustomerEditObject : VRObjectEditObject<Customer> {
        public CustomerEditObject(EditableObject parent, Guid customerOid)
            : base(parent, customerOid) {
            Update();
        }
        public new ICustomerEditObjectParent VideoRentEditObjectParent { get { return (ICustomerEditObjectParent)Parent; } }
    }
}
