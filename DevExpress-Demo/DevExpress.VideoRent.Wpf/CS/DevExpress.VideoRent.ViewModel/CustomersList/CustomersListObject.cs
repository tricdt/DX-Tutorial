using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;

namespace DevExpress.VideoRent.ViewModel {
    public class CustomersListObject : VRObjectsListObject<Customer>, ICustomersEditObjectParent {
        public CustomersListObject(Session session) : base(session) { }
        public override AllObjects<Customer> GetVideoRentObjects() {
            return new AllObjects<Customer>(Session, null, new SortProperty("[CustomerId]", SortingDirection.Ascending));
        }
        internal CustomersEditObject CustomersEditObject { get { return (CustomersEditObject)ListEditObject; } }
        internal CustomersViewOptionsEditObject CustomersViewOptionsEditObject { get { return (CustomersViewOptionsEditObject)ViewOptionsEditObject; } }
        protected override EditableSubobject CreateListEditObject() {
            return new CustomersEditObject(this);
        }
        protected override EditableSubobject CreateViewOptionsEditObject() {
            return new CustomersViewOptionsEditObject(this);
        }
    }
}
