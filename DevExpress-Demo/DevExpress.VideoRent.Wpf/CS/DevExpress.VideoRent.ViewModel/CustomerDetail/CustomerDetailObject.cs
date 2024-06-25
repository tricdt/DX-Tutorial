using System;
using System.Collections.Generic;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.Xpo;

namespace DevExpress.VideoRent.ViewModel {
    public class CustomerDetailObject : VRObjectDetailObject<Customer>, ICustomerEditObjectParent {
        CustomerEditObject customerEditObject;

        public CustomerDetailObject(Session session, Guid? customerOid) : base(session, customerOid) { }
        protected override Customer CreateNewObjectOverride() {
            return new Customer(Session);
        }
        #region Subobjects
        internal CustomerEditObject CustomerEditObject {
            get {
                if(customerEditObject == null)
                    customerEditObject = new CustomerEditObject(this, VideoRentObjectOid);
                return customerEditObject;
            }
        }
        internal override IEnumerable<EditableSubobject> Subobjects {
            get {
                List<EditableSubobject> list = new List<EditableSubobject>(base.Subobjects);
                if(customerEditObject != null)
                    list.Add(customerEditObject);
                return list;
            }
        }
        internal override bool ReleaseSubobject(EditableSubobject editableSubobject) {
            if(base.ReleaseSubobject(editableSubobject)) return true;
            if(editableSubobject == customerEditObject) {
                customerEditObject = null;
                return true;
            }
            return false;
        }
        #endregion
    }
}
