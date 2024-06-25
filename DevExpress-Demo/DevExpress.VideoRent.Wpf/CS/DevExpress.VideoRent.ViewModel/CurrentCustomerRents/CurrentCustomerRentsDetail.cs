using System;
using System.Collections.Generic;
using DevExpress.VideoRent.Helpers;
#if !SL //TODO
using DevExpress.VideoRent.ViewModel.Reports;
#endif
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.XtraReports.UI;

namespace DevExpress.VideoRent.ViewModel {
    public class CurrentCustomerRentsDetail : VRObjectsList<Receipt> {
        RentsPeriodEdit rentsPeriodEdit;

        public CurrentCustomerRentsDetail(CurrentCustomerRentsDetailObject editObject)
            : base(editObject) {
            ListEdit = new CurrentCustomerRentsEdit(VRObjectsListObject.CurrentCustomerRentsEditObject, this);
        }
        public new CurrentCustomerRentsDetailObject VRObjectsListObject { get { return (CurrentCustomerRentsDetailObject)EditObject; } }
        public CurrentCustomerRentsEdit CurrentCustomerRentsEdit { get { return (CurrentCustomerRentsEdit)ListEdit; } }
        public RentsViewOptionsEdit RentsViewOptionsEdit { get { return (RentsViewOptionsEdit)ViewOptionsEdit; } }
        #region Edits
        public RentsPeriodEdit RentsPeriodEdit {
            get { return rentsPeriodEdit; }
            private set { SetValue<RentsPeriodEdit>("RentsPeriodEdit", ref rentsPeriodEdit, value); }
        }
        protected override IEnumerable<ModuleObjectEdit> ModuleObjectEdits {
            get {
                List<ModuleObjectEdit> baseEdits = new List<ModuleObjectEdit>(base.ModuleObjectEdits);
                if(RentsPeriodEdit != null)
                    baseEdits.Add(RentsPeriodEdit);
                return baseEdits;
            }
        }
        #endregion
        public void ChangeCustomer() {
            ModulesManager.Current.OpenModuleObjectDetail(new FindCustomerDetailObject(ListEdit.VRObjectsEditObject.VideoRentObjects.Session), false);
        }
        public void ChangePeriod() {
            RentsPeriodEdit = new RentsPeriodEdit(VRObjectsListObject.RentsPeriodEditObject, this);
            RentsPeriodEdit.AfterDispose += OnRentsPeriodEditAfterDispose;
        }
        public void ReturnRents() {
            Guid? overdueReceiptOid = CurrentCustomerRentsEdit.ReturnRents();
            Save();
            if(overdueReceiptOid != null)
                ShowBill(SessionHelper.GetObjectByKey<Receipt>(overdueReceiptOid, CurrentCustomerRentsEdit.VRObjectsEditObject.VideoRentObjects.Session));
        }
        protected override ModuleObjectDetailBase OpenDetailOverride(Guid? vroOid, object parameter) {
            return ModulesManager.Current.OpenModuleObjectDetail(new CustomerDetailObject(CurrentCustomerRentsEdit.VRObjectsEditObject.VideoRentObjects.Session, CurrentCustomerRentsEdit.CurrentCustomer.Oid), true, parameter);
        }
        protected override ModuleObjectEdit CreateViewOptionsEditOverride() {
            return new RentsViewOptionsEdit(VRObjectsListObject.RentsViewOptionsEditObject, this);
        }
        void OnRentsPeriodEditAfterDispose(object sender, EventArgs e) {
            RentsPeriodEdit = null;
        }
        void ShowBill(Receipt overdueReceipt) {
            if(overdueReceipt == null) return;
#if !SL //TODO
            XtraReport report = new LatefeeReceipt(overdueReceipt);
            ReportDialog.ShowPrintDialog(report);
#endif
        }
        #region Commands
        public Action<object> CommandChangePeriod { get { return DoCommandChangePeriod; } }
        void DoCommandChangePeriod(object p) { ChangePeriod(); }
        public Action<object> CommandChangeCustomer { get { return DoCommandChangeCustomer; } }
        void DoCommandChangeCustomer(object p) { ChangeCustomer(); }
        public Action<object> CommandOpenCurrentCustomerDetail { get { return DoOpenCurrentCustomerDetail; } }
        void DoOpenCurrentCustomerDetail(object p) { OpenDetail(CurrentCustomerRentsEdit.CurrentCustomer.Oid, p); }
        public Action<object> CommandReturnRents { get { return DoCommandReturnRents; } }
        void DoCommandReturnRents(object p) { ReturnRents(); }
        #endregion
    }
}
