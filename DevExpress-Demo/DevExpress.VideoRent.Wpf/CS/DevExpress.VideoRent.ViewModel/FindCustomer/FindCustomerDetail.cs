using System;

namespace DevExpress.VideoRent.ViewModel {
    public class FindCustomerDetail : VRObjectsList<Customer> {
        public FindCustomerDetail(FindCustomerDetailObject editObject)
            : base(editObject) {
            ListEdit = new FindCustomerEdit(VRObjectsListObject.FindCustomerEditObject, this);
        }
        public new FindCustomerDetailObject VRObjectsListObject { get { return (FindCustomerDetailObject)EditObject; } }
        public FindCustomerEdit FindCustomerEdit { get { return (FindCustomerEdit)ListEdit; } }
        protected override void RaiseBeforeSave() {
            base.RaiseBeforeSave();
            if(ListEdit.CurrentRecord != null)
                CurrentCustomerProvider.Current.CurrentCustomerOid = ListEdit.CurrentRecord.Oid;
        }
    }
}
