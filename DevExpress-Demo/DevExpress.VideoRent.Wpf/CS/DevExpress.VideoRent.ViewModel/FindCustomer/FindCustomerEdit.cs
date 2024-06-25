using System;
using DevExpress.Data.Filtering;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class FindCustomerEdit : VRObjectsEdit<Customer> {
        public FindCustomerEdit(FindCustomerEditObject editObject, ModuleObjectDetail detail) : base(editObject, detail) {
            CurrentRecord = VRObjectsEditObject.VideoRentObjects.Session.FindObject<Customer>(CriteriaOperator.Parse("Oid = ?", CurrentCustomerProvider.Current.CurrentCustomerOid));
        }
        public new FindCustomerEditObject VRObjectsEditObject { get { return (FindCustomerEditObject)EditObject; } }
    }
}
