using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public interface ICustomersEditObjectParent : IVRObjectsEditObjectParent<Customer> { }
    public sealed class CustomersEditObject : VRObjectsEditObject<Customer> {
        public CustomersEditObject(EditableObject parent)
            : base(parent) {
            Update();
        }
        public new ICustomersEditObjectParent VRObjectsEditObjectParent { get { return (ICustomersEditObjectParent)Parent; } }
    }

}
