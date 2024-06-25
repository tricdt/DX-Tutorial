using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;

namespace DevExpress.VideoRent.ViewModel {
    public class FindCustomerDetailObject : VRObjectsListObject<Customer>, ICustomersEditObjectParent {
        public FindCustomerDetailObject(Session session) : base(session) { }
        public override object Key { get { return typeof(FindCustomerDetail); } }
        public override AllObjects<Customer> GetVideoRentObjects() {
            return new AllObjects<Customer>(Session, null, new SortProperty("[CustomerId]", SortingDirection.Ascending));
        }
        internal FindCustomerEditObject FindCustomerEditObject { get { return (FindCustomerEditObject)ListEditObject; } }
        protected override EditableSubobject CreateListEditObject() {
            return new FindCustomerEditObject(this);
        }
    }
}
