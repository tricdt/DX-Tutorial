using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class CustomerEdit : VRObjectEdit<Customer> {
        PersonGenderEditData personGenderEditData;
        DiscountLevelEditData discountLevelEditData;

        public CustomerEdit(CustomerEditObject editObject, ModuleObjectDetail detail) : base(editObject, detail) {
            PersonGenderEditData = new PersonGenderEditData();
            DiscountLevelEditData = new DiscountLevelEditData();
        }
        public new CustomerEditObject VRObjectEditObject { get { return (CustomerEditObject)EditObject; } }
        public PersonGenderEditData PersonGenderEditData {
            get { return personGenderEditData; }
            private set { SetValue<PersonGenderEditData>("PersonGenderEditData", ref personGenderEditData, value); }
        }
        public DiscountLevelEditData DiscountLevelEditData {
            get { return discountLevelEditData; }
            private set { SetValue<DiscountLevelEditData>("DiscountLevelEditData", ref discountLevelEditData, value); }
        }
        #region Commands
        public Action<object> CommandSendEmail { get { return DoCommandSendEmail; } }
        void DoCommandSendEmail(object p) { ObjectHelper.SendMessageTo(VRObjectEditObject.VideoRentObject.Email); }
        #endregion
    }
}
