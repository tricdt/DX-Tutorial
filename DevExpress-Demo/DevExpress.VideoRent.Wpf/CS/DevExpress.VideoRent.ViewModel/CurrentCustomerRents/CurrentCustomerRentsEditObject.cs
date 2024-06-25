using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public interface ICurrentCustomerRentsEditObjectParent : IVRObjectsEditObjectParent<Receipt> { }
    public sealed class CurrentCustomerRentsEditObject : VRObjectsEditObject<Receipt> {
        public CurrentCustomerRentsEditObject(EditableObject parent)
            : base(parent) {
            Update();
        }
        public new ICurrentCustomerRentsEditObjectParent VRObjectsEditObjectParent { get { return (ICurrentCustomerRentsEditObjectParent)Parent; } }
    }
}
