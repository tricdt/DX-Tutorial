using System;
using System.Collections.Generic;
using DevExpress.Data.Filtering;
using DevExpress.VideoRent.Resources;
using DevExpress.VideoRent.ViewModel.Helpers;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.Xpo;

namespace DevExpress.VideoRent.ViewModel {
    public class CurrentCustomerRentsEdit : VRObjectsEdit<Receipt> {
        DateTime startDate;
        DateTime endDate;
        int period;
        bool canReturnItems;
        bool canCheckActiveRents;
        bool checkedAllActiveRentsSignal;
        bool checkedRentsChangedSignal;
        bool flipLayoutSignal;
        bool showViewCaption;
        GridUIOptions gridUIOptions;
        bool periodChangeLock = false;
        Customer currentCustomer;
        XPCollection<Rent> currentCustomerActiveRents;
        Dictionary<Rent, bool> checkedRents = new Dictionary<Rent, bool>();
        string gridCaption;

        public CurrentCustomerRentsEdit(CurrentCustomerRentsEditObject editObject, ModuleObjectDetail detail)
            : base(editObject, detail) {
            Period = 12;
            LayoutManager.Current.Subscribe(OnLayoutManagerAfterLoad, OnLayoutManagerBeforeSave);
            CurrentCustomerProvider.Current.CurrentCustomerOidChanged += OnCurrentCustomerProviderCurrentCustomerOidChanged;
            AllObjects<Customer>.Set.Updated += OnCustomersSetUpdated;
        }
        public new CurrentCustomerRentsEditObject VRObjectsEditObject { get { return (CurrentCustomerRentsEditObject)EditObject; } }
        public DateTime StartDate {
            get { return startDate; }
            set { SetValue<DateTime>("StartDate", ref startDate, value, RaiseDatesChanged); }
        }
        public DateTime EndDate {
            get { return endDate; }
            set { SetValue<DateTime>("EndDate", ref endDate, value, RaiseDatesChanged); }
        }
        public int Period {
            get { return period; }
            set { SetValue<int>("Period", ref period, value, RaisePeriodChanged); }
        }
        public bool CanReturnItems {
            get { return canReturnItems; }
            set { SetValue<bool>("CanReturnItems", ref canReturnItems, value); }
        }
        public bool CanCheckActiveRents {
            get { return canCheckActiveRents; }
            set { SetValue<bool>("CanCheckActiveRents", ref canCheckActiveRents, value); }
        }
        public bool CheckedAllActiveRentsSignal {
            get { return checkedAllActiveRentsSignal; }
            set { SetValue<bool>("CheckedAllActiveRentsSignal", ref checkedAllActiveRentsSignal, value); }
        }
        public bool CheckedRentsChangedSignal {
            get { return checkedRentsChangedSignal; }
            set { SetValue<bool>("CheckedRentsChangedSignal", ref checkedRentsChangedSignal, value); }
        }
        public bool FlipLayoutSignal {
            get { return flipLayoutSignal; }
            set { SetValue<bool>("FlipLayoutSignal", ref flipLayoutSignal, value); }
        }
        public string GridCaption {
            get { return gridCaption; }
            set { SetValue<string>("GridCaption", ref gridCaption, value); }
        }
        public Customer CurrentCustomer {
            get { return currentCustomer; }
            private set { SetValue<Customer>("CurrentCustomer", ref currentCustomer, value, RaiseCurrentCustomerChanged); }
        }
        public XPCollection<Rent> CurrentCustomerActiveRents {
            get { return currentCustomerActiveRents; }
            set { SetValue<XPCollection<Rent>>("CurrentCustomerActiveRents", ref currentCustomerActiveRents, value); }
        }
        public bool ShowViewCaption {
            get { return showViewCaption; }
            set { SetValue<bool>("ShowViewCaption", ref showViewCaption, value); }
        }
        public GridUIOptions GridUIOptions {
            get { return gridUIOptions; }
            set { SetValue<GridUIOptions>("GridUIOptions", ref gridUIOptions, value); }
        }
        public bool GetRecordChecked(Rent rent) {
            return checkedRents.ContainsKey(rent);
        }
        public void SetRecordChecked(Rent rent, bool isChecked) {
            if(isChecked) {
                if(!checkedRents.ContainsKey(rent))
                    checkedRents.Add(rent, true);
            } else {
                if(checkedRents.ContainsKey(rent))
                    checkedRents.Remove(rent);
            }
            CanReturnItems = checkedRents.Keys.Count > 0;
        }
        public Guid? ReturnRents() {
            Receipt overdueReceipt = null;
            if(MessageBox.Show(ReturningLate() ? ConstStrings.Get("ReturnAndPaymentLateFeeQuestion") : ConstStrings.Get("ReturnQuestion"), ConstStrings.Get("Question"), MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) {
                List<Rent> returns = new List<Rent>();
                foreach(Rent rent in checkedRents.Keys)
                    returns.Add(rent);
                overdueReceipt = CurrentCustomer.ReturnRents(returns);
            }
            checkedRents.Clear();
            CheckedRentsChangedSignal = true;
            CheckedRentsChangedSignal = false;
            CanReturnItems = false;
            return overdueReceipt == null ? null : (Guid?)overdueReceipt.Oid;
        }
        public void CheckAllActiveRents() {
            foreach(Rent item in CurrentCustomerActiveRents) {
                if(item.ActiveType != (int)ActiveRentType.Active)
                    SetRecordChecked(item, true);
            }
            CheckedRentsChangedSignal = true;
            CheckedRentsChangedSignal = false;
            CheckedAllActiveRentsSignal = true;
            CheckedAllActiveRentsSignal = false;
        }
        public void FlipLayout() {
            FlipLayoutSignal = true;
            FlipLayoutSignal = false;
        }
        protected override void OnEditObjectUpdated(object sender, EventArgs e) {
            base.OnEditObjectUpdated(sender, e);
            UpdateCurrentCustomer();
            UpdateGridCaption();
        }
        protected override void DisposeManaged() {
            CurrentCustomerProvider.Current.CurrentCustomerOidChanged -= OnCurrentCustomerProviderCurrentCustomerOidChanged;
            CurrentCustomer = null;
            base.DisposeManaged();
        }
        void OnCustomersSetUpdated(object sender, EditableObjectEventArgs e) {
            if(CurrentCustomer != null)
                CurrentCustomer.Reload();
        }
        void RaiseDatesChanged(DateTime oldValue, DateTime newValue) {
            if(periodChangeLock) return;
            UpdatePeriod();
        }
        void RaisePeriodChanged(int oldValue, int newValue) {
            if(newValue != 0)
                UpdateStartEndDates();
        }
        void UpdatePeriod() {
            int period = EndDate.Day - StartDate.Day == 0 ? (EndDate.Year - StartDate.Year) * 12 + EndDate.Month - StartDate.Month : 0;
            Period = ItemsSourceHelper.PredefinedPeriods.Contains(period) ? period : 0;
            UpdateGridCaption();
        }
        void UpdateStartEndDates() {
            periodChangeLock = true;
            EndDate = DateTime.Now;
            periodChangeLock = false;
            StartDate = EndDate.AddMonths(-Period);
            UpdateGridCaption();
        }
        void UpdateGridCaption() {
            UpdateReceiptsFilter();
            string caption;
            try {
                caption = ConstStrings.Get("Receipts") + "|" + string.Format(ConstStrings.Get("DatePeriodCaption"), StartDate, EndDate);
            } catch(ArgumentOutOfRangeException) {
                caption = string.Empty;
            }
            GridCaption = caption;
        }
        void UpdateReceiptsFilter() {
            CurrentCustomer.Receipts.Filter = CriteriaOperator.Parse("Date >= ? and Date <= ?", StartDate, EndDate);
        }
        void OnCurrentCustomerProviderCurrentCustomerOidChanged(object sender, EventArgs e) {
            UpdateCurrentCustomer();
        }
        void UpdateCurrentCustomer() {
            CurrentCustomer = VRObjectsEditObject.VideoRentObjects.Session.FindObject<Customer>(CriteriaOperator.Parse("Oid = ?", CurrentCustomerProvider.Current.CurrentCustomerOid));
            UpdateActiveRents();
            ClearCheckedRents();
            UpdateReceiptsFilter();
        }
        void ClearCheckedRents() {
            checkedRents.Clear();
            CanReturnItems = false;
        }
        void UpdateActiveRents() {
            CurrentCustomerActiveRents = CurrentCustomer == null ? null : CurrentCustomer.ActiveRents;
            CanCheckActiveRents = CurrentCustomer != null && CurrentCustomerActiveRents.Count > 0;
        }
        void RaiseCurrentCustomerChanged(Customer oldValue, Customer newValue) {
            if(Disposed) return;
            CurrentCustomerProvider.Current.CurrentCustomerOid = CurrentCustomer.Oid;
        }
        bool ReturningLate() {
            foreach(DevExpress.VideoRent.Rent rent in checkedRents.Keys)
                if(rent.ActiveType == (int)ActiveRentType.Overdue) return true;
            return false;
        }
        void OnLayoutManagerAfterLoad(object sender, EventArgs e) {
            ViewModelLayoutData layoutData = ViewModelLayoutData.GetLayoutData();
            ShowViewCaption = layoutData.CurrentCustomerRentsEditShowViewCaption;
            GridUIOptions = layoutData.CurrentCustomerRentsEditGridUIOptions;
        }
        void OnLayoutManagerBeforeSave(object sender, EventArgs e) {
            ViewModelLayoutData layoutData = ViewModelLayoutData.GetLayoutData();
            layoutData.CurrentCustomerRentsEditShowViewCaption = ShowViewCaption;
        }
        #region Commands
        public Action<object> CommandWriteRecordChecked { get { return DoCommandWriteRecordChecked; } }
        void DoCommandWriteRecordChecked(object p) {
            CommandEventArgs e = (CommandEventArgs)p;
            SetRecordChecked((Rent)e.Parameter, (bool)e.Value);
        }
        public Action<object> CommandReadRecordChecked { get { return DoCommandReadRecordChecked; } }
        void DoCommandReadRecordChecked(object p) {
            CommandEventArgs e = (CommandEventArgs)p;
            e.Value = GetRecordChecked((Rent)e.Parameter);
        }
        public Action<object> CommandCheckAllActiveRents { get { return DoCommandCheckAllActiveRents; } }
        void DoCommandCheckAllActiveRents(object p) {
            CheckAllActiveRents();
        }
        public Action<object> CommandFlipLayout { get { return DoCommandFlipLayout; } }
        void DoCommandFlipLayout(object p) {
            FlipLayout();
        }
        #endregion
    }
}
