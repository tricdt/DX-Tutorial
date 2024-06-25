using System;
using System.Collections.Generic;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.Xpo;

namespace DevExpress.VideoRent.ViewModel {
    public class CurrentCustomerRentsDetailObject : VRObjectsListObject<Receipt>, ICurrentCustomerRentsEditObjectParent {
        RentsPeriodEditObject rentsPeriodEditObject;

        public CurrentCustomerRentsDetailObject(Session session) : base(session) { }
        internal CurrentCustomerRentsEditObject CurrentCustomerRentsEditObject { get { return (CurrentCustomerRentsEditObject)ListEditObject; } }
        internal RentsViewOptionsEditObject RentsViewOptionsEditObject { get { return (RentsViewOptionsEditObject)ViewOptionsEditObject; } }
        protected override EditableSubobject CreateListEditObject() {
            return new CurrentCustomerRentsEditObject(this);
        }
        protected override EditableSubobject CreateViewOptionsEditObject() {
            return new RentsViewOptionsEditObject(this);
        }
        #region Subobjects
        internal RentsPeriodEditObject RentsPeriodEditObject {
            get {
                if(rentsPeriodEditObject == null)
                    rentsPeriodEditObject = new RentsPeriodEditObject(this);
                return rentsPeriodEditObject;
            }
        }
        internal override IEnumerable<EditableSubobject> Subobjects {
            get {
                List<EditableSubobject> list = new List<EditableSubobject>(base.Subobjects);
                if(RentsPeriodEditObject != null)
                    list.Add(RentsPeriodEditObject);
                return list;
            }
        }
        internal override bool ReleaseSubobject(EditableSubobject editableSubobject) {
            if(base.ReleaseSubobject(editableSubobject)) return true;
            if(editableSubobject == rentsPeriodEditObject) {
                rentsPeriodEditObject = null;
                return true;
            }
            return false;
        }
        #endregion
    }
}
